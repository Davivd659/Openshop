//  首页 搜索 脚本 
$(document).ready(function () {
    $(".flash-search-btn").bind("click", function () {
        searchPro();
    });
    $(".flash-search-input").change(function () {
        //if (event.keyCode == 13) {
        //    // searchPro();
        //}
    });
});

function searchPro() {
    var w = $(".flash-search-input").val();
    var url = "/project/search?keys=" + encodeURIComponent(w);
    window.location.href = url;
}

