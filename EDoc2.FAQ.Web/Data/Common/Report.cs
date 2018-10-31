using System;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Common
{
    /// <summary>
    /// 举报
    /// </summary>
    public class Report
    {
        public int Id { get; set; }
        public string ReporterId { get; set; }
        public string TargetId { get; set; }
        public ReportTargetType ReportTargetType { get; set; }
        public ReportSubType SubType { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }
        public ReportResult Result { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string ProcessMsg { get; set; }

        public virtual AppUser Reporter { get; set; }
    }
}
