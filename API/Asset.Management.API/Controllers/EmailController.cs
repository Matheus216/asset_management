using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Asset.Management.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("{daysToExpiration}")]
    public async Task<Result<string>> Send(string daysToExpiration)
    {
        try
        {
            var result = await _emailService.SendEmailAsync(daysToExpiration);
            return result; 
            
        }
        catch (Exception ex)
        {
            return new Result<string>(ex.Message);
        }
    }
}