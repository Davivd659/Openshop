
(function () {
	se.ui.view.ShopModal = ShopModalClass;
	se.ui.view.ShopModal.Settings = ShopModalSettings;
	function ShopModalClass(settings) {
		var _self = this;

		function _init() {
			_self.init = init;
		}

		function init() {
			settings.Sourse.click(function () {
				settings.Modal.modal("show");
			});
			settings.Sourse.next().children(".btn").click(function () {
				settings.OnSelected.call(this, "", "");
			});
			settings.Modal.on("click", "a.j-option", function () {
				var shopId = $(this).closest("tr").data("id");
				var shopName = $.trim($(this).text());
				settings.OnSelected.call(this, shopId, shopName);
				settings.Modal.modal("hide");
			});
		}


		_init();
	}

	function ShopModalSettings(settings) {
		this.Sourse = $(".j-shopModal");
		this.Modal = $("#shopModal");
		this.OnSelected = function () {

		}
		$.extend(this, settings);
	}
})();
