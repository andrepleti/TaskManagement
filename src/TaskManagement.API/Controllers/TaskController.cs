using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
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

        [HttpPost]
        public IActionResult Post([FromBody] TaskManagement.Domain.Entities.Task task)
        {
            try
            {
                _service.Add(task);

                return Ok(new { Mensagem = "Tarefa cadastrada com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }

        [HttpPut("{userId}")]
        public IActionResult Put(int userId, TaskManagement.Domain.Entities.Task task)
        {
            try
            {
                _service.Update(userId, task);

                return Ok(new { Mensagem = "Tarefa atualizada com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);

                return Ok(new { Mensagem = "Tarefa deletada com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }

        [HttpGet("averagecompleted")]
        [Authorize(Roles = "manager")]
        public IActionResult GetAverageTasksCompletedByUserOverLast30Days()
        {
            try
            {
                return Ok(_service.GetAverageTasksCompletedByUserOverLast30Days());
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }
    }
}