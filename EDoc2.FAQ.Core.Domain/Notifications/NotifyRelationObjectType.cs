using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public class NotifyRelationObjectType : Enumeration
    {
        public static NotifyRelationObjectType Administrator = new NotifyRelationObjectType(1, "管理员");
        public static NotifyRelationObjectType Moderator = new NotifyRelationObjectType(2, "版主");
        public static NotifyRelationObjectType Account = new NotifyRelationObjectType(3, "会员");
        public static NotifyRelationObjectType Article = new NotifyRelationObjectType(4, "文章/帖子");
        public static NotifyRelationObjectType Comment = new NotifyRelationObjectType(5, "评论");

        public NotifyRelationObjectType()
        {
        }

        public NotifyRelationObjectType(int id, string name)
        : base(id, name)
        {

        }

        public static IEnumerable<NotifyRelationObjectType> List() => new[] { Administrator, Moderator, Account, Article, Comment };

        public static NotifyRelationObjectType FromName(string name)
        {
            var type = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (type == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return type;
        }

        public static NotifyRelationObjectType From(int id)
        {
            var type = List().SingleOrDefault(s => s.Id == id);
            if (type == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return type;
        }
    }
}
