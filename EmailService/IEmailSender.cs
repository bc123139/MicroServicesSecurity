using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSender
    {
        Task SendAsync(UserEmailOptions userEmailOptions);
    }
}
