using Ordering.Application.Contacts.Infrastructure;
using Ordering.Application.Models;
using QuickMailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructer.Mail
{
    public class EmailService : IEmailService
    {
        public async Task<bool>SendEmailAsync(EmailMessage emailMessage)
        {
            Email email =  new Email();
            email.SendEmail(emailMessage.To, "", "", emailMessage.Subject, emailMessage.Body, []);
            throw new NotImplementedException();
        }
    }
}
