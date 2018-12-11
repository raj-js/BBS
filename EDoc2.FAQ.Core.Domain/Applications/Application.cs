using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class Application : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string IconBase64 { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 其他设置
        /// </summary>
        public virtual ICollection<ApplicationSetting> Settings { get; set; }

        private ApplicationSetting GetOrCreateSetting(string name, string @default = null, string description = null)
        {
            if (Settings == null)
                Settings = new List<ApplicationSetting>();

            var setting = Settings.SingleOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (setting != null) return setting;

            setting = new ApplicationSetting
            {
                Name =  name,
                Value = @default,
                Description = description
            };
            Settings.Add(setting);

            return setting;
        }

        private T GetSettingValue<T>(string name, string @default = null, string description = null, Func<string, T> converter = null)
        {
            var setting = GetOrCreateSetting(name, @default, description);

            return converter == null ? (T) (setting.Value as object) : converter(setting.Value);
        }

        public void SetSettingValue(string name, string value, string description = null)
        {
            var setting = GetOrCreateSetting(name);
            setting.Value = value;

            if (description != null)
                setting.Description = description;
        }

        /// <summary>
        /// 文章是否需要审核
        /// </summary>
        public bool IsArticleAuditing => GetSettingValue(ApplicationSetting.IsArticleAuditing, converter: bool.Parse);

        /// <summary>
        /// 回复是否需要审核
        /// </summary>
        public bool IsCommentAuditing => GetSettingValue(ApplicationSetting.IsCommentAuditing, converter: bool.Parse);

        /// <summary>
        /// 操作时间间隔（秒）
        /// </summary>
        public int OperationInterval => GetSettingValue(ApplicationSetting.OperationInterval, converter: int.Parse);

        /// <summary>
        /// 缓存过期时间 （秒）
        /// </summary>
        public int CacheExpireInterval => GetSettingValue(ApplicationSetting.CacheExpireInterval, converter: int.Parse);

        /// <summary>
        /// 单用户/单Ip 查看文章， 增长文章访问量的间隔时长 （分钟）
        /// </summary>
        public int ViewInterval => GetSettingValue(ApplicationSetting.ViewInterval, converter: int.Parse);
    }
}
