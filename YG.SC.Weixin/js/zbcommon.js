/**
 * Created by steven.zhang on 2015/6/27.
 */
//RSA加密处理
function rsaEncryptNameAndPhone(dataObj, type, unReEmpty) {
    var type = type || false,
        date = new Date(),
        chenghu = '',
        phoneObj = dataObj.phoneObj,
        chenhuObj = dataObj.chenhuObj,
        phoneTmp = phoneObj.val(),
        rsadata, data;

    if (chenhuObj) {//有称呼
        chenghu = chenhuObj.val();
    }
    rsadata = RSAUtils.encryptfun(phoneObj.val() + ',' + chenghu + ',' + date.getTime() + ',' + Math.random());
    if (!unReEmpty) {//清空
        phoneObj.val('').trigger('blur');
        chenhuObj && chenhuObj.val('').trigger('blur');
    }

    if (type) {
        return 'phone=' + phoneTmp + '&' + rsadata;
    } else {
        return '&rsadata=' + rsadata + '&rsastatus=1';
    }
}