using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DataContext _salesContext;

    public ProductsController (DataContext salesContext)
    {
        _salesContext = salesContext;
    }

    [HttpGet]
    public ActionResult Get(int take = 10, int skip = 0)
    {
        return Ok(_salesContext?.Products?.OrderBy(p => p.ProductId).Skip(skip).Take(take));
    }
}