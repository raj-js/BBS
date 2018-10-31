(function ($, layui) {
    layui.use(['layer', 'laypage', 'laytpl', 'jquery', 'util'], function () {
        var laypage = layui.laypage,
            layer = layui.layer,
            laytpl = layui.laytpl,
            $ = layui.jquery,
            articleTotal = $('#articleTotal').val(),
            favoriteTotal = $('#favoriteTotal').val(),
            template = articleTpl.innerHTML,
            articleContainer = document.getElementById('articlesContainer'),
            favoriteContainer = document.getElementById('favoriteContainer'),
            urls = {
                loadArticles: "/Article/GetSelfArticles",
                loadFavorite: "/Article/GetSelfFavorite"
            };

        var articlePager = {
            limit: 15,
            count: articleTotal,
            groups: 3,
            layout: ['prev', 'page', 'next'],
            jump: function (options, first) {
                var loading = layer.load();
                var start = (options.curr - 1) * options.limit;
                var length = options.limit;
                var query = "?start=" + start + "&length=" + length;
                $.ajax({
                    url: urls.loadArticles + query,
                    async: true,
                    dataType: "json",
                    method: "GET",
                    success: function (result) {
                        if (result) {
                            laytpl(template).render(result,
                                function (html) {
                                    articleContainer.innerHTML = html;
                                });
                        }
                    }
                }).done(function () {
                    layer.close(loading);
                });
            }
        };

        var favoritePager = {
            limit: 15,
            count: favoriteTotal,
            groups: 3,
            layout: ['prev', 'page', 'next'],
            jump: function (options, first) {
                var loading = layer.load();
                var start = (options.curr - 1) * options.limit;
                var length = options.limit;
                var query = "?start=" + start + "&length=" + length;
                $.ajax({
                    url: urls.loadFavorite + query,
                    async: true,
                    dataType: "json",
                    method: "GET",
                    success: function (result) {
                        if (result) {
                            laytpl(template).render(result,
                                function (html) {
                                    favoriteContainer.innerHTML = html;
                                });
                        }
                    }
                }).done(function () {
                    layer.close(loading);
                });
            }
        };

        if (articleTotal != 0)
            laypage.render($.extend(articlePager, { elem: 'articlesPager' }));

        if (favoriteTotal != 0)
            laypage.render($.extend(favoritePager, { elem: 'favoritePager' }));

    });
})($, layui);