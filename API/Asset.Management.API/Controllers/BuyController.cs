using Microsoft.AspNetCore.Mvc;

namespace Asset.Manement.API; 

[ApiController]
[Route("api/v1/[controller]")]
public class BuyController : Controller 
{

   [HttpGet]
   public ActionResult GetAsync()
   {
      return Ok("200");
   }
}