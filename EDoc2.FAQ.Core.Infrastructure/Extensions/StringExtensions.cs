namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self) || string.IsNullOrWhiteSpace(self);
        }
    }
}
