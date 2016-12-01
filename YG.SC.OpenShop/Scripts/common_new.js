// JavaScript Document header_search_submit
//console.log处理   

var $ = jQuery.noConflict();

function getCookie(name, pre)
 {
    if (pre)
    name = 'to8to_' + name;
    var r = new RegExp("(\\b)" + name + "=([^;]*)(;|$)");
    var m = document.cookie.match(r);
    var res = !m ? "": decodeURIComponent(m[2]);
    var result = stripscript(res);
    return result;

};

/****************XSS过滤********************/
function stripscript(s)
 {
    var pattern = new RegExp("[%--`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]");
    //格式 RegExp("[在中间定义特殊过滤字符]")
    var rs = "";
    for (var i = 0; i < s.length; i++) {
        rs = rs + s.substr(i, 1).replace(pattern, '');

    }
    return rs;

};

//头部底部初始化
!(function(){
    var xiatan  = {
        init:function(){
            hfDocReady();//header and footer docReadyFunction
        }
    };
       window.searchtage = '';
       window.searchclicktage = '';
        var tcode = getCookie("tcode",true);
            tcode = tcode?tcode:'sz',
             searchput='keyword', sHref='',sText=''
            tRight = 0;
    function initsearch(sName)
        {
           sName = $.trim(sName);
           switch(sName)
           {
                case '效果图': 
                  
                   searchclicktage='1_8_2_1'; 
                   sHref= 'http://xiaoguotu.to8to.com/search.php';
                   sText ='海量精美效果图任你选'; 
                   tRight = 70;
                break;   
                case '装修公司':
                  
                   searchclicktage='1_8_2_2';
                   sHref= "http://"+tcode+".to8to.com/company/";
                   sText ='挑选您心仪的装修公司';
                   tRight = 58;
                break;   
                case '小区':
                    
                    searchclicktage='1_8_2_3';
                    sHref=  "http://"+tcode+".to8to.com/zwj/";
                    sText ='找找您家小区的装修案例';
                    tRight = 82;
                    break;   
                case '文章':
                    searchclicktage='1_8_2_4';
                    sHref= 'http://www.to8to.com/yezhu/xzx_search.php';
                    sText ='了解装修相关的资讯知识';
                    tRight = 82;
                    break;   
                case '问答': 
                    
                    searchclicktage='1_8_2_5';    
                    sHref= 'http://www.to8to.com/ask/search.php';
                    sText ='解决您的装修疑问';
                    tRight = 82;
                    break;   
                case '家居建材': 
                   
                    searchclicktage='1_8_2_6';
                    sHref= 'http://mall.to8to.com/search';
                    sText ='挑选优质家居建材'; 
                    tRight = 58;
                    break;    
           }
           
           $('.header_search_input_text').attr('v',tRight).css("right",''+tRight+'px');
           if(sName=='全站'||sName=='文章' || sName=='小区')
           {
                searchput ='keyword_zh';
           }
           else
           {
              searchput='keyword';
           }
            $('.header_search_input').attr('name',searchput);
            $('#searchform').attr('action',sHref);
            $('.header_search_input_text').html(sText);
        }
    
    function hfDocReady(){
        
        var doc = {};
        doc.hs = $('.header_select');
        doc.si =  $('.header_search_input');
        doc.si.val("");
        doc.hs.on('mouseenter', function(){
          $(this).addClass('on');
          $(this).find('ul').show();
        });
        doc.hs.on('mouseleave', function(){
          $(this).removeClass('on');
          $(this).find('ul').hide();
        });
        var currentTxt = doc.hs .find('a >span>em').text();
        initsearch(currentTxt);
        doc.hs.find('ul > li').on("click", function(){
            var sName = $(this).find('a').text(),
                siWidth = doc.si.width();
                hsWidth = $('.header_select').width();
            $('.header_select_sort').find('span > em').text(sName);
            initsearch(sName);
            try{clickStream.getCvParams(searchclicktage);}catch(e){}
            newHsWidth = $('.header_select').width();
            doc.si.width(siWidth - (newHsWidth-hsWidth));
            var rm = $('.header_search_input_text').attr('v');
            if(rm == undefined) rm = tRight;

            var rightMargin = rm-(newHsWidth-hsWidth);
            $('.header_search_input_text').css('right', ''+rightMargin+'px').attr('v',''+rightMargin+'');
            $(this).parent().hide();
            doc.si.focus();
        });
        $('.header_search_input_text').on("click", function(){
            doc.si.focus();
        });
        doc.si.on("keydown", function(){
            $('.header_search_input_text').hide();
        });
        doc.si.blur(function(){
            if($(this).val() =="" )  $('.header_search_input_text').show();
        });
        $('.header_menu >ul > li').on("mouseenter", function(){
            $(this).addClass('menu_hover');
        });
        $('.header_menu >ul > li').on("mouseleave", function(){
            $(this).removeClass('menu_hover');
        });
        $('#searchform').on('submit',function(){
            var searchtage=$('.header_search_submit').attr('searchtage');
            if(searchtage)
            {
                try{clickStream.getCvParams(searchtage);clickStream.sendPv();}catch(e){} 
            }
            if($('.header_search_input').val()==""){
               $('.header_search_input').focus();
               return false;
             }else{
               return true;
             }
        });
        doc.ftc = $('.footer_top_container');
        doc.ftc.li =  doc.ftc.find('div.ftc_left > ul > li');
        doc.ftc.li.on("click", function(){
          doc.ftc.li.removeClass('on');
          $(this).addClass('on');
          var n = doc.ftc.li.index($(this));
          $('.ftclt_content').removeClass('on');
          $('.ftclt_content').eq(n).addClass('on');
        });
        $('.pop_wechat').on("mouseenter", function(){
          $(this).find('div.wechat_bg').show();
        });
        $('.pop_wechat').on("mouseleave", function(){
          $(this).find('div.wechat_bg').hide();
        });
        $('.q_code').on("mouseenter",function(){
          $(this).find('div.q_code_layer').show();
          clickStream.getCvParams('1_7_1_1');
        });
        $('.q_code').on("mouseleave",function(){
          $(this).find('div.q_code_layer').hide();
        });
        $('.header_bottom > .header_menu > ul > li.has_homeIcon').mouseenter(function(){
          $(this).find('em').show();
        });
        $('.header_bottom > .header_menu > ul > li.has_homeIcon').mouseleave(function(){
          $(this).find('em').hide();
        });
        $('.gpm_name').mouseenter(function(){
          $('.gpm_content').show();
        });
        $('.gpm_content').mouseleave(function(){
          $(this).hide();
        });
        };
        window.headerFooter = xiatan;
    
})(jQuery);



