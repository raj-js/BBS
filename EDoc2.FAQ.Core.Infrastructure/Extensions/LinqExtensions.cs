using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// 如果满足 condition, 则使用 expression 过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereTure<T>(this IQueryable<T> @this, bool condition, Expression<Func<T, bool>> expression)
        {
            return condition ? @this.Where(expression) : @this;
        }

        public static IQueryable<T> WhereFalse<T>(this IQueryable<T> @this, bool condition,
            Expression<Func<T, bool>> expression)
        {
            return @this.WhereTure(!condition, expression);
        }

        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> @this, object obj, Expression<Func<T, bool>> expression)
        {
            return @this.WhereTure(obj != null, expression);
        }

        public static IQueryable<T> WhereNull<T>(this IQueryable<T> @this, object obj, Expression<Func<T, bool>> expression)
        {
            return @this.WhereTure(obj == null, expression);
        }

        /// <summary>
        /// 根据字段进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="expression">条件的字符串形式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> @this, string expression, bool isAsc)
        {
            return @this.OrderBy($"{expression} {(isAsc ? "asc" : "desc")}");
        }
    }
}
