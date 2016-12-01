/// <reference path="/_references.js" />
(function () {
    webExpress.utility.date = new DateClass();
    function DateClass() {
        var _self = this;
        var _defaultDateDelimiter = "-";
        var _mvcDateTimeZoneOffset = ((-1 * new Date().getTimezoneOffset()) / 60);
        var _mvcDateRegex = new RegExp("^\s*\/Date\\((-?\\d+)\\)\\/\s*$");
        function _init() {
            _self.toDate = toDate;
            _self.toStandardDateString = toStandardDateString;
        }

        function toDate(dateExpression) {
            var ticks = dateExpression.replace(_mvcDateRegex, "$1") - _mvcDateTimeZoneOffset;
            return new Date(ticks);
        }

        function toStandardDateString(dateStr, dateDelimiter) {
            var date = dateStr;
            if (!(dateStr instanceof Date)) {
                date = toDate(dateStr);
            }

            if (dateDelimiter == null) {
                dateDelimiter = _defaultDateDelimiter;
            }
            return date.getFullYear() + dateDelimiter + webExpress.utility.string.padLeft((date.getMonth() + 1), 2, "0") + dateDelimiter + webExpress.utility.string.padLeft(date.getDate(), 2, "0");
        };

        _init();
    }
})();