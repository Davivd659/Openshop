(function () {
    se.ui.control.Eventable = EventableClass;
    function EventableClass() {
        var _self = this;
        var _events = {};
        function _init() {
            _self.on = on;
            _self.fire = fire;
        }

        function on(eventName, handler) {
            if (_events[eventName] === undefined) {
                _events[eventName] = [];
            }
            _events[eventName].push(handler);
        }

        function fire(eventName, argsArray) {
            var handlers = _events[eventName];
            for (var i = 0; i < handlers.length; i++) {
                handlers[i].apply(this, argsArray);
            }
        }

        _init();
    }
})();
