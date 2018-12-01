using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class EfCoreExtensions
    {
        /// <summary>
        /// 将实体附加到 ChangeTracker
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity"></param>
        public static void AttachIfNot<T>(this DbContext @this, T entity) where T : class
        {
            if (!@this.ChangeTracker.Entries().Any(s => s.Entity.Equals(entity)))
                @this.Attach(entity);
        }

        /// <summary>
        /// 部分更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        public static void UpdatePartly<T>(this DbContext @this, T entity, params string[] properties) where T : class
        {
            var entry = @this.Entry(entity);

            entry.Properties
                .ToList()
                .ForEach(s => s.IsModified = properties.Contains(s.Metadata.Name));
        }
    }
}
