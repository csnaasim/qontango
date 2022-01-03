using ChipsEmailProvider.Interfaces;
using ChipsEmailProvider.Provider;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChipsEmailProvider.Factories
{
    public static class EmailFact
    {
        public static IEmailProvider GetMailServer(string ServerType)
        {
            if (ServerType == "smtp")
            {
                return new SmtpProvider();
            }
            else
            {
                return new SendGridProvider();
            }

        }

        public static ITemplateProvider GetTemplateProvider()
        {

            return new TemplateProvider();

        }

    public static ITemplateProvider GetTemplateRejectedProvider()
    {

      return new TemplateProvider();

    }

    public static ITemplateProvider GetTemplateReleasedProvider()
    {

      return new TemplateProvider();

    }

        public static ITemplateProvider GetTemplateSendInvoiceProvider()
        {

            return new TemplateProvider();

        }

    }
}
