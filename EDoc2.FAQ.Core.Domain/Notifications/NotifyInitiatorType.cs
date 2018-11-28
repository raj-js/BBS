using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public class NotifyInitiatorType : Enumeration
    {
        public static NotifyInitiatorType System = new NotifyInitiatorType(1, "系统");
        public static NotifyInitiatorType Moderator = new NotifyInitiatorType(2, "版主");
        public static NotifyInitiatorType Account = new NotifyInitiatorType(3, "会员");

        public NotifyInitiatorType()
        {
        }

        public NotifyInitiatorType(int id, string name)
        : base(id, name)
        {

        }

        public static IEnumerable<NotifyInitiatorType> List() => new[] { System, Moderator, Account };

        public static NotifyInitiatorType FromName(string name)
        {
            var type = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (type == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return type;
        }

        public static NotifyInitiatorType From(int id)
        {
            var type = List().SingleOrDefault(s => s.Id == id);
            if (type == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return type;
        }
    }
}
