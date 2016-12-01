$(function () {
    $('.select-area').on('click', function (e) {
        $('.select-province').show().addClass('showprovince');

    });
    $('.address').on('click', function (e) {
        $('.modifyaddress').show();
    });
    $('.showmodifyinvoice').on('click', function (e) {
        $('.modifyinvoice').show();
    });
    $('.showmodifyremark').on('click', function (e) {
        $('.modifyremark').show();
    });

    $('#hideSel').on('touchstart', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('.select-province').hide().addClass('hideprovince');

    });
    $('#hideborrow').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('.modifyaddress').hide();

    });
    $('#hidebill').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('.modifyinvoice').hide()

    });
    $('#hideremark').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('.modifyremark').hide()
    });

    (function () {
         var $navs = $('[href^="#product_in"],[href^="#tab-"],[class^="show-"],[href^="tab-order-"]');
        $navs.on('touchstart', function (e) {
            $(this).addClass('active');
            $(this).parent().siblings().find('a').removeClass('active');
            var $li = $(this).parent();
            e.preventDefault();
            if ($li.hasClass('active')) {
                return false;
            }
            $navs.parent().removeClass('active');
            $li.addClass('active');

            $navs.each(function () {
                var $content = $($(this).attr('href'));
                $content.hide();
            });
            $($(this).attr('href')).show();
            return false;
        });
    })();

    // $('.select-inov h1').on('touchstart', function () {
    //     var chks = $(this).find('input[name="chk"]');

    //     if (!chks.prop('checked')) {
    //         chks.prop('checked', true);
    //     }
    //     // else {
    //     //     chks.prop('checked', false);
    //     // }
    // })


    $('.select-inov h1 a').on('touchstart', function () {

        var chks = $(this).parent().find('input[name="chk"]');
        if (!chks.prop('checked')) {
            chks.prop('checked', true);
        }
        // else {
        //     chks.prop('checked', false);
        // }
    })

    /*支付方式*/
    /*$('.pay-methods-list li').on('touchstart',function(){

     var chks=$(this).find('input[name="pay-m"]');
     if(! chks.prop('checked'))
     {
     chks.prop('checked',true);
     }
     else
     {
     chks.prop('checked',false);
     }
     })*/


    /*省市区选择*/

    function ShowRankList($el) {
        this._$el = $el;
        this._init();
    }

    ShowRankList.prototype = {
        activeHead: null,
        activeBody: null,
        _init: function () {
            this.showList();
            /*this._$el.find('.h1-titles').eq(0).trigger('click');*/
        },
        showList: function (e) {
            var self = this;
            this._$el.find('.h1-titles').live('click', function (e) {
                if (self.activeHead) {
                    self.activeHead.removeClass("active");
                    self.activeHead.find('span').removeClass("actives");
                }
                self.activeHead = $(this);
                self.activeHead.find('span').addClass("actives");
                self.activeHead.addClass("active");

                if (self.activeBody) {
                    self.activeBody.hide();
                }
                self.activeBody = $(this).next(".leftlist-content");
                self.activeBody.toggle(0);
            });
        }
    }

    $('.productList').each(function () {
        new ShowRankList($(this));
    });
    var areah = $(window).height();
    $('.productList').height(areah);

    /*省市区选择end*/

    ShowRankList.prototype = {
        activeHead : null,
        activeBody : null,
        _init : function(){
            this.showList();
            
        },
        showList:function(e){
            var self = this;
            this._$el.find('.h1titles').on('touchstart', function(e){
                if(self.activeHead){
                    self.activeHead.removeClass("active");
                    self.activeHead.find('b').removeClass("actives");
                }
                self.activeHead = $(this);
                self.activeHead.addClass("active");
                self.activeHead.find('b').addClass('active')
                if(self.activeBody){
                    self.activeBody.hide();
                    self.activeBody.prev("h1").find('b').removeClass('active')
                }
                self.activeBody = $(this).next(".show-content");
                self.activeBody.toggle();
            });
        }
    }

    // $('.product-attr').each(function(){
    //     new ShowRankList($(this));
    // });

    $(".h1titles").on('touchstart',function(){
        var self = $(this);
        if(self.hasClass("active")){
            self.removeClass("active");
            self.find('b').removeClass("active");
            // self.prev("h1").find('b').removeClass('active');
            self.next(".show-content").hide();
        }else{
            self.addClass("active");
            self.find('b').addClass("active");
            // self.prev("h1").find('b').addClass('active');
            self.next(".show-content").show();
        }
    });


})

//20150602




