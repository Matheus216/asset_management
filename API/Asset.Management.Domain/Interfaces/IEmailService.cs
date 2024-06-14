namespace Asset.Management.Domain.Interfaces;


public interface IEmailService
{
    Task<bool> SendEmailAsync();
}