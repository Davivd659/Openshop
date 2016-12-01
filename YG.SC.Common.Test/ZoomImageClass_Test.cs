
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using NUnit.Framework;

namespace YG.SC.Common.Test
{
    /// <summary>
    /// 类名称：ZoomImageClass_Test
    /// 命名空间：YG.SC.Common.Test
    /// 类功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/10/22 14:35
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    [TestFixture]
    public class ZoomImageClass_Test
    {
        private const string SourcePath = @"c:\ZoomImage\source.png";

        [Test]
        public void TestAmplification()
        {
            const int targetWidth = 500;
            const int targetHeight = 300;

            var sourceImage = new Bitmap(SourcePath);
            var saveImage = new Bitmap(targetWidth, targetHeight);
            var g = Graphics.FromImage(saveImage);
            g.InterpolationMode = InterpolationMode.Low;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //g.Clear(Color.White);

            //Console.WriteLine("width:{0} height:{1}", sourceImage.Width, sourceImage.Height);
            //int intWidth;
            //int intHeight;
            ////宽度比目的图片宽度大，长度比目的图片长度小  
            //if (sourceImage.Width > targetWidth && sourceImage.Height <= targetHeight)
            //{
            //    intWidth = targetWidth;
            //    intHeight = (intWidth * sourceImage.Height) / sourceImage.Width;
            //}
            ////宽度比目的图片宽度小，长度比目的图片长度大  
            //else if (sourceImage.Width <= targetWidth && sourceImage.Height > targetHeight)
            //{
            //    intHeight = targetHeight;
            //    intWidth = (intHeight * sourceImage.Width) / sourceImage.Height;
            //}
            ////长宽比目的图片长宽都小  
            //else if (sourceImage.Width <= targetWidth && sourceImage.Height <= targetHeight)
            //{
            //    //intHeight = sourceImage.Width;
            //    //intWidth = sourceImage.Width;
            //    intHeight = targetHeight;
            //    intWidth = targetWidth;
            //}
            //else//长宽比目的图片的长宽都大  
            //{
            //    intWidth = targetWidth;
            //    intHeight = (intWidth*sourceImage.Height)/sourceImage.Width;
            //    if (intHeight > targetHeight)//重新计算  
            //    {
            //        intHeight = targetHeight;
            //        intWidth = (intHeight*sourceImage.Width)/sourceImage.Height;
            //    }
            //}
            //g.DrawImage(sourceImage, (targetWidth - intWidth) / 2, (targetHeight - intHeight) / 2, intWidth, intHeight);
            g.DrawImage(sourceImage, 0, 0, targetWidth, targetHeight);
            sourceImage.Dispose();
            saveImage.Save(@"C:\ZoomImage\1.jpg");
        }

        [Test]
        public void CompressImage()
        {
            const string dFile = @"c:\ZoomImage\2.png";
            const int dHeight = 170;
            const int dWidth = 170;
            const int flag = 50;

            var iSource = Image.FromFile(SourcePath);
            var tFormat = iSource.RawFormat;
            int sW = 0, sH = 0;
            //按比例缩放
            var temSize = new Size(iSource.Width, iSource.Height);

            if (temSize.Width > dHeight || temSize.Width > dWidth)
            {
                if ((temSize.Width * dHeight) > (temSize.Height * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * temSize.Height) / temSize.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (temSize.Width * dHeight) / temSize.Height;
                }
            }
            else
            {
                sW = temSize.Width;
                sH = temSize.Height;
            }
            var ob = new Bitmap(dWidth, dHeight);
            var g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            //以下代码为保存图片时，设置压缩质量
            var ep = new EncoderParameters();
            var qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            var eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                var arrayIci = ImageCodecInfo.GetImageEncoders();
                var jpegIcIinfo = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));
                if (jpegIcIinfo == null)
                {
                    ob.Save(dFile, tFormat);
                }
                else
                {
                    ob.Save(dFile, jpegIcIinfo, ep); //dFile是压缩后的新路径
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }

        }
    }
}
