using System;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> Bool<T>(bool b) { return m => b; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.Or(left.Body, invokedExpr), left.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters);
            return Expression.Lambda<Func<T, bool>>(Expression.And(left.Body, invokedExpr), left.Parameters);
        }
    }
}
