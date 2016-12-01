using System.Web.Mvc;
using YG.SC.Service.IService;
using System;
using YG.SC.Common;
using System.Web;
using YG.SC.DataAccess;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace YG.SC.OpenShop.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenShopController : Controller
    {
        //
        // GET: /OpenShop/
        private readonly IOpenShopService _iOpenShopService;
        private readonly IShopAdPositionService _iShopAdPositionService;
        private readonly ICustomerLogService _ICustomerLogService;


        public OpenShopController(IOpenShopService iOpenShopService, IShopAdPositionService shopPositionService, ICustomerLogService ICustomerLogService)
        {
            _iOpenShopService = iOpenShopService;
            _iShopAdPositionService = shopPositionService;
            _ICustomerLogService = ICustomerLogService;
        }

        protected override void Dispose(bool disposing)
        {
            this._iOpenShopService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Index(int pg = 1)
        {
            var model = this._iOpenShopService.GetEntitsByName(pg, 0, "");
            //DateTime Date = DateTime.Now;
            //var ad_jiaju = _iShopAdPositionService.SearchAdPosition((int)PositionCode.开店帮家具帮, (int)EnumProjectType.装修, Date);
            //var ad_rongzi = _iShopAdPositionService.SearchAdPosition((int)PositionCode.开店帮融资帮, (int)EnumProjectType.装修, Date);
            //var ad_tuiguang = _iShopAdPositionService.SearchAdPosition((int)PositionCode.开店帮推广帮, (int)EnumProjectType.装修, Date);
            //var ad_zhuangxiug = _iShopAdPositionService.SearchAdPosition((int)PositionCode.开店帮装修帮, (int)EnumProjectType.装修, Date);

            return View(model);
        }

        public ActionResult OpenShop(int openShopId = 1)
        {
            var model = this._iOpenShopService.GetById(openShopId);
            int type = 0;
            foreach (OpenShopAttributeValues attr in model.OpenShopAttributeValues)
            {
                if (attr.ShopAttributeValues.ValueStr == "装修帮")
                {
                    type = 3;
                }
                else if (attr.ShopAttributeValues.ValueStr == "家具帮")
                {
                    type = 4;
                }
                else if (attr.ShopAttributeValues.ValueStr == "融资帮")
                {
                    type = 5;
                }
                else if (attr.ShopAttributeValues.ValueStr == "推广帮")
                {
                    type = 6;
                }
            }
            CustomerLog log = new CustomerLog()
            {
                Customer = YG.SC.OpenShop.UserContext.Current.Id,
                addtime = DateTime.Now,
                Targetsubject = type,
                ip = Request.UserHostAddress,
                ProjectId = model.Id
            };
            this._ICustomerLogService.Insert(log);
            return View(model);
        }

        /// <summary>
        /// 获取图片宽高别设置img宽高和空余
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetWH(string path, int width, int height)
        {
            string strStyle = "";

            Image imageFrom = null;

            string pathStr = System.Web.HttpContext.Current.Server.MapPath(path);

            //检查图片路径是否正确
            if (System.IO.File.Exists(pathStr))
            {
                imageFrom = Image.FromFile(pathStr);
            }
            if (imageFrom == null)
            {
                strStyle = "width:" + width + "px;height:" + height + "px;";
                return strStyle;
            }

            //原图宽高
            int imageWidth = imageFrom.Width;
            int imageHeight = imageFrom.Height;

            if (imageWidth * height > imageHeight * width)
            {
                strStyle = "width:" + width + "px;height:" + imageHeight * width / imageWidth + "px;margin-top:" + (height - imageHeight * width / imageWidth) / 2 + "px;";
            }
            else
            {
                strStyle = "width:" + imageWidth * height / imageHeight + "px;height:" + height + "px;margin-left:" + (width - imageWidth * height / imageHeight) / 2 + "px;";
            }

            return strStyle;
        }
    }
}
