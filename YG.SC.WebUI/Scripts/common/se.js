window.se = webExpress;

$(document).on("blur", ":text,textarea", function () {
    var val = $.trim($(this).val());
    $(this).val(val);
});

se.helper = {};
se.helper.ajax = new AjaxHelperClass();
function AjaxHelperClass() {
    var _self = this;

    function _init() {
        _self.request = request;
    }

    function request(settings) {
        settings.container = settings.container || $(document);
        if (!settings.onFailure) {
            settings.onFailure = function (msg) {
                bootbox.alert(msg);
            }
        }
        if (settings.confirmMessage) {
            bootbox.confirm(settings.confirmMessage, function (isConfirmed) {
                if (isConfirmed) {
                    requestCore(settings);
                }
            })
        } else {
            requestCore(settings);
        }
        
    }

    function requestCore(settings) {
        settings.container.mask("Loading...");
        webExpress.utility.ajax.request(settings.url, settings.model,
            function (data) {
                settings.container.unmask();
                if (data.IsSuccess) {
                    if (settings.onSuccess) {
                        settings.onSuccess.call(this);
                    }
                }
                else {
                    if (settings.onFailure) {
                        settings.onFailure.call(this, data.Message);
                    }
                }
            }, function () {
                settings.container.unmask();
                settings.onError.call(this);
        });
    }

    _init();
}