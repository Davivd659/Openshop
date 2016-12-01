(function () {
    webExpress.ui.control.confirmWindow = ConfirmWindowClass;
    webExpress.ui.control.confirmWindow.events = {};
    webExpress.ui.control.confirmWindow.events.ok = "ok";
    webExpress.ui.control.confirmWindow.events.cancel = "cancel";
    function ConfirmWindowClass(title, message) {
        var _self = this;

        function _init() {
            _self.show = show;
        }

        function show() {

        }
        _init();
    }
})()