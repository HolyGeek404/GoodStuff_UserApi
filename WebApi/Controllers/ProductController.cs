using Microsoft.AspNetCore.Mvc;
using Model.DataAccess.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductDao productDao) : Controller
{
    [HttpGet]
    [Route("getallproductsbytype")]
    public async Task<IActionResult> GetAllProductsByType(string type)
    {
        if (string.IsNullOrEmpty(type))
            return BadRequest("Product type cannot be null or empty.");

        var products = await productDao.GetAllProductsByType(type.ToUpper());
        if (products.Length==0)
            return NotFound($"No products found for type: {type}");

        return Ok(products);
    }
}
