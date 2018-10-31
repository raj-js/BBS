(function ($, layui, hljs) {
    var urls = {
        getReplies: "GetReplies",
        operateComments: "OperateComment",
        addFavorite: "/Account/AddFavorite",
        loadBuzzTopics: "/Article/GetBuzzTopics",
        loadTags: "/Article/GetTags",
        closeArticle: "/Article/ClosingArticle"
    },
        start = 0,
        length = 15,
        articleId = $("#Id").val(),
        template = commentTpl.innerHTML,
        view = document.getElementById('comments'),
        tagsTemplate = tagTpl.innerHTML,
        tagsContainer = document.getElementById('tagsContainer'),
        buzzTopicTemplate = buzzTopicTpl.innerHTML,
        buzzTopicContainer = document.getElementById('buzzTopicContainer'),
        elemIsFavorite = $('#IsFavorite'),
        elemFavorite = $('#favorite'),
        firstLoad = true;

    var bindOp = function () {
        $(".replyComment").unbind("click");
        $(".replyComment").click(function () {
            $("#toCommentId").val($(this).attr("data-commentId"));
        });

        $('a.commentOp').unbind('click');
        $('a.commentOp').on('click', function () {
            var op = $(this).attr('data-op'),
                commentId = $(this).attr('data-commentId'),
                $praise = $("a.commentOp[data-op='praise'][data-commentId='" + commentId + "']"),
                $tread = $("a.commentOp[data-op='tread'][data-commentId='" + commentId + "']");

            $.ajax({
                url: urls.operateComments + "?type=" + op + "&commentId=" + commentId,
                async: true,
                dataType: "json",
                method: "POST",
                success: function (result) {
                    layui.use(['layer'], function () {
                        var layer = layui.layer;
                        if (result) {
                            $praise.children('em').text(result.item1);
                            $tread.children('em').text(result.item2);
                        } else {
                            layer.msg('操作失败');
                        }
                    });
                }
            });
        });
    }

    var bindReporter = function () {
        $('a.reporter').unbind('click');
        $('a.reporter').on('click', function () {
            var $self = $(this);
            layui.use(['sys'], function () {
                var sys = layui.sys;
                sys.report({
                    area: ['650px', '360px'],
                    targetId: $self.attr('data-id'),
                    targetType: $self.attr('data-type')
                });
            });
        });
    }
    bindReporter();

    var closeArticle = function (articleId, isSatisfied, commentId, success) {
        layui.use(['sys'], function () {
            var sys = layui.sys;
            sys.authorizeHttp({
                url: urls.closeArticle,
                dataType: "json",
                type: "POST",
                data: {
                    ArticleId: articleId,
                    IsSatisfied: isSatisfied,
                    CommentId: commentId
                },
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                success: function (result) {
                    if (result) {
                        window.location.reload();
                    } else {
                        layer.msg('操作失败');
                    }
                }
            });
        });
    };
    var bindCloseArticle = function () {
        $('a.closeArticle').unbind('click');
        $('a.closeArticle').on('click', function () {
            var self = $(this),
                commentId = self.attr("data-commentId"),
                isSatisfied = commentId !== undefined,
                title = self.attr("title");

            layui.use(['layer', 'jquery'], function () {
                var layer = layui.layer,
                    $ = layui.jquery;

                if (isSatisfied) {
                    layer.confirm('确定将此回答采纳为最佳答案吗?',
                        {
                            icon: 3,
                            title: title,
                            anim: 5,
                        }, function (index) {
                            layer.close(index);
                            closeArticle(articleId, isSatisfied, commentId);
                        }
                    );
                } else {
                    layer.confirm('退还一半悬赏值，若悬赏值为奇数，则-1后取半，是否确定结贴?',
                        {
                            icon: 3,
                            title: title,
                            anim: 5,
                        }, function (index) {
                            layer.close(index);
                            closeArticle(articleId, isSatisfied, commentId);
                        }
                    );
                }
            });
        });
    };

    var loadReplies = function () {
        $.ajax({
            url: urls.getReplies + "?articleId=" + articleId + "&start=" + start + "&length=" + length,
            async: true,
            dataType: "json",
            method: "GET",
            success: function (result) {
                layui.use(['layer', 'laytpl', 'util'],
                    function () {
                        if (!result) {
                            layer.msg("获取数据异常");
                            return;
                        }

                        var laytpl = layui.laytpl,
                            layer = layui.layer;

                        if (result.length === 0 && !firstLoad) {
                            layer.msg("没有更多评论了");
                            return;
                        }
                        firstLoad = false;

                        start += result.length;
                        laytpl(template).render(result,
                            function (html) {
                                view.innerHTML = html;
                            });

                        $('pre code').each(function (i, block) {
                            hljs.highlightBlock(block);
                        });

                        bindOp();
                        bindReporter();
                        bindCloseArticle();
                    });
            }
        });
    }
    loadReplies();

    var bindFavorite = function () {
        elemFavorite.on('click', function () {
            var self = $(this);
            if (self.children('i').attr('class').indexOf('layui-icon-loading') !== -1) return;
            self.children('i').addClass("layui-icon-loading");

            layui.use(['layer', 'sys'], function () {
                var sys = layui.sys,
                    layer = layui.layer;
                sys.authorizeHttp({
                    url: urls.addFavorite,
                    dataType: "json",
                    type: "POST",
                    data: {
                        articleId: articleId
                    },
                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                    success: function (result) {
                        if (result) {
                            if (elemIsFavorite.val().toLowerCase() === "true") {
                                elemIsFavorite.val(false);
                                self.children('i').removeClass("layui-icon-star-fill").addClass("layui-icon-star");
                                self.attr("title", "收藏");
                                layer.msg("已取消收藏");
                            } else {
                                elemIsFavorite.val(true);
                                self.children('i').removeClass("layui-icon-star").addClass("layui-icon-star-fill");
                                self.attr("title", "取消收藏");
                                layer.msg("收藏成功");
                            }
                        } else {
                            layer.msg('操作失败');
                        }
                    }
                }).done(function () {
                    self.children('i').removeClass("layui-icon-loading");
                });
            });
        });
    };
    bindFavorite();

    var loadTags = function () {
        $.ajax({
            url: urls.loadTags,
            async: true,
            dataType: "json",
            method: "GET",
            success: function (result) {
                if (result) {
                    layui.use(['laytpl', 'util'], function () {
                        var laytpl = layui.laytpl,
                            util = layui.util;

                        laytpl(tagsTemplate).render(result,
                            function (html) {
                                tagsContainer.innerHTML = html;
                            });
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
                    layui.use(['layer', 'laytpl', 'util'], function () {
                        var laytpl = layui.laytpl,
                            util = layui.util;

                        laytpl(buzzTopicTemplate).render(result,
                            function (html) {
                                buzzTopicContainer.innerHTML = html;
                            });
                    });
                }
            }
        });
    }
    loadBuzzTopic();

    hljs.initHighlightingOnLoad();
})($, layui, hljs);