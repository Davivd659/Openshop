/*
	name: jquery相册插件（防qq空间相册系统）
	添加自己播放功能
	添加viewMode 0没有缩略图 1有缩略图  2缩略图。全屏幕。可以对图片进行左右转
	args{
		preBtn: Dom  大图的左右按钮
		nextBtn :Dom  大图的左右按钮
		
		miniPreBtn: Dom  小图的左右按钮
		miniNextBtn :Dom  小图的左右按钮
		
		imgListWrap: DOM 包裹小图片列表的元素
		slideObj: DOM 动画的ul
		
		dataNode: DOM 一般是数据内容，内容放在textarea里实现延迟加载。
		添加键盘事件的支持
		
		//设置动画参数
		pageNum: Number 设置一屏放几个小的缩略图
		}
*/
(function($){
	function Album(args,m){
		 $.extend(this,defaultArgs,args);
		 var _this = this;
         if(m){
            this.loadNextPic(m);
         }
         else{
           this.loadNextPic(0); 
         }		 
		 this.realImg.show();
		 this.imgLoading.hide();
		 this.realImg.load(function(){
		 		_this.imgLoading.hide();
				_this.realImg.show();
		 });
		 if(this.albumDataList.val() == '') return ;		 
		 this.photoList.html(this.albumDataList.val());
         if(m){
		 this.photoList.find("li:eq("+m.toString()+")").addClass('on');
		 }
         else{
            this.photoList.find("li").first().addClass('on');
         }
		 this.imgListLen = this.photoList.find('li').length;
		this.liW = this.photoList.find('li').outerWidth(true);
		if(this.imgListLen<this.picNum){
			this.picNum = this.imgListLen; 
		}
		this.thumWrap.width(this.liW*this.picNum);
		
		if(m){
		  	this.index=m;
		}
        else{
            this.index=0;
        }
		this.page = 1;
		this.nPageCount=Math.ceil(this.imgListLen*this.liW /(this.liW *this.picNum));
		//大图左右点击
		this.nextBtn.click(function(){
			_this.nextItem();
		});	
		this.preBtn.click(function(){
			_this.preItem();
		});
		//show hide
		this.preBtn.hover(function(){
			$(this).find('i').css('visibility','visible')
		},function(){
			$(this).find('i').css('visibility','hidden')
		});
		this.nextBtn.hover(function(){
			$(this).find('i').css('visibility','visible')
		},function(){
			$(this).find('i').css('visibility','hidden')
		});
		//小图左右点击
		this.thumNextBtn.click(function(){
			_this.doNextMove();

		});
		this.thumPreBtn.click(function(){
			_this.doPreMove();

		});		
		//点击每个小图
		this.photoList.find('li').each(function(i,o){
			$(this).click(function(){
				  	_this.loadNextPic(i);
					_this.showPic(i);
				_this.index=i;
				_this.photoList.find('li').eq(i).addClass('on').siblings().removeClass('on');
			});
		});
		this.init();
		var fullHeight = $(window).height();
		var crumH = $('.crumbs').outerHeight(true);
		var bottH = $('#thumBox').outerHeight(true);
		$('.valin').height(fullHeight-crumH - bottH);
		this.nextBtn.height(fullHeight-crumH - bottH);
		this.preBtn.height(fullHeight-crumH - bottH);
		this.nextBtn.find('i').css('top',((fullHeight-crumH - bottH)-84)/2);
		this.preBtn.find('i').css('top',((fullHeight-crumH - bottH)-84)/2);
	    if(fullHeight < 620){			
			this.thumBox.css({'bottom':'auto'});
		}else{		 
			this.thumBox.css({'bottom':'0'});
			}
		$(window).resize(function(){
			var fullHeight2 = $(window).height();ar
		var crumH = $('.crumbs').outerHeight(true);
		var bottH = $('#thumBox').outerHeight(true);
		$('.valin').height(fullHeight2-crumH - bottH);
		_this.nextBtn.height(fullHeight2-crumH - bottH);
		_this.preBtn.height(fullHeight2-crumH - bottH);
		_this.nextBtn.find('i').css('top',((fullHeight2-crumH - bottH)-84)/2);
		_this.preBtn.find('i').css('top',((fullHeight2-crumH - bottH)-84)/2);
			if(fullHeight < 500 || fullHeight2<fullHeight){					
				_this.thumBox.css({'bottom':'auto'});
			}else if(fullHeight2>fullHeight){
				_this.thumBox.css({'bottom':'0'});
			}else if(fullHeight2==fullHeight){
				_this.thumBox.css({'bottom':'0'});
			}
		});
	}	
	Album.prototype={
		init:function(){			
            var path=window.location.pathname.replace("/","").split("-");
            var  _num=_current_num;
			this.loadNextPic(_num);					
		},
		loadNextPic:function(n){
			if(0<=n<=this.imgListLen-1){//解决左右按钮失效
				this.nextBtn.css('visibility','visible');
				this.preBtn.css('visibility','visible');
			}			
			this.realImg.hide();
			this.imgLoading.show();
			var nextPicUrl = this.photoList.find('li').eq(n).find('i').html();
			var imgAlt = this.photoList.find('li').eq(n).find('.imgAlt').html();
		 if(!img){
			var img = new Image();
			var _this = this;
			var w = 800,h=500;
			var o = this.realImg.get(0);
			img.onload=function(){
				img.onload = null;
				
			 if (img.width > 0 && img.height > 0) {
				if (img.width / img.height >= w / h) {
					if (img.width > w) {
						o.width = w;
						o.height = (img.height * w) / img.width;
					} else {
						o.width = img.width;
						o.height = img.height;
					}
					o.alt = imgAlt;
				} else {
					if (img.height > h) {
						o.height = h;
						o.width = (img.width * h) / img.height;
					} else {
						o.width = img.width;
						o.height = img.height;
					}
					o.alt = imgAlt;
					}
				}
				
			}
			img.src = nextPicUrl;
			}
		    this.realImg.attr('src',img.src);
			this.realImg.load(function(){
				_this.imgLoading.hide();
				_this.realImg.show();
				
			});
			this.showPic(n);			
		},			
		showPic:function(n){			
			var oLi = this.photoList.find('li');
			this.txtDes.html('');
			this.txtDes.html(oLi.eq(n).find('p').html());
		},
		nextItem:function(){
			if(this.index == this.imgListLen-1){//针对chrome 点到最后一页的bug
				this.index = this.imgListLen -1;
				this.nextBtn.css('visibility','hidden');
				this.preBtn.css('visibility','visible');
				return;
				
			}
			var nextItemIndex = this.index + 1;
			this.preBtn.css('visibility','visible');
			if(nextItemIndex > this.imgListLen){
				nextItemIndex = this.imgListLen -1;
				this.index = this.imgListLen -1;
				this.nextBtn.css('visibility','hidden');
			}
			this.loadNextPic(nextItemIndex);
			this.index=nextItemIndex;
			this.photoList.find('li').eq(nextItemIndex).addClass('on').siblings().removeClass('on');
			if(nextItemIndex+1 > this.page*this.picNum){//如果到了第一版面底部，则小缩略图自动加载下一屏
				this.thumNextBtn.trigger("click");	
			}
		},
		preItem:function(){
			if(this.index <= 0){
				this.index = 0;
				this.nextBtn.css('visibility','visible');
				this.preBtn.css('visibility','hidden');
				return;
			}
			var preItemIndex = this.index - 1;
			this.nextBtn.css('visibility','visible');
			if(preItemIndex <= 0){
				preItemIndex = 0;
				this.index = 0;
				this.preBtn.css('visibility','hidden');
			}
			this.index=preItemIndex;
			//this.showPic(preItemIndex);
			this.loadNextPic(preItemIndex);//add
			this.photoList.find('li').eq(preItemIndex).addClass('on').siblings().removeClass('on');		
			if(preItemIndex < (this.page-1)*this.picNum){//如果到了第一版面底部，则小缩略图自动加载下一屏
				this.thumPreBtn.trigger("click");	
			}
		},
		doNextMove:function(){
			var _this=this;		
			if(!this.photoList.is(":animated")){
				this.thumNextBtn.attr('title','').css('opacity',"1");
			 if(this.page >= this.nPageCount){
				 this.thumNextBtn.attr('title','已到最后一页了').css('opacity',"0.5");
		    }else{
		    	this.thumPreBtn.attr('title','').css('opacity',"1");
		        this.photoList.animate({left:'-='+this.picNum*this.liW+'px'},1000);     
			    this.page++;			    
		    }
			}
		},
		doPreMove:function(){		
			if(!this.photoList.is(":animated")){
				this.thumNextBtn.attr('title','').css('opacity',"1");
			 if(this.page <= 1){
				 this.thumPreBtn.attr('title','已到第一页了').css('opacity',"0.5");
			   }else{
				   this.thumNextBtn.attr('title','').css('opacity',"1");
		        this.photoList.animate({left:'+='+this.picNum*this.liW+'px'},1000);
			        this.page--;
			    }
			}

		}
		
	};
	var defaultArgs = {};	
	$(function(){
	   var path=window.location.pathname.replace("/","").split("-");
     var  num=_current_num;
		new Album({
			viewMode:0,
			preBtn:$('#preBtn'),
			nextBtn:$('#nextBtn'),
			thumPreBtn:$('#thumPreBtn'),
			thumNextBtn:$('#thumNextBtn'),
			realImg:$('#realImg'),
			txtDes:$('#txtDes'),
			thumWrap:$('#thumWrap'),
			photoList:$('#photoList'),
			thumPreBtn:$('#thumPreBtn'),
			thumNextBtn:$('#thumNextBtn'),
			albumDataList:$('#albumDataList'),
			picNum:8,//设置thum上一屏放几个小缩略图
			imgLoading:$('#imgLoading'),
			thumBox:$('#thumBox')
		},num);		   
	});	    
})(jQuery);