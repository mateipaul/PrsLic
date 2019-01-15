using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MiscUtilities
{
    public static class EmailHelper
    {
        private static object syncObject;
        public static void SendEmail(string destination, string subject, string body)
        {
            lock (syncObject)
            {
                if (string.IsNullOrEmpty(destination))
                {
                    return;
                }

                try
                {
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Host = @"smtp.gmail.com";
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(@"notifications.prs.lic@gmail.com", @"1234qwerQWER!");

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.BodyEncoding = Encoding.GetEncoding("utf-8");
                            mail.From = new MailAddress(@"notifications.prs.lic@gmail.com");
                            mail.To.Add(destination);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = false;

                            smtpClient.Send(mail);
                        }
                    }
                }
                catch (Exception e)
                {
                   
                }
            }
        }

    }
}
