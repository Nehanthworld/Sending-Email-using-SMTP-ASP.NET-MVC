using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace SendingEmailFromSMTP.Controllers
{
    public class HomeController : Controller
    {
        string MailBody = "<!DOCTYPE html>" +
                                "<html> " +
                                    "<body style=\"background -color:#ff7f26;text-align:center;\"> " +
                                    "<h1 style=\"color:#051a80;\">Welcome to Nehanth World</h1> " +
                                    "<h2 style=\"color:#fff;\">Please find the attached files.</h2> " +
                                    "<label style=\"color:orange;font-size:100px;border:5px dotted;border-radius:50px\">N</label> " +
                                    "</body> " +
                                "</html>";
        string subject = "Welcome to Nehanth World.";
        string mailTitle = "Email from .Net Core App";
        string fromEmail = "Your Email";
        string fromEmailPassword = "Your Password";
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string toEmail)
        {
            //Email & Content 
            MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = MailBody;
            message.IsBodyHtml = true;


            //Server Details
            SmtpClient smtp = new SmtpClient();
            //Outlook ports - 465 (SSL) or 587 (TLS)
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Credentials
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();
            credentials.UserName = fromEmail;
            credentials.Password = fromEmailPassword;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credentials;

            smtp.Send(message);

            ViewBag.EmailSentMessage = "Email sent successfully";

            return View();
        }
        
    }
}

//gmail ports - 465 (SSL) or 587 (TLS)
//smtp.Host = "smtp.gmail.com";
//Godaddy ports - 465 (SSL) 587 (TSL/SSL - Mac) 80, 3535 or 25
//smtp.Host = "smtpout.secureserver.net";