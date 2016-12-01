se.ui.view.SearchViewBase = SearchViewBaseClass;
se.ui.view.SearchViewBase.Settings = SearchViewBaseSettings;

function SearchViewBaseClass(settings) {
    var _criteria = settings.criteria;
    var _self = this;
    function _init() {
        _self.init = init;

        _self.search = search;

        _self.refresh = refresh;

        _self.modules = settings.modules;
    }

    function init() {
        se.ui.control.Pager.setPageSize(settings.criteria.PagingRequest.PageSize);

        $(search);

        settings.searchButton.click(function () {
            search();
        });

        for (var cmd in settings.rowCommands) {
            var selector = "tr [command-name='" + cmd + "']";
            var handler = settings.rowCommands[cmd];
            var handlerProxy = (function (callback) {
                return function () {
                    var tr = $(this).closest("tr");
                    var str = tr.attr("model-field-entry");
                    var strNormalized = str.replace(/:(?=,)|:(?=$)/g, ":null");
                    var entries = strNormalized.split(",");
                    var model = webExpress.utility.string.getObject(entries);

                    var tds = $(this).closest("tr").find("td[model-field]");
                    for (var i = 0; i < tds.length; i++) {
                        var td = $(tds[i]);
                        var key = td.attr("model-field");
                        var val = td.text();
                        model[key] = val;
                    }
                    callback.call(this, model);
                }
            })(handler);

            $(document).on("click", selector, handlerProxy);
        }

        for (var vCmd in settings.viewCommands) {
            var selector = ".search-action [command-name='" + vCmd + "']";
            var handlerProxy = settings.viewCommands[vCmd];
            $(document).on("click", selector, handlerProxy);
        }

        for (var key in settings.modules) {
            var module = settings.modules[key];
            module.view = _self;
            module.init();
        }

        se.ui.control.Pager.enablePaging(settings.searchResultContainer, refresh);
    }

    function search() {
        var model = settings.getCriteriaModel();
        $.extend(_criteria, model);
        refresh(0);
    }

    function refresh(pageIndex) {
        if (pageIndex !== undefined) {
            _criteria.PagingRequest.PageIndex = pageIndex;
        }
        _criteria.PagingRequest.PageSize = se.ui.control.Pager.getPageSize() || settings.criteria.PagingRequest.PageSize;
        settings.searchResultContainer.mask("loading...");
        webExpress.utility.ajax.request(settings.url, _criteria,
            function (data) { 
                if (data.IsSuccess === false) {
                    settings.searchFeedback.text(data.Message);
                }
                else {
                    settings.searchResultHandler.call(settings, data);
                }
                 
                settings.searchResultContainer.unmask();
                settings.searchFeedback.text("");
            },
            function (state, response, status) {
                alert(arguments);
            }
         );
    }

    _init();
}

function SearchViewBaseSettings(settings) {
    this.url = "";
    this.viewCommands = {};
    this.rowCommands = {};
    this.modules = {};
    this.searchButton = $(".btn[role='search']");
    this.searchFeedback = $(".search-feedback");
    this.searchResultContainer = $(".search-result");
    this.searchResultHandler = function (data) {
        this.searchResultContainer.html(data);
    }
    this.criteria = { PagingRequest: { PageIndex: 0, PageSize: 10 } };
    this.getCriteriaModel = function () {
        var model = $(".search-criteria form").serializeObject();
        return model;
    }

    $.extend(this, settings);
}