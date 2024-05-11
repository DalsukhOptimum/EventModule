using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Models;

namespace Library
{
    public class SendMailBL
    {
        public void sendMail(EmailEntity emailobj) {

            InsertLog.WriteErrrorLog("EmailVarificationBL=>SendEmail=>Started");
            try
            {
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("thisiscoder43@gmail.com", "rfspydfhkdbsiusz")
                };


                //making mail content like adding to,from,cc,content etc..
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("thisiscoder43@gmail.com", "");
                mail.To.Add(new MailAddress(emailobj.Email));
                mail.CC.Add(new MailAddress("naneradalsukh27@gmail.com"));
                mail.IsBodyHtml = true;
                mail.Subject = emailobj.Subject;
                mail.Body = emailobj.Body;

                //storing file in Expense directory and making userid name of the file

                //sending mail from smtp 
                smtp.Send(mail);
         

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                InsertLog.WriteErrrorLog("SendMailBL=>SendEmail=>Exception" + ex.Message + ex.StackTrace);
            }
        }
    }
}
