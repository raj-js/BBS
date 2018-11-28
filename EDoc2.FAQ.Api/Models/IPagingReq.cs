namespace EDoc2.FAQ.Api.Models
{
    public interface IPagingReq
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        string OrderBy { get; }

        /// <summary>
        /// 是否升序
        /// </summary>
        bool IsAscending { get; }

        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 每页条目数
        /// </summary>
        int PageSize { get; }
    }
}
