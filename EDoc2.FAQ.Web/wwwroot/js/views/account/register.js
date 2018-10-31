(function ($, layui) {
    var urls = {
        getImgCode:"GetImgCode",
        sendVerCode: "SendVerCode"
    };

    layui.use(["layer"], function () {
        $("#edoc-imagecode").click(function() {
            $(this).attr("src", urls.getImgCode + "?_=" + new Date().getTime());
        });

        var sendVerCodeBtn = $("#edoc-getvercode");
        var originalText = sendVerCodeBtn.text();
        var verCodeInterval;

        var interval = {
            start: function (onStop) {
                sendVerCodeBtn.addClass("layui-btn-disabled");
                sendVerCodeBtn.attr("disabled", true);
                var seconds = 60;
                verCodeInterval = setInterval(function () {
                    if (seconds <= 0) {
                        clearInterval(verCodeInterval);
                        sendVerCodeBtn.removeClass("layui-btn-disabled");
                        sendVerCodeBtn.attr("disabled", false);
                        sendVerCodeBtn.text(originalText);
                        onStop ? onStop() : 0;
                    }

                    sendVerCodeBtn.text(seconds + "秒后可重新获取");
                    seconds--;
                }, 1000);
            }
        };

        sendVerCodeBtn.click(function () {
            var email = $("#Email").val();
            var code = $("#ImageCode").val();

            var regex = /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/;
            if (!regex.test(email)) {
                layer.msg("无效的邮箱");
                return;
            }

            if (code.length === 0) {
                layer.msg("请填写图形验证码");
                return;
            }

            var loading = layer.load();
            $.ajax({
                url: urls.sendVerCode + "?email=" + encodeURI(email) + "&code=" + encodeURI(code),
                async: true,
                dataType: "json",
                method: "GET",
                xhrFields: {
                    withCredentials: true
                },
                success: function (result) {
                    if (!result) {
                        layer.msg("无效的邮箱或者图形验证码错误");
                        return;
                    }
                    layer.msg("验证码已发送至您的邮箱，请查收");
                    interval.start();
                }
            }).done(function() {
                layer.close(loading);
            });
        });
    });
})($, layui);