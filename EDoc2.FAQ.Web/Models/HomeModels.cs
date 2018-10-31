using System.Collections.Generic;

namespace EDoc2.FAQ.Web.Models
{
    public class VmHomeIndex
    {
        /// <summary>
        /// 所属产品
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 文章分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 文章标签
        /// </summary>
        public string Tag { get; set; }

        public int Total { get; set; }
        public string State { get; set; }
        public List<VmTopReplie> TopReplies { get; set; } = new List<VmTopReplie>();

        public VmNav Nav { get; set; }
    }

    public class VmTopReplie
    {
        public VmTopReplie(string appUserId, string appUserName, int replies)
        {
            UserId = appUserId;
            User = appUserName;
            Replies = replies;
        }

        public string UserId { get; set; }
        public string User { get; set; }
        public int Replies { get; set; }
    }
}
