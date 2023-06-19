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
                const string subject = "Verify your email address";
                var fromAddress = new MailAddress("abclearneducation@gmail.com");
                var toAddress = new MailAddress(email);
                OTP = new Random().Next(100000, 1000000);
                string body = $"To finish setting up your ABCLearn account, we just need to make sure this email address is yours.\n\nTo verify your email address use this security code: {OTP} " +
                    $"\n\n If you didn't request this code, you can safely ignore this email. Someone else might have typed your email address by mistake." +
                    $"\n\nThanks,\nThe ABCLearn account team";
                if (forgot == "reset")
                {
                    body = $"your confirmation code to reset password is: {OTP}" +
                         $"\n\n If you didn't request this code, you can safely ignore this email. Someone else might have typed your email address by mistake." +
                        $"\n\nThanks,\nThe ABCLearn account team"; ;
                }
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
    }
}
