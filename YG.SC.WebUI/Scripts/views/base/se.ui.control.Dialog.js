(function () {
    //se.ui.control.Dialog = DialogClass;
    se.ui.control.dialog = {};
    se.ui.control.dialog.factory = new DialogFactory();
    function DialogClass(container) {
        se.ui.control.Eventable.call(this);
        var _container = container;
        var _self = this;

        function _init() {
            _self.show = show;
            _self.hide = hide;
            _self.save = save;
            init();
        }

        function init() {
            _container.find(".btn-ok").click(function () {
                var data = _container.find("form").serializeObject();
                _self.fire("ok", [data]);
            });
            _container.on('hidden.bs.modal', function (e) {
                _container.find("form")[0].reset();
            });
        }

        function show() {
            _container.modal("show");
        }

        function hide() {
            _container.modal("hide");
        }

        function save(url, model) {
            _container.mask("Loading...");
            webExpress.utility.ajax.request(url, model, function (data) {
                _container.unmask();
                if (data.IsSuccess) {
                    _self.hide();
                }
                else {
                    alert(data.Message);
                }
            }, function () {
                console.log("error");
            });
        }

        _init();
    }

    function DialogFactory() {
        var _cache = {};
        var _self = this;

        function _init() {
            _self.get = get;
        }

        function get(settings) {
            if (_cache[settings.container] === undefined) {
                var dialog = new DialogClass(settings.container);
                dialog.on("ok", settings.okHandler);
                _cache[settings.container] = dialog;
            }
            return _cache[settings.container];
        }

        _init();
    }
})();
