//用于保存姓名首字母与姓名的键值对
var nameList = new Array();
$(document).ready(function () {
    $(".m_c li").live("click", function () {
        getnode($(this).find(".hotareid").val());
        loaddata();
    })

        var $li = $('.m_let li');
        var $ul = $('.m_city_z ul');

        $li.mouseover(function () {
            var $this = $(this);
            var $t = $this.index();
            $li.removeClass();
            $this.addClass('current');
            //获取筛选内容长度
            var keyLength = $this.text().length;
            var word = ""; //记录符合条件的中文字词
            //遍历索引，执行查找
            for (var i = 0; i < nameList.length; i++) {
                //截取等长字符与筛选字母组合进行比较
                if (nameList[i][0].substr(0, keyLength) == $this.text()) {
                    if (word == "") {
                        word += "<li> " + nameList[i][1] + "</li>";
                    } else {
                        word += "<li> " + nameList[i][1] + "</li>";
                    }
                }
            }
            $("#divName").html(word);
        })

    //选择城市
    $(".m_area_off").click(function () {
        $("#dengxiang").css("display", "none");
        $(".m_area").css("display", "none");
    });

    //头部固定导航
    $(window).scroll(function (event) {
        var scroll_h = $(window).scrollTop();
        if (scroll_h > 100) {
            $(".m_nav2").fadeIn(500);
        }
        else {
            $('.m_nav2').fadeOut(500);
        }
    });
    //  点击固定头部导航的购物车，出现购物车下来菜单
    $(".m_cart").click(function () {
        $(this).parent().find(".m_car_all").toggle();
    });


    // 商品加入购物车
    var add_1 = $("#con_goods_id_1").parent(".con_goods");
    //点击图片下面的小图小购车
    $(".m_content li  ol .goods_car").click(function () {
        var $this = $(this).parents("li");
        //    固定头部  购物车div加
        var img = $this.find("img").attr("src");
        var name = $this.find("h3").text();
        var id = $this.find(".goodsid").val();
        var price = $this.find(".m_price span").text();
        var mon_pay = $this.find(".m_price strong b").text(); //单价
        var kinds = parseInt($(".m_cart_z span").first().text());
        var num = $this.find(".num_choice").val();
        var gsid = "";
        $(".m_cart_goods").find(".gsid").each(function (i, goods) {
            gsid += $(goods).val() + ",";
        });
        if ($(".m_cart_goods").length == 0 || gsid.indexOf(id) < 0) {
            var html = '<div class="m_cart_goods"><a class="goods_img" href="#"><img src="' + img + '"></a><div class="m_cart_name"><p class="m_cart_name_tit">' + name + '<span>' + price + '</span></p><p class="m_cart_num">&yen;<span class="red">' + mon_pay + '</span>' + '&times;' + '<em>' + num + '</em></p></div><strong class="goods_off"></strong>' +
                '<input type="hidden" class="gsid" name="GoodsId" value="' + id + '" /> <input type="hidden" class="GoodsCount"  name="GoodsCount" value="' + num + '" /><input type="hidden" class="Price"  name="Price" value="' + price + '" /></div>';
            $(".m_car_all .m_z").append(html);
            $this.find(".cart_prom").show();
            $this.find(".cart_prom").fadeOut(1000);
            $(".m_cart_z span").text(kinds + 1);
        }
        else {
            $(".m_cart_goods").find(".gsid").each(function (i, goods) {
                if ($(goods).val() == id) {
                    $(this).parents(".m_cart_goods").find("em").text(parseInt($(this).parents(".m_cart_goods").find("em").text()) + parseInt(num));
                    $(this).parents(".m_cart_goods").find(".GoodsCount").val(parseInt($(this).parents(".m_cart_goods").find(".GoodsCount").val) + parseInt(num));
                    $this.find(".cart_prom").show();
                    $this.find(".cart_prom").fadeOut(1000);
                }



            });
        }


        // 计算头部购物车里的总计
        // 循环头部购物车里的每种商品
        total_price();
        var Goods = {
            GoodsId: id,
            Price: price,
            GoodsCount: num
        };
        var option = {
            url: '/Mall/AddCarts',
            type: 'POST',
            data: Goods,
            dataType: 'html',
            success: function (result) { alert(result); }
        };
        $.ajax(option);

    });
    // 循环头部购物车  计算总价
    function total_price() {
        var total_price = 0;
        $(".m_cart_goods").find(".gsid").each(function () {
            var g_num = $(this).parents(".m_cart_goods").find("em").text();
            var g_price = $(this).parents(".m_cart_goods").find(".red").text();
            total_price += g_num * g_price * 0.5;
        });
        $(".c_total").text(changeTwoDecimal(total_price));
    }



    // js保留2位小数
    function changeTwoDecimal(x) {
        var f_x = parseFloat(x);
        if (isNaN(f_x)) {
            alert('function:changeTwoDecimal->parameter error');
            return false;
        }
        var f_x = Math.round(x * 100) / 100;

        return f_x;
    }

    //点击购物车里的删除商品图标
    $(".goods_off").live("click", function () {
        var gsid = $(this).parents(".m_cart_goods").find(".gsid").val();
        $(".m_cart_goods").find(".gsid").each(function (i, goods) {
            if ($(goods).val() == gsid) {
                $(this).parents(".m_cart_goods").remove();
            }
        });
        total_price();
    });

    //首页的商品加减号
    //点击  首页  -  数量减1
    $(".m_content li  ol .choice1").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");

        if (parseInt($(goods_fix_num).val()) > 1) {
            //赋值 - 1
            var new_val = parseInt($(goods_fix_num).val()) - 1;
        } else {
            return false;
        }
        goods_fix_num.val(new_val);      //获取元素赋新值
    });
    //点击  首页  +  数量加1
    $(".m_content li  ol .choice2").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");
        goods_fix_num.val(parseInt($(goods_fix_num).val()) + 1);      //获取元素赋新值
    });

    //商品列表页的加减开始
    //点击  列表页  -  数量减1

    $(".check_con .car_goods_shop li .choice1").click(function () {

        var goods_fix_num = $(this).parent().find(".num_choice");

        if (parseInt($(goods_fix_num).val()) > 1) {
            //赋值 - 1
            var new_val = parseInt($(goods_fix_num).val()) - 1;
        } else {
            return false;
        }
        goods_fix_num.val(new_val);      //获取元素赋新值
        total_price2();     //计算价格
    });
    //点击  列表页  +  数量加1
    $(".check_con .car_goods_shop li .choice2").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");
        goods_fix_num.val(parseInt($(goods_fix_num).val()) + 1);      //获取元素赋新值
        total_price2();     //计算价格
    });
    //商品列表页的加减结束
    //商品列表页的点击删除该商品
    $(".car_goods1_operate").click(function () {
        var this_parent = $(this).parents(".car_goods");
        $(this).parent(".car_goods_shop").fadeOut();
        $(this).parent(".car_goods_shop").remove();
        total_price2();
        console.log(this_parent.find(".car_goods_shop").length)

        if (this_parent.find(".car_goods_shop").length == 0) {

            this_parent.fadeOut();

        } else {


        }
    });


    //购物车页面的总价计算开始   函数
    function total_price2() {
        //计算价格2
        var total_price = 0;
        $(".check_con").find(".car_goods_shop").each(function () {
            var price = $(this).find(".cg1_price").text();
            var num = $(this).find(".num_choice").val();
            total_price += price * num
        });

        $(".all_pay3_num em").text(total_price);
    }
    //购物车页面的总价计算结束   函数



    // 选择支付方式变换背景
    $(".pay_method .choose_pay").click(function () {

        $(".pay_method .choose_pay").removeClass("choose_pay1");
        $(this).addClass("choose_pay1");
    })
    // 收货地址核对变换背景
    $(".address li").click(function () {

        $(".address li").removeClass("address1");
        $(this).addClass("address1");
    });

    //计算列表页商品总价

    // 购物车页面店铺商品选中状态
    $(".car_goods .shops .all_but1").click(function () {
        if ($(this).is(":checked")) {

            $(this).parents(".car_goods").find(".car_goods_shop .all_but1").prop("checked", true);
        } else {
            $(this).parents(".car_goods").find(".car_goods_shop .all_but1").prop("checked", false);
        }

    });

    //全选
    $(".select_all").click(function () {

        if ($(this).prop("checked") == true) {
            //循环将all_but1类的checked状态改为选中状态
            $(".all_but1").each(function (i, obj) {
                $(obj).prop("checked", true);
            });
            //同时将另一个.select_all变为选中状态
            $(".select_all").prop("checked", true);
        } else {
            //循环将all_but1类的checked状态改为反选状态
            $(".all_but1").each(function (i, obj) {
                $(obj).prop("checked", false);
            });
            //同时将另一个.select_all变为反选状态
            $(".select_all").prop("checked", false);
        }
    });

    $(".pay_del").click(function () {
        //点击删除按钮的时候，将所有选中的商品删除
        //循环.all_but1，如果它的checked状态为true，则将其删除
        $(".all_but1").each(function (i, obj) {
            if ($(obj).prop("checked") == true) {
                $(obj).parent().parent().remove();
            }
            //删除选中的商品后，重新计算价格
            total_price2();
        });

        //店铺的商品被删除完的时候，也要删除店铺
        $(".car_goods_shop").each(function (i, obj) {
            //判断店铺中的剩余商品数是否为0，为0则删除
        });
    });



    //结尾read
});

function loaddata() {
    nameList.length = 0;
    //当页面加载完成后，初始化“nameList”。
    //提取ID为“divName”中的所有姓名
    var div = document.getElementById("divName");
    if (div) {
        var names = [];
        //兼容Firefox与Chrome；
        if (div.textContent) {
            //通过replace(/\s/ig," ")将所有空白字符，包括空格、制表符、换页符等等都替换为空格
            names = div.textContent.replace(/\s/ig, " ").split(" ");
        } else {
            names = div.innerText.replace(/\s/ig, " ").split(" ");
        }
        for (var i = 0; i < names.length; i++) {
            if (names[i] != "") {
                //获取拼音首字母缩写
                var arrRslt = makePy(names[i]);
                //将拼音与中文的对应关系添加到数组中
                nameList.push(new Array(arrRslt.toString(), names[i]));
            }
        }
    }
}
function getnode(id) {
    $("#divName").empty();
    $.ajax({
        url: "/Merchant/GetNodelist",
        cache: false,
        async: false,
        type: 'post',
        data: { id: id },
        success: function (data) {
            if (data == "") {
                $("#divName").append("<li>无&nbsp;</li>");
            }
            $.each(data, function (i, item) {
                $("#divName").append("<li>" + item["Text"] + "&nbsp;</li>");

            })
        }
    })
}

