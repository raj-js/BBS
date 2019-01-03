using EDoc2.FAQ.Core.Domain.Accounts;
using System;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Core.Domain.Notifies.Analyzer.ToObject
{
    internal class ToSingle : IToObject
    {
        private User _user;

        public bool Accept(User user)
        {
            this._user = user;

            return user != null;
        }

        public Expression<Func<Notify, bool>> Filter
        {
            get
            {
                return s => s.ToObjectId == _user.Id &&
                            s.ToObjectType == NotifyToObjectType.Single;
            }
        }
    }
}
