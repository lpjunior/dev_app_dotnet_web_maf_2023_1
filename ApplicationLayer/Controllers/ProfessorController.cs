using DomainLayer.Interfaces.Service;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApplicationLayer.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar o cadastro de professores
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ILogger<ProfessorController> _logger;
        private readonly IProfessorService _professorService;

        /// <inheritdoc />
        public ProfessorController(ILogger<ProfessorController> logger, IProfessorService professorService)
        {
            _logger = logger;
            _professorService = professorService;
        }

        /// <summary>
        /// Método responsável por cadastrar um professor
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>201, 400</returns>
        [HttpPost]
        [SwaggerOperation("Cadastra um novo professor")]
        [SwaggerResponse(201)] // create
        [SwaggerResponse(400)] // bad request
        public async Task<ActionResult> RegisterAsync([FromBody] ProfessorCadastroViewModel viewModel)
        {
            if (!viewModel.Conhecimentos.Any())
            {
                return BadRequest();
            }

            try
            {
                await _professorService.Registra(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ProfessorController]-[RegisterAsync] -> [Exception]: Message -> {ex.Message}");
                return BadRequest();
            }

            return Created("", "");
        }


        /// <summary>
        /// Método responsável por retornar a lista de professores
        /// </summary>
        /// <returns>200, 400</returns>
        [HttpGet("lista")]
        [SwaggerOperation("Lista os prefessores")]
        [SwaggerResponse(200)] // ok
        [SwaggerResponse(400)] // bad request
        public async Task<ActionResult<IEnumerable<Professor>>> ListaAsync()
        {
            var professores = await _professorService.Lista();

            return Ok(professores);
        }

        /// <summary>
        /// Método responsável por retornar um professor localizado pelo nome
        /// </summary>
        /// <param name="nome">Nome do professor</param>
        /// <returns>200, 400</returns>
        [HttpGet("busca")]
        [SwaggerOperation("busca um professor pelo nome")]
        [SwaggerResponse(200)] // ok
        [SwaggerResponse(400)] // bad request
        public ActionResult<IEnumerable<Professor>> Busca(string nome)
        {
            return Ok(_professorService.Busca(nome));
        }

        /// <summary>
        /// Método responsável por atualizar um professor
        /// </summary>
        /// <param name="professor">Objeto de professor</param>
        /// <returns>200, 400</returns>
        [HttpPatch()]
        [SwaggerOperation("atualiza um professor")]
        [SwaggerResponse(200)] // ok
        [SwaggerResponse(400)] // bad request
        public ActionResult<Professor> Atualiza(Professor professor)
        {
            return Ok(_professorService.Atualiza(professor));
        }

        /// <summary>
        /// Método responsável por apagar um professor localizado pelo id
        /// </summary>
        /// <param name="id">Identificador único do professor</param>
        /// <returns>202, 400</returns>
        [HttpDelete()]
        [SwaggerOperation("apaga um professor pelo id")]
        [SwaggerResponse(202)] // aceito
        [SwaggerResponse(400)] // bad request
        public ActionResult Deleta(Guid id)
        {
            _professorService.Apaga(id);
            return Accepted();
        }
    }
}