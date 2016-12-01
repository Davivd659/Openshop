jQuery.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Please enter exactly {0} characters."));

jQuery.validator.addMethod("mobilePhoneNumber", function (value, element) {
    var length = value.length;
    var mobile = /^\d{11}$/;
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "手机号码格式错误");