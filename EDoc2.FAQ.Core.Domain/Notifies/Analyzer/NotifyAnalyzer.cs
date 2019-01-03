using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Notifies.Analyzer.ToObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Core.Domain.Notifies.Analyzer
{
    internal class NotifyAnalyzer
    {
        private static IToObject[] _toObjects;

        private static IToObject[] ToObjects
        {
            get
            {
                return _toObjects ?? (_toObjects = typeof(IToObject).Assembly.GetTypes()
                           .Where(t => typeof(IToObject).IsAssignableFrom(t) && t != typeof(IToObject))
                           .Select(t => (IToObject) Activator.CreateInstance(t))
                           .ToArray());
            }
        }

        public static IEnumerable<IToObject> FilterToObjects(User user)
        {
            return ToObjects.Where(s => s.Accept(user));
        }

        public static IQueryable<Notify> FiterNotifies(IQueryable<Notify> notifies, IEnumerable<IToObject> toObjects)
        {
            if (toObjects == null || toObjects.Any()) return notifies;

            Expression<Func<Notify, bool>> predicate = s => true;
            foreach (var toObject in toObjects)
            {
                var expression = Expression.Invoke(toObject.Filter, predicate.Parameters);
                predicate = Expression.Lambda<Func<Notify, bool>>(Expression.Or(predicate.Body, expression), predicate.Parameters);
            }
            return notifies.Where(predicate);
        }
    }
}
