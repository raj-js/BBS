using System.Collections.Generic;

namespace EDoc2.FAQ.Web.Models
{
    public class Nav
    {
        public string ControllerName { get; set; } = "Home";
        public string ActionName { get; set; } = "Index";
        public string LinkText { get; set; }
        public object RouteValues { get; set; }
        public string Class { get; set; }

        private static readonly Dictionary<string, string> Navs = new Dictionary<string, string>
        {
            { "index", "首页" },
            { "faq", "问答" },
            { "share", "分享" },
            { "discuss", "讨论" },
            { "suggest", "建议" },
            { "notice", "公告" }
        };

        private static readonly Dictionary<string, string> SubNavs = new Dictionary<string, string>
        {
            { "index", "综合" },
            { "unsolve", "未结" },
            { "solved", "已结" },
            { "wonderful", "精华" }
        };

        public static IEnumerable<Nav> SelectNav(string nav)
        {
            var selected = Navs.ContainsKey(nav) ? nav : "index";
            foreach (var key in Navs.Keys)
            {
                yield return new Nav
                {
                    LinkText = Navs[key],
                    RouteValues = new { nav = key },
                    Class = selected.Equals(key) ? "layui-this" : string.Empty
                };
            }
        }

        public static IEnumerable<Nav> SelectSubNav(string nav, string subNav)
        {
            nav = Navs.ContainsKey(nav) ? nav : "index";
            var selected = SubNavs.ContainsKey(subNav) ? subNav : "index";
            foreach (var key in SubNavs.Keys)
            {
                yield return new Nav
                {
                    LinkText = SubNavs[key],
                    RouteValues = new { nav, subNav = key },
                    Class = selected.Equals(key) ? "layui-this" : string.Empty
                };
            }
        }
    }
}
