$("#zxd-form-submit").click(function () {
    var flag = 1;
    // 验证 
    if ($.trim($("#yixiang").val()) == "-1") {
        $(".zx-yixiang").css("display", "");
        flag = 0;
    } else {
        $(".zx-yixiang").css("display", "none");
    }
    if ($.trim($("#PName").val())== "") {
        $(".zx-pname").css("display", "");
        flag = 0;
    } else {
        $(".zx-pname").css("display", "none");
    } 
    if ($.trim($("#Format").val())== "") {
        $(".zx-zsyt").css("display", "");
        flag = 0;
    } else {
        $(".zx-zsyt").css("display", "none");
    }
    if ($.trim($("#district").val()) == "-1") {
        $(".zx-quyu").css("display", "");
        flag = 0;
    } else {
        $(".zx-quyu").css("display", "none");
    }
    if ($.trim($("#square").val())== "") {
        $(".zx-square").css("display", "");
        flag = 0;
    } else {
        $(".zx-square").css("display", "none");
    }
    if ($.trim($("#price").val())== "") {
        $(".zx-price").css("display", "");
        flag = 0;
    } else {
        $(".zx-price").css("display", "none");
    } 
    if ($.trim($("#Ptype").val()) == "-1") {
        $(".zx-Ptype").css("display", "");
        flag = 0;
    } else {
        $(".zx-Ptype").css("display", "none");
    }
    if ($.trim($("#Pinfo").val())== "") {
        $(".zx-Pinfo").css("display", "");
        flag = 0;
    } else {
        $(".zx-Pinfo").css("display", "none");
    }
    if ($.trim($("#Endtiem").val())== "") {
        $(".zx-jzrq").css("display", "");
        flag = 0;
    } else {
        $(".zx-jzrq").css("display", "none");
    }
    if ($.trim($("#Contacts").val())== "") {
        $(".zx-contacts").css("display", "");
        flag = 0;
    } else {
        $(".zx-contacts").css("display", "none");
    }
    if ($.trim($("#Mobile").val())== "") {
        $(".zx-mobile").css("display", "");
        flag = 0;
    } else {
        $(".zx-mobile").css("display", "none");
    }
    if (flag == 0) {
        return false;
    } else {
        $("#form_zx").submit();
    }
    //$.ajax({
    //    type: "GET",
    //    url: "/pOSTINGS/ADD",
    //    data: { Phone: $("#setphone").val(), Code: $("#valCode").val() },
    //    success: function (data) {
    //        if (data == 'ok') {
    //            $("#timer").val(times);
    //        } else {
    //            $("#yzm_div").next('h3').text("验证码不正确！");
    //            return false;
    //        }
    //    }
    //});

});

//$(function () {
//    $('#Endtiem').datetimepicker({
//        startDate: Date.now,
//        minView: "month", //选择日期后，不会再跳转去选择时分秒
//        format: "yyyy-mm-dd", //选择日期后，文本框显示的日期格式
//        language: 'zh-CN', //汉化
//        autoclose: true //选择日期后自动关闭
//    });
//})