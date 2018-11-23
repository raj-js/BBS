namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static string GenerateKey<T>(string key)
        {
            return $"{nameof(T)}_{key}";
        }
    }
}
