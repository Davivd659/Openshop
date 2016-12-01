$(function(){
	////页脚置底兼容写法
	var bodyW = $('html').width();
		
	if(bodyW == 320){
		$('.content').css({
			minHeight:'130%',
			_height:'130%'
		})
	}
	
	

});
//定义ajax请求头
if(!window.PATH){
	window.PATH = 'http://' + window.location.host + '/h5/';
}
if (!window.JD) {
	window.JD = {};
}
//获取url参数
JD.UrlParams = {};
(function () {
	var i, aParams = document.location.search.substr(1).split('&'), aParam;
	for (i=0; i<aParams.length; i++){
		aParam = aParams[i].split('=');
		if (aParam[0].length > 0) {
			JD.UrlParams[aParam[0]] = decodeURI(aParam[1]);
		}	
	}
})();