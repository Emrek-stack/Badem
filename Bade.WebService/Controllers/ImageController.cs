using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bade.WebService.Controllers
{
    public class ImageController : BaseApiController
    {
        private readonly bool _allowLogoResize = true;//Convert.ToBoolean(ConfigurationManager.AppSettings["LogoResize:AllowLogoResize"]);
        private readonly string _rootPath = @"F:\D";//GlobalConfiguration.ImageRootPath;

        public HttpResponseMessage CountryLogoResizer(int width, int height, ResizeMode resizeMode, string fileName)
        {
            string fullFilePath = Path.Combine(_rootPath, "countries", fileName);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            var imageResult =  Resize(width, height, resizeMode, DefaultPicture.Blank, fullFilePath).Content.ReadAsStreamAsync();
            response.Content = new StreamContent(imageResult.Result);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            return response;

        }

        public HttpResponseMessage LogoResizer(int width, int height, ResizeMode resizeMode, string fileName, string moduleName, string sportName)
        {
            string fullFilePath = Path.Combine(_rootPath, moduleName, sportName, fileName);
            //Response.Cache.SetCacheability(HttpCacheability.Public);
            //Response.Cache.SetLastModified(DateTime.Now);
            //Response.Cache.SetExpires(DateTime.Now.AddMonths(1));
            switch (moduleName)
            {
                case "managers":
                    return Resize(width, height, resizeMode, DefaultPicture.Person, fullFilePath);
                case "players":
                    return Resize(width, height, resizeMode, DefaultPicture.Person, fullFilePath);
                case "referees":
                    return Resize(width, height, resizeMode, DefaultPicture.Person, fullFilePath);
                case "stadiums":
                    return Resize(width, height, resizeMode, DefaultPicture.Stadium, fullFilePath);
                default:
                    return Resize(width, height, resizeMode, DefaultPicture.Blank, fullFilePath);
            }
        }

        private HttpResponseMessage Resize(int width, int height, ResizeMode resizeMode, DefaultPicture defaultPicture, string fullFilePath)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            if (fullFilePath.Contains("-"))
                fullFilePath = fullFilePath.Remove(fullFilePath.IndexOf("-", StringComparison.Ordinal), fullFilePath.LastIndexOf(".", StringComparison.Ordinal) - fullFilePath.IndexOf("-", StringComparison.Ordinal));
            Bitmap image;
            if (new FileInfo(fullFilePath).Exists)
            {
                image = new Bitmap(fullFilePath);
            }
            else
            {
                switch (defaultPicture)
                {
                    case DefaultPicture.Blank:
                        image = new Bitmap(Path.Combine(_rootPath, "defaults", "blank.png"));
                        break;
                    case DefaultPicture.Person:
                        image = new Bitmap(Path.Combine(_rootPath, "defaults", "person.png"));
                        break;
                    case DefaultPicture.Stadium:
                        image = new Bitmap(Path.Combine(_rootPath, "defaults", "stadium.png"));
                        break;
                    default:
                        image = new Bitmap(Path.Combine(_rootPath, "defaults", "blank.png"));
                        break;
                }
            }
            MemoryStream memoryStream = new MemoryStream();
            EncoderParameters encoderParams = new EncoderParameters(1);
            ImageCodecInfo encoder = imageEncoders.FirstOrDefault(item => item.FormatID == image.RawFormat.Guid);
            int height1 = height;
            int width1 = width;
            double num = Math.Min(width / (double)image.Width, height / (double)image.Height);
            switch (resizeMode)
            {
                case ResizeMode.ScaleWidth:
                    width1 = height * image.Width / image.Height;
                    break;
                case ResizeMode.ScaleHeight:
                    height1 = width * image.Height / image.Width;
                    break;
                case ResizeMode.PreventEnlarge:
                    width1 = (int)(image.Width * num);
                    height1 = (int)(image.Height * num);
                    break;
                case ResizeMode.AutoFit:
                    width1 = height * image.Width / image.Height;
                    break;
            }
            if (resizeMode == ResizeMode.AutoFit)
            {
                width1 = (int)(image.Width * num);
                height1 = (int)(image.Height * num);
                float x = 0.0f;
                float y = 0.0f;
                if (width1 < width)
                    x = (width - width1) / 2f;
                if (height1 < height)
                    y = (height - height1) / 2f;
                Bitmap bitmap = new Bitmap(width, height);
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(fullFilePath.EndsWith(".png") ? new SolidBrush(Color.Transparent) : new SolidBrush(Color.White), 0, 0, width, height);
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, x, y, width1, height1);
                }
                image = bitmap;
            }
            if (resizeMode != ResizeMode.NoResize && _allowLogoResize && resizeMode != ResizeMode.AutoFit)
            {
                Bitmap bitmap = new Bitmap(width1, height1);
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);
                }
                image = bitmap;
            }
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
            image.Save(memoryStream, encoder, encoderParams);
            image.Dispose();
            memoryStream.Position = 0L;

            var response = Request.CreateResponse();
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.Content = new StreamContent(memoryStream);
            return response;
            //return Request.CreateResponse(memoryStream.ToArray(), );
        }

        public enum ResizeMode
        {
            NoResize,
            ScaleWidth,
            ScaleHeight,
            PreventEnlarge,
            AutoFit,
            AutoFill,
        }

        public enum DefaultPicture
        {
            Blank,
            Person,
            Stadium,
        }
    }
}