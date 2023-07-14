using System.Net.Mail;
using System.Net;

namespace ABCLearn.Services
{
    public class Email
    {
        private static Email instance;
        public static Email Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Email();
                }
                return instance;
            }
        }

        public (bool, int) sendOTP(string email, string forgot = null)
        {
            bool isSend = false;
            int OTP = 0;
            try
            {
                const string frompass = "jtrrwlmnlafsxahp";
                const string subject = "VERIFY YOUR EMAIL ADDRESS";
                var fromAddress = new MailAddress("abclearneducation@gmail.com");
                var toAddress = new MailAddress(email);
                OTP = new Random().Next(100000, 1000000);
                string body = "<p>Thank you, in order to proceed to the next steps you will need to verify your account.</p>";
                string note = "If you didn't request this code, you can safely ignore this email. Someone else might have typed your email address by mistake.";
                if (forgot == "reset")
                {
                    body = "<p>You have submitted a password reset request, make sure you are the owner of this account.</p>";
                    note = "If you are not the person making the request, please change the password to avoid future problems";
                }
                string main = "<!DOCTYPE html>\r\n" +
                    "<html lang=\"en\">\r\n" +
                    "<head>\r\n" +
                    "<meta charset=\"utf-8\" />\r\n " +
                    " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"\r\n/>\r\n" +
                    "<meta name=\"description\" content=\"\" />\r\n" +
                    "<meta name=\"author\" content=\"Mark Otto, Jacob Thornton, and Bootstrap contributors\"\r\n/>\r\n" +
                    "<meta name=\"generator\" content=\"Jekyll v4.1.1\" />\r\n<title>Floating labels example · Bootstrap</title>\r\n\r\n" +
                    "<link rel=\"canonical\" href=\"https://getbootstrap.com/docs/4.5/examples/floating-labels/\"\r\n/>\r\n\r\n" +
                    "<!-- Bootstrap core CSS -->\r\n" +
                    "<link href=\"/docs/4.5/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2\" crossorigin=\"anonymous\"\r\n/>\r\n\r\n" +
                    "<!-- Favicons -->\r\n" +
                    "<link rel=\"apple-touch-icon\" href=\"/docs/4.5/assets/img/favicons/apple-touch-icon.png\" sizes=\"180x180\"\r\n/>" +
                    "\r\n<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon-32x32.png\" sizes=\"32x32\" type=\"image/png\"\r\n/>\r\n" +
                    "<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon-16x16.png\" sizes=\"16x16\" type=\"image/png\"\r\n/>\r\n" +
                    "<link rel=\"manifest\" href=\"/docs/4.5/assets/img/favicons/manifest.json\" />\r\n" +
                    "<link rel=\"mask-icon\" href=\"/docs/4.5/assets/img/favicons/safari-pinned-tab.svg\" color=\"#563d7c\"\r\n/>\r\n" +
                    "<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon.ico\" />\r\n" +
                    "<meta name=\"msapplication-config\" content=\"/docs/4.5/assets/img/favicons/browserconfig.xml\"\r\n/>\r\n" +
                    "<meta name=\"theme-color\" content=\"#563d7c\" />\r\n<style> body {   font-family: Arial, sans-serif; } " +
                    ".container {   max-width: 600px;   font-weight: bold;   margin: auto;   width: 60%;   height: 50px;   text-align: center; } " +
                    ".form-signin {   background-color: rgb(236, 233, 233); } .ma-otp {   width: 80%;   height: 200px;   margin: auto;   padding: auto;   text-align: center;   background-color: white; } .otp-code {   padding-top: 80px; } " +
                    "h1 {   color: #86b817;   font-family: \"Nunito\", sans-serif; }\r\n</style>\r\n" +
                    "</head>\r\n" +
                    "<body style=\"background-color: #86b817;height:80vh;\">\r\n" +
                    "<div class=\"container\">\r\n" +
                    "<form class=\"form-signin\">\r\n" +
                    "<div class=\"text-center mb-4 m-5\" style='height:80vh; padding:20px 0;'>\r\n" +
                    "<h1 class=\"text-primary m-0\" style='font-size:xxx-large'>\r\nABCLEARN\r\n</h1>\r\n" +
                    "<p style=\"font-size: 23px\"><strong>OTP verification</strong></p>\r\n" +
                     body +
                    "\r\n <p>This code must not be given to anyone in any way</p> \n <p>Your OTP:</p>\r\n" +
                    "<div class=\"ma-otp\" style='margin: 30px auto;'>\r\n" +
                    $"<h1 class=\"otp-code\" style=\"color: red; font-size: xxx-large\">{OTP}</h1>\r\n" +
                    "</div>\r\n" +
                    "<div class=\"container\">" + note + "</div>\r\n" +
                    "</div>\r\n" +
                    "</form>\r\n" +
                    "</div>\r\n" +
                    "</body>\r\n" +
                    "</html>\r\n";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = main,
                    IsBodyHtml = true,
                })
                {
                    smtp.Send(message);
                }
                isSend = true;
            }
            catch
            {
                isSend = false;
            }
            return (isSend, OTP);
        }

        public bool sendContact(string email, string name, string contact, string subject)
        {
            bool isSend = false;
            try
            {
                DateTime time = CurrentDateTime.GetcurrentDateTime;
                const string frompass = "jtrrwlmnlafsxahp";
                var fromAddress = new MailAddress("abclearneducation@gmail.com");
                var toAddress = new MailAddress(email);
                string body = "<!DOCTYPE html>\r\n" +
                    "<html lang=\"en\">\r\n" +
                    "<head>\r\n" +
                    "<meta charset=\"utf-8\" />\r\n " +
                    " <meta name=\"viewport\" content=\"width=device-width, initial-scale=1, shrink-to-fit=no\"\r\n/>\r\n" +
                    "<meta name=\"description\" content=\"\" />\r\n" +
                    "<meta name=\"author\" content=\"Mark Otto, Jacob Thornton, and Bootstrap contributors\"\r\n/>\r\n" +
                    "<meta name=\"generator\" content=\"Jekyll v4.1.1\" />\r\n<title>Floating labels example · Bootstrap</title>\r\n\r\n" +
                    "<link rel=\"canonical\" href=\"https://getbootstrap.com/docs/4.5/examples/floating-labels/\"\r\n/>\r\n\r\n" +
                    "<!-- Bootstrap core CSS -->\r\n" +
                    "<link href=\"/docs/4.5/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2\" crossorigin=\"anonymous\"\r\n/>\r\n\r\n" +
                    "<!-- Favicons -->\r\n" +
                    "<link rel=\"apple-touch-icon\" href=\"/docs/4.5/assets/img/favicons/apple-touch-icon.png\" sizes=\"180x180\"\r\n/>" +
                    "\r\n<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon-32x32.png\" sizes=\"32x32\" type=\"image/png\"\r\n/>\r\n" +
                    "<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon-16x16.png\" sizes=\"16x16\" type=\"image/png\"\r\n/>\r\n" +
                    "<link rel=\"manifest\" href=\"/docs/4.5/assets/img/favicons/manifest.json\" />\r\n" +
                    "<link rel=\"mask-icon\" href=\"/docs/4.5/assets/img/favicons/safari-pinned-tab.svg\" color=\"#563d7c\"\r\n/>\r\n" +
                    "<link rel=\"icon\" href=\"/docs/4.5/assets/img/favicons/favicon.ico\" />\r\n" +
                    "<meta name=\"msapplication-config\" content=\"/docs/4.5/assets/img/favicons/browserconfig.xml\"\r\n/>\r\n" +
                    "<meta name=\"theme-color\" content=\"#563d7c\" />\r\n<style> body {   font-family: Arial, sans-serif; } " +
                    ".container {   max-width: 600px;   font-weight: bold;   margin: auto;   width: 60%;   height: 50px;   text-align: center; } " +
                    ".form-signin {   background-color: rgb(236, 233, 233); } .ma-otp {   width: 80%;   height: 200px;   margin: auto;   padding: auto;   text-align: center;   background-color: white; } .otp-code {   padding-top: 80px; } " +
                    "h1 {   color: #86b817;   font-family: \"Nunito\", sans-serif; }\r\n</style>\r\n" +
                    "</head>\r\n" +
                    "<body style=\"background-color: #86b817;height:80vh;\">\r\n" +
                    "<div class=\"container\">\r\n" +
                    "<form class=\"form-signin\">\r\n" +
                    "<div class=\"text-center mb-4 m-5\" style='height:auto; padding:20px 0;'>\r\n" +
                    "<h1 class=\"text-primary m-0\" style='font-size:xxx-large'>\r\nABCLEARN\r\n</h1>\r\n" +
                    "<div class=\"ma-otp\" style='margin: 30px auto;'>\r\n" +
                    "<h1  style=\"color: red; \">Thank you</h1>\r\n " +
                    "<p  style=\"color: red; \">Your Name: " + name + "</p>\r\n" +
                    "<p  style=\"color: red; \">Your email: " + email + "</p>\r\n" +
                    "<p  style=\"color: red; \">Your contact: " + contact + "</p>\r\n" +
                    "<p  style=\"color: red; \">Date create: " + time.ToString() + "</p>\r\n" +
                    "</div>\r\n" +
                    "<div class=\"container\">If you didn't request this code, you can safely ignore this email. Someone else might have typed your email address by mistake.</div>\r\n" +
                    "</div>\r\n" +
                    "</form>\r\n" +
                    "</div>\r\n" +
                    "</body>\r\n" +
                    "</html>\r\n";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                })
                {
                    smtp.Send(message);
                }
                isSend = true;
            }
            catch
            {
                isSend = false;
            }
            return isSend;
        }
    }
}
