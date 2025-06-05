using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Controllers;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;

namespace SagasBack.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : BaseController<Project>
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(int userId)
        {
            try
            {
                return Ok(_service.GetListBy(userId));
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }
    }
}