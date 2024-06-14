using Microsoft.AspNetCore.Mvc;

namespace Asset.Management.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    [HttpPost]
    public async Task Send()
    {

    }
}