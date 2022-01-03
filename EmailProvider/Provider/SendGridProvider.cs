using ChipsEmailProvider.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChipsEmailProvider.Provider
{
    class SendGridProvider : IEmailProvider
    {
        public Task SendEmailToMultipleRecipientsAsync(string EmailFrom, string EmailTo, string Subject, string Template)
        {
            throw new NotImplementedException();
        }

        async Task IEmailProvider.SendEmailAsync(string EmailFrom, string EmailTo, string Subject, string Template)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable(Common.SENDGRID_KEY);
                //  var client = new SendGridClient("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");

                //var client = new SendGridClient("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");
                var client = new SendGridClient(Common.SENDGRID_TOKEN);
                var from = new EmailAddress(Common.SENDGRID_EMAIL, Common.SENDGRID_DISPLAYNAME);

                var subject = Subject;
                //List<EmailAddress> to = new List<EmailAddress>();

                var plainTextContent = "CHIPS Support";
                var htmlContent = Template;
                foreach (var address in EmailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var to = new EmailAddress(address);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = await client.SendEmailAsync(msg);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
