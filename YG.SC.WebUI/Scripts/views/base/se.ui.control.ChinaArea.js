(function () {
    //se.ui.control.Dialog = DialogClass;
    se.ui.control.ChinaArea = ChinaAreaClass;
    //Events:Changed TODO
    function ChinaAreaClass(container, model) {
        //se.ui.control.Eventable.call(this);
        var _container = container;
        var _self = this;
        var _model = model;
        var _viewModel;

        function _init() {
            _self.getValue = getValue;

            init();
        }

        function getValue() {
            return _viewModel;
        }

        function init() {
            if (!_model) {
                _model = {
                    ProvinceId: "",
                    CityId: "",
                    CountyId: ""
                };
            }
            _model.SourceProvinces = webExpress.data.china.getProvinces();
            if (_model.ProvinceId > 0) {
                _model.SourceCities = webExpress.data.china.getCities(model.ProvinceId);
            }
            else {
                _model.SourceCities = [{ Value: "", Text: "未填" }];
            }

            if (_model.CityId > 0) {
                _model.SourceCounties = webExpress.data.china.getCounties(model.CityId);
            }
            else {
                _model.SourceCounties = [{ Value: "", Text: "未填" }];
            }

            _viewModel = kendo.observable(_model);
            _viewModel.bind("change", function (item) {
                if (item.field == "ProvinceId") {
                    var cities = webExpress.data.china.getCities(_viewModel.ProvinceId);
                    if (cities.length == 0) {
                        cities = [{ Value: "", Text: "未填" }];
                    }
                    _viewModel.set("SourceCities", cities);
                    if (cities.length == 2) {
                        _viewModel.set("CityId", cities[1].Value);
                    }
                    else {
                        _viewModel.set("CityId", "");
                    }
                }

                if (item.field == "CityId") {
                    var counties = webExpress.data.china.getCounties(_viewModel.CityId);
                    if (counties.length == 0) {
                        counties = [{ Value: "", Text: "未填" }];
                    }
                    _viewModel.set("SourceCounties", counties);
                    _viewModel.set("CountyId", "");
                }
            });


            var provinceControl = $("[name='ProvinceId']", _container);
            var cityControl = $("[name='CityId']", _container);
            var countyControl = $("[name='CountyId']", _container);
            provinceControl.attr("data-bind", "value:ProvinceId,source:SourceProvinces");
            provinceControl.attr("data-value-field", "Value");
            provinceControl.attr("data-text-field", "Text");

            cityControl.attr("data-bind", "value:CityId,source:SourceCities");
            cityControl.attr("data-value-field", "Value");
            cityControl.attr("data-text-field", "Text");

            countyControl.attr("data-bind", "value:CountyId,source:SourceCounties");
            countyControl.attr("data-value-field", "Value");
            countyControl.attr("data-text-field", "Text");

            kendo.bind($(_container), _viewModel);
        }

        _init();
    }

    $.fn.chinaArea = function () {
        return se.ui.control.ChinaArea($(this));
    }
})();
