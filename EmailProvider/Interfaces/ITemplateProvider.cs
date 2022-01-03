using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChipsEmailProvider.Interfaces
{
    public interface ITemplateProvider
    {
        string GetForgotPasswordTemplate(IHostingEnvironment _env, string UserName, string ResetLink, string OS, string Browser);
        string RejectedContainer(IHostingEnvironment _env, string UserName, string ContainerNo, string OS, string Browser);
        string ReleasedContainer(IHostingEnvironment _env, string Name,string ContainerNo,string JobNo,string Sealno, string Sealno1, string ReleasedTime,string BagType,string Count,string weight, string OS, string Browser); 
       string GetNotificationAlertTemplate(IHostingEnvironment _env, string person_name, string mask, string temperature, string buisness_id, string location, string scanner_name, string pass_time, string alert_type, string person_image);
        string SendInvoice(IHostingEnvironment _env, string InvoiceNo, string DueDate, string TotalAmount, string OS, string Browser);
    }
}
