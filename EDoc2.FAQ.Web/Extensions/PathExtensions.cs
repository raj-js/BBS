using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class PathExtensions
    {
        public static string MapPath(this IHostingEnvironment @this, string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(@this.WebRootPath, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }

        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }
    }
}
