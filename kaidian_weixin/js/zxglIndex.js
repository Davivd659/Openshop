jQuery(function(a){function b(){var b=a.trim(d.val());b&&(location.href=f+encodeURIComponent(b))}var c=a("#askSearchBtn"),d=a("#askSearchKey"),e=a(".icon-clear"),f=c.data("url");d.bind("keyup input",function(){var b=a.trim(d.val());e.toggleClass("active",!!b)}),e.bind("touchstart click",function(){return d.val("").focus(),e.removeClass("active"),!1}),d.bind("keyup",function(a){13==a.which&&b()}),c.bind("click touchstart",function(){return b(),!1})});
//# sourceMappingURL=zxglIndex.min.js.map