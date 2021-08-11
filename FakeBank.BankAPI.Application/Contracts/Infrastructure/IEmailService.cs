using FakeBank.BankAPI.Application.Model.Mail;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
