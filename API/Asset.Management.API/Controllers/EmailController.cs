using Asset.Management.Domain.Interfaces;
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

    [HttpPost]
    public async Task Send()
    {
        var result = await _emailService.SendEmailAsync();
    }
}