namespace EDoc2.FAQ.Core.Application.DtoBase
{
    public interface IPagingRequest
    {
        /// <summary>
        /// 过滤前 ‘PageIndex’ 条数据
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 获取 'PageSize' 条数据
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        string OrderBy { get; }

        /// <summary>
        /// 是否升序
        /// </summary>
        bool IsAscending { get; }
    }
}
