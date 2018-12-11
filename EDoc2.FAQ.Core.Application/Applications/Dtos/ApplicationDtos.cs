using EDoc2.FAQ.Core.Application.DtoBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EDoc2.FAQ.Core.Domain.Applications;

namespace EDoc2.FAQ.Core.Application.Applications.Dtos
{
    public partial class ApplicationDtos
    {
        #region 请求

        public class SettingReq
        {
            [Required]
            public int Id { get; set; }

            [Required]
            public string Value { get; set; }

            public ApplicationSetting To()
            {
                return new ApplicationSetting
                {
                    Id = Id,
                    Value = Value
                };
            }
        }

        public class UpdateSettingsReq : EntityDto<Guid>
        {
            [MaxLength(50)]
            [Required]
            public string Name { get; set; }

            [Required]
            [MaxLength(50)]
            public string Version { get; set; }

            public string IconBase64 { get; set; }

            [Required]
            [MaxLength(256)]
            public string Description { get; set; }

            public List<SettingReq> Settings { get; set; }

            public Domain.Applications.Application To()
            {
                return new Domain.Applications.Application
                {
                    Id = Id,
                    Name = Name,
                    Version = Version,
                    IconBase64 = IconBase64,
                    Description = Description,
                    Settings = Settings.Select(s => s.To()).ToList()
                };
            }
        }

        #endregion


        #region 响应

        public class Profile : EntityDto<Guid>
        {
            public string Name { get; set; }

            public string Version { get; set; }

            public string IconBase64 { get; set; }

            public string Description { get; set; }

            public List<AppSettings> Settings { get; set; } = new List<AppSettings>();

            public static Profile From(Domain.Applications.Application app)
            {
                var profile = new Profile
                {
                    Id = app.Id,
                    Name = app.Name,
                    Version = app.Version,
                    IconBase64 = app.IconBase64,
                    Description = app.Description
                };

                profile.Settings.AddRange(app.Settings.Select(s=> AppSettings.From(s)));
                return profile;
            }
        }

        public class AppSettings : EntityDto<int>
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
            public string Description { get; set; }

            public static AppSettings From(ApplicationSetting setting)
            {
                return new AppSettings
                {
                    Id = setting.Id,
                    Name = setting.Name,
                    Type = setting.Type,
                    Value = setting.Value,
                    Description = setting.Description
                };
            }
        }

        #endregion
    }
}
