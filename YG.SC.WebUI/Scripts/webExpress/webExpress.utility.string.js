/// <reference path="/_references.js" />
(function () {
    webExpress.utility.string = new StringClass();
    function StringClass() {

        var DEFAULT_PADDING_CHAR = " "; //space char

        var _self = this;
        var _temp = null;
        function _init() {
            _self.padLeft = padLeft;
            _self.padRight = padRight;
            _self.isNullOrEmpty = isNullOrEmpty;
            _self.getObject = getObject;
            _self.format = format;
        }

        //convert string such as "a:3,b:'test'" to the equivalent object
        function getObject(str) {
            eval("_temp = {" + str + "}")
            return _temp;
        }

        function padRight(str, totalWidth, padChar) {
            return padCore(str, totalWidth, padChar, 2);
        }

        function padLeft(str, totalWidth, padChar) {
            return padCore(str, totalWidth, padChar, 1);
        }

        function isNullOrEmpty(str) {
            return str == "" || str == null;
        }

        function format(str) {
            var args = Array.prototype.slice.call(arguments, 1);
            return str.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                  ? args[number]
                  : match
                ;
            });
        }

        function padCore(str, totalWidth, padChar, direction) {
            var builder = [];
            if (str == null) {
                str = "";
            }
            else if (typeof str !== "string") {
                str = str.toString();
            }

            if (padChar == null) {
                padChar = DEFAULT_PADDING_CHAR;
            }
            else if (typeof padChar !== "string") {
                padChar = padChar.toString();
            }
            if (padChar.length === 0) {
                return str;
            }

            if (direction === 2) {
                builder.push(str);
            }
            var count = str.length;
            while (count < totalWidth) {
                builder.push(padChar);
                count += padChar.length;
            }
            if (direction === 1) {
                builder.push(str);
            }
            return builder.join("");
        }
        _init();
    }
})();