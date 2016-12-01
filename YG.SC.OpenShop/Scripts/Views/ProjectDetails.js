$(function() {
    $('#btnApply').on('click', function () {
  
        var name = $('#ApplyName').val();
        var phone = $('#ApplyPhone').val();
        var grouptype = $("input[type='radio':check]").val();
        var projectid = $("#hdProjectId").val();

        if(name==""){
            alert("请输入姓名");
            return ;
            ;
        }
        alert(name);
        //var isMobile =isMobil(phone);
        //alert(isMobile);

        //var paramaters = {};
        //paramaters.ApplyName = name;
        //paramaters.ApplyPhone = phone;
        //paramaters.GroupType = grouptype;
        //paramaters.ProjectId = projectid;

        //var url = "/Project/Apply";
        //$.ajax({
        //    type: 'POST',
        //    url: url,
        //    data: paramaters,
        //    success: function (result) {
        //        if (result == "true") {
        //            alert("报名成功！");
        //        } else {
        //            alert("申请失败，请重试！")
        //        }
        //    }

        //});
    })
    
});
 
 
//手机号码验证信息
function isMobil(s)
{
    var patrn = /(^0{0,1}1[3|4|5|6|7|8|9][0-9]{9}$)/;
    if (!patrn.exec(s))
    {
        return false;
    } return true;
}