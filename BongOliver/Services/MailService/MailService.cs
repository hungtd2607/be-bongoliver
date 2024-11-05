using BongOliver.Repositories.UserRepository;
using BongOliver.Services.TokenService;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BongOliver.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        public MailService(IUserRepository userRepository, ITokenService tokenService, IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _config = config;
        }
        public bool SendEmail(string toMail, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:From").Value));
                email.To.Add(MailboxAddress.Parse(toMail));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Text) { Text = body };

                using var smtp = new SmtpClient();

                smtp.Connect(_config.GetSection("Email:Host").Value, int.Parse(_config.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("Email:From").Value, _config.GetSection("Email:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
