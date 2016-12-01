
namespace YG.SC.Common.Test
{
    using NUnit.Framework;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;


    [TestFixture]
    public class ZoomImageClass_Test2
    {
        private const string SourcePath3 = @"c:\ZoomImage\source3.png";

        [Test]
        public void SaveTheSameImage_Test()
        {
            SaveTheSameImage(new Bitmap(SourcePath3), @"c:\ZoomImage\_Temp\1.jpg");
            SaveTheCompressionImageMaxSize(@"c:\ZoomImage\_Temp\1.jpg", @"c:\ZoomImage\_Temp\1.jpg", 4);
            Console.WriteLine("success");
        }

        [Test]
        public void SaveTheCompressionImage_Test()
        {
            SaveTheCompressionImage(@"c:\ZoomImage\_Temp\t.jpg", @"c:\ZoomImage\_Temp\2.jpg", 50);

            Console.WriteLine("success");
        }

        [Test]
        public void SaveTheCompressionImageMaxSize_Test()
        {
            SaveTheCompressionImageMaxSize(@"c:\ZoomImage\_Temp\t.jpg", @"c:\ZoomImage\_Temp\3.jpg", 4);
            Console.WriteLine("SaveTheCompressionImageMaxSize");
        }

        [Test]
        public void ZoomImageAndCompressionAdapter_Test_1()
        {
            ZoomImageAndCompressionAdapter(@"c:\ZoomImage\_Temp\t.jpg", @"c:\ZoomImage\_Temp\4.jpg", 300, 300);
            var bitmap = new Bitmap(@"c:\ZoomImage\_Temp\4.jpg");
            var thumImg = bitmap.GetThumbnailImage(100, 100, null, new IntPtr());
            thumImg.Save(@"c:\ZoomImage\_Temp\4_1.jpg");
        }

        [Test]
        public void ZoomImageAndCompressionAdapter_Test_2()
        {
            ZoomImageAndCompressionAdapter(@"c:\ZoomImage\_Temp\t.jpg", @"c:\ZoomImage\_Temp\5.jpg", 100, 100, 4);
        }

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
        private static bool SaveTheSameImage(Bitmap sourceBitmap, string savePath)
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
        /// <param name="sourcePath">The sourcePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="compress">The compress</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static bool SaveTheCompressionImage(string sourcePath, string savePath, int compress)
        {
            var sourceImg = Image.FromFile(sourcePath);
            try
            {
                var templateImage = new Bitmap(sourceImg.Width, sourceImg.Height);
                var templateG = Graphics.FromImage(templateImage);
                templateG.InterpolationMode = InterpolationMode.Low;
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
        /// <param name="sourcePath">The sourcePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="maxSize">The maxSize</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:22
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static bool SaveTheCompressionImageMaxSize(string sourcePath, string savePath, int maxSize)
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
        /// <param name="sourcePath">The sourcePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeigth">The targetHeigth</param>
        /// <param name="compress">The compress</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:48
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static bool SaveTheZoomImageAndCompression(string sourcePath, string savePath, int targetWidth, int targetHeigth, int compress)
        {
            var sourceImage = Image.FromFile(sourcePath);
            if (compress == -1) compress = 100;

            //模版的宽高比例
            var templateRate = (double)targetWidth / targetHeigth;
            //原图片的宽高比例
            var initRate = (double)sourceImage.Width / sourceImage.Height;
            if (templateRate == initRate)
            {
                Image templateImage = new Bitmap(targetWidth, targetHeigth);
                var templateG = Graphics.FromImage(templateImage);
                templateG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                templateG.SmoothingMode = SmoothingMode.HighQuality;
                templateG.Clear(Color.White);
                templateG.DrawImage(sourceImage, new Rectangle(0, 0, targetWidth, targetHeigth), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                templateG.Dispose();

                var qualityParam = new EncoderParameter(Encoder.Quality, compress);
                var jpegCodec = GetEncoderInfo("image/jpeg");
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                templateImage.Save(savePath, jpegCodec, encoderParams);
                templateImage.Dispose();
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
                    pickedImage = new Bitmap(sourceImage.Width, (int)Math.Floor(sourceImage.Width / templateRate));
                    pickedG = Graphics.FromImage(pickedImage);

                    //裁剪源定位
                    fromR.X = 0;
                    fromR.Y = (int)Math.Floor((sourceImage.Height - sourceImage.Width / templateRate) / 2);
                    fromR.Width = sourceImage.Width;
                    fromR.Height = (int)Math.Floor(sourceImage.Width / templateRate);

                    //裁剪目标定位
                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = sourceImage.Width;
                    toR.Height = (int)Math.Floor(sourceImage.Width / templateRate);
                }
                //高为标准进行裁剪
                else
                {
                    pickedImage = new Bitmap((int)Math.Floor(sourceImage.Height * templateRate), sourceImage.Height);
                    pickedG = Graphics.FromImage(pickedImage);

                    fromR.X = (int)Math.Floor((sourceImage.Width - sourceImage.Height * templateRate) / 2);
                    fromR.Y = 0;
                    fromR.Width = (int)Math.Floor(sourceImage.Height * templateRate);
                    fromR.Height = sourceImage.Height;

                    toR.X = 0;
                    toR.Y = 0;
                    toR.Width = (int)Math.Floor(sourceImage.Height * templateRate);
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
                templateG.DrawImage(pickedImage, new Rectangle(0, 0, targetWidth, targetHeigth), new Rectangle(0, 0, pickedImage.Width, pickedImage.Height), GraphicsUnit.Pixel);
                templateG.Dispose();
                pickedImage.Dispose();

                var qualityParam = new EncoderParameter(Encoder.Quality, compress);
                var jpegCodec = GetEncoderInfo("image/jpeg");
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                templateImage.Save(savePath, jpegCodec, encoderParams);
                templateImage.Dispose();
            }
            sourceImage.Dispose();
            return true;
        }


        /// <summary>
        /// 缩放图片 到指定尺寸 限制图片大小
        /// </summary>
        /// <param name="sourcePath">The sourcePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeigth">The targetHeigth</param>
        /// <param name="maxSize">The maxSize</param>
        /// <returns>
        /// The Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 15:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static bool SaveTheZoomImageMaxSize(string sourcePath, string savePath, int targetWidth, int targetHeigth, int maxSize)
        {
            var sourceResult = SaveTheZoomImageAndCompression(sourcePath, savePath, targetWidth, targetHeigth, 100);
            return sourceResult && SaveTheCompressionImageMaxSize(savePath, savePath, maxSize);
        }

        /// <summary>
        /// 图片缩放压缩适配
        /// </summary>
        /// <param name="sourcePath">The sourcePath</param>
        /// <param name="savePath">The savePath</param>
        /// <param name="targetWidth">Width of the target.</param>
        /// <param name="targetHeight">Height of the target.</param>
        /// <param name="maxSize">The maxSize</param>
        /// <param name="compress">The compress</param>
        /// <returns>
        /// Boolean
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/11/6 14:02
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private static bool ZoomImageAndCompressionAdapter(string sourcePath, string savePath, int targetWidth = -1, int targetHeight = -1, int maxSize = -1, int compress = -1)
        {
            //原图存放
            if (targetWidth == -1 && targetHeight == -1 && maxSize == -1 && compress == -1) return SaveTheSameImage(new Bitmap(sourcePath), savePath);
            //不缩放 压缩图片 不限制大小 指定压缩比
            if (targetWidth == -1 && targetHeight == -1 && maxSize == -1 && compress != -1) return SaveTheCompressionImage(sourcePath, savePath, compress);
            //不缩放 压缩图片 限制图片大小 不需要指定压缩比
            if (targetWidth == -1 && targetHeight == -1 && maxSize != -1 && compress == -1) return SaveTheCompressionImageMaxSize(sourcePath, savePath, maxSize);
            //缩放图片，可能会存在裁剪 不限制大小，可以选择是否指定压缩比
            if (targetWidth != -1 && targetHeight != -1 && maxSize == -1) return SaveTheZoomImageAndCompression(sourcePath, savePath, targetWidth, targetHeight, compress);

            //缩放图片 到指定尺寸 限制图片大小
            return SaveTheZoomImageMaxSize(sourcePath, savePath, targetWidth, targetHeight, maxSize);
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





        //private void ZoomImage2Transform(string sourceFilePath, string fileSaveUrl, int maxWidth, int maxHeight, int maxSize = -1, bool isZoom = false)
        //{
        //    var quality = 100;
        //    //不缩放原图存放
        //    if (maxSize == -1)
        //    {
        //        var initImage = new Bitmap(sourceFilePath);
        //        ZoomImage2(initImage, fileSaveUrl, maxWidth, maxHeight, isZoom, quality);
        //    }


        //    if (maxSize == -1 || isZoom == false)
        //    {
        //        var initImage = new Bitmap(sourceFilePath);
        //        ZoomImage2(initImage, fileSaveUrl, maxWidth, maxHeight, isZoom, quality);
        //    }
        //    else
        //    {
        //        var initImage = Image.FromFile(sourceFilePath);
        //        ZoomImage2(initImage, fileSaveUrl, maxWidth, maxHeight, true, quality);
        //        var length = new FileInfo(fileSaveUrl).Length;
        //        while (length > maxSize && quality > 10)
        //        {
        //            var img = Image.FromFile(fileSaveUrl);
        //            KiSaveAsJpeg(img, fileSaveUrl, quality);
        //            quality -= 5;
        //        }
        //    }
        //}

        //public void KiSaveAsJpeg(Image initImage, string fileSaveUrl, int quality = 50)
        //{
        //    Image templateImage = new Bitmap(initImage.Width, initImage.Height);
        //    Graphics templateG = Graphics.FromImage(templateImage);
        //    templateG.InterpolationMode = InterpolationMode.Default;
        //    templateG.SmoothingMode = SmoothingMode.Default;
        //    templateG.Clear(Color.White);
        //    templateG.DrawImage(initImage, new Rectangle(0, 0, initImage.Width, initImage.Height), new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);
        //    initImage.Dispose();

        //    templateImage.Save(fileSaveUrl, ImageFormat.Jpeg);
        //}


        //private void ZoomImage2(Image initImage, string fileSaveUrl, int maxWidth, int maxHeight, bool isZoom = false, int quality = 50)
        //{
        //    if (isZoom)
        //    {
        //        //模版的宽高比例
        //        double templateRate = (double)maxWidth / maxHeight;
        //        //原图片的宽高比例
        //        double initRate = (double)initImage.Width / initImage.Height;
        //        if (templateRate == initRate)
        //        {
        //            Image templateImage = new Bitmap(maxWidth, maxHeight);
        //            Graphics templateG = Graphics.FromImage(templateImage);
        //            templateG.InterpolationMode = InterpolationMode.Default;
        //            templateG.SmoothingMode = SmoothingMode.Default;
        //            templateG.Clear(Color.White);
        //            templateG.DrawImage(initImage, new Rectangle(0, 0, maxWidth, maxHeight), new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);
        //            templateImage.Save(fileSaveUrl, ImageFormat.Jpeg);
        //        }
        //        else
        //        {
        //            //裁剪对象
        //            Image pickedImage;
        //            Graphics pickedG;

        //            //定位
        //            var fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
        //            var toR = new Rectangle(0, 0, 0, 0);//目标定位

        //            //宽为标准进行裁剪
        //            if (templateRate > initRate)
        //            {
        //                //裁剪对象实例化
        //                pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)System.Math.Floor(initImage.Width / templateRate));
        //                pickedG = System.Drawing.Graphics.FromImage(pickedImage);

        //                //裁剪源定位
        //                fromR.X = 0;
        //                fromR.Y = (int)System.Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
        //                fromR.Width = initImage.Width;
        //                fromR.Height = (int)System.Math.Floor(initImage.Width / templateRate);

        //                //裁剪目标定位
        //                toR.X = 0;
        //                toR.Y = 0;
        //                toR.Width = initImage.Width;
        //                toR.Height = (int)System.Math.Floor(initImage.Width / templateRate);
        //            }
        //            //高为标准进行裁剪
        //            else
        //            {
        //                pickedImage = new System.Drawing.Bitmap((int)System.Math.Floor(initImage.Height * templateRate), initImage.Height);
        //                pickedG = System.Drawing.Graphics.FromImage(pickedImage);

        //                fromR.X = (int)System.Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
        //                fromR.Y = 0;
        //                fromR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
        //                fromR.Height = initImage.Height;

        //                toR.X = 0;
        //                toR.Y = 0;
        //                toR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
        //                toR.Height = initImage.Height;
        //            }

        //            //设置质量
        //            pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            pickedG.SmoothingMode = SmoothingMode.HighQuality;

        //            //裁剪
        //            pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);

        //            //按模版大小生成最终图片
        //            Image templateImage = new Bitmap(maxWidth, maxHeight);
        //            Graphics templateG = Graphics.FromImage(templateImage);
        //            templateG.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            templateG.SmoothingMode = SmoothingMode.HighQuality;
        //            templateG.Clear(Color.White);
        //            templateG.DrawImage(pickedImage, new Rectangle(0, 0, maxWidth, maxHeight), new Rectangle(0, 0, pickedImage.Width, pickedImage.Height), GraphicsUnit.Pixel);

        //            var ps = new EncoderParameters(1);
        //            var p = new EncoderParameter(Encoder.Quality, 100);
        //            ps.Param[0] = p;
        //            var encoders = ImageCodecInfo.GetImageEncoders();
        //            var encoder = encoders.First(i => i.FormatID == initImage.RawFormat.Guid);

        //            templateImage.Save(fileSaveUrl, encoder, ps);


        //            //释放资源
        //            templateG.Dispose();
        //            templateImage.Dispose();

        //            pickedG.Dispose();
        //            pickedImage.Dispose();
        //        }
        //    }
        //    initImage.Dispose();
        //}


        //public static bool ZoomImage(Image iSource, string savePath, int targetWidth, int targetHeight, bool isZoom = false, int compress = 50)
        //{
        //    var tFormat = iSource.RawFormat;
        //    int sW = 0, sH = 0;
        //    if (isZoom)
        //    {
        //        //按比例缩放
        //        var temSize = new Size(iSource.Width, iSource.Height);

        //        if (temSize.Width > targetHeight || temSize.Width > targetWidth)
        //        {
        //            if ((temSize.Width * targetHeight) > (temSize.Height * targetWidth))
        //            {
        //                sW = targetWidth;
        //                sH = (targetWidth * temSize.Height) / temSize.Width;
        //            }
        //            else
        //            {
        //                sH = targetHeight;
        //                sW = (temSize.Width * targetHeight) / temSize.Height;
        //            }
        //        }
        //        else
        //        {
        //            sW = temSize.Width;
        //            sH = temSize.Height;
        //            //sW = targetWidth;
        //            //sH = targetHeight;
        //        }
        //    }

        //    var ob = new Bitmap(targetWidth, targetHeight);
        //    var g = Graphics.FromImage(ob);
        //    g.Clear(Color.WhiteSmoke);
        //    g.CompositingQuality = CompositingQuality.HighSpeed;
        //    g.SmoothingMode = SmoothingMode.Default;
        //    g.InterpolationMode = InterpolationMode.Low;
        //    if (isZoom)
        //    {
        //        g.DrawImage(iSource, new Rectangle((targetWidth - sW) / 2, (targetHeight - sH) / 2, sW, sH), 0, 0,
        //            iSource.Width, iSource.Height, GraphicsUnit.Pixel);
        //    }
        //    else
        //    {
        //        g.DrawImage(iSource, new Rectangle(0, 0, targetWidth, targetHeight), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
        //    }


        //    g.Dispose();
        //    //以下代码为保存图片时，设置压缩质量
        //    var ep = new EncoderParameters();
        //    var qy = new long[1];
        //    qy[0] = compress;//设置压缩的比例1-100
        //    var eParam = new EncoderParameter(Encoder.Quality, qy);
        //    ep.Param[0] = eParam;
        //    try
        //    {
        //        var arrayIci = ImageCodecInfo.GetImageEncoders();
        //        var jpegIcIinfo = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
        //        if (jpegIcIinfo == null)
        //        {
        //            ob.Save(savePath, tFormat);
        //        }
        //        else
        //        {
        //            ob.Save(savePath, jpegIcIinfo, ep); //dFile是压缩后的新路径
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        iSource.Dispose();
        //        ob.Dispose();
        //    }
        //    return true;
        //}
    }

}
