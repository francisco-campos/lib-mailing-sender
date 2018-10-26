using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace mailing_sender
{
    public abstract class Mail
    {
        public string ServerSmtp { get; }
        public string Domain { get; }
        protected string Subject, Body, MailOrigin, MailOriginPass;
        protected List<string> Mails;

        public string GetSubject()
        {
            return this.Subject;
        }

        public string GetBody()
        {
            return this.Body;
        }

        public List<string> GetMails()
        {
            return this.Mails;
        }

        public string GetMailOrigin()
        {
            return this.MailOrigin;
        }

        public string GetMailOriginPass()
        {
            return this.MailOriginPass;
        }


        protected Mail()
        {
            this.ServerSmtp = Config.serverSmtp;
            this.Domain = Config.domain;
        }

        abstract public string GetTemplate() { }

        public void SendMail()
        {
            try
            {
                var msg = new MailMessage
                {
                    From = new MailAddress(this.MailOrigin),
                    BodyEncoding = System.Text.Encoding.UTF8,
                    HeadersEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    Priority = MailPriority.Normal,
                    IsBodyHtml = true,
                    Subject = this.Subject,
                    Body = this.Body
                };
                LoadMailsTo(msg);
                SmtpSend(msg);
            }
            catch(Exception e)
            {
                throw new Exception("Error SendMail.", e);
            }
        }

        private  void LoadMailsTo(MailMessage msg)
        {
            if(this.Mails != null)
            {
                foreach(var mail in this.Mails)
                {
                    msg.To.Add(mail);
                }
            }
        }

        private void SmtpSend(MailMessage msg)
        {
            var smtp = new SmtpClient(this.ServerSmtp, Convert.ToInt32(this.Domain))
            {
                Credentials = new System.Net.NetworkCredential(this.MailOrigin, this.MailOriginPass),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            smtp.Send(msg);
        }

    }
}
