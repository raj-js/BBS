using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class Application : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string IconBase64 { get; set; }

        public string Description { get; set; }

        public DateTime LastModifyTime { get; set; }

        public virtual ICollection<Setting> Settings { get; set; }
    }
}
