using Microsoft.AspNetCore.Mvc;

namespace Asset.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{

    [HttpPost]
    public async Task CreateTransactionAsync()
    {
    }
}