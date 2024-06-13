using Asset.Management.Domain.Entities;
using Asset.Management.Domain.Interfaces;
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


    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] Product product)
    {
        return Ok(_productService.CreateAsync(product));
    }
}