using DataModels.ViewModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DataUtilities
{
    public class EmailSender
    {
        public async Task<int> registrationemail(vmEmailConfig emConfig, string emailto, string title, string subject, string emailBody, int mailType)
        {
            int result = 0; string ccMail = string.Empty;
            try
            {

                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(emailto));
                    if (mailType < (int)StaticInfos.Mail.NoCCmail)
                    {
                        ccMail = mailType == (int)StaticInfos.Mail.UserRegistrationMail ? StaticInfos.SalesMailAccount : StaticInfos.CloudMailAccount;
                        mail.CC.Add(new MailAddress(ccMail));
                    }
                    mail.Subject = subject;
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    MailAddress mailaddress = new MailAddress(emConfig.EmailSenderId, title);
                    mail.From = mailaddress;



                    using (SmtpClient smtp = new SmtpClient(emConfig.EmailSenderHost))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(emConfig.EmailSenderId, emConfig.EmailSenderPassword);

                        smtp.EnableSsl = Convert.ToBoolean(emConfig.EmailSenderEnableSsl);
                        await smtp.SendMailAsync(mail);

                        result = 1;
                    }
                }


            }
            catch (Exception ex)
            {
                //Logs.WriteBug(ex.ToString());
                result = 0;
            }
            return result;
        }
    }
}
