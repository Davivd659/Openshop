
(function () {
    se.ui.view.Module = ModuleClass;
    se.ui.view.Module.Settings = ModuleSettings;
    function ModuleClass(settings) {
        se.ui.control.Eventable.call(this);
        var _self = this;
        var _settings = settings;
        function _init() {
            _self.container = settings.container;

            _self.setTitle = setTitle;

            _self.activate = activate;

            _self.inactivate = inactivate;
        }

        function setTitle(title) {
            _settings.setTitle.call(this, title);
        }

        function activate() {
            _settings.activate.call(this);
        }

        function inactivate() {
            _settings.inactivate.call(this);
        }

        _init();
    }

    function ModuleSettings(settings) {
        this.activate = function () {

        }

        this.inactivate = function () {

        }

        this.setTitle = function (title) {

        }

        $.extend(this, settings);
    }
})();
