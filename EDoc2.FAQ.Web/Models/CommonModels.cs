using System.ComponentModel.DataAnnotations;
using EDoc2.FAQ.Web.Data.Common;

namespace EDoc2.FAQ.Web.Models
{
    public class VmReport
    {
        [Required]
        public string TargetId { get; set; }
        [Required]
        public ReportTargetType ReportTargetType { get; set; }
        [Required]
        public ReportSubType SubType { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
    }
}
