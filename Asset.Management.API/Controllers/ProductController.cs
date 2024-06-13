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
    public async Task<Product> CreateAsync([FromBody] Product product)
    {
        return _productService.CreateAsync(product);
    }
}