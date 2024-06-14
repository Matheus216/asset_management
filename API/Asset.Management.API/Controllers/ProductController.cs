using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Asset.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetListAsync()
    {
        return Ok(await _productService.GetListAsync());
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetById(string id)
    {
        return Ok(await _productService.GetById(id));
    }

    [HttpGet("expiration/{daysToExpiration}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetCloseExpirationAsync(int daysToExpiration)
    {
        return Ok(await _productService.GetListCloseExpirationAsync(daysToExpiration));
    }
}