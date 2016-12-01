(function () {
    webExpress.ui.control.binders = new BindersClass();
    webExpress.ui.control.binders.nativeInputText = new NativeInputTextBinderClass();
    webExpress.ui.control.binders.nativeInputDate = new NativeInputDateBinderClass();
    webExpress.ui.control.binders.nativeInputPassword = new NativeInputPasswordBinderClass();
    webExpress.ui.control.binders.nativeInputRadio = new NativeInputRadioBinderClass();
    webExpress.ui.control.binders.nativeInputFile = new NativeInputFileBinderClass();

    webExpress.ui.control.binders.nativeSelect = new NativeSelectBinderClass();
    webExpress.ui.control.binders.nativeSelectMultiple = new NativeSelectMultipleBinderClass();

    webExpress.ui.control.binders.select2Multiple = new Select2MultipleBinderClass();

    webExpress.ui.control.binders.chinaAreas = new ChinaAreasBinderClass();
    webExpress.ui.control.binders.listItems = new ListItemsBinderClass();
    webExpress.ui.control.binders.singleItem = new SingleItemBinderClass();
    function BindersClass() {
        var _self = this;
        function _init() {
            _self.get = get;
        }
        function get(controlType) {
            return webExpress.ui.control.binders[controlType];
        }

        _init();
    }

    function NativeInputBinderClass() {
        var _self = this;
        function _init() {
            _self.build = build;
        }
        function build($property) {
            var control = $property.find("*").andSelf().filter(this.selector);
            var propName = $property.attr("property-name");
            var expression = $(control).attr("data-bind");
            expression = "value:" + propName;
            var containerBinding = $property.attr("container-bind")
            if (containerBinding) {
                $property.attr("data-bind", containerBinding);
            }
            $(control).attr("name", propName);
            $(control).attr("data-bind", expression);
        }
        _init();
    }

    function NativeInputTextBinderClass() {
        NativeInputBinderClass.call(this);
        var _self = this;
        function _init() {
            _self.selector = ":text";
        }
        _init();
    }

    function NativeInputDateBinderClass() {
        NativeInputBinderClass.call(this);
        var base = {};
        NativeInputBinderClass.call(base);
        var _self = this;
        function _init() {
            _self.selector = "input[type=date]";

            _self.build = build;
        }

        function build($property) {
            base.build.call(_self, $property);
            var control = $property.find("*").andSelf().filter(this.selector);
            var propName = $property.attr("property-name");
            control.attr("id", propName);
            $property.find(".input-group-addon").click(function () {
                WdatePicker({
                    el: propName,
                    dateFmt: 'yyyy-MM-dd',
                    onpicked: function () {
                        $(this).trigger("change");
                    }
                })
            });
        }
        _init();
    }

    function NativeInputPasswordBinderClass() {
        NativeInputBinderClass.call(this);
        var _self = this;
        function _init() {
            _self.selector = ":password";
        }

        _init();
    }

    function NativeInputRadioBinderClass() {
        var _self = this;
        function _init() {
            _self.build = build;
        }
        function build($property) {
            var controls = $property.find("*").andSelf().filter(":radio");
            var propName = $property.attr("property-name");
            for (var i = 0; i < controls.length; i++) {
                var control = controls[i];
                var expression = $(control).attr("data-bind");
                expression = "checked:" + propName;
                $(control).attr("name", propName);
                $(control).attr("data-bind", expression);
            }
        }
        _init();
    }

    function NativeInputFileBinderClass() {
        var _self = this;
        function _init() {
            _self.build = build;
        }
        function build($property) {
            var imgControl = $property.find("*").andSelf().filter("img");
            var propName = $property.attr("property-name");
            var expression = $(imgControl).attr("data-bind");
            //expression = "attr:{src:" + propName + "}";
            expression = "imgSrc:" + propName;

            $(imgControl).attr("name", propName);
            $(imgControl).attr("data-bind", expression);

            var fileControl = $property.find("*").andSelf().filter(":file");
            var url = $property.attr("upload-url");
            fileControl.fileupload({
                autoUpload: true,
                url: url,
                dataType: "json",
                done: function (e, data) {
                    if (data.result.IsSuccess) {
                        imgControl.attr("src", data.result.Data);
                    }
                    else {
                        alert(data.result.Message);
                    }

                }
            });
        }
        _init();
    }

    function NativeSelectBinderClass() {
        var _self = this;

        function _init() {
            _self.build = build;
        }
        function build($property) {
            //source-property-name
            var control = $property.find("*").andSelf().filter("select");
            var propName = $property.attr("property-name");
            var expression = $(control).attr("data-bind");
            expression = "value:" + propName;
            var source = $property.attr("source-property");
            if (source) {
                expression += ",source:" + source;
            }
            $(control).attr("name", propName);
            $(control).attr("data-bind", expression);
        }
        _init();
    }

    function NativeSelectMultipleBinderClass() {
        var _self = this;
        NativeSelectBinderClass.call(this);

        function _init() {

        }

        _init();
    }

    function Select2MultipleBinderClass() {
        var _self = this;
        NativeSelectMultipleBinderClass.call(this);
        var base = $.extend({}, this);

        function _init() {
            _self.build = build;
        }

        function build($property) {
            base.build.call(base, $property);
            var control = $property.find("*").andSelf().filter("select");
            control.select2();
        }

        _init();
    }

    function ListItemsBinderClass() {
        var _self = this;

        function _init() {
            _self.build = build;
        }

        function build($property) {
            var control = $property.find(".items");
            var propName = $property.attr("property-name");
            var expression = $(control).attr("data-bind");
            expression = "items:" + propName;
            $(control).attr("data-bind", expression);
        }

        _init();
    }

    function SingleItemBinderClass() {
        var _self = this;

        function _init() {
            _self.build = build;
        }

        function build($property) {
            var control = $property.find(".items");
            var propName = $property.attr("property-name");
            var expression = $(control).attr("data-bind");
            expression = "item:" + propName;
            $(control).attr("data-bind", expression);
        }

        _init();
    }

    function ChinaAreasBinderClass() {
        var _self = this;
        function _init() {
            _self.build = build;

            _self.buildModel = buildModel;

            _self.buildViewModel = buildViewModel;
        }
        function build($property) {
            var provinceControl = $("[name='ProvinceId']", $property);
            var cityControl = $("[name='CityId']", $property);
            var countyControl = $("[name='CountyId']", $property);
            provinceControl.attr("data-bind", "value:ProvinceId,source:SourceProvinces");
            provinceControl.attr("data-value-field", "Value");
            provinceControl.attr("data-text-field", "Text");

            cityControl.attr("data-bind", "value:CityId,source:SourceCities");
            cityControl.attr("data-value-field", "Value");
            cityControl.attr("data-text-field", "Text");

            countyControl.attr("data-bind", "value:CountyId,source:SourceCounties");
            countyControl.attr("data-value-field", "Value");
            countyControl.attr("data-text-field", "Text");
        }

        function buildModel(model) {
            model.ProvinceId = model.ProvinceId || "";
            model.CityId = model.CityId || "";
            model.CountyId = model.CountyId || "";

            model.SourceProvinces = webExpress.data.china.getProvinces();
            if (model.ProvinceId > 0) {
                model.SourceCities = webExpress.data.china.getCities(model.ProvinceId);
            }
            else {
                model.SourceCities = [{ Value: "", Text: "未填" }];
            }

            if (model.CityId > 0) {
                model.SourceCounties = webExpress.data.china.getCounties(model.CityId);
            }
            else {
                model.SourceCounties = [{ Value: "", Text: "未填" }];
            }
        }

        function buildViewModel(viewModel) {
            viewModel.bind("change", function (item) {
                if (item.field == "ProvinceId") {
                    var cities = webExpress.data.china.getCities(viewModel.ProvinceId);
                    if (cities.length == 0) {
                        cities = [{ Value: "", Text: "未填" }];
                    }
                    viewModel.set("SourceCities", cities);
                    if (cities.length == 2) {
                        viewModel.set("CityId", cities[1].Value);
                    }
                    else {
                        viewModel.set("CityId", 0);
                    }
                }

                if (item.field == "CityId") {
                    var counties = webExpress.data.china.getCounties(viewModel.CityId);
                    if (counties.length == 0) {
                        counties = [{ Value: "", Text: "未填" }];
                    }
                    viewModel.set("SourceCounties", counties);
                    viewModel.set("CountyId", 0);
                }
            });
        }
        _init();
    }
})();