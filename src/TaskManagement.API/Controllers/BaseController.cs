using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.API.Controllers
{
    public class BaseController<TEntity>(IBaseService<TEntity> baseClinicaService) : ControllerBase where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service = baseClinicaService;

        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            try
            {
                _service.Add(entity);

                return Ok(new { Mensagem = "Cadastrado(a) com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);

                return Ok(new { Mensagem = "Deletado(a) com sucesso." });
            }
            catch (Exception erro)
            {
                return BadRequest(new { Mensagem = erro.Message });
            }
        }
    }
}