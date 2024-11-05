namespace BongOliver.Services.MailService
{
    public interface IMailService
    {
        bool SendEmail(string toMail, string subject, string body);
    }
}
