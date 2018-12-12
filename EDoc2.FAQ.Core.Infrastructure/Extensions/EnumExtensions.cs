using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class Enums
    {
        public static int Id<T>(this T @this)
        {
            return (int) (@this as object);
        }

        public static string Name<T>(this T @this)
        {
            var type = @this.GetType();
            var field = type.GetField(@this.ToString());
            var attribute = field.GetCustomAttribute<DisplayNameAttribute>();
            return attribute?.DisplayName ?? string.Empty;
        }

        public static IEnumerable<TEnum> List<TEnum>() where TEnum : struct
        {
            foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
            {
                yield return Enum.Parse<TEnum>(enumValue.ToString());
            }
        }
    }
}
