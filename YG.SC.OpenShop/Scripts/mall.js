//���ڱ�����������ĸ�������ļ�ֵ��
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
            //��ȡɸѡ���ݳ���
            var keyLength = $this.text().length;
            var word = ""; //��¼���������������ִ�
            //����������ִ�в���
            for (var i = 0; i < nameList.length; i++) {
                //��ȡ�ȳ��ַ���ɸѡ��ĸ��Ͻ��бȽ�
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

    //ѡ�����
    $(".m_area_off").click(function () {
        $("#dengxiang").css("display", "none");
        $(".m_area").css("display", "none");
    });

    //ͷ���̶�����
    $(window).scroll(function (event) {
        var scroll_h = $(window).scrollTop();
        if (scroll_h > 100) {
            $(".m_nav2").fadeIn(500);
        }
        else {
            $('.m_nav2').fadeOut(500);
        }
    });
    //  ����̶�ͷ�������Ĺ��ﳵ�����ֹ��ﳵ�����˵�
    $(".m_cart").click(function () {
        $(this).parent().find(".m_car_all").toggle();
    });


    // ��Ʒ���빺�ﳵ
    var add_1 = $("#con_goods_id_1").parent(".con_goods");
    //���ͼƬ�����СͼС����
    $(".m_content li  ol .goods_car").click(function () {
        var $this = $(this).parents("li");
        //    �̶�ͷ��  ���ﳵdiv��
        var img = $this.find("img").attr("src");
        var name = $this.find("h3").text();
        var id = $this.find(".goodsid").val();
        var price = $this.find(".m_price span").text();
        var mon_pay = $this.find(".m_price strong b").text(); //����
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


        // ����ͷ�����ﳵ����ܼ�
        // ѭ��ͷ�����ﳵ���ÿ����Ʒ
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
    // ѭ��ͷ�����ﳵ  �����ܼ�
    function total_price() {
        var total_price = 0;
        $(".m_cart_goods").find(".gsid").each(function () {
            var g_num = $(this).parents(".m_cart_goods").find("em").text();
            var g_price = $(this).parents(".m_cart_goods").find(".red").text();
            total_price += g_num * g_price * 0.5;
        });
        $(".c_total").text(changeTwoDecimal(total_price));
    }



    // js����2λС��
    function changeTwoDecimal(x) {
        var f_x = parseFloat(x);
        if (isNaN(f_x)) {
            alert('function:changeTwoDecimal->parameter error');
            return false;
        }
        var f_x = Math.round(x * 100) / 100;

        return f_x;
    }

    //������ﳵ���ɾ����Ʒͼ��
    $(".goods_off").live("click", function () {
        var gsid = $(this).parents(".m_cart_goods").find(".gsid").val();
        $(".m_cart_goods").find(".gsid").each(function (i, goods) {
            if ($(goods).val() == gsid) {
                $(this).parents(".m_cart_goods").remove();
            }
        });
        total_price();
    });

    //��ҳ����Ʒ�Ӽ���
    //���  ��ҳ  -  ������1
    $(".m_content li  ol .choice1").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");

        if (parseInt($(goods_fix_num).val()) > 1) {
            //��ֵ - 1
            var new_val = parseInt($(goods_fix_num).val()) - 1;
        } else {
            return false;
        }
        goods_fix_num.val(new_val);      //��ȡԪ�ظ���ֵ
    });
    //���  ��ҳ  +  ������1
    $(".m_content li  ol .choice2").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");
        goods_fix_num.val(parseInt($(goods_fix_num).val()) + 1);      //��ȡԪ�ظ���ֵ
    });

    //��Ʒ�б�ҳ�ļӼ���ʼ
    //���  �б�ҳ  -  ������1

    $(".check_con .car_goods_shop li .choice1").click(function () {

        var goods_fix_num = $(this).parent().find(".num_choice");

        if (parseInt($(goods_fix_num).val()) > 1) {
            //��ֵ - 1
            var new_val = parseInt($(goods_fix_num).val()) - 1;
        } else {
            return false;
        }
        goods_fix_num.val(new_val);      //��ȡԪ�ظ���ֵ
        total_price2();     //����۸�
    });
    //���  �б�ҳ  +  ������1
    $(".check_con .car_goods_shop li .choice2").click(function () {
        var goods_fix_num = $(this).parent().find(".num_choice");
        goods_fix_num.val(parseInt($(goods_fix_num).val()) + 1);      //��ȡԪ�ظ���ֵ
        total_price2();     //����۸�
    });
    //��Ʒ�б�ҳ�ļӼ�����
    //��Ʒ�б�ҳ�ĵ��ɾ������Ʒ
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


    //���ﳵҳ����ܼۼ��㿪ʼ   ����
    function total_price2() {
        //����۸�2
        var total_price = 0;
        $(".check_con").find(".car_goods_shop").each(function () {
            var price = $(this).find(".cg1_price").text();
            var num = $(this).find(".num_choice").val();
            total_price += price * num
        });

        $(".all_pay3_num em").text(total_price);
    }
    //���ﳵҳ����ܼۼ������   ����



    // ѡ��֧����ʽ�任����
    $(".pay_method .choose_pay").click(function () {

        $(".pay_method .choose_pay").removeClass("choose_pay1");
        $(this).addClass("choose_pay1");
    })
    // �ջ���ַ�˶Ա任����
    $(".address li").click(function () {

        $(".address li").removeClass("address1");
        $(this).addClass("address1");
    });

    //�����б�ҳ��Ʒ�ܼ�

    // ���ﳵҳ�������Ʒѡ��״̬
    $(".car_goods .shops .all_but1").click(function () {
        if ($(this).is(":checked")) {

            $(this).parents(".car_goods").find(".car_goods_shop .all_but1").prop("checked", true);
        } else {
            $(this).parents(".car_goods").find(".car_goods_shop .all_but1").prop("checked", false);
        }

    });

    //ȫѡ
    $(".select_all").click(function () {

        if ($(this).prop("checked") == true) {
            //ѭ����all_but1���checked״̬��Ϊѡ��״̬
            $(".all_but1").each(function (i, obj) {
                $(obj).prop("checked", true);
            });
            //ͬʱ����һ��.select_all��Ϊѡ��״̬
            $(".select_all").prop("checked", true);
        } else {
            //ѭ����all_but1���checked״̬��Ϊ��ѡ״̬
            $(".all_but1").each(function (i, obj) {
                $(obj).prop("checked", false);
            });
            //ͬʱ����һ��.select_all��Ϊ��ѡ״̬
            $(".select_all").prop("checked", false);
        }
    });

    $(".pay_del").click(function () {
        //���ɾ����ť��ʱ�򣬽�����ѡ�е���Ʒɾ��
        //ѭ��.all_but1���������checked״̬Ϊtrue������ɾ��
        $(".all_but1").each(function (i, obj) {
            if ($(obj).prop("checked") == true) {
                $(obj).parent().parent().remove();
            }
            //ɾ��ѡ�е���Ʒ�����¼���۸�
            total_price2();
        });

        //���̵���Ʒ��ɾ�����ʱ��ҲҪɾ������
        $(".car_goods_shop").each(function (i, obj) {
            //�жϵ����е�ʣ����Ʒ���Ƿ�Ϊ0��Ϊ0��ɾ��
        });
    });



    //��βread
});

function loaddata() {
    nameList.length = 0;
    //��ҳ�������ɺ󣬳�ʼ����nameList����
    //��ȡIDΪ��divName���е���������
    var div = document.getElementById("divName");
    if (div) {
        var names = [];
        //����Firefox��Chrome��
        if (div.textContent) {
            //ͨ��replace(/\s/ig," ")�����пհ��ַ��������ո��Ʊ������ҳ���ȵȶ��滻Ϊ�ո�
            names = div.textContent.replace(/\s/ig, " ").split(" ");
        } else {
            names = div.innerText.replace(/\s/ig, " ").split(" ");
        }
        for (var i = 0; i < names.length; i++) {
            if (names[i] != "") {
                //��ȡƴ������ĸ��д
                var arrRslt = makePy(names[i]);
                //��ƴ�������ĵĶ�Ӧ��ϵ��ӵ�������
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
                $("#divName").append("<li>��&nbsp;</li>");
            }
            $.each(data, function (i, item) {
                $("#divName").append("<li>" + item["Text"] + "&nbsp;</li>");

            })
        }
    })
}

