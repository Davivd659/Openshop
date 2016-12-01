$(document).ready(
    function () {
        $(document).on("click", "#btn_site_search", function () {
            var _projectkey = $("#txtKeys").val();
            window.location.href = "/project/search?keys=" + _projectkey;

        });
});