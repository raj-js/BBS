using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Auditings
{
    public class AuditingResult : Enumeration
    {
        public static AuditingResult Passed = new AuditingResult(1, "通过");
        public static AuditingResult Rejected = new AuditingResult(2, "驳回");

        public AuditingResult() { }

        public AuditingResult(int id, string name) : base(id, name) { }

        public static IEnumerable<AuditingResult> List() => new[] { Passed, Rejected };

        public static AuditingResult FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static AuditingResult From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
