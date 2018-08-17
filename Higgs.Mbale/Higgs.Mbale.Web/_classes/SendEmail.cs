using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using System.IO;
using log4net;
using Helpers = Higgs.Mbale.Helpers;

namespace Higgs.Mbale.Web._classes
{
    public class SendEmail
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(SendEmail));
        public void SendForgotPasswordEmail(string emailAddress, string firstName, string callBackUrl)
        {
            StringBuilder sb = new StringBuilder();
            string strNewPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["forgotPasswordEmail"]);
            using (StreamReader sr = new StreamReader(strNewPath))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                }
            }



            string body = sb.ToString().Replace("#FIRSTNAME#", firstName);
            body = body.Replace("#CALLBACKURL#", callBackUrl);
            body = body.Replace("#EMAILADDRESS#", emailAddress);
            Helpers.Email email = new Helpers.Email();
            email.MailBodyHtml = body;
            email.MailToAddress = emailAddress;
            email.MailFromAddress = ConfigurationManager.AppSettings["EmailAddressFrom"];
            email.Subject = ConfigurationManager.AppSettings["forgotPassword_email_subject"];


            try
            {
                email.SendMail();
                logger.Debug("Email sent");

            }
            catch (Exception ex)
            {
                logger.Debug("Email has not been sent " + ex.Message);
            }
        }

    }
}