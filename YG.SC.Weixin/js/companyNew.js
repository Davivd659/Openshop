function SimpleFixGhostClick(releaseTimeout){
    this._releaseTimeout = releaseTimeout || 500;
    this._timeoutId = -1;
    this._init();
}
SimpleFixGhostClick.prototype = {
    constructor : SimpleFixGhostClick,
    _init : function(){
        var self = this;
        self._preventClick();
        self._timeoutId = setTimeout(function(){
            self._releaseClick();    
        }, this._releaseTimeout );
    },
    _preventClick : function(){
        $(document.body).click(this._stopPreventEvent);
    },
    _releaseClick : function(){
        $(document.body).unbind("click",this._stopPreventEvent);
    },
    _stopPreventEvent : function(e){
        e.stopPropagation();
        e.stopImmediatePropagation();
        e.preventDefault();
        return false;
    },
    release  : function(){
        if(this._timeoutId != -1){
            clearTimeout(this._timeoutId);
            this._timeoutId = -1; 
        }
        this._releaseClick();
    },
    lock : function(){
        if(this._timeoutId != -1){
            clearTimeout(this._timeoutId);
            this._timeoutId = -1; 
        }
        this._init();
    }
};


function WidgetPagination($container){
    this._$container = $container;
    this._init();
}
WidgetPagination.prototype = {
    constructor : WidgetPagination,
    _init : function(){
        this._$curA = this._$container.find('.widget-pagination-current-page');
        this._$pagesSelect = this._$container.find('.widget-pagination-pages');
        this._regEvent();
    },
    _regEvent : function(){
        this._$pagesSelect.bind('change', $.proxy(this._pageChangeHandle, this));
    },
    _pageChangeHandle : function(){
        var $selected = this._$pagesSelect.find('option:selected');

        this._$curA.text($selected.text());
        location.href = $selected.val();
    }
};

$(function(){
    if( $('#menu').get(0)){
        FastClick.attach( $('#menu').get(0) );    
    }else{
        return;
    }
    $('#menu a').addClass('needsclick');
    
    var browser = (function(){
        var sUserAgent = navigator.userAgent.toLowerCase(); 
        var IsWeixin= sUserAgent.match(/MicroMessenger/i)=="micromessenger"
        var IsIpad = sUserAgent.match(/ipad/i) == "ipad";    
        var IsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";  
        var IsMidp = sUserAgent.match(/midp/i) == "midp";  
        var IsUc7 = sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";  
        var IsUc = sUserAgent.match(/ucweb/i) == "ucweb";  
        var IsAndroid = sUserAgent.match(/android/i) == "android";  
        var IsCE = sUserAgent.match(/windows ce/i);
        var IsIphoneOs = sUserAgent.match(/iphone os/i) == "iphone os";  
        var IsWM = sUserAgent.match(/windows mobile/i) == "windows mobile";
        var ios = !!sUserAgent.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/);
        var chrome = !!sUserAgent.match(/chrome\/([\d.]+)/);

        return {
            android : IsAndroid,
            chrome : chrome
        }
    })();

    var companySelect = {
        bindEvent : function(){
            this._$menuItems.click($.proxy(this.menuItemClickHandle, this));
            //$('.submenu > li a').click($.proxy(this.stopSelect, this));
            this._$overlay.bind('touchstart', $.proxy(this.stopSelectOverlay, this));
            this._$overlay.click($.proxy(this.stopSelectOverlay, this));
        },
        startScroll : function($el){
            //滚动
            var self = this,
            	id = $el.attr('id');
            
            if( (id == 'subMenuType' || id == 'subMenuArea') ){
                if($el.data('iscroll')){
                    $el.data('iscroll').refresh();
                }else{
                    $el.data('iscroll', 
                        new IScroll($el.get(0),{
                            mouseWheel: true,
                            scrollbars: true,
                            preventDefaultException : { tagName: /^(INPUT|TEXTAREA|BUTTON|SELECT|A)$/ } //,
                            //click: browser.android && browser.chrome ? true : false
                        })
                    );
                }
            }
        },
        menuItemClickHandle : function(e){
            var $el = $(e.currentTarget);

            if( $el.parent().attr('id') !== 'menu'){
                return ;
            }
            if( $el.get(0) == this._$currentLi.get(0)){
                this.stopSelect(e);
                return;
            }

            var top = $el.offset().top,
                outerHeight = $el.outerHeight(),
                bottom = top + outerHeight,
                $content = $el.find('.submenu'),
                submenuHeight = $content.outerHeight();



            this._$menuItems.removeClass('active');
            $el.addClass('active');

            this._$submenus.removeClass('active');
            $content.addClass('active');

            this._$currentLi = $el;
            this._$currentContent.hide();
            this._$overlay.css({'top':bottom + submenuHeight, 'display':'block'});
            $content.css({'top':bottom}).show();
            this._$currentContent = $content;
            this._$body.css({'position':"fixed",'width':"100%"});

            this.startScroll($content);
        },
        init : function(){
            this._$menu = $('#menu');
            this._$menuItems = $('#menu > li'),
            this._$submenus = this._$menu.find('.submenu'),
            this._$overlay = $('#overlay'),
            this._$currentContent = $(),
            this._$body = $(document.body),
            this._$currentLi = $();
            this.bindEvent();
        },
        stopSelect : function (e){
            this._$currentContent.hide();
            this._$overlay.hide();
            this._$body.css({'position':"static",width:"auto"});
            this._$menuItems.removeClass('active');
            this._$submenus.removeClass('active');
            this._$currentContent = $();
            this._$currentLi = $();
            e.stopPropagation();
        },
        stopSelectOverlay : function(e){
            var self = this;

            this.stopSelect(e);
            e.preventDefault();
            new SimpleFixGhostClick();
            return false;
        }

    };
    companySelect.init();



});

;jQuery(function($){

    //装修公司首页 搜索
    var $askSearchBtn = $('#widgetSearchBtn'),
        $askSearchInput = $('#widgetSearchKey'),
        $clearBtn = $('.icon-clear'),
        searchUrl = $askSearchBtn.data('url');

    function askSearch(){
        var key = $.trim($askSearchInput.val());

        if(key){
            location.href = searchUrl + encodeURIComponent(key);
        }
    }
    $askSearchInput.bind('keyup input', function(){
        var key = $.trim($askSearchInput.val());

        $clearBtn.toggleClass('active', !!key);
    });
    $clearBtn.bind('touchstart click', function(){
        $askSearchInput.val('').focus();
        $clearBtn.removeClass('active');
        return false;
    });
    $askSearchInput.bind('keyup',function(e){
        if(e.which == 13){
            askSearch();
        }
    });
    $askSearchBtn.bind('click touchstart', function(e){
        askSearch();
        return false;
    });
    /*  装修问答首页 end */
});