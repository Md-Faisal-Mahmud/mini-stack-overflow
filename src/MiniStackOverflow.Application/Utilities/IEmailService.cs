namespace MiniStackOverflow.Application.Utilities
{
    public interface IEmailService
    {
        Task SendSingleEmail(string receiverName, string receiverEmail,
            string subject, string body);
    }
}