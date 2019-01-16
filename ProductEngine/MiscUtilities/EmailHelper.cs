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
        private static object syncObject = new object();
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


                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(@"notifications.prs.lic@gmail.com", "EBXmw63Sh7w6eUh");
                        //smtpClient.Credentials = new NetworkCredential(@"matei.paul94@gmail.com", "SerpentLiquid1994");


                        using (MailMessage mail = new MailMessage())
                        {
                            mail.BodyEncoding = Encoding.GetEncoding("utf-8");
                            mail.From = new MailAddress(@"notifications.prs.lic@gmail.com");
                            mail.To.Add(destination);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;

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
