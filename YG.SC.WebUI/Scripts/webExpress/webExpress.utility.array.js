/// <reference path="/_references.js" />
(function () {
    webExpress.utility.array = new ArrayClass();
    function ArrayClass() {

        var DEFAULT_PADDING_CHAR = " "; //space char

        var _self = this;
        var _temp = null;
        function _init() {
            _self.find = find;

            _self.exist = exist;

            _self.create = create;
        }

        function create(length, initValue) {
            var arr = [];
            for (var i = 0; i < length; i++) {
                arr[i] = initValue;
            }
            return arr;
        }

        function find(arr, predicate) {
            for (var i = 0; i < arr.length; i++) {
                var item = arr[i];
                if (predicate(item)) {
                    return item;
                }
            }
        }

        function exist(arr, predicate) {
            var item = find(arr, predicate);
            return item != undefined;
        }
        _init();
    }
})();