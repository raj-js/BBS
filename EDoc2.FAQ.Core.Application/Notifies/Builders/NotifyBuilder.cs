using EDoc2.FAQ.Core.Application.Notifies.Dtos;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Articles.Services;
using EDoc2.FAQ.Core.Domain.Notifies;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Application.Notifies.Builders
{
    internal class NotifyBuilder
    {
        private readonly User Operator;
        private readonly Notify _notify;
        private readonly IAccountService _accountService;
        private readonly IArticleService _articleService;

        public readonly NotifyDtos.Notification Output;

        public NotifyBuilder(User @operator,
            Notify notify,
            IAccountService accountService,
            IArticleService articleService)
        {
            Operator = @operator;
            _notify = notify ?? throw new ArgumentNullException(nameof(notify));
            _accountService = accountService;
            _articleService = articleService;

            Output = new NotifyDtos.Notification();
        }

        public NotifyBuilder Build()
        {
            return this;
        }

        public async Task SolveInitiator()
        {
            switch (_notify.InitiatorType)
            {
                case NotifyInitiatorType.Administrator:
                case NotifyInitiatorType.Moderator:
                case NotifyInitiatorType.Member:
                    {
                        var initiator = await _accountService.FindUserByIdAsync(_notify.InitiatorId, false);
                        Output.Initiator = $"用户‘{initiator.Nickname}’";
                        break;
                    }
                case NotifyInitiatorType.System:
                    {
                        Output.Initiator = "系统";
                        break;
                    }
            }

            Output.InitiatorType = _notify.InitiatorType;
            Output.InitiatorId = _notify.InitiatorId;
        }

        public void SolveOperation()
        {
            Output.Operation = _notify.OperationType.Name();
            Output.OperationType = _notify.OperationType;
        }

        public async Task SolvedRelationObject()
        {
            var prefix = _notify.RelationObjectType.Name();
            switch (_notify.RelationObjectType)
            {
                case NotifyRelationObjectType.Administrator:
                case NotifyRelationObjectType.Moderator:
                case NotifyRelationObjectType.Member:
                    {
                        var relationObject = await _accountService.FindUserByIdAsync(_notify.RelationObjectId, false);
                        Output.RelationObject = $"{prefix} ‘{relationObject.Nickname}’";
                        break;
                    }
                case NotifyRelationObjectType.Article:
                case NotifyRelationObjectType.Question:
                    {
                        var relationObject = await _articleService.FindById(Operator, Guid.Parse(_notify.RelationObjectId));
                        Output.RelationObject = $"{prefix} ‘{relationObject.Title}’";
                        break;
                    }
                case NotifyRelationObjectType.Comment:
                    {
                        //var relationObject = await _articleService.FindCommentById(Operator, long.Parse(_notify.RelationObjectId));
                        //Output.RelationObject = $"{prefix} ‘{relationObject.Content}’";
                        break;
                    }
            }

            Output.RelationObjectType = _notify.RelationObjectType;
            Output.RelationObjectId = _notify.RelationObjectId;
        }

        public async Task SolvedToObject()
        {
            var toObject = await _accountService.FindUserByIdAsync(_notify.InitiatorId, false);
            Output.ToObject = toObject.Nickname;
            Output.ToObjectId = Operator.Id;
        }


        public async Task SolvedState()
        {

        }
    }
}
