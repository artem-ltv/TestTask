using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class EmailSender
    {
        private string _emailAdressFrom = "jesterfaber@gmail.com";
        private string _emailAdressTo = "litwinenkoartem1@gmail.com";
        private string _emailSubject = "Результат проверки";

        private string _smtpHost = "smtp.gmail.com";
        private string _password = "liypnoujtpmepvlp";
        public void Send(string textMessage)
        {
            MailAddress adressFrom = new MailAddress(_emailAdressFrom);
            MailAddress adressTo = new MailAddress(_emailAdressTo);

            using (MailMessage message = new MailMessage(adressFrom, adressTo))
            using (SmtpClient smtpClient = new SmtpClient()) 
            {
                message.Subject = _emailSubject;
                message.Body = textMessage;

                smtpClient.Host = _smtpHost;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(adressFrom.Address, _password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                smtpClient.Send(message);
            }
        }
    }
}
