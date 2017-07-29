using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TonyBlogs.WebApp.Areas.Admin.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Admin/Image/

        public ActionResult Upload()
        {
            //文件保存目录路径
            String savePath = "/upload/";

            //定义允许上传的文件扩展名
            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");

            //最大文件大小
            int maxSize = 1000000;

            HttpPostedFileBase imgFile = Request.Files["imgFile"];
            if (imgFile == null)
            {
                return Content("error|请选择文件。");
            }

            String dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String dirName = Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                return Content("error|目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
                return Content("error|上传文件大小超过限制。");
            }

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return Content("error|上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

            //创建文件夹
            dirPath += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            String filePath = dirPath + newFileName;

            //imgFile.SaveAs(filePath);

            //获取图片
            Image image = System.Drawing.Image.FromStream(imgFile.InputStream);
            var percentImage = PercentImage(image);
            Compress(percentImage, filePath, 50);

            String fileUrl = savePath + "image/" + ymd + "/" + newFileName;
            return Content(fileUrl);
        }

        public static Bitmap PercentImage(Image srcImage)
        {

            int newW = srcImage.Width < 1130 ? srcImage.Width : 1130;

            int newH = int.Parse(Math.Round(srcImage.Height * (double)newW / srcImage.Width).ToString());

            try
            {

                Bitmap b = new Bitmap(newW, newH);

                Graphics g = Graphics.FromImage(b);

                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

                g.DrawImage(srcImage, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, srcImage.Width, srcImage.Height), GraphicsUnit.Pixel);

                g.Dispose();

                return b;

            }

            catch (Exception)
            {

                return null;

            }

        }

        public static bool Compress(Image iSource, string outPath, int flag)
        {

            ImageFormat tFormat = iSource.RawFormat;

            EncoderParameters ep = new EncoderParameters();

            long[] qy = new long[1];

            qy[0] = flag;

            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);

            ep.Param[0] = eParam;

            try
            {

                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageDecoders();

                ImageCodecInfo jpegICIinfo = null;

                for (int x = 0; x < arrayICI.Length; x++)
                {

                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {

                        jpegICIinfo = arrayICI[x];

                        break;

                    }

                }

                if (jpegICIinfo != null)

                    iSource.Save(outPath, jpegICIinfo, ep);

                else

                    iSource.Save(outPath, tFormat);

                return true;

            }

            catch
            {

                return false;

            }

            finally {
                iSource.Dispose();
            }

        }

    }
}
