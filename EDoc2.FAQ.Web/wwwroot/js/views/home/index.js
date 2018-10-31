(function (layui) {
    layui.use(['layer', 'laypage', 'laytpl', 'jquery', 'util', 'sys'], function () {
        var laypage = layui.laypage,
            layer = layui.layer,
            laytpl = layui.laytpl,
            $ = layui.jquery,
            sys = layui.sys,
            total = $('#total').val(),
            productId = $("#productId").val(),
            categoryId = $("#categoryId").val(),
            tagId = $("#tagId").val(),
            state = $("#state").val(),
            articleTemplate = articleTpl.innerHTML,
            articleContainer = document.getElementById('articlesContainer'),
            topArticleContainer = document.getElementById('topArticleContainer'),
            tagsTemplate = tagTpl.innerHTML,
            tagsContainer = document.getElementById('tagsContainer'),
            buzzTopicTemplate = buzzTopicTpl.innerHTML,
            buzzTopicContainer = document.getElementById('buzzTopicContainer'),
            urls = {
                loadArticles: "/Article/GetArticles",
                loadBuzzTopics: "/Article/GetBuzzTopics",
                loadTags: "/Article/GetTags",
                loadTopArticles: "/Article/GetTopArticles",
                loadDailySignIn: "/Account/LoadDailySign",
                dailySignIn: "/Account/DailySignIn",
                activeTop: "/Account/ActiveTop"
            };

        var loadPage = function (options) {
            var loading = layer.load();
            var start = (options.curr - 1) * options.limit;
            var length = options.limit;
            var query = "?product=" + productId + "&category=" + categoryId + "&tag=" + tagId + "&state=" + state + "&start=" + start + "&length=" + length;
            $.ajax({
                url: urls.loadArticles + query,
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

        if (total !== 0)
            laypage.render($.extend(pagerOption, { elem: 'footPager' }));

        var loadTopArticles = function () {
            var query = "?product=" + productId + "&category=" + categoryId + "&tag=" + tagId + "&state=" + state;
            $.ajax({
                url: urls.loadTopArticles + query,
                async: true,
                dataType: "json",
                method: "GET",
                success: function (result) {
                    if (result && result.length > 0) {
                        laytpl(articleTemplate).render(result,
                            function (html) {
                                topArticleContainer.innerHTML = html;
                            });
                    }
                }
            });
        }
        loadTopArticles();

        var loadTags = function () {
            $.ajax({
                url: urls.loadTags,
                async: true,
                dataType: "json",
                method: "GET",
                success: function (result) {
                    if (result) {
                        laytpl(tagsTemplate).render(result,
                            function (html) {
                                tagsContainer.innerHTML = html;
                            });
                    }
                }
            });
        }
        loadTags();

        var loadBuzzTopic = function () {
            $.ajax({
                url: urls.loadBuzzTopics,
                async: true,
                dataType: "json",
                method: "GET",
                success: function (result) {
                    if (result) {
                        laytpl(buzzTopicTemplate).render(result,
                            function (html) {
                                buzzTopicContainer.innerHTML = html;
                            });
                    }
                }
            });
        }
        loadBuzzTopic();

        var loadDailySignIn = function (callback) {
            var elemSignin = $('#signin'),
                elemKeepSignInDays = $('#keepSignInDays'),
                elemSignInScore = $('#signInScore'),
                elemScoreTip = $('#scoreTip');

            $.ajax({
                url: urls.loadDailySignIn,
                dataType: "json",
                type: "Get",
                success: function (result) {
                    if (result) {
                        if (result.isTodaySignIn) {
                            elemSignin.addClass(" layui-btn-disabled");
                            elemSignin.text('今日已签到');
                            elemScoreTip.text('已获得');
                        }
                        elemKeepSignInDays.text(result.keepSignInDays);
                        elemSignInScore.text(result.signInScore);
                    }
                }
            });
        };
        loadDailySignIn();

        var bindSignElements = function () {
            var elemSigninHelp = $('#signinHelp'),
                elemSigninTop = $('#signinTop'),
                elemSignin = $('#signin');

            elemSignin.on('click', function () {
                if (elemSignin.attr('class').indexOf('layui-btn-disabled') !== -1) return;
                elemSignin.addClass(" layui-btn-disabled");
                sys.authorizeHttp({
                    url: urls.dailySignIn,
                    dataType: "json",
                    type: "Get",
                    success: function (result) {
                        if (result) {
                            loadDailySignIn();
                            layer.msg("签到成功");
                        } else {
                            layer.msg('您可能已签到过了哦');
                        }
                    }
                });
            });
            elemSigninHelp.on('click', function () {
                layer.open({
                    type: 1
                    , title: '签到说明'
                    , area: '320px'
                    , shade: 0.8
                    , shadeClose: true
                    , content: ['<div class="layui-text" style="padding: 20px;">'
                        , '<blockquote class="layui-elem-quote">“签到”可获得社区财富值，规则如下</blockquote>'
                        , '<table class="layui-table">'
                        , '<thead>'
                        , '<tr><th>连续签到天数</th><th>每天可获财富值</th></tr>'
                        , '</thead>'
                        , '<tbody>'
                        , '<tr><td>＜5</td><td>5</td></tr>'
                        , '<tr><td>≥5</td><td>10</td></tr>'
                        , '<tr><td>≥15</td><td>15</td></tr>'
                        , '<tr><td>≥30</td><td>20</td></tr>'
                        , '<tr><td>≥100</td><td>30</td></tr>'
                        , '<tr><td>≥365</td><td>50</td></tr>'
                        , '</tbody>'
                        , '</table>'
                        , '<ul style="padding-top: 0; padding-bottom: 0;">'
                        , '<li>中间若有间隔，则连续天数重新计算</li>'
                        , '<li style="color: #FF5722;">不可利用程序自动签到，否则财富值清零</li>'
                        , '</ul>'
                        , '</div>'].join('')
                });
            });
            elemSigninTop.on('click', function () {
                layer.open({
                    type: 2,
                    title: '签到活跃榜 - TOP 15',
                    area: ['320px', '620px'],
                    shade: 0.8,
                    shadeClose: true,
                    content: urls.activeTop
                });
            });
        };
        bindSignElements();

    });
})(layui);