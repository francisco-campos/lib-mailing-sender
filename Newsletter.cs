using System;
using System.Collections.Generic;

namespace mailing_sender
{
    public class Newsletter : Mail
    {
        public Newsletter(List<string> mails)
        {
            this.MailOrigin = Config.newsletterMailOrigin;
            this.MailOriginPass = Config.newsletterMailOriginPass;
            this.Subject = "Welcome to newsletter";
            this.Body = "";
            this.Mails = mails;
        }
    }
}
