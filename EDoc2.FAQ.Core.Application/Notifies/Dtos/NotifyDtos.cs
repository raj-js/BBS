using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Domain.Notifies;
using System;

namespace EDoc2.FAQ.Core.Application.Notifies.Dtos
{
    public partial class NotifyDtos
    {
        #region Request

        /// <summary>
        /// 搜索信息
        /// </summary>
        public class SearchReq : IPagingRequest
        {
            /// <summary>
            /// 发起对象类型
            /// </summary>
            public NotifyInitiatorType? InitiatorType { get; set; }

            /// <summary>
            /// 发起时间
            /// </summary>
            public (DateTime? Begin, DateTime? End) InitiationTime { get; set; }

            /// <summary>
            /// 操作类型
            /// </summary>
            public NotifyOperationType? OperationType { get; set; }

            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string OrderBy { get; set; } = "InitiationTime";
            public bool IsAscending { get; set; }
        }

        /// <summary>
        /// 搜索自己的信息
        /// </summary>
        public class SearchSelfNotifyReq : IPagingRequest
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string OrderBy { get; set; } = "InitiationTime";
            public bool IsAscending { get; set; }
        }

        #endregion

        #region Response

        public class ListItem : EntityDto<Guid>
        {

            public static ListItem From(Notify notify)
            {
                return new ListItem();
            }
        }

        public class Notification : EntityDto<Guid>
        {
            /// <summary>
            /// 消息标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 发起者类型
            /// </summary>
            public NotifyInitiatorType InitiatorType { get; set; }

            /// <summary>
            /// 发起者编号
            /// </summary>
            public string InitiatorId { get; set; }

            /// <summary>
            /// 发起者称呼
            /// </summary>
            public string Initiator { get; set; }

            /// <summary>
            /// 操作类型
            /// </summary>
            public NotifyOperationType OperationType { get; set; }

            /// <summary>
            /// 操作类型显示
            /// </summary>
            public string Operation { get; set; }

            /// <summary>
            /// 关联对象类型
            /// </summary>
            public NotifyRelationObjectType RelationObjectType { get; set; }

            /// <summary>
            /// 关联对象编号
            /// </summary>
            public string RelationObjectId { get; set; }

            /// <summary>
            /// 关联对象称呼
            /// </summary>
            public string RelationObject { get; set; }

            /// <summary>
            /// 被通知对象编号
            /// </summary>
            public string ToObjectId { get; set; }

            /// <summary>
            /// 被通知对象称呼
            /// </summary>
            public string ToObject { get; set; }

            /// <summary>
            /// 发起时间
            /// </summary>
            public DateTime InitiationTime { get; set; }
        }

        #endregion
    }
}
