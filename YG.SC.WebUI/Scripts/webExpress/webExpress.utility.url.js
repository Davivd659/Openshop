/// <reference path="/_references.js" />
(function () {
	webExpress.utility.url = new UrlClass();
	function UrlClass() {
		var _self = this;

		function _init() {
		    _self.getFullUrl = getFullUrl;
		}

		function getFullUrl(partUrl) {
		    var url = window.location.protocol + "//" + window.location.host + "//" + partUrl;
		    return url;
		}

		_init();
	}
})();