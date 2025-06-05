using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Controllers;
using TaskManagement.Domain.Interfaces.Services;

namespace SagasBack.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : BaseController<TaskManagement.Domain.Entities.Task>
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service) : base(service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get(int projectId)
        {
            try
            {
                return Ok(_service.GetListBy(projectId));
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }

        [HttpPut]
        public IActionResult Put(TaskManagement.Domain.Entities.Task task)
        {
            try
            {
                _service.Update(task);

                return Ok(new { Mensagem = "Tarefa atualizada com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }
    }
}