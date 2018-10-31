using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Extensions;
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
            { string.Empty, "首页" },
            { CategoryConsts.Question, "问答" },
            { CategoryConsts.Share, "分享"},
            { CategoryConsts.Discuss, "讨论" },
            { CategoryConsts.Suggest, "建议" }
        };

        private static readonly Dictionary<string, string> SubNavs = new Dictionary<string, string>
        {
            { string.Empty, "综合" },
            { ArticleState.NotSolve.ToString("F"), "未结" },
            { ArticleState.Solved.ToString("F"), "已结" },
            { ArticleState.Wonderful.ToString("F"), "精华" }
        };

        public static IEnumerable<Nav> SelectNav(VmNav vm)
        {
            foreach (var key in Navs.Keys)
            {
                object routeValues = null;
                if (string.IsNullOrEmpty(key))
                    routeValues = new VmNav { Product = vm.Product };
                else
                    routeValues = new VmNav
                    {
                        Product = vm.Product,
                        Category = key,
                        State = vm.State,
                        Tag = vm.Tag
                    };

                yield return new Nav
                {
                    LinkText = Navs[key],
                    RouteValues = routeValues,
                    Class = key.Equals(vm.Category) ? "layui-this" : string.Empty
                };
            }
        }

        public static IEnumerable<Nav> SelectSubNav(VmNav vm)
        {
            foreach (var key in SubNavs.Keys)
            {
                object routeValues = null;
                if (string.IsNullOrEmpty(vm.Category))
                    routeValues = new VmNav { Product = vm.Product, State = key };
                else
                    routeValues = new VmNav
                    {
                        Product = vm.Product,
                        Category = vm.Category,
                        State = key,
                        Tag = vm.Tag
                    };

                yield return new Nav
                {
                    LinkText = SubNavs[key],
                    RouteValues = routeValues,
                    Class = key.Equals(vm.State) ? "layui-this" : string.Empty
                };
            }
        }
    }

    public class VmNav
    {
        public string Product { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public string State { get; set; }
    }
}
