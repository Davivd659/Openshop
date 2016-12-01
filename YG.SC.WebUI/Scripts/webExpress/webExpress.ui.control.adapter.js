
webExpress.ui.control.adapter = new AdapterClass();
function AdapterClass() {
    var _self = this;

    _self.simpleTextbox = {
        valueField: "value",
        controlSelector: ":text"
    };

    _self.simpleTextarea = {
        valueField: "value",
        controlSelector: "textarea"
    };

    _self.simpleRadioButton = {
        valueField: "checked",
        controlSelector: ":radio"
    };

    _self.dateSelects = {
        valueField: "date",
        controlSelector: ".date"
    };

    _self.datetimeControl = {
        valueField: "datetime",
        controlSelector: ".datetime"
    };

    _self.simpleDropdown = {
        valueField: "value",
        controlSelector: "select"
    };

    _self.simpleCheckbox = {
        valueField: "checked",
        controlSelector: ":checkbox"
    };

    _self.simpleImg = {
        valueField: function (propName) {
            return "attr:{src:" + propName + "}";
        },
        controlSelector: "img"
    };

}

