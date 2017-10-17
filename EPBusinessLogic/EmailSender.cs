using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using EPCommon.Contracts;

namespace EPBusinessLogic
{
    public class EmailSender : IEmailSender
    {
        public void SendMail(string destination, string subject, string body)
        {
            var isEnabled =Convert.ToBoolean(ConfigurationManager.AppSettings["isEmailEnabled"]);
            if(isEnabled)
            { 
            var myMessage = new MailMessage();
            myMessage.To.Add(destination);
            myMessage.From = new MailAddress(
                "nadeemiimt@gmail.com", "Mohd Nadeem");
            myMessage.Subject = subject;
            myMessage.Body = body;
            myMessage.IsBodyHtml = true;

                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    var credentials = new NetworkCredential(
                        ConfigurationManager.AppSettings["mailAccount"],
                        ConfigurationManager.AppSettings["mailPassword"]
                    );

                    smtpServer.Port = 25;
                    smtpServer.Credentials = credentials;
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(myMessage);
                }
            }
        }
    }
}
