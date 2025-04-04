using System.Net.Mail;
using System.Threading.Tasks;
using ApiPortalEtico.Application.Common.Models;

namespace ApiPortalEtico.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailMessage emailMessage);
    }
}

