! function() {
	function a() {
		return document.documentElement.scrollTop || window.pageYoffset || document.body.scrollTop
	}

	function b() {
		var a = new Array;
		return document.documentElement ? (a[0] = document.documentElement.clientWidth, a[1] = document.documentElement.clientHeight) : (a[0] = document.body.clientWidth, a[1] = document.body.clientHeight), a
	}
	var c = "undefined" != typeof jQuery ? jQuery : null,
		d = null,
		e = 0,
		f = !1,
		g = {
			goBack: function() {
				window.history.go(-1)
			},
			download: function(a) {
				var b = navigator.userAgent.toLowerCase(),
					c = "";
				b.indexOf("iphone") > -1 ? c = "zxgj" == a ? "https://itunes.apple.com/cn/app/zhuang-xiu-guan-jia/id578507321?mt=8" : "https://itunes.apple.com/cn/app/zhuang-xiu-ti-yan-guan-shen/id610304054?mt=8" : b.indexOf("android") > -1 ? c = "zxgj" == a ? "http://www.to8to.com/app/to8to_zxgj_android_v1_3.apk" : "http://www.to8to.com/app/com.to8to.zxtyg_v_1_1_sharelink.apk" : b.indexOf("ipad") > -1 && (c = "zxgj" == a ? "https://itunes.apple.com/cn/app/zhuang-xiu-guan-jia/id646352049?mt=8" : "https://itunes.apple.com/cn/app/zhuang-xiu-ti-yan-guanhd-tu/id633995916?mt=8"), c ? location.href = c : alert("Soryy,该应用目前只提供android/iphone的手机下载")
			},
			G: function(a) {
				return document.getElementById(a)
			},
			showArea: function() {
				c(".town_list").slideToggle(400)
			},
			changeTown: function(a, b) {
				c("#company_area").html(b), c(".town_list").slideToggle(400), c("#page").val(1), c("#townid").val(a), this.getPageData()
			},
			getPageData: function() {
				var a = c("#page").val(),
					b = c("#townid").val();
				c(".company_list_loading").show(), window.scrollTo(0, 0);
				var d = pageurl.replace(/townidvalue|pagenum/gi, function(c) {
					return "townidvalue" == c ? b : a
				});
				c.ajax({
					url: d,
					dataType: "json",
					error: function() {
						c(".company_list_loading").hide()
					},
					success: function(a) {
						a.allpage > 0 ? (c("#company_list_div").html(a.html), c("#allpage").val(a.allpage)) : alert("数据读取失败,请重试"), c(".company_list_loading").hide()
					}
				})
			},
			changeRadio: function(a, b, d) {
				for (var e = c("." + b), f = 0, g = e.length; g > f; f++) {
					var h = c(e[f]).find("span");
					e[f] == a ? (c("#" + d).val(c(a).attr("data-value")), c(h[1]).removeClass("radio-inner").addClass("radio-inner-checked"), c(h[2]).removeClass("radio-txt").addClass("radio-txt-checked")) : (c(h[1]).removeClass("radio-inner-checked").addClass("radio-inner"), c(h[2]).removeClass("radio-txt-checked").addClass("radio-txt"))
				}
			},
			applySubmit: function(a) {
				var b = c(".apply_btn").html();
				if ("确定申请" != b) return !1;
				c(".apply_btn").html("正在申请中....");
				var d = this.G("chenghu").value,
					e = this.G("phone").value,
					f = this.G("User_Shen").value,
					g = this.G("User_City").value,
					h = this.G("area").value,
					i = this.G("zxys").value,
					j = /^((\(\d{2,3}\))|(\d{3}\-))?(13|15|18)\d{9}$/,
					k = 1;
				if (d ? j.test(e) ? f ? g ? h ? 0 == i && (alert("请选择您的装修预算"), c("#zxys")[0].focus(), k = 0) : (alert("请填写您的房屋面积"), c("#area")[0].focus(), k = 0) : (alert("请选择您所在的市!"), c("#User_City")[0].focus(), k = 0) : (alert("请选择您所在有省份!"), c("#User_Shen")[0].focus(), k = 0) : (alert("请填写真实的联系电话"), c("#phone")[0].focus(), k = 0) : (alert("请填写您的称呼"), c("#chenghu")[0].focus(), k = 0), 0 == k) return c(".apply_btn").html("确定申请"), !1;
				var l = c("#applyform").serialize();
				c.ajax({
					type: "POST",
					url: a[1],
					dataType: "json",
					data: l,
					success: function(a) {
						1 == a.status ? (c(".apply_notice").css({
							top: "44px"
						}), c(".apply_box").css({
							top: "-340px"
						})) : c(".apply_btn").html("确定申请"), c("body").append('<div style="display:none;"><iframe src="http://www.to8to.com/api/wap_test.php"></iframe></div>')
					}
				})
			},
			showTelBox: function() {
				var a = c("#oarea");
				if (a.length > 0) {
					var b = parseInt(a.val());
					if (isNaN(b)) return alert("请填写正确的房屋面积"), a[0].focus(), !1
				}
				var d = c("#zxzt");
				if (d.length > 0) {
					var e = d.val();
					if (-1 == e) return alert("请选择装修状态"), d[0].focus(), !1
				}
				c(".apply-notice-box-cover").show(), c(".apply-notice-box").css({
					top: "0px"
				}), c(".apply-notice-box-body-title").html(c("#notice-title").val()), c("#submit-btn").css({
					background: "#278101"
				}), window.scrollTo(0, 0)
			},
			noticeClose: function() {
				c(".apply-notice-box-cover").hide(), c(".apply-notice-box").css({
					top: "-210px"
				}), c(".apply-notice-box-body").css({
					top: "0px"
				}), c("#submit-btn").css({
					background: ""
				}), f && window.location.reload()
			},
			submitApply: function() {
				var a = c("#apply_btn_ok").html();
				if ("提交" != a) return !1;
				var b = c("#chenghu").val(),
					d = c("#phone").val(),
					e = /^((\(\d{2,3}\))|(\d{3}\-))?(13|15|18)\d{9}$/,
					h = 1;
				if (b ? e.test(d) || (alert("请填写真实的联系电话"), c("#phone")[0].focus(), h = 0) : (alert("请填写您的称呼"), c("#chenghu")[0].focus(), h = 0), 0 == h) return c("#apply_btn_ok").html("提交"), !1;
				var i = c("#form_zx").serialize();
				i += "&chenghu=" + b + "&phone=" + d;
				var j = c("#post_url").val();
				$("#apply_btn_ok").unbind("click").removeAttr("onclick"), c.ajax({
					type: "POST",
					url: j,
					dataType: "json",
					data: i,
					success: function(a) {
						1 == a.status ? (c(".apply-notice-box-body").css({
							top: "-210px"
						}), c(".apply-notice-result").show(), c(".apply_btn").html("申请成功"), f = !0) : 5 == a.status ? (alert("您所在的城市尚未开通此项服务，我们会在开通后第一时间为您服务。"), g.noticeClose(), c(".apply_btn").html("确定申请")) : (a.msg && alert(a.msg), g.noticeClose(), c(".apply_btn").html("确定申请"))
					}
				})
			},
			zxbGoDown: function() {
				var b = a(),
					c = [0, 600, 1200, 1700, 2200],
					d = 0;
				for (var e in c)
					if (c[e] > b) {
						d = c[e];
						break
					}
				window.scrollTo(0, d)
			},
			leftTime: function() {
				if (leftsecond > 0) {
					var a = Math.floor(leftsecond / 3600),
						b = Math.floor((leftsecond - 3600 * a) / 60),
						e = Math.floor(leftsecond - 3600 * a - 60 * b),
						f = a + "小时" + b + "分" + e + "秒";
					leftsecond -= 1, c(".group-detail-time").html(f), d = null, d = setTimeout(function() {
						g.leftTime()
					}, 1e3)
				} else clearTimeout(d), d = null
			},
			getGroupListData: function() {
				var d = a(),
					f = ".group-list-box",
					g = c(f).height(),
					h = c(f).offset(),
					i = b();
				if (d + i[1] >= g - h.top && 0 == e) {
					e = 1;
					var j = now_page + 1,
						k = data_url.replace("pagenum", j);
					c.ajax({
						type: "POST",
						url: k,
						dataType: "json",
						success: function(a) {
							a.html ? (now_page += 1, c(f).append(a.html), e = 0) : e = 1
						}
					})
				}
			},
			getZxzsListData: function() {
				var d = a(),
					f = ".xzx-list-box",
					g = c(f).height(),
					h = c(f).offset(),
					i = b();
				if (d + i[1] >= g - h.top && 0 == e) {
					e = 1;
					var j = now_page + 1,
						k = data_url.replace("pagenum", j);
					c.ajax({
						type: "POST",
						url: k,
						dataType: "json",
						success: function(a) {
							a.html ? (now_page += 1, c(f).append(a.html), e = 0) : e = 1
						}
					})
				}
			}
		};
	if (window.m_to8to = g, window.console) {
		var h = window.console;
		h.log("我们期待你一起来为开店ing添砖加瓦!\nhttp://wwww.kaidianing.com/about/join_us.php#6\n请在邮件中注明%c来自:wap-console", "color:green;font-weight:bold;")
	}
}(),
function() {
	function a(c, d) {
		function e(a, b) {
			return function() {
				return a.apply(b, arguments)
			}
		}
		var f;
		if (d = d || {}, this.trackingClick = !1, this.trackingClickStart = 0, this.targetElement = null, this.touchStartX = 0, this.touchStartY = 0, this.lastTouchIdentifier = 0, this.touchBoundary = d.touchBoundary || 10, this.layer = c, this.tapDelay = d.tapDelay || 200, !a.notNeeded(c)) {
			for (var g = ["onMouse", "onClick", "onTouchStart", "onTouchMove", "onTouchEnd", "onTouchCancel"], h = this, i = 0, j = g.length; j > i; i++) h[g[i]] = e(h[g[i]], h);
			b && (c.addEventListener("mouseover", this.onMouse, !0), c.addEventListener("mousedown", this.onMouse, !0), c.addEventListener("mouseup", this.onMouse, !0)), c.addEventListener("click", this.onClick, !0), c.addEventListener("touchstart", this.onTouchStart, !1), c.addEventListener("touchmove", this.onTouchMove, !1), c.addEventListener("touchend", this.onTouchEnd, !1), c.addEventListener("touchcancel", this.onTouchCancel, !1), Event.prototype.stopImmediatePropagation || (c.removeEventListener = function(a, b, d) {
				var e = Node.prototype.removeEventListener;
				"click" === a ? e.call(c, a, b.hijacked || b, d) : e.call(c, a, b, d)
			}, c.addEventListener = function(a, b, d) {
				var e = Node.prototype.addEventListener;
				"click" === a ? e.call(c, a, b.hijacked || (b.hijacked = function(a) {
					a.propagationStopped || b(a)
				}), d) : e.call(c, a, b, d)
			}), "function" == typeof c.onclick && (f = c.onclick, c.addEventListener("click", function(a) {
				f(a)
			}, !1), c.onclick = null)
		}
	}
	var b = navigator.userAgent.indexOf("Android") > 0,
		c = /iP(ad|hone|od)/.test(navigator.userAgent),
		d = c && /OS 4_\d(_\d)?/.test(navigator.userAgent),
		e = c && /OS ([6-9]|\d{2})_\d/.test(navigator.userAgent),
		f = navigator.userAgent.indexOf("BB10") > 0;
	a.prototype.needsClick = function(a) {
		switch (a.nodeName.toLowerCase()) {
			case "button":
			case "select":
			case "textarea":
				if (a.disabled) return !0;
				break;
			case "input":
				if (c && "file" === a.type || a.disabled) return !0;
				break;
			case "label":
			case "video":
				return !0
		}
		return /\bneedsclick\b/.test(a.className)
	}, a.prototype.needsFocus = function(a) {
		switch (a.nodeName.toLowerCase()) {
			case "textarea":
				return !0;
			case "select":
				return !b;
			case "input":
				switch (a.type) {
					case "button":
					case "checkbox":
					case "file":
					case "image":
					case "radio":
					case "submit":
						return !1
				}
				return !a.disabled && !a.readOnly;
			default:
				return /\bneedsfocus\b/.test(a.className)
		}
	}, a.prototype.sendClick = function(a, b) {
		var c, d;
		document.activeElement && document.activeElement !== a && document.activeElement.blur(), d = b.changedTouches[0], c = document.createEvent("MouseEvents"), c.initMouseEvent(this.determineEventType(a), !0, !0, window, 1, d.screenX, d.screenY, d.clientX, d.clientY, !1, !1, !1, !1, 0, null), c.forwardedTouchEvent = !0, a.dispatchEvent(c)
	}, a.prototype.determineEventType = function(a) {
		return b && "select" === a.tagName.toLowerCase() ? "mousedown" : "click"
	}, a.prototype.focus = function(a) {
		var b;
		c && a.setSelectionRange && 0 !== a.type.indexOf("date") && "time" !== a.type ? (b = a.value.length, a.setSelectionRange(b, b)) : a.focus()
	}, a.prototype.updateScrollParent = function(a) {
		var b, c;
		if (b = a.fastClickScrollParent, !b || !b.contains(a)) {
			c = a;
			do {
				if (c.scrollHeight > c.offsetHeight) {
					b = c, a.fastClickScrollParent = c;
					break
				}
				c = c.parentElement
			} while (c)
		}
		b && (b.fastClickLastScrollTop = b.scrollTop)
	}, a.prototype.getTargetElementFromEventTarget = function(a) {
		return a.nodeType === Node.TEXT_NODE ? a.parentNode : a
	}, a.prototype.onTouchStart = function(a) {
		var b, e, f;
		if (a.targetTouches.length > 1) return !0;
		if (b = this.getTargetElementFromEventTarget(a.target), e = a.targetTouches[0], c) {
			if (f = window.getSelection(), f.rangeCount && !f.isCollapsed) return !0;
			if (!d) {
				if (e.identifier && e.identifier === this.lastTouchIdentifier) return a.preventDefault(), !1;
				this.lastTouchIdentifier = e.identifier, this.updateScrollParent(b)
			}
		}
		return this.trackingClick = !0, this.trackingClickStart = a.timeStamp, this.targetElement = b, this.touchStartX = e.pageX, this.touchStartY = e.pageY, a.timeStamp - this.lastClickTime < this.tapDelay && a.preventDefault(), !0
	}, a.prototype.touchHasMoved = function(a) {
		var b = a.changedTouches[0],
			c = this.touchBoundary;
		return Math.abs(b.pageX - this.touchStartX) > c || Math.abs(b.pageY - this.touchStartY) > c ? !0 : !1
	}, a.prototype.onTouchMove = function(a) {
		return this.trackingClick ? ((this.targetElement !== this.getTargetElementFromEventTarget(a.target) || this.touchHasMoved(a)) && (this.trackingClick = !1, this.targetElement = null), !0) : !0
	}, a.prototype.findControl = function(a) {
		return void 0 !== a.control ? a.control : a.htmlFor ? document.getElementById(a.htmlFor) : a.querySelector("button, input:not([type=hidden]), keygen, meter, output, progress, select, textarea")
	}, a.prototype.onTouchEnd = function(a) {
		var f, g, h, i, j, k = this.targetElement;
		if (!this.trackingClick) return !0;
		if (a.timeStamp - this.lastClickTime < this.tapDelay) return this.cancelNextClick = !0, !0;
		if (this.cancelNextClick = !1, this.lastClickTime = a.timeStamp, g = this.trackingClickStart, this.trackingClick = !1, this.trackingClickStart = 0, e && (j = a.changedTouches[0], k = document.elementFromPoint(j.pageX - window.pageXOffset, j.pageY - window.pageYOffset) || k, k.fastClickScrollParent = this.targetElement.fastClickScrollParent), h = k.tagName.toLowerCase(), "label" === h) {
			if (f = this.findControl(k)) {
				if (this.focus(k), b) return !1;
				k = f
			}
		} else if (this.needsFocus(k)) return a.timeStamp - g > 100 || c && window.top !== window && "input" === h ? (this.targetElement = null, !1) : (this.focus(k), this.sendClick(k, a), c && "select" === h || (this.targetElement = null, a.preventDefault()), !1);
		return c && !d && (i = k.fastClickScrollParent, i && i.fastClickLastScrollTop !== i.scrollTop) ? !0 : (this.needsClick(k) || (a.preventDefault(), this.sendClick(k, a)), !1)
	}, a.prototype.onTouchCancel = function() {
		this.trackingClick = !1, this.targetElement = null
	}, a.prototype.onMouse = function(a) {
		return this.targetElement ? a.forwardedTouchEvent ? !0 : a.cancelable && (!this.needsClick(this.targetElement) || this.cancelNextClick) ? (a.stopImmediatePropagation ? a.stopImmediatePropagation() : a.propagationStopped = !0, a.stopPropagation(), a.preventDefault(), !1) : !0 : !0
	}, a.prototype.onClick = function(a) {
		var b;
		return this.trackingClick ? (this.targetElement = null, this.trackingClick = !1, !0) : "submit" === a.target.type && 0 === a.detail ? !0 : (b = this.onMouse(a), b || (this.targetElement = null), b)
	}, a.prototype.destroy = function() {
		var a = this.layer;
		b && (a.removeEventListener("mouseover", this.onMouse, !0), a.removeEventListener("mousedown", this.onMouse, !0), a.removeEventListener("mouseup", this.onMouse, !0)), a.removeEventListener("click", this.onClick, !0), a.removeEventListener("touchstart", this.onTouchStart, !1), a.removeEventListener("touchmove", this.onTouchMove, !1), a.removeEventListener("touchend", this.onTouchEnd, !1), a.removeEventListener("touchcancel", this.onTouchCancel, !1)
	}, a.notNeeded = function(a) {
		var c, d, e;
		if ("undefined" == typeof window.ontouchstart) return !0;
		if (d = +(/Chrome\/([0-9]+)/.exec(navigator.userAgent) || [, 0])[1]) {
			if (!b) return !0;
			if (c = document.querySelector("meta[name=viewport]")) {
				if (-1 !== c.content.indexOf("user-scalable=no")) return !0;
				if (d > 31 && document.documentElement.scrollWidth <= window.outerWidth) return !0
			}
		}
		if (f && (e = navigator.userAgent.match(/Version\/([0-9]*)\.([0-9]*)/), e[1] >= 10 && e[2] >= 3 && (c = document.querySelector("meta[name=viewport]")))) {
			if (-1 !== c.content.indexOf("user-scalable=no")) return !0;
			if (document.documentElement.scrollWidth <= window.outerWidth) return !0
		}
		return "none" === a.style.msTouchAction ? !0 : !1
	}, a.attach = function(b, c) {
		return new a(b, c)
	}, window.FastClick = a
}(),
function(a, b) {
	a.tapHandling = !1, a.tappy = !0;
	var c = function(c) {
			return c.each(function() {
				function c(a) {
					b(a.target).trigger("tap", [a, b(a.target).attr("href")])
				}

				function d(a) {
					var b = a.originalEvent || a,
						c = b.touches || b.targetTouches;
					return c ? [c[0].pageX, c[0].pageY] : null
				}

				function e(a) {
					if (a.touches && a.touches.length > 1 || a.targetTouches && a.targetTouches.length > 1) return !1;
					var b = d(a);
					j = b[0], i = b[1]
				}

				function f(a) {
					if (!k) {
						var b = d(a);
						b && (Math.abs(i - b[1]) > m || Math.abs(j - b[0]) > m) && (k = !0)
					}
				}

				function g(b) {
					if (clearTimeout(h), h = setTimeout(function() {
						a.tapHandling = !1, k = !1
					}, 1e3), !(b.which && b.which > 1 || b.shiftKey || b.altKey || b.metaKey || b.ctrlKey)) {
						if (b.preventDefault(), k || a.tapHandling && a.tapHandling !== b.type) return void(k = !1);
						a.tapHandling = b.type, c(b)
					}
				}
				var h, i, j, k, l = b(this),
					m = 10;
				l.bind("touchstart.tappy MSPointerDown.tappy", e).bind("touchmove.tappy MSPointerMove.tappy", f).bind("touchend.tappy MSPointerUp.tappy", g).bind("click.tappy", g)
			})
		},
		d = function(a) {
			return a.unbind(".tappy")
		};
	if (b.event && b.event.special) b.event.special.tap = {
		add: function() {
			c(b(this))
		},
		remove: function() {
			d(b(this))
		}
	};
	else {
		var e = b.fn.bind,
			f = b.fn.unbind;
		b.fn.bind = function(a) {
			return /(^| )tap( |$)/.test(a) && c(this), e.apply(this, arguments)
		}, b.fn.unbind = function(a) {
			return /(^| )tap( |$)/.test(a) && d(this), f.apply(this, arguments)
		}
	}
}(this, jQuery), ! function(a, b, c, d) {
	var e = a(b);
	a.fn.lazyload = function(f) {
		function g() {
			var b = 0;
			i.each(function() {
				var c = a(this);
				if (!j.skip_invisible || c.is(":visible"))
					if (a.abovethetop(this, j) || a.leftofbegin(this, j));
					else if (a.belowthefold(this, j) || a.rightoffold(this, j)) {
					if (++b > j.failure_limit) return !1
				} else c.trigger("appear"), b = 0
			})
		}
		var h, i = this,
			j = {
				threshold: 0,
				failure_limit: 0,
				event: "scroll",
				effect: "show",
				container: b,
				data_attribute: "original",
				skip_invisible: !0,
				appear: null,
				load: null,
				placeholder: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAANSURBVBhXYzh8+PB/AAffA0nNPuCLAAAAAElFTkSuQmCC"
			};
		return f && (d !== f.failurelimit && (f.failure_limit = f.failurelimit, delete f.failurelimit), d !== f.effectspeed && (f.effect_speed = f.effectspeed, delete f.effectspeed), a.extend(j, f)), h = j.container === d || j.container === b ? e : a(j.container), 0 === j.event.indexOf("scroll") && h.bind(j.event, function() {
			return g()
		}), this.each(function() {
			var b = this,
				c = a(b);
			b.loaded = !1, (c.attr("src") === d || c.attr("src") === !1) && c.is("img") && c.attr("src", j.placeholder), c.one("appear", function() {
				if (!this.loaded) {
					if (j.appear) {
						var d = i.length;
						j.appear.call(b, d, j)
					}
					a("<img />").bind("load", function() {
						var d = c.attr("data-" + j.data_attribute);
						c.hide(), c.is("img") ? c.attr("src", d) : c.css("background-image", "url('" + d + "')"), c[j.effect](j.effect_speed), b.loaded = !0;
						var e = a.grep(i, function(a) {
							return !a.loaded
						});
						if (i = a(e), j.load) {
							var f = i.length;
							j.load.call(b, f, j)
						}
					}).attr("src", c.attr("data-" + j.data_attribute))
				}
			}), 0 !== j.event.indexOf("scroll") && c.bind(j.event, function() {
				b.loaded || c.trigger("appear")
			})
		}), e.bind("resize", function() {
			g()
		}), /(?:iphone|ipod|ipad).*os 5/gi.test(navigator.appVersion) && e.bind("pageshow", function(b) {
			b.originalEvent && b.originalEvent.persisted && i.each(function() {
				a(this).trigger("appear")
			})
		}), a(c).ready(function() {
			g()
		}), this
	}, a.belowthefold = function(c, f) {
		var g;
		return g = f.container === d || f.container === b ? (b.innerHeight ? b.innerHeight : e.height()) + e.scrollTop() : a(f.container).offset().top + a(f.container).height(), g <= a(c).offset().top - f.threshold
	}, a.rightoffold = function(c, f) {
		var g;
		return g = f.container === d || f.container === b ? e.width() + e.scrollLeft() : a(f.container).offset().left + a(f.container).width(), g <= a(c).offset().left - f.threshold
	}, a.abovethetop = function(c, f) {
		var g;
		return g = f.container === d || f.container === b ? e.scrollTop() : a(f.container).offset().top, g >= a(c).offset().top + f.threshold + a(c).height()
	}, a.leftofbegin = function(c, f) {
		var g;
		return g = f.container === d || f.container === b ? e.scrollLeft() : a(f.container).offset().left, g >= a(c).offset().left + f.threshold + a(c).width()
	}, a.inviewport = function(b, c) {
		return !(a.rightoffold(b, c) || a.leftofbegin(b, c) || a.belowthefold(b, c) || a.abovethetop(b, c))
	}, a.extend(a.expr[":"], {
		"below-the-fold": function(b) {
			return a.belowthefold(b, {
				threshold: 0
			})
		},
		"above-the-top": function(b) {
			return !a.belowthefold(b, {
				threshold: 0
			})
		},
		"right-of-screen": function(b) {
			return a.rightoffold(b, {
				threshold: 0
			})
		},
		"left-of-screen": function(b) {
			return !a.rightoffold(b, {
				threshold: 0
			})
		},
		"in-viewport": function(b) {
			return a.inviewport(b, {
				threshold: 0
			})
		},
		"above-the-fold": function(b) {
			return !a.belowthefold(b, {
				threshold: 0
			})
		},
		"right-of-fold": function(b) {
			return a.rightoffold(b, {
				threshold: 0
			})
		},
		"left-of-fold": function(b) {
			return !a.rightoffold(b, {
				threshold: 0
			})
		}
	})
}(jQuery, window, document),
function(a) {
	"function" == typeof define && define.amd ? define(["jquery"], a) : a("object" == typeof exports ? require("jquery") : jQuery)
}(function(a) {
	function b(a) {
		return h.raw ? a : encodeURIComponent(a)
	}

	function c(a) {
		return h.raw ? a : decodeURIComponent(a)
	}

	function d(a) {
		return b(h.json ? JSON.stringify(a) : String(a))
	}

	function e(a) {
		0 === a.indexOf('"') && (a = a.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, "\\"));
		try {
			return a = decodeURIComponent(a.replace(g, " ")), h.json ? JSON.parse(a) : a
		} catch (b) {}
	}

	function f(b, c) {
		var d = h.raw ? b : e(b);
		return a.isFunction(c) ? c(d) : d
	}
	var g = /\+/g,
		h = a.cookie = function(e, g, i) {
			if (arguments.length > 1 && !a.isFunction(g)) {
				if (i = a.extend({}, h.defaults, i), "number" == typeof i.expires) {
					var j = i.expires,
						k = i.expires = new Date;
					k.setTime(+k + 864e5 * j)
				}
				return document.cookie = [b(e), "=", d(g), i.expires ? "; expires=" + i.expires.toUTCString() : "", i.path ? "; path=" + i.path : "", i.domain ? "; domain=" + i.domain : "", i.secure ? "; secure" : ""].join("")
			}
			for (var l = e ? void 0 : {}, m = document.cookie ? document.cookie.split("; ") : [], n = 0, o = m.length; o > n; n++) {
				var p = m[n].split("="),
					q = c(p.shift()),
					r = p.join("=");
				if (e && e === q) {
					l = f(r, g);
					break
				}
				e || void 0 === (r = f(r)) || (l[q] = r)
			}
			return l
		};
	h.defaults = {}, a.removeCookie = function(b, c) {
		return void 0 === a.cookie(b) ? !1 : (a.cookie(b, "", a.extend({}, c, {
			expires: -1
		})), !a.cookie(b))
	}
}), $(function() {
	$("select").addClass("needsclick");
	var a = {
		init: function() {
			this._$el = $("#dt-hd"), this._$dtHdNavWrap = $("#dt-hd-navs-wrap"), this._$dthdnav = $("#dt-hd-nav"), this._$searchNav = $(".nav-search"), this._$searchWraperPanel = $(".nav-search-wraper"), this._$searchTypes = this._$searchWraperPanel.find(".nav-search-types"), this._$searchType = this._$searchWraperPanel.find(".nav-search-type"), this._$currentSearchType = this._$searchWraperPanel.find(".nav-search-type-current"), this._$searchCancel = this._$searchWraperPanel.find(".nav-search-cancel"), this._$searchKey = this._$searchWraperPanel.find(".nav-search-key"), this._$searchClear = this._$searchWraperPanel.find(".nav-search-clear"), this._$searchForm = this._$searchWraperPanel.find(".nav-search-form"), this._$searchCurrentType = this._$searchWraperPanel.find(".nav-search-type-current"), this._$searchUrl = this._$searchWraperPanel.find(".nav-search-type-url"), this._$zxaskMainNav = this._$el.find("#zxask-main-nav"), this._$zxaskMask = this._$el.find(".zxask-mask"), this._$zxaskMaskHref = this._$el.find('[data-href="#zxask-main-nav"]'), this._$askIcon = this._$zxaskMaskHref.children().children(), this._$body = $(document.body), this._bindEvent()
		},
		_stopScroll: function() {
			this._$body.css({
				position: "fixed",
				width: "100%"
			})
		},
		_unlimitScroll: function() {
			this._$body.css({
				position: "static"
			})
		},
		_bindEvent: function() {
			var a = this,
				b = -1 !== navigator.userAgent.indexOf("Android"),
				c = b ? "click" : "tap";
			this._$dthdnav.click($.proxy(this._dthdnavClickHandle, this)), this._$dtHdNavWrap.find(".dt-hd-navs li").bind(c, function(a) {
				a.stopPropagation()
			}), this._$dtHdNavWrap.bind(c, $.proxy(this._dtHdNavWrapClickHandle, this)).bind("webkitAnimationStart", function() {
				a.inAnimation = !0
			}).bind("webkitAnimationEnd", function() {
				a.inAnimation = !1
			}), this._$dtHdNavWrap.find("a").bind(c, function() {
				return a.inAnimation || (location.href = this.href), !1
			}), this._$searchNav.click($.proxy(this._searchNavClickHandle, this)), this._$searchType.click($.proxy(this._searchTypeClickHandle, this)), this._$searchCancel.click($.proxy(this._searchCancelClickHandle, this)), this._$searchKey.bind("keyup input", $.proxy(this._searchKeyupHandle, this)).val(""), this._$searchClear.click($.proxy(this._searchClearClickHandle, this)), this._$searchWraperPanel.click($.proxy(this._searchWraperClickHandle, this)), this._$searchForm.click($.proxy(this._searchFormClickHandle, this)), this._$zxaskMaskHref.click(function(b) {
				b.preventDefault(), a._toggleZxaskMaiNav()
			}), this._$zxaskMask.click(function() {
				a._toggleZxaskMaiNav()
			})
		},
		_dthdnavClickHandle: function(a) {
			var b = $(a.currentTarget),
				c = b.hasClass("dt-hd-active"),
				d = this;
			d.inAnimation || (d.inAnimation = !0, b.toggleClass("dt-hd-active"), c ? this._hide() : this._show(), $(window).unbind("resize mousemove"), $(document).unbind("resize mousemove"))
		},
		_searchWraperClickHandle: function(a) {
			this._hideSearchTypes(), this._$searchKey.not(":focus") && this._$searchKey.focus(), a.stopPropagation()
		},
		_searchFormClickHandle: function(a) {
			this._hideSearchTypes(), this._$searchKey.not(":focus") && this._$searchKey.focus(), a.stopPropagation()
		},
		_show: function() {
			this._$dtHdNavWrap.removeClass("menuout").addClass("menuin"), this._stopScroll()
		},
		_hide: function() {
			var a = this;
			this._$dtHdNavWrap.removeClass("menuin").addClass("menuout").one("webkitAnimationEnd", function() {
				a._unlimitScroll(), a._$dtHdNavWrap.removeClass("menuout menuin"), a.inAnimation = !1
			})
		},
		_dtHdNavWrapClickHandle: function() {
			return this._$dthdnav.removeClass("dt-hd-active"), this._hide(), !1
		},
		_searchNavClickHandle: function(a) {
			return a.preventDefault(), a.stopPropagation(), this._showSearchPanel(), this._$searchKey.not(":focus") && this._$searchKey.focus(), !1
		},
		_showSearchPanel: function() {
			this._$searchWraperPanel.show()
		},
		_hideSearchPanel: function() {
			this._$searchWraperPanel.hide()
		},
		_searchTypeClickHandle: function(a) {
			var b = a.target,
				c = b.nodeName.toLowerCase(),
				d = $(b);
			this._toggleSearchTypes(), "li" == c && (this._$currentSearchType.text(d.text()), this._$searchUrl.val(d.data("url")), this._$searchKey.not(":focus") && this._$searchKey.focus()), a.stopPropagation()
		},
		_toggleSearchTypes: function() {
			this._$searchTypes.toggle()
		},
		_showSearchTypes: function() {
			this._$searchTypes.show()
		},
		_hideSearchTypes: function() {
			this._$searchTypes.hide()
		},
		_searchCancelClickHandle: function(a) {
			var b = a.currentTarget;
			a.stopPropagation(), $(b).hasClass("search-state-active") ? this._search($.trim(this._$searchKey.val())) : (this._hideSearchPanel(), this._hideSearchTypes())
		},
		_searchKeyupHandle: function(a) {
			var b = $.trim(this._$searchKey.val());
			a.stopPropagation(), b ? (this._$searchCancel.addClass("search-state-active").text("搜索"), this._showClearBtn()) : (this._hideClearBtn(), this._$searchCancel.removeClass("search-state-active").text("取消")), "13" == a.which && this._search(b)
		},
		_search: function(a) {
			var b = this._$searchUrl.val();
			b += encodeURIComponent(a), location.href = b
		},
		_showClearBtn: function() {
			this._$searchClear.show()
		},
		_hideClearBtn: function() {
			this._$searchClear.hide()
		},
		_searchClearClickHandle: function(a) {
			a.stopPropagation(), this._hideClearBtn(), this._$searchKey.val(""), this._$searchCancel.removeClass("search-state-active").text("取消"), this._$searchKey.not(":focus") && this._$searchKey.focus()
		},
		_toggleZxaskMaiNav: function() {
			this._$zxaskMainNav.toggle(), this._$zxaskMask.toggle(), this._$askIcon.hasClass("zxask-icon-down") ? (this._$askIcon.removeClass("zxask-icon-down").addClass("zxask-icon-up"), this._stopScroll()) : (this._$askIcon.removeClass("zxask-icon-up").addClass("zxask-icon-down"), this._unlimitScroll())
		}
	};
	a.init()
}), $(function() {
	$(".click-point").click(function(a) {
		var b = $(a.currentTarget),
			c = b.data("point");
		c && "undefined" != typeof clickStream && clickStream.getCvParams(c)
	}), $(".point_float").click(function(a) {
		var b = $(a.currentTarget),
			c = b.data("point");
		c && $('input[name="ptag"]').val(c)
	});
	var a, b = $(".city-update-left");
	0 < b.length && $.post("/index/CityCode", {
		_t: Math.random()
	}, function(c) {
		a = c, b.each(function() {
			var a = $.trim($(this).attr("href"));
			"" != a && $(this).attr("href", "/" + c + a)
		})
	})
}), $(document).ready(function() {
	0 !== $(".goIndex").length && $.ajax({
		url: "http://m.to8to.com/index/CityCode?" + Math.random(),
		cache: !1,
		type: "post",
		data: {
			__t: Math.random()
		},
		success: function(a) {
			a && $(".goIndex").attr("href", "http://m.to8to.com/" + a)
		}
	})
}), $(function(a) {
	function b(a) {
		this._$container = a, this._init()
	}
	b.prototype = {
		constructor: b,
		_init: function() {
			this._$curA = this._$container.find(".widget-pagination-current-page"), this._$pagesSelect = this._$container.find(".widget-pagination-pages"), this._regEvent()
		},
		_regEvent: function() {
			this._$pagesSelect.bind("change", a.proxy(this._pageChangeHandle, this))
		},
		_pageChangeHandle: function() {
			var a = this._$pagesSelect.find("option:selected");
			this._$curA.text(a.text()), location.href = a.val()
		}
	}, new b(a("[data-role=widget-pagination]")), a.fn.lazyload && a("img.lazy").lazyload({
		effect: "fadeIn",
		threshold: 300
	})
}), jQuery(function(a) {
	var b = {
		_cookieName: "article-detail-app-ad",
		init: function() {
			var b = this._cookieName;
			return this._$container = a("#detailPageAppDownlad"), 0 == this._$container.length ? null : void(a.cookie(b) || (this._$container.show(), this._$container.find(".close").bind("click touchstart", a.proxy(this.close, this)), a(".fix-nav").css("bottom", "50px")))
		},
		close: function() {
			return this._$container.hide(), a.cookie(this._cookieName, "hide", {
				expires: 1,
				path: "/"
			}), !1
		}
	};
	b.init(), window.msgalert = function() {
		var b, c = a('<div id="alertOveryLayer" style="position: fixed; left: 0px; top: 0px;   z-index: 99999; text-align: center;  bottom: 0px; right: 0px; background-color: rgba(0, 0, 0, 0.298039);"><div style="  position: absolute;display: -moz-box;  display: -webkit-box;  display: -webkit-flex;  display: -moz-flex;  display: -ms-flexbox;  display: -ms-flex;  display: flex;top: 30%;width: 100%;"><div class="alert-lay" style="/* display: table-cell; */vertical-align: middle;padding: 0 10%;-webkit-box-sizing: border-box;  -moz-box-sizing: border-box;  box-sizing: border-box;  display: block;  -webkit-box-flex: 1;  -moz-box-flex: 1;  -webkit-flex: 1 1 0;  -moz-flex: 1 1 0;  -ms-flex: 1 1 0;  flex: 1 1 0;  text-align: center;"><div class="overlay-bd" style="background-color: #fff;font-size: 14px;color: #333333;line-height: 24px;padding: 20px 15px;text-align: center;border-radius: 7px 7px 0 0;">asdf</div><div class="overlay-foot" style="border-top: 1px solid #e3e3e3;"><div class="overlay-btn overlay-btn-ok" style="background-color: #fff;cursor: pointer;font-size: 16px;color: #00a1ff;height: 42px;line-height: 42px;border-radius: 0 0 7px 7px;">好的</div></div></div></div></div>'),
			d = null;
		return c.appendTo(top.document.body), c.hide(), c.find(".overlay-btn-ok").click(function() {
				c.hide(), "function" == typeof d && d(), d = null, clearInterval(b)
			}),
			function(a, e) {
				c.find(".overlay-bd").html(a), c.show(), d = e, b = setInterval(function() {
					document.body.scrollIntoView()
				}, 50)
			}
	}()
});
//# sourceMappingURL=common.min.js.map