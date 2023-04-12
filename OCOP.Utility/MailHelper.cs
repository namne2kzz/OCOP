using OCOP.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OCOP.Utility
{
    public class MailHelper
    {          
        public void SendMail(string toEmaiAddress, string subject, string content, EmailConfig emailConfig)
        {                       
            string body = content;
            MailMessage message = new MailMessage(new MailAddress(emailConfig.FromEmailAddress, emailConfig.DisplayEnaimName), new MailAddress(toEmaiAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(emailConfig.FromEmailAddress, emailConfig.FromEmailPassword);
            client.Host = emailConfig.SMTPHost;
            client.EnableSsl = emailConfig.EnableSSL;
            client.Port = !string.IsNullOrEmpty(emailConfig.SMTPPort) ? Convert.ToInt32(emailConfig.SMTPPort) : 0;
            client.Send(message);

        }
    }
}
