using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LitJson;
using YG.SC.Common;
using YG.SC.DataAccess;
using YG.SC.Service.IService;
using YG.SC.Common.AttributeExtention;
using System.Web.Helpers;

namespace YG.SC.WebUI.Controllers
{
	public class OpenShopController : WebBaseController
	{
		private static int _openShopId;
		private readonly IOpenShopService _iOpenShopService;
		private readonly IShopAttributesService _iShopAttributesService;
		private readonly ICustomerService _iCustomerService;
		public OpenShopController(IOpenShopService iOpenShopService, IShopAttributesService iShopAttributesServiceService, ICustomerService iCustomerService)
		{
			_iShopAttributesService = iShopAttributesServiceService;
			_iOpenShopService = iOpenShopService;
			_iCustomerService = iCustomerService;
		}
		protected override void Dispose(bool disposing)
		{
			this._iOpenShopService.Dispose();
			_iShopAttributesService.Dispose();
			base.Dispose(disposing);
		}
		//
		// GET: /OpenShop/
		public ActionResult Index(int pg = 1, int Type = 0, string txtName = "")
		{
			var model = this._iOpenShopService.GetEntitsByName(pg, Type, txtName);
			return View(model);
		}

		/// <summary>
		/// 开店帮类型
		/// </summary>
		int OpenShopAttrId = 20;
		[HttpGet]
		public ActionResult Add(OpenShop openShop)
		{
			var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
			var typelist = (from m in attrsTypes
							select new SelectListItem
							{
								Text = m.ValueStr,
								Value = m.AttributeId.ToString()
							}).ToList();
			ViewBag.shopType = typelist;


			List<CodeDescription> codes = new List<CodeDescription>();
			var shopAttributes = _iShopAttributesService.GetAll();
			if (shopAttributes != null)
			{
				foreach (var item in shopAttributes)
				{
					var attributeValues = _iShopAttributesService.GetListByAttributeId(item.Id);
					if (attributeValues != null)
					{
						foreach (var tmp in attributeValues)
						{
							codes.Add(new CodeDescription(tmp.Id.ToString(), tmp.ValueStr, item.AttributeName));
						}
					}
				}
			}
			CodeManager.codes = codes.ToArray();

			return View();
		}
		[HttpPost]
		[ActionName("Add")]
		public ActionResult AddPost(OpenShop openShop)
		{
			var fileLogoName = UploadImgUtility.UploadImage(Request.Files["Logo"], Server.MapPath(CommonContorllers.FileUploadOpenShopLogoImgPath));
			openShop.Logo = CommonContorllers.FileUploadLogoImgPath + fileLogoName;
			openShop.CreateTime = DateTime.Now;
			openShop.Recsts = 1;
			this._iOpenShopService.Insert(openShop);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult Edit(int id = 0)
		{
			var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
			var typelist = (from m in attrsTypes
							select new SelectListItem
							{
								Text = m.ValueStr,
								Value = m.Id.ToString()
							}).ToList();

			var entity = this._iOpenShopService.GetById(id);
			var typeItem = typelist.Where(m => m.Value == entity.Type.ToString()).FirstOrDefault();
			if (typeItem != null)
			{
				typeItem.Selected = true;
			}
			ViewBag.shopType = typelist;
			return View(entity);
		}


		[HttpPost]
		[ActionName("Edit")]
		[ValidateInput(false)]
		public ActionResult EditPost(OpenShop shopBrand)
		{
			var entity = this._iOpenShopService.GetById(shopBrand.Id);

			var fileLogoName = UploadImgUtility.UploadImage(Request.Files["LogoImg"], Server.MapPath(CommonContorllers.FileUploadOpenShopLogoImgPath));
			if (!string.IsNullOrEmpty(fileLogoName))
			{
				entity.Logo = CommonContorllers.FileUploadOpenShopLogoImgPath + fileLogoName;
			}
			entity.Name = shopBrand.Name;
			entity.Rmark = shopBrand.Rmark;
			entity.Url = shopBrand.Url;
			entity.VidoUrl = shopBrand.VidoUrl;
			this._iOpenShopService.Update(entity);

			return RedirectToAction("Index");
		}
		public void Delete(int id, string state)
		{
			var entity = this._iOpenShopService.GetById(id);

			var recsts = state == "delete" ? -1 : 0;
			entity.Recsts = recsts;
			this._iOpenShopService.Update(entity);
			TempData["SelRecsts"] = entity.Recsts;
		}

		public void Update(int id)
		{
			var entity = this._iOpenShopService.GetById(id);
			if (entity != null)
			{
				entity.Recsts = 1;
				entity.CreateTime = DateTime.Now;
				this._iOpenShopService.Update(entity);
				TempData["SelRecsts"] = entity.Recsts;
			}

		}


		public ActionResult Item(int id = 0)
		{
			var attrsTypes = this._iShopAttributesService.GetListByAttributeId(OpenShopAttrId);
			var typelist = (from m in attrsTypes
							select new SelectListItem
							{
								Text = m.ValueStr,
								Value = m.Id.ToString()
							}).ToList();

			var entity = this._iOpenShopService.GetById(id);
			var typeItem = typelist.Where(m => m.Value == entity.Type.ToString()).FirstOrDefault();
			if (typeItem != null)
			{
				typeItem.Selected = true;
			}
			ViewBag.shopType = typelist;
			return View(entity);
		}
		[HttpPost]
		[ActionName("Item")]
		public ActionResult Itemaudit(OpenShop moudel)
		{
			var openshop = this._iOpenShopService.GetById(moudel.Id);
			openshop.isaudit = 1;
			this._iOpenShopService.Update(openshop);
            ViewBag.msg = "审核通过";
			return View(openshop);
		}

		[HttpPost]
		public JsonResult GetInfo(int id)
		{
			string contact = string.Empty;
			string mobile = string.Empty;
			OpenShop model = _iOpenShopService.GetById(id);
			if (model == null)
			{
				return null;
			}
			Customer c = _iCustomerService.GetEntitsByGroupAndCompanyId(CommonEnum.GroupOfCustomer.OpenShop, model.Id);
			if (c == null)
			{
				return null;
			}
			return Json(new { Contact = c.Name, Mobile = c.Mobile });
		}

		#region//开店帮图片
		public ActionResult OpenShopPhoto(int pg = 1, int Id = 0, string txtName = "")
		{
			_openShopId = Id;
			var model = this._iOpenShopService.GetPhotoByBrandId(pg, Id, txtName);
			return View(model);
		}

		[HttpGet]
		public ActionResult PhotoAdd(ShopBrandPhoto shopBrand)
		{
			return View();
		}


		[HttpPost]
		[ActionName("PhotoAdd")]
		public ActionResult PhotoAddPost(OpenShopPhoto Photo)
		{

			var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
			if (!string.IsNullOrEmpty(fileName))
			{
				Photo.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
				Photo.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
				Photo.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
				Photo.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
			}
			Photo.OpenShopId = _openShopId;
			Photo.Recsts = 1;
			this._iOpenShopService.InsertPhoto(Photo);

			return Redirect("/OpenShop/OpenShopPhoto?Id=" + _openShopId);
		}

		/// <summary>
		/// The method will 
		/// </summary>
		/// <param name="id">The id</param>
		/// <returns>
		/// The ActionResult
		/// </returns>
		/// 创建者：孟祺宙
		/// 创建日期：2014/10/22 16:09
		/// 修改者：
		/// 修改时间：
		/// ----------------------------------------------------------------------------------------
		[HttpGet]
		public ActionResult PhotoEdit(int id = 0)
		{
			var entity = this._iOpenShopService.GetPhotoById(id);
			return View(entity);
		}

		/// <summary>
		/// Edits the post.
		/// </summary>
		/// <param name="shopBrand"></param>
		/// <returns>
		/// ActionResult
		/// </returns>
		/// 创建者：孟祺宙
		/// 创建日期：2014/10/22 16:09
		/// 修改者：
		/// 修改时间：
		/// ----------------------------------------------------------------------------------------
		[HttpPost]
		[ActionName("PhotoEdit")]
		public ActionResult PhotoEditPost(OpenShopPhoto Photo)
		{

			var entity = this._iOpenShopService.GetPhotoById(Photo.Id);
			var fileName = UploadImgUtility.UploadBrangImg(Request.Files["OpenShopPhoto"], Server.MapPath(CommonContorllers.FileUploadOpenShopImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSmallImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopSquareImgPath), Server.MapPath(CommonContorllers.FileUploadOpenShopRectangleImgPath));
			if (!string.IsNullOrEmpty(fileName))
			{
				entity.Photo = CommonContorllers.FileUploadOpenShopImgPath + fileName;
				entity.PhotoSmall = CommonContorllers.FileUploadOpenShopSmallImgPath + fileName;
				entity.PhotoSquare = CommonContorllers.FileUploadOpenShopSquareImgPath + fileName;
				entity.PhotoRectangle = CommonContorllers.FileUploadOpenShopRectangleImgPath + fileName;
			}
			this._iOpenShopService.UpdatePhoto(entity);

			return Redirect("/OpenShop/OpenShopPhoto?Id=" + _openShopId);
		}

		public void PhotoDelete(int id, string state)
		{
			var entity = this._iOpenShopService.GetPhotoById(id);

			var recsts = state == "delete" ? -1 : 0;
			entity.Recsts = recsts;
			this._iOpenShopService.UpdatePhoto(entity);
			TempData["SelRecsts"] = entity.Recsts;
		}

		public void PhotoUpdate(int id)
		{
			var entity = this._iOpenShopService.GetPhotoById(id);
			if (entity != null)
			{
				entity.Recsts = 1;
				this._iOpenShopService.UpdatePhoto(entity);
				TempData["SelRecsts"] = entity.Recsts;
			}

		}
		#endregion//

		#region
		public ActionResult OpenShopProject(int pg = 1, string txtSearchName = "", string selRecsts = "")
		{
			var model = this._iOpenShopService.GetEntitsByName(pg, txtSearchName, selRecsts);
			return View(model);
		}
		public void OpenProjectDelete(int id, string state)
		{
			var entity = this._iOpenShopService.GetOpenShopProjectById(id);

			var recsts = ((state == "delete") ? -1 : 0);
			entity.Recsts = recsts;
			this._iOpenShopService.Update(entity);
			//TempData["SelRecsts"] = entity.Recsts;
		}
		public void OpenProjectUpdate(int id)
		{
			var entity = this._iOpenShopService.GetOpenShopProjectById(id);
			if (entity != null)
			{
				entity.Recsts = 1;
				this._iOpenShopService.Update(entity);
				// TempData["SelRecsts"] = entity.Recsts;
			}

		}
		[HttpGet]
		public ActionResult OpenProjectAdd(OpenShopShopProject osp)
		{
			ViewBag.OpenShopLsit = this._iOpenShopService.GetAllOpenShop();
			ViewBag.OpenShopProjectLsit = this._iOpenShopService.GetAllShopProject();
			return View();
		}


		/// <summary>
		/// Adds the post.
		/// </summary>
		/// <param name="sctAdPicture">The sctAdPicture</param>
		/// <param name="selSkuGoods">The selSkuGoods</param>
		/// <returns>
		/// ActionResult
		/// </returns>
		/// 创建者：孟祺宙
		/// 创建日期：2014/10/22 16:09
		/// 修改者：
		/// 修改时间：
		/// ----------------------------------------------------------------------------------------
		[HttpPost]
		[ActionName("OpenProjectAdd")]
		public ActionResult OpenProjectAddPost(OpenShopShopProject osp)
		{
			osp.Recsts = 1;
			this._iOpenShopService.Insert(osp);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult OpenProjectEdit(int id = 0)
		{
			var entity = this._iOpenShopService.GetOpenShopProjectById(id);
			ViewBag.OpenShopLsit = this._iOpenShopService.GetAllOpenShop();
			ViewBag.OpenShopProjectLsit = this._iOpenShopService.GetAllShopProject();
			return View(entity);
		}

		/// <summary>
		/// Edits the post.
		/// </summary>
		/// <param name="sctAdPicture">The sctAdPicture</param>
		/// <returns>
		/// ActionResult
		/// </returns>
		/// 创建者：孟祺宙
		/// 创建日期：2014/10/22 16:09
		/// 修改者：
		/// 修改时间：
		/// ----------------------------------------------------------------------------------------
		[HttpPost]
		[ActionName("OpenProjectEdit")]
		public ActionResult OpenProjectEditPost(OpenShopShopProject osp)
		{

			var entity = this._iOpenShopService.GetOpenShopProjectById(osp.Id);

			entity.OpenShopId = osp.OpenShopId;
			entity.ShopProjectId = osp.ShopProjectId;
			entity.Recsts = osp.Recsts;
			this._iOpenShopService.Update(entity);

			return RedirectToAction("Index");
		}
		#endregion

		#region//富文本编辑器
		//public ActionResult EditRmark(int id)
		//{
		//    var entity = this._iOpenShopService.GetById(id);

		//    return View(entity);
		//}

		//[ValidateInput(false)]
		//public ActionResult Process(int id, string data)
		//{
		//    var entity = this._iOpenShopService.GetById(id);
		//    entity.Rmark = data;
		//    this._iOpenShopService.Update(entity);
		//    return RedirectToAction("Index");
		//}

		public void ProcessFile()
		{
			String aspxUrl = ""; // Request.Path.Substring(0,  Request.Path.LastIndexOf("/") + 1);

			//文件保存目录路径
			String savePath = "/attached/";

			//文件保存目录URL
			String saveUrl = aspxUrl + "/attached/";

			//定义允许上传的文件扩展名
			Hashtable extTable = new Hashtable();
			extTable.Add("image", "gif,jpg,jpeg,png,bmp");
			extTable.Add("flash", "swf,flv");
			extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
			extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

			//最大文件大小
			int maxSize = 1000000;
			this.context = context;

			var imgFile = Request.Files["imgFile"];
			if (imgFile == null)
			{
				showError("请选择文件。");
			}

			String dirPath = Server.MapPath(savePath);
			if (!Directory.Exists(dirPath))
			{
				showError("上传目录不存在。");
			}

			String dirName = Request.QueryString["dir"];
			if (String.IsNullOrEmpty(dirName))
			{
				dirName = "image";
			}
			if (!extTable.ContainsKey(dirName))
			{
				showError("目录名不正确。");
			}

			String fileName = imgFile.FileName;
			String fileExt = Path.GetExtension(fileName).ToLower();

			if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
			{
				showError("上传文件大小超过限制。");
			}

			if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
			{
				showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
			}

			//创建文件夹
			dirPath += dirName + "/";
			saveUrl += dirName + "/";
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}
			String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
			dirPath += ymd + "/";
			saveUrl += ymd + "/";
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
			String filePath = dirPath + newFileName;

			imgFile.SaveAs(filePath);

			String fileUrl = saveUrl + newFileName;

			Hashtable hash = new Hashtable();
			hash["error"] = 0;
			hash["url"] = fileUrl;
			Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			Response.Write(JsonMapper.ToJson(hash));
			Response.End();

		}


		public HttpContext context;

		public void ProcessFileManger()
		{
			//String aspxUrl =  Request.Path.Substring(0,  Request.Path.LastIndexOf("/") + 1);

			//文件保存目录路径
			String savePath = "/attached/";

			//文件保存目录URL
			String saveUrl = "/attached/";

			//定义允许上传的文件扩展名
			Hashtable extTable = new Hashtable();
			extTable.Add("image", "gif,jpg,jpeg,png,bmp");
			extTable.Add("flash", "swf,flv");
			extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
			extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

			//最大文件大小
			int maxSize = 1000000;
			this.context = context;

			var imgFile = Request.Files["imgFile"];
			if (imgFile == null)
			{
				showError("请选择文件。");
			}

			String dirPath = Server.MapPath(savePath);
			if (!Directory.Exists(dirPath))
			{
				showError("上传目录不存在。");
			}

			String dirName = Request.QueryString["dir"];
			if (String.IsNullOrEmpty(dirName))
			{
				dirName = "image";
			}
			if (!extTable.ContainsKey(dirName))
			{
				showError("目录名不正确。");
			}

			String fileName = imgFile.FileName;
			String fileExt = Path.GetExtension(fileName).ToLower();

			if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
			{
				showError("上传文件大小超过限制。");
			}

			if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
			{
				showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
			}

			//创建文件夹
			dirPath += dirName + "/";
			saveUrl += dirName + "/";
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}
			String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
			dirPath += ymd + "/";
			saveUrl += ymd + "/";
			if (!Directory.Exists(dirPath))
			{
				Directory.CreateDirectory(dirPath);
			}

			String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
			String filePath = dirPath + newFileName;

			imgFile.SaveAs(filePath);

			String fileUrl = saveUrl + newFileName;

			Hashtable hash = new Hashtable();
			hash["error"] = 0;
			hash["url"] = fileUrl;
			Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			Response.Write(JsonMapper.ToJson(hash));
			Response.End();
		}

		private void showError(string message)
		{
			Hashtable hash = new Hashtable();
			hash["error"] = 1;
			hash["message"] = message;
			Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			Response.Write(JsonMapper.ToJson(hash));
			Response.End();
		}

		#endregion//富文本编辑器
	}
}
