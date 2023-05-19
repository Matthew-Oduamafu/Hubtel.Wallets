using Hubtel.Wallets.Application.Models;
using System.Threading.Tasks;

namespace Hubtel.Wallets.Application.Contracts.Infrastructure.Mail
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}