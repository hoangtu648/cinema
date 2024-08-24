using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace cinema
{
    public class MailHelper
    {
        public bool Send(string from, string to, string subject, string content)
        {
            try
            {
                // Thông tin cấu hình SMTP
                var host = "smtp.gmail.com";
                var port = 587;  // Port số không có khoảng trắng
                var username = "atun123456789cu@gmail.com"; // Địa chỉ email của bạn
                var password = "qnwbzznkduhrogmw"; // Mật khẩu email của bạn (hoặc mật khẩu ứng dụng)
                var enableSsl = true; // Gmail yêu cầu SSL/TLS

                // Tạo đối tượng SmtpClient
                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enableSsl,
                    Credentials = new NetworkCredential(username, password)
                };

                // Tạo đối tượng MailMessage
                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = content,
                    IsBodyHtml = true
                };

                // Gửi email
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }

    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
