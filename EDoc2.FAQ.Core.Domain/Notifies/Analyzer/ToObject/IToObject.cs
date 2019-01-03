using EDoc2.FAQ.Core.Domain.Accounts;
using System;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Core.Domain.Notifies.Analyzer.ToObject
{
    internal interface IToObject
    {
        bool Accept(User user);

        Expression<Func<Notify, bool>> Filter { get; }
    }
}
