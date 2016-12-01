
namespace YG.SC.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// 类名称：ZoomAndCompressImageUtility
    /// 命名空间：YG.SC.Common
    /// 类功能：图片缩放压缩帮助类
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/11/6 18:27
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class ZoomAndCompressImageUtility
    {
        /// <summary>
        /// 原样保存图片不做任何处理
        /// </summary>
        /// <param name="sourceBitmap">The sourceBitmap</param>
        /// <param name="savePath">The saveThePath</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 13:53
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool SaveTheSameImage(Bitmap sourceBitmap, string savePath)
        {
            try
            {
                sourceBitmap.Save(savePath);
                sourceBitmap.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩图片 不限制图片大小
        /// </summary>
        /// <param name="sourcePath">原始图片路径</param>
        /// <param name="savePath">保存图片路径</param>
        /// <param name="compress">压缩比：取值范围 1 - 100</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool SaveTheCompressionImage(string sourcePath, string savePath, int compress)
        {
            var sourceImg = Image.FromFile(sourcePath);
            try
            {
                var templateImage = new Bitmap(sourceImg.Width, sourceImg.Height);
                var templateG = Graphics.FromImage(templateImage);
                templateG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                templateG.SmoothingMode = SmoothingMode.HighSpeed;
                templateG.Clear(Color.White);
                templateG.DrawImage(sourceImg, new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), new Rectangle(0, 0, sourceImg.Width, sourceImg.Height), GraphicsUnit.Pixel);
                templateG.Dispose();

                var qualityParam = new EncoderParameter(Encoder.Quality, compress);
                var jpegCodec = GetEncoderInfo("image/jpeg");
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                sourceImg.Dispose();
                templateImage.Save(savePath, jpegCodec, encoderParams);
                templateImage.Dispose();
                return true;
            }
            catch
            {
                sourceImg.Dispose();
                return false;
            }
        }

        /// <summary>
        /// 压缩图片 限制图片大小
        /// </summary>
        /// <param name="sourcePath">原始图片路径</param>
        /// <param name="savePath">保存图片路径</param>
        /// <param name="maxSize">图片最大尺寸</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:22
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool SaveTheCompressionImageMaxSize(string sourcePath, string savePath, int maxSize)
        {
            var compress = 100;

            var sourceSaveResuelt = SaveTheCompressionImage(sourcePath, savePath, compress);
            if (!sourceSaveResuelt) return false;

            int maxSizeByte = maxSize * 1024;
            var size = new FileInfo(savePath).Length;
            if (size <= maxSizeByte) return true;

            while (size > maxSizeByte && compress > 10)
            {
                try
                {
                    compress -= 5;
                    SaveTheCompressionImage(sourcePath, savePath, compress);
                    size = new FileInfo(savePath).Length;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 缩放图片 到指定尺寸 不限制图片大小 存在裁剪 指定压缩比
        /// </summary>
        /// <param name="sourcePath">原始图片路径</param>
        /// <param name="savePath">保存图片路径</param>
        /// <param name="targetWidth">目标图片宽度</param>
        /// <param name="targetHeigth">目标图片高度</param>
        /// <param name="compress">压缩比1-100</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool SaveTheZoomImageAndCompression(string sourcePath, string savePath, int targetWidth, int targetHeigth, int compress = -1)
        {
            var sourceImage = Image.FromFile(sourcePath);

            try
            {
                if (compress == -1) compress = 100;

                //模版的宽高比例
                var templateRate = (double) targetWidth/targetHeigth;
                //原图片的宽高比例
                var initRate = (double) sourceImage.Width/sourceImage.Height;
                if (templateRate == initRate)
                {
                    Image templateImage = new Bitmap(targetWidth, targetHeigth);
                    var templateG = Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    templateG.SmoothingMode = SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(sourceImage, new Rectangle(0, 0, targetWidth, targetHeigth),
                        new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                    templateG.Dispose();

                    var qualityParam = new EncoderParameter(Encoder.Quality, compress);
                    var jpegCodec = GetEncoderInfo("image/jpeg");
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = qualityParam;
                    sourceImage.Dispose();

                    templateImage.Save(savePath, jpegCodec, encoderParams);
                    templateImage.Dispose();
                    return true;
                }
                else
                {
                    //裁剪对象
                    Image pickedImage;
                    Graphics pickedG;

                    //定位
                    var fromR = new Rectangle(0, 0, 0, 0); //原图裁剪定位
                    var toR = new Rectangle(0, 0, 0, 0); //目标定位

                    //宽为标准进行裁剪
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化
                        pickedImage = new Bitmap(sourceImage.Width, (int) Math.Floor(sourceImage.Width/templateRate));
                        pickedG = Graphics.FromImage(pickedImage);

                        //裁剪源定位
                        fromR.X = 0;
                        fromR.Y = (int) Math.Floor((sourceImage.Height - sourceImage.Width/templateRate)/2);
                        fromR.Width = sourceImage.Width;
                        fromR.Height = (int) Math.Floor(sourceImage.Width/templateRate);

                        //裁剪目标定位
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = sourceImage.Width;
                        toR.Height = (int) Math.Floor(sourceImage.Width/templateRate);
                    }
                        //高为标准进行裁剪
                    else
                    {
                        pickedImage = new Bitmap((int) Math.Floor(sourceImage.Height*templateRate), sourceImage.Height);
                        pickedG = Graphics.FromImage(pickedImage);

                        fromR.X = (int) Math.Floor((sourceImage.Width - sourceImage.Height*templateRate)/2);
                        fromR.Y = 0;
                        fromR.Width = (int) Math.Floor(sourceImage.Height*templateRate);
                        fromR.Height = sourceImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int) Math.Floor(sourceImage.Height*templateRate);
                        toR.Height = sourceImage.Height;
                    }

                    //设置质量
                    pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = SmoothingMode.HighQuality;

                    //裁剪
                    pickedG.DrawImage(sourceImage, toR, fromR, GraphicsUnit.Pixel);
                    pickedG.Dispose();

                    //按模版大小生成最终图片
                    var templateImage = new Bitmap(targetWidth, targetHeigth);
                    var templateG = Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    templateG.SmoothingMode = SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new Rectangle(0, 0, targetWidth, targetHeigth),
                        new Rectangle(0, 0, pickedImage.Width, pickedImage.Height), GraphicsUnit.Pixel);
                    templateG.Dispose();
                    pickedImage.Dispose();

                    var qualityParam = new EncoderParameter(Encoder.Quality, compress);
                    var jpegCodec = GetEncoderInfo("image/jpeg");
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = qualityParam;
                    sourceImage.Dispose();

                    templateImage.Save(savePath, jpegCodec, encoderParams);
                    templateImage.Dispose();
                    return true;
                }
            }
            catch
            {
                sourceImage.Dispose();
                return false;
            }
        }


        /// <summary>
        /// 缩放图片 到指定尺寸 限制图片大小
        /// </summary>
        /// <param name="sourcePath">原始图片路径</param>
        /// <param name="savePath">保存图片路径</param>
        /// <param name="targetWidth">目标图片宽度</param>
        /// <param name="targetHeigth">目标图片高度</param>
        /// <param name="maxSize">最大图片字节</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 15:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool SaveTheZoomImageMaxSize(string sourcePath, string savePath, int targetWidth, int targetHeigth, int maxSize)
        {
            var sourceResult = SaveTheZoomImageAndCompression(sourcePath, savePath, targetWidth, targetHeigth, 100);
            return sourceResult && SaveTheCompressionImageMaxSize(savePath, savePath, maxSize);
        }

        /// <summary>
        /// Gets the encoder information.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <returns>
        /// ImageCodecInfo
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 16:14
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.FirstOrDefault(t => t.MimeType == mimeType);
        }

        ///// <summary>
        ///// 图片缩放压缩适配
        ///// </summary>
        ///// <param name="sourcePath">The sourcePath</param>
        ///// <param name="savePath">The savePath</param>
        ///// <param name="targetWidth">Width of the target.</param>
        ///// <param name="targetHeight">Height of the target.</param>
        ///// <param name="maxSize">The maxSize</param>
        ///// <param name="compress">The compress</param>
        ///// <returns>
        ///// Boolean
        ///// </returns>
        ///// 创建者：孟祺宙
        ///// 创建日期：2014/11/6 14:02
        ///// 修改者：
        ///// 修改时间：
        ///// ----------------------------------------------------------------------------------------
        //private static bool ZoomImageAndCompressionAdapter(string sourcePath, string savePath, int targetWidth = -1, int targetHeight = -1, int maxSize = -1, int compress = -1)
        //{
        //    //原图存放
        //    if (targetWidth == -1 && targetHeight == -1 && maxSize == -1 && compress == -1) return SaveTheSameImage(new Bitmap(sourcePath), savePath);
        //    //不缩放 压缩图片 不限制大小 指定压缩比
        //    if (targetWidth == -1 && targetHeight == -1 && maxSize == -1 && compress != -1) return SaveTheCompressionImage(sourcePath, savePath, compress);
        //    //不缩放 压缩图片 限制图片大小 不需要指定压缩比
        //    if (targetWidth == -1 && targetHeight == -1 && maxSize != -1 && compress == -1) return SaveTheCompressionImageMaxSize(sourcePath, savePath, maxSize);
        //    //缩放图片，可能会存在裁剪 不限制大小，可以选择是否指定压缩比
        //    if (targetWidth != -1 && targetHeight != -1 && maxSize == -1) return SaveTheZoomImageAndCompression(sourcePath, savePath, targetWidth, targetHeight, compress);

        //    //缩放图片 到指定尺寸 限制图片大小
        //    return SaveTheZoomImageMaxSize(sourcePath, savePath, targetWidth, targetHeight, maxSize);
        //}
    }

}
