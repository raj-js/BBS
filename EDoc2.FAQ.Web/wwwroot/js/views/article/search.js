(function (layui) {
    layui.use(['layer', 'laypage', 'laytpl', 'jquery', 'util'], function () {
        var laypage = layui.laypage,
            layer = layui.layer,
            laytpl = layui.laytpl,
            $ = layui.jquery,
            total = $('#total').val(),
            keyword = $('#keyword').val(),
            articleTemplate = articleTpl.innerHTML,
            articleContainer = document.getElementById('articlesContainer'),
            urls = {
                search: "/Article/SearchArticles"
            };

        var loadPage = function (options) {
            var loading = layer.load();
            var start = (options.curr - 1) * options.limit;
            var length = options.limit;
            var query = "?keyword=" + encodeURIComponent(keyword) + "&start=" + start + "&length=" + length;
            $.ajax({
                url: urls.search + query,
                async: true,
                dataType: "json",
                method: "GET",
                success: function (result) {
                    if (result) {
                        laytpl(articleTemplate).render(result,
                            function (html) {
                                articleContainer.innerHTML = html;
                            });
                    }
                }
            }).done(function () {
                layer.close(loading);
            });
        }

        var pagerOption = {
            limit: 20,
            limits: [20, 50, 75, 100],
            count: total,
            groups: 3,
            layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
            jump: function (obj, first) {
                loadPage(obj);
            }
        };

        if (total != 0)
            laypage.render($.extend(pagerOption, { elem: 'pager' }));
    });
})(layui);