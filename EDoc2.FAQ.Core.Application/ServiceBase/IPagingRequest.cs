namespace EDoc2.FAQ.Core.Application.ServiceBase
{
    public interface IPagingRequest
    {
        /// <summary>
        /// 过滤前 ‘Skip’ 条数据
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// 获取 'Take' 条数据
        /// </summary>
        int Take { get; }

        /// <summary>
        /// 排序字段
        /// </summary>
        string OrderBy { get; }

        /// <summary>
        /// 是否升序
        /// </summary>
        bool IsAsc { get; }
    }
}
