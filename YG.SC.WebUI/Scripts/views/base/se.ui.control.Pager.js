
se.ui.control.Pager = {
    enablePaging: function (pagerContainer, redirectToPage) {
        $(pagerContainer).on("click", ".grid-pager a[pageIndex]", function () {
            var pageIndex = $(this).attr("pageIndex");
            redirectToPage(pageIndex);
        });

        $(pagerContainer).on("click", ".t-arrow-prev", function () {
            var pageIndex = getCurrentPageIndex() - 1;
            redirectToPage(pageIndex);
        });

        $(pagerContainer).on("click", ".t-arrow-next", function () {
            var pageIndex = getCurrentPageIndex() + 1;
            redirectToPage(pageIndex);
        });
        $(pagerContainer).on("click", ".t-arrow-last", function () {
            var pageIndex = parseInt($(this).attr("pageIndex")) - 1;
            redirectToPage(pageIndex);
        });
        $(pagerContainer).on("click", ".t-arrow-first", function () {
            redirectToPage(0);
        });
        $(pagerContainer).on("click", ".t-refresh", function () {
            redirectToPage();
        });

        $(pagerContainer).on("change", "select.pagesize", function (e) {
            redirectToPage(0);
        });

        function getCurrentPageIndex() {
            return $(".t-state-active", pagerContainer).text() - 1;
        }
    },
    getPageSize: function () {
        return $("select.pagesize").val();
    },
    setPageSize: function (pagesize) {
        $("select.pagesize").val(pagesize);
    }
}