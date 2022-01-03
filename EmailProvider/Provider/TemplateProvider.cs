using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChipsEmailProvider.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace ChipsEmailProvider.Provider
{
    public class TemplateProvider : ITemplateProvider
    {
        private string GetTemplate(string TemplateName, IHostingEnvironment _env)
        {
            string Template = "";
            try
            {
                //  string path= "WelcomeEmail.html";
                FileStream s = File.Open(_env.WebRootPath + "/Templates/" + TemplateName, FileMode.Open);
                using (StreamReader reader = new StreamReader(s))
                {

                    Template = reader.ReadToEnd();
                    return Template;
                    // Template = reader.ReadToEnd();

                }
            }
            catch (Exception e)
            {
                throw;
            }

        }



        string ITemplateProvider.GetForgotPasswordTemplate(IHostingEnvironment _env, string UserName, string ResetLink, string OS, string Browser)
        {
            string TemplateName = "forgot_password.html";
            var Template = GetTemplate(TemplateName, _env);
            Template = Template.Replace("{{name}}", UserName);
            Template = Template.Replace("{{action_url}}", ResetLink);
            Template = Template.Replace("{{operating_system}}", OS);
            Template = Template.Replace("{{browser_name}}", Browser);
            return Template;
        }

        public string GetNotificationAlertTemplate(IHostingEnvironment _env, string person_name, string mask, string temperature, string buisness_id, string location, string scanner_name, string pass_time, string alert_type, string person_image)
        {
            throw new NotImplementedException();
        }

        public string RejectedContainer(IHostingEnvironment _env, string UserName, string ContainerNo, string OS, string Browser)
        {

            string TemplateName = "reject_container.html";
            var Template = GetTemplate(TemplateName, _env);
            Template = Template.Replace("{{name}}", UserName);
            Template = Template.Replace("{{Container_no}}", ContainerNo);
            Template = Template.Replace("{{operating_system}}", OS);
            Template = Template.Replace("{{browser_name}}", Browser);
            return Template;

        }

        public string ReleasedContainer(IHostingEnvironment _env, string Name, string ContainerNo, string JobNo, string Sealno, string Sealno1, string ReleasedTime, string BagType, string Count, string weight, string OS, string Browser)
        {
            string TemplateName = "release_container.html";
            var Template = GetTemplate(TemplateName, _env);
            Template = Template.Replace("{{name}}", Name);
            Template = Template.Replace("{{Container_no}}", ContainerNo);
            Template = Template.Replace("{{job_no}}", Convert.ToString(JobNo));
            Template = Template.Replace("{{seal_no1}}", Sealno);
            Template = Template.Replace("{{seal_no2}}", Sealno1);
            Template = Template.Replace("{{bag_type}}", BagType);
            Template = Template.Replace("{{count}}", Convert.ToString(Count));
            Template = Template.Replace("{{weight}}", Convert.ToString(weight));
            Template = Template.Replace("{{release_time}}", ReleasedTime);
            Template = Template.Replace("{{operating_system}}", OS);
            Template = Template.Replace("{{browser_name}}", Browser);
            return Template;
        }

        public string SendInvoice(IHostingEnvironment _env, string InvoiceNo, string DueDate, string TotalAmount, string OS, string Browser)
        {
            string TemplateName = "send_invoice.html";
            var Template = GetTemplate(TemplateName, _env);
            Template = Template.Replace("{{Invoice_no}}", InvoiceNo);
            Template = Template.Replace("{{due_date}}", DueDate);
            Template = Template.Replace("{{TotalAmount}}", TotalAmount);
            Template = Template.Replace("{{operating_system}}", OS);
            Template = Template.Replace("{{browser_name}}", Browser);
            return Template;
        }
        //string ITemplateProvider.GetNotificationAlertTemplate(IHostingEnvironment _env, string person_name, string mask, string temperature, string buisness_id, string location, string scanner_name, string pass_time, string alert_type, string person_image)
        //{
        //    string TemplateName = "notification_alert.html";
        //    var Template = GetTemplate(TemplateName, _env);
        //    Template = Template.Replace("{{person_name}}", person_name);
        //    Template = Template.Replace("{{mask}}", mask);
        //    Template = Template.Replace("{{temperature}}", temperature);
        //    Template = Template.Replace("{{buisness_id}}", buisness_id);
        //    Template = Template.Replace("{{location}}", location);
        //    Template = Template.Replace("{{scanner_name}}", scanner_name);
        //    Template = Template.Replace("{{pass_time}}", pass_time);
        //    Template = Template.Replace("{{alert_type}}", alert_type);
        //    Template = Template.Replace("{{person_image}}", person_image);


        //    return Template;
        //}




    }
}
