using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;

namespace MyWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ProjectContext _projectContext;

    public ProjectController (ProjectContext projectContext)
    {
        _projectContext = projectContext;
    }

    [HttpGet]
    public ActionResult Get(int take = 10, int skip = 0)
    {
        return Ok(_projectContext?.EntityTypes?.OrderBy(p => p.ID).Skip(skip).Take(take));
    }

    [HttpGet("entities")]
    public ActionResult GetEntities(int take = 5, int skip = 0)
    {
        return Ok(_projectContext?.Approvals?.OrderBy(p => p.ID).Skip(skip).Take(take));
    }
}