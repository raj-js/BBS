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
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> @this, bool condition, Expression<Func<T, bool>> expression)
        {
            return condition ? @this.Where(expression) : @this;
        }

        public static IQueryable<T> WhereIfNot<T>(this IQueryable<T> @this, bool condition,
            Expression<Func<T, bool>> expression)
        {
            return @this.WhereIf(!condition, expression);
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
