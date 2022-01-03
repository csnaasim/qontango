using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using RoleUserApi.Model.Repositories.Implementation;
using RoleUserApi.Model.Repositories.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Telerik.Reporting.Cache.File;
//using Telerik.Reporting.Services;
//using Telerik.Reporting.Services.AspNetCore;
using ChipsEmailProvider;
using RoleUserApi.Common;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RoleUserApi.Controllers
{
    [Route("api/reports")]
    [EnableCors]
    
    public class ReportsController : ReportsControllerBase
    {
        readonly string reportsPath = string.Empty;

        //[System.Obsolete]
        public ReportsController(IReportServiceConfiguration reportServiceConfiguration)
             : base(reportServiceConfiguration)
        {
            
        }

        [HttpPost]
        protected override  HttpStatusCode SendMailMessage(MailMessage mailMessage)
        {
            var apiKey = Environment.GetEnvironmentVariable(Constants.SENDGRID_KEY);
            //  var client = new SendGridClient("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");

            //var client = new SendGridClient("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");
            var client = new SendGridClient(Constants.SENDGRID_TOKEN);
            var from = new EmailAddress(Constants.SENDGRID_EMAIL, Constants.SENDGRID_DISPLAYNAME);

            var subject = mailMessage.Subject;
            //List<EmailAddress> to = new List<EmailAddress>();

            var plainTextContent = "CHIPS Support";
            var htmlContent = mailMessage.Body;
            List<Task> tasklist = new List<Task>();
            SendGrid.Helpers.Mail.Attachment Att = new SendGrid.Helpers.Mail.Attachment();
            //foreach (var att in mailMessage.Attachments)
            //{
            //    //var item = new SendGrid.Helpers.Mail.Attachment();
            //    //item.Type = att.ContentType.MediaType;
            //    //item.Content = ConvertToBase64(att.ContentStream);
            //    //msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>();
            //    //msg.Attachments.Add(item);
            //    Att.a("Invoice.pdf", ConvertToBase64(att.ContentStream));
            //}
            Att.Filename = "Invoice.pdf";
            Att.Content = ConvertToBase64(mailMessage.Attachments[0].ContentStream);
            foreach (var reciever in mailMessage.To)
            {
                var to = new EmailAddress() { Name= reciever.DisplayName,Email=reciever.Address };

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                msg.AddAttachment(Att);
                //tasklist.Add( client.SendEmailAsync(msg));
                var res= client.SendEmailAsync(msg).Result;
            }

            foreach (var reciever in mailMessage.CC)
            {
                var to = new EmailAddress() { Name = reciever.DisplayName, Email = reciever.Address };

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                msg.AddAttachment(Att);
                //tasklist.Add( client.SendEmailAsync(msg));
                var res = client.SendEmailAsync(msg).Result;
            }
            //Task.WhenAll(tasklist);
            return HttpStatusCode.OK;
        }

        public static string ConvertToBase64( Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
            
        }
    }
}