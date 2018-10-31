using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class EnumExtensions
    {
        public static string Display<T>(this T @this)
        {
            var type = @this.GetType();
            var field = type.GetField(@this.ToString());
            var attribute = field.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? string.Empty;
        }

        public static IEnumerable<TEnum> GetEnums<TEnum>() where TEnum: struct
        {
            foreach (var enumValue in Enum.GetValues(typeof(TEnum)))
            {
                yield return Enum.Parse<TEnum>(enumValue.ToString());
            }
        }
    }
}
