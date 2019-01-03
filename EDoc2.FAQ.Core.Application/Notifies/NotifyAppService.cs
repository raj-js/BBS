using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Notifies.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Notifies.Dtos.NotifyDtos;

namespace EDoc2.FAQ.Core.Application.Notifies
{
    public class NotifyAppService: AppServiceBase, INotifyAppService
    {
        private readonly INotifyService _notifyService;

        public NotifyAppService(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        public async Task<RespWapper> Search(SearchReq req)
        {
            var query = _notifyService.GetNotifies();

            query = query
                .WhereTure(req.InitiatorType.HasValue, s => s.InitiatorType == req.InitiatorType)
                .WhereTure(req.OperationType.HasValue, s => s.OperationType == req.OperationType)
                .WhereTure(req.InitiationTime.Begin.HasValue, s => s.InitiationTime >= req.InitiationTime.Begin)
                .WhereTure(req.InitiationTime.End.HasValue, s => s.InitiationTime <= req.InitiationTime.End);

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> DeleteNotify(Guid id)
        {
            if (CurrentUser == null || !CurrentUser.IsAdministrator)
                return RespWapper.Failed();

            var notify = await _notifyService.GetNotify(id);
            if (notify == null || notify.IsDeleted)
                return RespWapper.Failed();

            await _notifyService.DeleteNotify(CurrentUser, notify);
            await UnitOfWork.SaveChangesAsync();

            return RespWapper.Successed();
        }

        public async Task<RespWapper> SearchSelfNotifies(SearchSelfNotifyReq req)
        {
            var query = await _notifyService.GetNotifies(CurrentUser);

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> ReadMyNotify(Guid id)
        {
            var notify = await _notifyService.GetNotify(id);

            if (notify == null || notify.IsDeleted)
                return RespWapper.Failed();

            if (notify.ExpirationTime < DateTime.Now)
                return RespWapper.Failed();

            await _notifyService.ReadNotify(CurrentUser, notify);
            await UnitOfWork.SaveChangesAsync();

            return RespWapper.Successed();
        }

        public async Task<RespWapper> DeleteMyNotify(Guid id)
        {
            var notify = await _notifyService.GetNotify(id);

            if (notify == null || notify.IsDeleted)
                return RespWapper.Failed();

            if (notify.ExpirationTime < DateTime.Now)
                return RespWapper.Failed();

            await _notifyService.DeleteMyNotify(CurrentUser, notify);
            await UnitOfWork.SaveChangesAsync();

            return RespWapper.Successed();
        }
    }
}
