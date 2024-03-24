using Microsoft.AspNetCore.Mvc;
using Shoppinglist_back.Dtos.ProductDtos;
using Shoppinglist_back.Services;

namespace Shoppinglist_back.Controllers;

[Controller]
[Route("[Controller]")]
public class ProductController : ControllerBase
{
    private ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var newProduct = await _productService.Create(dto);
        return CreatedAtAction(nameof(GetOneById), new { productId = newProduct.Id }, newProduct);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetOneById(int productId)
    {
        var newProduct = await _productService.GetOneById(productId);
        return Ok(newProduct);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery(Name = "name")] string productName, [FromQuery(Name = "Limit")] int limit = 5, [FromQuery(Name = "Skip")] int skip = 0)
    {
        var products = await _productService.GetAll(new GetAllProductDto 
                                                            { 
                                                                ProductName = productName,
                                                                Limit = limit,
                                                                Skip = skip
                                                            });
        return Ok(products);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateOne(int productId, [FromBody] UpdateProductDto dto)
    {
        var newProduct = await _productService.UpdateOne(productId, dto);
        return Ok(newProduct);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteOne(int productId)
    {
        await _productService.DeleteOne(productId);
        return NoContent();
    }
}
