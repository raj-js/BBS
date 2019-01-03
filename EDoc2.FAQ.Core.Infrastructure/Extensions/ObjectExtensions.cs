namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object self)
        {
            return self == null;
        }

        public static bool IsNotNull(this object self)
        {
            return !self.IsNull();
        }
    }
}
