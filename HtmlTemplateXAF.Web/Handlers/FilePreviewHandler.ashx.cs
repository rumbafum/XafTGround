using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HtmlTemplateXAF.Web.Handlers
{
    /// <summary>
    /// Summary description for FilePreviewHandler
    /// </summary>
    public class FilePreviewHandler : IHttpHandler
    {

        private bool IsImage(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpeg" || extension == ".jpg" || extension == ".bmp"
                || extension == ".png" || extension == ".gif" || extension == ".tiff";
        }

        private string GetContentTypeString(string filePath)
        {
            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".bmp":
                    return "image/bmp";
                case ".gif":
                    return "image/gif";
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".tiff":
                    return "image/tiff";
                case ".ico":
                    return "image/x-icon";
                case ".htm":
                case ".html":
                    return "text/html";
                case ".xml":
                    return "application/xml";
                case ".mp3":
                    return "audio/mp3";
                case ".zip":
                    return "application/x-zip-compressed";
                case ".txt":
                    return "text/plain";
                case ".avi":
                    return "video/avi";
                case ".mpeg":
                    return "video/mpeg";
                case ".asx":
                    return "video/x-ms-asf";
                case ".wm":
                    return "video/x-ms-wm";
                case ".wmv":
                    return "video/x-ms-wmv";
                default:
                    return "application/octet-stream";
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var fileId = context.Request["fileId"];
            var filePath = context.Server.MapPath("~/Handlers/" + fileId);
            if (File.Exists(filePath))
            {
                context.Response.ContentType = GetContentTypeString(filePath);
                context.Response.AddHeader("Content-Disposition", "attachment; filename=teste");
                //if (IsImage(filePath))
                //{
                //    using(MemoryStream ms = new MemoryStream())
                //    using(Image img = Image.FromFile(filePath))
                //    {
                //        Image thumb = ImageExtensions.GetThumbnailImage(img, 500, 
                //            Convert.ToInt16((500 * img.PhysicalDimension.Height) / img.PhysicalDimension.Width));
                //        thumb.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                //    }

                //}
                //else
                context.Response.BinaryWrite(File.ReadAllBytes(filePath)); // byte[]
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}