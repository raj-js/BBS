using System;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    public class CommentOp
    {
        public string CommentId { get; set; }

        public string OperatorId { get; set; }

        public DateTime OperateDate { get; set; }

        public OperateType Type { get; set; }

        public virtual AppUser Operator { get; set; }

        public virtual ArticleComment Comment { get; set; }
    }
}
