using EDoc2.FAQ.Core.Domain.Accounts;
using System;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Core.Domain.Notifies.Analyzer.ToObject
{
    internal class ToAdministrator : IToObject
    {
        public bool Accept(User user)
        {
            return user != null && user.IsAdministrator;
        }

        public Expression<Func<Notify, bool>> Filter
        {
            get
            {
                return s => s.ToObjectType == NotifyToObjectType.Administrator;
            }
        }
    }
}
