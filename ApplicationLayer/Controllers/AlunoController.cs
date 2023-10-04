using DomainLayer.Interfaces.Repository;
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
        public async Task<ActionResult<IEnumerable<Aluno>>> ListaAsync()
        {
            var alunos = await _alunoService.Lista();

            return Ok(alunos);
        }

        /// <summary>
        /// Método responsável por registrar um aluno no sistema
        /// </summary>
        /// <returns>201, 400</returns>
        [HttpPost]
        [SwaggerOperation("Registra aluno")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> RegistraAsync([FromBody] Aluno aluno)
        {
            await _alunoService.Registra(aluno);

            return Created("", "");
        }

        /// <summary>
        /// Método responsável por buscar aluno pelo nome
        /// </summary>
        /// <returns>200, 400</returns>
        [HttpGet("{nome}")]
        [SwaggerOperation("Busca o(s) aluno(s)")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<ActionResult<IEnumerable<Aluno>>> BuscaAsync([FromRoute] string nome)
        {
            var alunos = await _alunoService.Busca(nome);

            return Ok(alunos);
        }

        /// <summary>
        /// Método responsável por atualizar um aluno no sistema
        /// </summary>
        /// <returns>204, 400</returns>
        [HttpPatch()]
        [SwaggerOperation("Atualiza aluno")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> AtualizaAsync([FromBody] Aluno aluno)
        {
            await _alunoService.Atualiza(aluno);

            return NoContent();
        }

        /// <summary>
        /// Método responsável por apaga um aluno no sistema
        /// </summary>
        /// <returns>204, 400</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation("Apaga aluno")]
        [SwaggerResponse(204)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> ApagaAsync([FromRoute] Guid id)
        {
            await _alunoService.Apaga(id);

            return NoContent();
        }
    }
}