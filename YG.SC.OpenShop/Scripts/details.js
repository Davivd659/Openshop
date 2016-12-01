$(document).ready(function () {
    // 1 项目亮点项目亮点项目亮点 

    //项目介绍
    $("#li_1").click(function () {
        project_switch('js', this);

    });
    //项目亮点
    $("#li_2").click(function () {
        project_switch('ld', this);
    });
    //项目信息
    $("#li_3").click(function () {
        project_switch('xx', this);
    });
    //房源介绍
    $("#li_4").click(function () {
        project_switch('fy', this);
    });
    //房源介绍
    $("#li_5").click(function () {
        project_switch('fy', this);
    });
    //房源介绍
    $("#li_6").click(function () {
        project_switch('fy', this);
    });
    //房源介绍
    $("#li_7").click(function () {
        project_switch('fy', this);
    });
    //服务团队
    $("#li_8").click(function () {
        project_switch('fw', this);
    });
    //位置
    $("#li_9").click(function () {
        project_switch('wz', this);
    });
    //流程图
    $("#li_10").click(function () {
        project_switch('lc', this);
    });
    function project_switch(name, obj) {
        $(".warp-box").hide();
        $("#xiangmu_" + name).show();
        $(".warp_2 li").removeClass('li_hover');
        $(obj).addClass('li_hover');
    };

    $(".warp_2 li").mouseout(function () {
        $(this).css(" border", "none");
    })
    $(".warp_2 li").mousedown(function () {
        $(this)(
          {
              "border": '1px solid #dedede',
              "border-top": '2px solid #c00',
              "background": '#f2f2f2',
              "border-bottom": '1px solid #f2f2f2'
          }

        );
    })
    //北京电影学院创意图（形象图hove切换）

    $(".del-x li").hover(function () {
        $(".del-s img").attr("src", $(this).find("img").attr("src"))
    });
    //   北京电影学院创意图 灯箱效果幻灯片
    $(".del-x").click(function () {
        $("#dengxiang").show();
        $(".dx_guan").show();
        $("#dx_slide").show();
        $(".valin img").attr("src", $(this).find("img").attr('src'));


    });
    //轮播加载数据
    $(".del-x li,.shanpan, .huxing li,.xm-img li").bind("click", function () {

        var type = $(this).find(".typeid").val();
        var ProjectId = $("#ProjectId").val();
        var paramaters = {};
        paramaters.ShopProjectId = ProjectId;
        paramaters.Type = type;
        var url = "/Project/Getphoto";
        $.ajax({
            type: 'post',
            url: url,
            cache: false,
            async: false,
            dataType: "json",
            data: paramaters,
            success: function (result) {
                var Photohtml = "";
                if (result != null) {
                    $.each(result, function (i, data) {
                        Photohtml += "<li><div><img src=" + data.PhotoUrl + " onload=\"scaleImage(this,99,75)\"></div><p>" + data.PhotoName + "</p>"
                        Photohtml += "<i class=\"dn\">" + data.PhotoUrl + "</i></li>"
                    })
                    $("#photoList").html(Photohtml);
                    $("#thumWrap").css("width", "100%");
                    $("#photoList").find("li").first().addClass("on");
                } else {
                    alert("报名失败，请重试！")
                }
            }, error: function (result) {
                console.log(result);
            }
        });


        $("#dengxiang").show();
        $(".dx_guan").show();
        $("#dx_slide").show();
        $(".dx_guan").show();
        $(".valin img").attr("src", $(this).find("img").attr('src'));


    });
    //轮播点击事件
    $("#photoList").on("click", "li", function () {
        $(this).parent("ul").find("li").removeClass("on");
        $(this).addClass("on");
        $(".valin img").attr("src", $(this).find("img").attr('src'));
    });
    $(".dx_guan").click(function () {
        $(this).hide();
        $("#dx_slide").hide();
        $("#dengxiang").hide();
    });
    $("#dengxiang").click(function () {
        $(this).hide();
        $("#dx_slide").hide();

    });
    //项目图片
    $(".del-tit2").find("ul li").last().css("border", "none");
    // 弹出的报名申请框
    $(".shenqing").click(function () {
        $("#dengxiang").show();
        $(".warp_1 .del_baoming").show();
    });

    $(".guanbi").click(function () {
        $("#dengxiang").hide();
        $(".warp_1 .del_baoming").hide();
    });
    $("#dengxiang").click(function () {
        $("#dengxiang").hide();
        $(".warp_1 .del_baoming").hide();
    });

    // 弹出的报名申请框
})

// 计时器
function ShowCountDown(date, divname) {
    var now = new Date();
    var endDate = new Date(date);
    var leftTime = endDate.getTime() - now.getTime();
    var leftsecond = parseInt(leftTime / 1000);
    //var day1=parseInt(leftsecond/(24*60*60*6)); 
    var day1 = Math.floor(leftsecond / (60 * 60 * 24));
    var hour = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
    var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour * 3600) / 60);
    var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour * 3600 - minute * 60);
    var cc = document.getElementById(divname);
    cc.innerHTML = "距离结束&nbsp;<b style='background: #d79c0d; display: inline-block; width:30px; height:30px; border-radius: 5px;   text-align: center; color:#fff; margin: 0 10px; font-weight: 900;'>" + day1 + "</b>天<b style='background: #d79c0d; display: inline-block; width:30px; height:30px; border-radius: 5px;   text-align: center; color:#fff; margin: 0 10px; font-weight: 900;'>" + hour + "</b>小时<b style='background: #d79c0d; display: inline-block; width:30px; height:30px; border-radius: 5px;   text-align: center; color:#fff; margin: 0 10px; font-weight: 900;'>" + minute + "</b>分<b style='background: #d79c0d; display: inline-block; width:30px; height:30px; border-radius: 5px;   text-align: center; color:#fff; margin: 0 10px; font-weight: 900;'>" + second + "</b>秒";
}

