layui.define(['layer', 'jquery', 'form', 'layedit'], function (exports) {
    "use strict";

    var layer = layui.layer,
        $ = layui.jquery,
        urls = {
            AddReport: '/Home/AddReport'
        },
        MOD_NAME = "sys",
        sys = {};

    sys.report = function (options) {
        var config = {
            elements: {
                $main: $('#dialogReport'),
                $subType: $("#dialogReport select[name='SubType']"),
                $description: $("#dialogReport textarea[name='Description']"),
            },
            area: ['650px', '320px']
        };
        config = $.extend({}, config, options);
        var elements = config.elements;

        if (!elements.$main || elements.$main.length === 0) {
            layer.msg('sys.config.element 未指定');
            return;
        }
        if (!config.targetId) {
            layer.msg('未指定 targetId');
        }
        if (!config.targetType) {
            layer.msg('未指定 targetType');
        }

        layer.open({
            title: "举 报",
            type: 1,
            anim: 5,
            resize: false,
            area: config.area,
            content: elements.$main,
            btn: ['确定', '取消'],
            yes: function (index) {
                sys.authorizeHttp({
                    url: urls.AddReport,
                    dataType: "json",
                    type: "POST",
                    data: {
                        TargetId: config.targetId,
                        TargetType: config.targetType,
                        SubType: elements.$subType.val(),
                        Description: elements.$description.val()
                    },
                    contentType: "application/x-www-form-urlencoded; charset=utf-8",
                    success: function (result) {
                        if (result) {
                            layer.msg("提交信息成功，如果情况属实，举报者可获取相应奖励");
                        } else {
                            layer.msg('提交失败');
                        }
                    }
                }).done(function () {
                    elements.$subType.val(0);
                    elements.$description.val('');
                    layer.close(index);
                });
            },
            cancel: function () { }
        });
    };

    sys.authorizeHttp = function (options) {
        var config = {};
        config = $.extend({}, config, options);
        return $.ajax(config)
            .complete(function (XHR, TS) {
                console.log(XHR, TS);
                if (XHR.status === 401) {
                    sys.redirectToLogin();
                }
            });
    };

    sys.redirectToLogin = function () {
        var location = window.location;
        var returnUrl = 'returnUrl=' + encodeURI(location.pathname + location.search);
        var loginUrl = window.location.origin + '/Account/Login';
        window.location.href = loginUrl + '?' + returnUrl;
    };

    sys.msg = {
        error: function (msg, focus) {
            layer.msg(msg, { icon: 5 })
            if (focus) {
                $(focus).focus();
            }
        }
    };

    exports(MOD_NAME, sys);
})