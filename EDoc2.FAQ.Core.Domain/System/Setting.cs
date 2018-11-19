using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.System
{
    /// <summary>
    /// 应用程序设置
    /// </summary>
    public class Setting : Entity
    {
        /// <summary>
        /// 设置项
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
