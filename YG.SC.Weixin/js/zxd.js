$(function () {
    function a() {
        var a = $("#zxd-name").val(),
        c = $("#zxd-tel").val(),
        d = $("#User_Shen").val(),
        e = $("#User_City").val(),
        f = $("#zxd-type").val(),
        i = $("#post_url").val(),
        j = $.trim($("[name=area]").val()),
        k = $.trim($("[name=oarea]").val()),
        l = $.trim($("[name=zxys]").val()),
        m = $.trim($("[name=hometype]").val()),
        n = $("#form_zx").serialize(),
        o = $("#form_zx"),
        p = $('input[name="userTime"]', o),
        q = $('input[name="address"]', o),
        r = $('input[name="ptag"]', o).val();
        if ("undefined" != typeof clickStream && clickStream.getCvParams(r), !a) return void msgalert("请填写您的称呼");
        if (!c) return void msgalert("请填写您真实的联系电话");
        if ($("[name=area]").length && (!j || parseFloat(j) + "" != j + "")) return void msgalert("请您填写真实的房屋面积");
        if ($("[name=oarea]").length && (!k || parseFloat(k) + "" != k + "")) return void msgalert("请您填写真实的房屋面积");
        if ($("[name=zxys]").length && !l) return void msgalert("请您选择您的装修预算");
        if ($("[name=hometype]").length && !m) return void msgalert("请您选择装修类型");
        if ("省" == d) return void msgalert("请填写您所在的省");
        if ("市" == e) return void msgalert("请填写您所在的市");
        if (0 < $("#zxd-type").length && !f) return void msgalert("请填写您的装修方式");
        var s = /^(1[3|4|5|7|8])[\d]{9}$/;
        if (!s.test(c)) return void msgalert("请填写正确的联系电话");
        if ($(".zxd-form-tip").hide(), 0 < q.length && "" == $.trim(q.val())) return msgalert("请填写您的小区名"),
        !1;
        var t = $.trim($(p).val());
        if (0 < p.length) {
            if (!/^20\d{2}[0|1]\d{1}[0-3]\d$/.test(t)) return msgalert("请填写正确的时间"),
            !1;
            var u = "",
            v = new Date,
            w = v.getFullYear(),
            x = v.getMonth() + 1,
            y = v.getDate(),
            z = parseInt(t.substr(4, 2));
            if (D = 2 == z ? 0 == w % 4 && 0 != w % 100 && w % 200 != 0 && w % 300 != 0 && w % 400 != 0 ? 29 : 28 : 4 == z || 6 == z || 9 == z || 11 == z ? 30 : 31, u += v.getFullYear().toString(), u += 1 == x.toString().length ? "0" + x.toString() : x.toString(), u += 1 == y.toString().length ? "0" + y.toString() : y.toString(), parseInt(u) > parseInt(t)) return msgalert("验房时间不能小于当天"),
            !1;
            if (t.substr(4, 1) >= 1 && 2 < t.substr(5, 1)) return msgalert("请填写正确的时间"),
            !1;
            if (D < parseInt(t.substr(6, 2))) return msgalert("请填写正确的时间"),
            !1
        }
        $.ajax({
            type: "POST",
            url: i,
            dataType: "json",
            data: n,
            success: function (a) {
                var c = "",
                d = "",
                e = $("p.zxd-dialog-title"),
                f = $("p.zxd-dialog-info");
                if (1 == a.status || 5 == a.status) {
                    if (1 == a.status) {
                        c = "恭喜您，申请成功！",
                        d = "土巴兔客服将在24小时内与您电话联系，审核信息真实性，以便安排服务。",
                        h = a.tmpYid;
                        var i = $("body").data("jump_back");
                        if (h > 0 && $("#zxsj-wrap").length) {
                            var j = g("to8to_wap_tcode");
                            j = j ? j : "sz";
                            var k = document.location.hash;
                            return void (location.href = i && "" != i ? "/" + j + "/zb/completeinfo?yid=" + h + "&refer=" + i + k : "/" + j + "/zb/completeinfo?yid=" + h + k)
                        }
                    } else c = "很抱歉",
                    d = "您所在的城市尚未开通此项服务，我们会在开通后第一时间为您服务。";
                    e.html(c),
                    f.html(d),
                    $(".zxd-mask, .zxd-dialog").show(),
                    b()
                } else a.msg && msgalert(a.msg)
            }
        }),
        $("#zxd-form-submit").unbind("click").removeAttr("onclick").attr("disabled", !0)
    }
    function b() {
        document.body.style.overflow = "hidden",
        document.body.style.position = "fixed"
    }
    function c() {
        document.body.style.overflow = "auto",
        document.body.style.position = "static"
    }
    function d(a) {
        if (0 == $(a).length) return !0;
        var b = $(window).scrollTop(),
        c = b + $(window).height(),
        d = $(a).offset().top,
        e = d + $(a).height();
        return c >= e && d >= b
    }
    function e() {
        i && clearTimeout(i),
        i = setTimeout(function () {
            d("#zxd-name") || d("#zxd-form-submit") ? $(".zxd-apply").hide() : ($(".zxd-apply").show(), $("input:focus").blur())
        },
        100)
    }
    function f() {
        var a = 400 / 864e5,
        b = (new Date).getTime(),
        c = Math.round(a * b);
        $("#zxb_addamount").html(c - 61e5),
        setTimeout("autoAddZxbNumber()", 18e4)
    }
    function g(a) {
        var b, c = null,
        d = "" + document.cookie + ";",
        e = a + "=",
        f = d.indexOf(e);
        return -1 != f && (f += e.length, b = d.indexOf(";", f), c = d.substring(f, b)),
        c
    }
    var h = 0;
    $(".mfsj-cost").change(function () {
        var a = $(this).val();
        a ? $(this).next().hide() : $(this).next().show()
    }),
    $(".zxd-mask").height($(document).height()),
    $("#zxd-tel").blur(function () {
        var a = /^(1[3|4|5|7|8])[\d]{9}$/,
        b = $("#zxd-tel").val();
        a.test(b) ? $(".zxd-form-tip").hide() : $(".zxd-form-tip").show(),
        $("#zxd-tel").val() || $(".zxd-form-tip").hide()
    }),
    $("#zxd-form-submit").click(function (b) {
        b.preventDefault(),
        a()
    }),
    $("#zxd-dialog-ok").on("touchend",
    function (a) {
        if (-1 === location.href.indexOf("&openid=") && a.preventDefault(), $(".zxd-mask, .zxd-dialog").hide(), -1 === location.href.indexOf("&openid=") && c(), h > 0 && $("#zxsj-wrap").length) {
            var b = g("to8to_wap_tcode");
            b = b ? b : "sz";
            var d = document.location.hash;
            location.href = "/" + b + "/zb/completeinfo?yid=" + h + d
        } else location.reload()
    });
    var i;
    $(window).scroll(function () {
        e()
    }),
    e(),
    $('input[name="ptag"]').buriedPoint(),
    f()
}),
jQuery.fn.buriedPoint = function () {
    var a = location.href.split("#"),
    b = "";
    for (var c in a) {
        var d = a[c].split("=");
        if (d && "point" == d[0] && d[1]) {
            b = d[1];
            break
        }
    }
    return this.each(function (a, c) {
        "" != b && (c.value = b)
    }),
    this
};
//# sourceMappingURL=zxd.min.js.map
