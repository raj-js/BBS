using System;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Common
{
    public class LogScore
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public int Total { get; set; }
        public DateTime DateTime { get; set; }

        public virtual AppUser User { get; set; }
    }
}
