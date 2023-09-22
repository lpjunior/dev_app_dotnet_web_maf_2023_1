using DomainLayer.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApplicationLayer.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar o cadastro de alunos
    /// </summary>
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly ILogger<AlunoController> _logger;
        private readonly IAlunoService _alunoService;

        /// <inheritdoc />
        public AlunoController(ILogger<AlunoController> logger, IAlunoService alunoService)
        {
            _logger = logger;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Método responsável por listar todos os alunos cadastrados
        /// </summary>
        /// <returns>200, 400</returns>
        [HttpGet]
        [SwaggerOperation("Lista os alunos")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public ActionResult<IEnumerable<Aluno>> Lista()
        {
            var alunos = _alunoService.Lista();

            return Ok(alunos);
        }
    }
}