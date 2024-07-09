using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Asset.Management.Domain.DTOs;

namespace Asset.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger logger; 

    public ProductController(IProductService productService, ILogger<ProductController> loggerProvider)
    {
        _productService = productService;
        logger = loggerProvider;
    }


    [HttpGet]
    public async Task<ActionResult<Result<IEnumerable<Product>>>> GetListAsync()
    {
        try
        {
            logger.LogInformation("Iniciado processo de busca..");
            var response = await _productService.GetListAsync();

            if ((response?.Success ?? false) == false)
                return BadRequest(response);

            if (response?.Success == true && !(response?.Data ?? new List<Product>()).Any())
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new Result<IEnumerable<Product>>(ex.Message));
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Result<Product>>> GetById(string id)
    {
        try
        {
            var response = await _productService.GetByIdAsync(id);

            if ((response?.Success ?? false) == false)
                return BadRequest(response);

            if (response?.Success == true && response?.Data == null)
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new Result<IEnumerable<Product>>(ex.Message));
        }
    }

    [HttpGet("expiration/{daysToExpiration}")]
    public async Task<ActionResult<Result<IEnumerable<Product>>>> GetCloseExpirationAsync(int daysToExpiration)
    {
        try
        {
            var response = await _productService.GetListCloseExpirationAsync(daysToExpiration);

            if ((response?.Success ?? false) == false)
                return BadRequest(response);

            if (response?.Success == true && !(response?.Data ?? new List<Product>()).Any())
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new Result<IEnumerable<Product>>(ex.Message));
        }
    }
}