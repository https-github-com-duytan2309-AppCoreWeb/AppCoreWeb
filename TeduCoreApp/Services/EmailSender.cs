using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using MimeKit;

//using MailKit.Net.Smtp;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

//using TeduCoreApp.Models;
//using ContentType = MimeKit.ContentType;

//using MimeKit;

using System.Text;
using System.Text.RegularExpressions;

namespace TeduCoreApp.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient(_configuration["MailSettings:Server"])
            {
                UseDefaultCredentials = false,
                Port = int.Parse(_configuration["MailSettings:Port"]),
                EnableSsl = bool.Parse(_configuration["MailSettings:EnableSsl"]),
                Credentials = new NetworkCredential(_configuration["MailSettings:UserName"], _configuration["MailSettings:Password"])
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["MailSettings:FromEmail"], _configuration["MailSettings:UserName"]),
            };

            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;

            client.Send(mailMessage);
            return Task.CompletedTask;
        }

        public Task SendEmailBillMailAsync(string email, string subject, string message, List<string> images)
        {
            //string image = @"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct01.jpg";
            //string image2 = @"\uploaded\images\20191026\bo-ngu-cho-be-cotton-100-kkct35.jpg";

            //int countImg = 1;
            //var imgString = "";
            //foreach (var item in images)
            //{
            //    imgString += @"<img src='cid:EmbeddedContent_" + countImg + "' style = 'width: 120px; height: 70px;' /><br/><h4>" + @item + @"</h4>";
            //    countImg++;
            //}
            //countImg = 0;

            //string htmlMessage = @"<html><head></head><body><h1>Here is Send Mail with Images</h1>"
            //     +
            //     /*  @"<img src = 'cid:EmbeddedContent_1' style = 'width: 100px; height: 50px' /><br/>"*/
            //     imgString
            //     +
            //    "</body></html>";

            SmtpClient client = new SmtpClient(_configuration["MailSettings:Server"])
            {
                UseDefaultCredentials = false,
                Port = int.Parse(_configuration["MailSettings:Port"]),
                EnableSsl = bool.Parse(_configuration["MailSettings:EnableSsl"]),
                Credentials = new NetworkCredential(_configuration["MailSettings:UserName"], _configuration["MailSettings:Password"])
            };

            MailMessage msg = new MailMessage
            {
                From = new MailAddress(_configuration["MailSettings:FromEmail"], _configuration["MailSettings:UserName"]),
            };

            // Create the HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                                                         /*htmlMessage*/message,
                                                         Encoding.UTF8,
                                                         MediaTypeNames.Text.Html);

            // Create a plain text message for client that don't support HTML
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                                                        Regex.Replace(/*htmlMessage*/message,
                                                                      "<[^>]+?>",
                                                                      string.Empty),
                                                        Encoding.UTF8,
                                                        MediaTypeNames.Text.Plain);

            //string mediaType = MediaTypeNames.Image.Jpeg;

            int count = 1;
            foreach (var item in images)
            {
                //var image = item.Replace('/', '\\');
                string mediaType = GetContentType("wwwroot" + item);
                LinkedResource img = new LinkedResource("wwwroot" + item, mediaType);
                img.ContentId = "EmbeddedContent_" + count;
                img.ContentType.MediaType = mediaType;
                img.TransferEncoding = TransferEncoding.Base64;
                img.ContentType.Name = img.ContentId;
                img.ContentLink = new Uri("cid:" + img.ContentId);

                htmlView.LinkedResources.Add(img);
                count++;
            }
            count = 0;
            //string mediaType = GetContentType(@"wwwroot" + image);
            //LinkedResource img = new LinkedResource(@"wwwroot" + image, mediaType);

            //string mediaType2 = GetContentType(@"wwwroot" + image2);
            //LinkedResource img2 = new LinkedResource(@"wwwroot" + image2, mediaType2);

            //img.ContentId = "EmbeddedContent_" + 1;
            //img.ContentType.MediaType = mediaType;
            //img.TransferEncoding = TransferEncoding.Base64;
            //img.ContentType.Name = img.ContentId;
            //img.ContentLink = new Uri("cid:" + img.ContentId);
            //htmlView.LinkedResources.Add(img);

            //img2.ContentId = "EmbeddedContent_" + 2;
            //img2.ContentType.MediaType = mediaType;
            //img2.TransferEncoding = TransferEncoding.Base64;
            //img2.ContentType.Name = img.ContentId;
            //img2.ContentLink = new Uri("cid:" + img.ContentId);
            //htmlView.LinkedResources.Add(img2);

            var link = "<a href='/admin/bill/index' >Vào đây</a>";
            // Create the HTML view
            AlternateView linkHTML = AlternateView.CreateAlternateViewFromString(
                                                        link,
                                                         Encoding.UTF8,
                                                         MediaTypeNames.Text.Html);

            msg.AlternateViews.Add(plainView);
            msg.AlternateViews.Add(htmlView);
            msg.IsBodyHtml = true;
            msg.Subject = subject;
            msg.Body = message/*htmlMessage*/;
            msg.To.Add(email);
            client.Send(msg);

            //SendEmailAsync("binhit201195@gmail.com", "Thông Báo!!!", "Tài Khoản " + email + " Đã mua hàng của bạn!!! Click " + link + " để kiểm tra!");
            return Task.CompletedTask;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}