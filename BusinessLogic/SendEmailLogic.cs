using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class SendEmailLogic
    {
        //2 functions take seek request as parameters for post detail page.

        public static void SendEmailtoRoomSeeker(this SeekRoomRequest request, EmailContent emailContent)
        {
            var authorEmail = request.Username;
            var receiverEmail = request.ContactPersonEmail;
            
            var email = emailContent.ContactEmail;
            var number = emailContent.ContactNumber;
            var message = emailContent.Message;

            MailMessage mailMessage = new MailMessage()
            {
                Subject = "You have a message from a MetroRent member",
                Body = "<h4>Dear MetroRent Member,<br>" +
                       "Below is a message from a tenant seeker interested in your seek: <br><i>" + request.Title + "</i><br><br></h4>" +
                       "<h3>Contact email: " + email + "<br>" +
                       "Phone#: " + number + "<br>" +
                       "Message: " + message + "</h3><br><br>" +
                       "<h4>Wish you find your dream room soon!<br>" +
                       "All the best,<br> MetroRent Team</h4>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(authorEmail));
            if (receiverEmail != null && !receiverEmail.Equals(authorEmail))
            {
                mailMessage.To.Add(new MailAddress(receiverEmail));
            }
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        public static void SendEmailtoTenantSeeker(this SeekTenantRequest request, EmailContent emailContent)
        {
            var authorEmail = request.Username;
            var receiverEmail = request.ContactPersonEmail;

            var address = request.Address;
            var creation = request.RequestCreateTime;
            var rental = request.MonthlyRentalAmount;
            var post = "<div style=\"border: solid;\">" +
                       "<tr><td>Date</td><td>Address</td><td>Rental</td></tr>" +
                       "<tr><td>" + creation + "</td><td>" + address + "</td><td>" + rental + "</td></tr></div>";

            var email = emailContent.ContactEmail;
            var number = emailContent.ContactNumber;
            var message = emailContent.Message;

            MailMessage mailMessage = new MailMessage()
            {
                Subject = "You have a message from a MetroRent member",
                Body = "<h4>Dear MetroRent Member,<br>" +
                       "Below is a message from a room seeker interested in your seek: <br> <i>" + request.Title + "</i>.<br><br></h4>" +
                       "<h3>Contact email: " + email + "<br>" +
                       "Phone#: " + number + "<br>" +
                       "Message: " + message + "</h3><br><br>" +
                       "<h4>Wish you rent your room out soon!<br>" +
                       "All the best,<br> MetroRent Team</h4>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(authorEmail));
            if (receiverEmail != null && !receiverEmail.Equals(authorEmail))
            {
                mailMessage.To.Add(new MailAddress(receiverEmail));
            }
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }
    }
}
