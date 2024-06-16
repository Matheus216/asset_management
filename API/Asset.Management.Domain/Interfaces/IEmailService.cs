using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;


public interface IEmailService
{
    Task<Result<string>> SendEmailAsync(string daysToExpiration);
}