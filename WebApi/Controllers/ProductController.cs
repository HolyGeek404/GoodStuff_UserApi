using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductDao productDao) : Controller
{
    [HttpGet]
    [Route("getallproductsbytype")]
    public IActionResult GetAllProductsByType(string type)
    {
        if (string.IsNullOrEmpty(type))
            return BadRequest("Product type cannot be null or empty.");

        var products = productDao.GetAllProductsByType(type);
        if (products == null || !products.Any())
            return NotFound($"No products found for type: {type}");

        return Ok(products);
    }

}
