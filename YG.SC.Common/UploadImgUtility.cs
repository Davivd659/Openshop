
namespace YG.SC.Common
{
    using System;
    using System.IO;
    using System.Web;

    /// <summary>
    /// 类名称：UploadImgUtility
    /// 命名空间：YG.SC.Common
    /// 类功能：上传图片
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/17 11:16
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class UploadImgUtility
    {

        /// <summary>
        /// 上传Banner图片
        /// </summary>
        /// <param name="httpPostedFile">The httpPostedFile</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="sourceTempPath">The sourceTempPath</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>
        /// String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/22 16:00
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UpLoadBannerImage(HttpPostedFileBase httpPostedFile, string savePath, string sourceTempPath, int width = 320, int height = 178)
        {
            if (httpPostedFile == null || string.IsNullOrEmpty(httpPostedFile.FileName)) return string.Empty;

            var sourceFileName = httpPostedFile.FileName;
            var targetFileName = DateTime.Now.ToString("yyyyMMddmmssfff");

            var suffix = sourceFileName.Substring(sourceFileName.LastIndexOf(".", StringComparison.Ordinal));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var sourceSaveFullTempPath = sourceTempPath  + targetFileName + suffix;
            httpPostedFile.SaveAs(sourceSaveFullTempPath);

            var saveFullFilePath = savePath + targetFileName + suffix;
            ZoomAndCompressImageUtility.SaveTheZoomImageAndCompression(sourceSaveFullTempPath, saveFullFilePath, width, height, 95);

            return targetFileName + suffix;
        }



        /// <summary>
        /// 生成略缩图.
        /// </summary>
        /// <param name="imagePath">The imagePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// 创建者：边亮
        /// 创建日期：2014/10/14 17:12
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static void UpLoadThumbnail(string imagePath, string savePath, int width = 240, int height = 200)
        {
            var imageName = imagePath.Split('\\')[imagePath.Split('\\').Length - 1];
            var strBitMapPath = savePath + imageName;

            ZoomAndCompressImageUtility.SaveTheZoomImageMaxSize(imagePath, strBitMapPath, width, height, 20);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="httpPostedFile">The httpPostedFile</param>
        /// <param name="savePath">保存的目录</param>
        /// <param name="sourceTempPath">原始图片保存目录</param>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>
        /// The String
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/17 11:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static string UploadGoodsImg(HttpPostedFileBase httpPostedFile, string savePath, string sourceTempPath, int width = 640, int height = 534)
        {
            if (httpPostedFile == null || string.IsNullOrEmpty(httpPostedFile.FileName)) return string.Empty;

            var sourceFileName = httpPostedFile.FileName;
            var targetFileName = DateTime.Now.ToString("yyyyMMddmmssfff");

            var suffix = sourceFileName.Substring(sourceFileName.LastIndexOf(".", StringComparison.Ordinal));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var sourceSaveFullTempPath = sourceTempPath +  targetFileName + suffix;
            var saveFullFilePath = savePath + targetFileName + suffix;

            httpPostedFile.SaveAs(sourceSaveFullTempPath);

            ZoomAndCompressImageUtility.SaveTheZoomImageAndCompression(sourceSaveFullTempPath, saveFullFilePath, width, height, 80);

            return targetFileName + suffix;
        }

   /// <summary>
   /// 上传图片
   /// </summary>
   /// <param name="httpPostedFile"></param>
   /// <param name="savePath"></param>
   /// <returns></returns>
        public static string UploadImage(HttpPostedFileBase httpPostedFile, string savePath)
        {
            if (httpPostedFile == null || string.IsNullOrEmpty(httpPostedFile.FileName)) return string.Empty;

            var sourceFileName = httpPostedFile.FileName;
            var targetFileName = DateTime.Now.ToString("yyyyMMddmmssfff");

            var suffix = sourceFileName.Substring(sourceFileName.LastIndexOf(".", StringComparison.Ordinal));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var saveFullFilePath = savePath + targetFileName + suffix;

            httpPostedFile.SaveAs(saveFullFilePath);

            return targetFileName + suffix;
        }
        /// <summary>
        /// 上传品牌图片
        /// </summary>
        /// <param name="httpPostedFile"></param>
        /// <param name="savePath"></param>
        /// <param name="sourceTempPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string UploadBrangImg(HttpPostedFileBase httpPostedFile, string savePath, string saveSamllPath, string saveSquarePath, string saveRectanglePath, int width = 640, int height = 534)
        {
            if (httpPostedFile == null || string.IsNullOrEmpty(httpPostedFile.FileName)) return string.Empty;

            var sourceFileName = httpPostedFile.FileName;
            var targetFileName = DateTime.Now.ToString("yyyyMMddmmssfff");

            var suffix = sourceFileName.Substring(sourceFileName.LastIndexOf(".", StringComparison.Ordinal));
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var saveFullFilePath = savePath + targetFileName + suffix;

            if (!Directory.Exists(saveSamllPath))
            {
                Directory.CreateDirectory(saveSamllPath);
            }
            if (!Directory.Exists(saveSquarePath))
            {
                Directory.CreateDirectory(saveSquarePath);
            }
            if (!Directory.Exists(saveRectanglePath))
            {
                Directory.CreateDirectory(saveRectanglePath);
            }
            var saveSmallFilePath = saveSamllPath +  targetFileName + suffix;
            var saveSquareFilePath = saveSquarePath + targetFileName + suffix;
            var saveRectangleFilePath = saveRectanglePath + targetFileName + suffix;

            httpPostedFile.SaveAs(saveFullFilePath);

            ZoomAndCompressImageUtility.SaveTheZoomImageAndCompression(saveFullFilePath, saveSmallFilePath, 585, 416, 80);
            ZoomAndCompressImageUtility.SaveTheZoomImageAndCompression(saveFullFilePath, saveSquareFilePath, 204, 204, 80);
            ZoomAndCompressImageUtility.SaveTheZoomImageAndCompression(saveFullFilePath, saveRectangleFilePath, 304, 204, 80);

            return targetFileName + suffix;
        }
    }
}
