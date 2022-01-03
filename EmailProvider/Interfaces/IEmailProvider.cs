using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChipsEmailProvider.Interfaces
{
    public interface IEmailProvider
    {
      Task SendEmailAsync(string EmailFrom, string EmailTo, string Subject, string Template);
        Task SendEmailToMultipleRecipientsAsync(string EmailFrom, string EmailTo, string Subject, string Template);
    }
}
