using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Auditings
{
    public class AuditingTargetType : Enumeration
    {
        public static AuditingTargetType Article = new AuditingTargetType(1, "帖子");
        public static AuditingTargetType Comment = new AuditingTargetType(2, "回复");

        public AuditingTargetType() { }

        public AuditingTargetType(int id, string name) : base(id, name) { }

        public static IEnumerable<AuditingTargetType> List() => new[] { Article, Comment };

        public static AuditingTargetType FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static AuditingTargetType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
