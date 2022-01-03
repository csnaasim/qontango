using ChipsEmailProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ChipsEmailProvider.Provider
{
    public class SmtpProvider : IEmailProvider
    {
        public Task SendEmailToMultipleRecipientsAsync(string EmailFrom, string EmailTo, string Subject, string Template)
        {
            throw new NotImplementedException();
        }

        async Task IEmailProvider.SendEmailAsync(string EmailFrom, string EmailTo, string Subject, string Template)
        {
            try
            {
                using (MailMessage mm = new MailMessage())
                {
                    mm.From = new MailAddress(Common.SENDGRID_EMAIL);
                    foreach (var address in EmailTo.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.To.Add(new MailAddress(address));
                    }
                    
                    mm.Subject = Subject;


                    mm.Body = Template;

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = Common.SMTP_HOST;
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(Common.SMTP_EMAIL, Common.SMTP_PASS);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt32(Common.SMTP_PORT);
                        smtp.Send(mm);
                    }

                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
