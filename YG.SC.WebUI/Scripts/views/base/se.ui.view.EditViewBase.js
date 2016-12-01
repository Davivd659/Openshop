
(function () {
    se.ui.view.EditViewBase = EditViewBase;
    se.ui.view.EditViewBase.Settings = EditViewBaseSettings;
    function EditViewBase(settings) {
        var _self = this;
        var _viewModel;
        function _init() {
            _self.init = init;

            _self.bindModel = bindModel;
        }

        function init() {
            attachBindingAttribute();

            initValidation();
             
            settings.saveButton.click(function () {
                var isValid = $("form").valid();
                if (isValid) {
                    save();
                }
            });
        }

        function bindModel(model) {
            this.adaptModel(model);

            _viewModel = kendo.observable(model);

            this.adaptViewModel(_viewModel);

            kendo.bind($("form"), _viewModel);
        }

        function attachBindingAttribute() {
            var properties = $("[property-name]");
            for (var i = 0; i < properties.length; i++) {
                var $property = $(properties[i]);
                var controlType = $property.attr("control-type");
                var controlBinder = webExpress.ui.control.binders.get(controlType);
                controlBinder.build($property);
            }
        }

        function initValidation() {
            var options = {
                errorElement: "span",
                rules: {}
            };

            var properties = $("[property-name]");
            for (var i = 0; i < properties.length; i++) {
                var $property = $(properties[i]);
                var attrRule = $property.attr("validate-rule");
                if (attrRule && attrRule.contains("required:true")) {
                    var propName = $property.attr("property-name");
                    options.rules[propName] = { required: true };
                }
            }

            $("form").validate(options);
        }

        function save() {
            var model = settings.getSaveModel(_viewModel);
            var url = settings.getSaveModelId(model) > 0 ? settings.updateUrl : settings.addUrl;
            $(".panel-body").mask("loading...");
            webExpress.utility.ajax.request(url, model,
            function (response) {
                $(".panel-body").unmask();
                if (response.IsSuccess) {
                    alert("保存成功");
                } else {
                    alert("保存错误," + response.Message);
                }
            });
        }
        _init();
    }

    function EditViewBaseSettings(settings) {
        this.getSaveModel = function (viewModel) {
            return viewModel;
        }
        this.getSaveModelId = function (saveModel) {
            return saveModel.Id;
        };
        this.saveButton = $(".btn-save");
        this.addUrl = "";
        this.updateUrl = "";

        $.extend(this, settings);
    }
})();
