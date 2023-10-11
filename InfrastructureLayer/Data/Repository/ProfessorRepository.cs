using ApplicationLayer;
using Dapper;
using DomainLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces.Repository;
using DomainLayer.ViewModels;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InfrastructureLayer.Data.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {

        private readonly IDbContext _context;
        private readonly ILogger<ProfessorRepository> _logger;

        public ProfessorRepository(ILogger<ProfessorRepository> logger, IDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Método responsável por salvar os dados de um professor
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task Registra(ProfessorCadastroViewModel viewModel)
        {
            _logger.LogInformation($"[ProfessorRepository]-[Registra] -> [Start]: Payload -> {JsonSerializer.Serialize(viewModel)}");

            using var connection = _context.CreateConnection;

            const string query = @"MERGE INTO Professor AS target
                                   USING (VALUES (@Nome, @Matricula)) AS source (Nome, Matricula)
                                   ON (target.Matricula = source.Matricula)
                                   WHEN MATCHED THEN
	                                   UPDATE SET Nome = target.Nome
                                   WHEN NOT MATCHED THEN
	                                   INSERT (Nome, Matricula) VALUES (source.Nome, source.Matricula)
                                   OUTPUT inserted.Id;";

            var parameters = new
            {
                viewModel.Nome,
                viewModel.Matricula
            };

            try
            {
                var professorId = await connection.QuerySingleAsync<Guid>(query, parameters);

                const string queryConhecimento = @"MERGE INTO Professor_Conhecimento AS target
                                   USING (VALUES (@Conhecimento, @ProfessorId)) AS source (Conhecimento, Professor_Id)
                                   ON ((target.Conhecimento = source.Conhecimento) AND (target.Professor_Id = source.Professor_Id))
                                   WHEN MATCHED THEN
	                                  UPDATE SET Conhecimento = target.Conhecimento
                                   WHEN NOT MATCHED THEN
	                                  INSERT (Conhecimento, Professor_Id) VALUES (source.Conhecimento, source.Professor_Id);";

                foreach (var conhecimento in viewModel.Conhecimentos)
                {
                    await connection.ExecuteAsync(
                        queryConhecimento, 
                        new {
                            Conhecimento = conhecimento,
                            ProfessorId = professorId 
                    });
                }
            }
            catch (Exception exception)
            {
                _logger.LogError($"[ProfessorRepository]-[Registra] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[ProfessorRepository]-[Registra] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[ProfessorRepository]-[Registra] -> [Finish]");
        }

        public async Task<IEnumerable<Professor>> Lista()
        {
            _logger.LogInformation($"[ProfessorRepository]-[Lista] -> [Start]");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM view_professor_conhecimento;";

            try
            {
                var result = await connection.QueryAsync<ProfessorBuscaViewModel>(query);
                var professorGrouped = result.GroupBy(pro => pro.Matricula).ToList();
                var professores = new List<Professor>();

                foreach (var group in professorGrouped)
                {
                    var prof = new Professor
                    {
                        Id = group.FirstOrDefault()!.Id,
                        Nome = group.FirstOrDefault()!.Nome,
                        Matricula = group.FirstOrDefault()!.Matricula,
                        Conhecimentos = group.Select(p => p.Conhecimento)
                    };

                    professores.Add(prof);
                }

                _logger.LogInformation("[ProfessorRepository]-[Lista] -> [Finish]");
                return professores;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[ProfessorRepository]-[Lista] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[ProfessorRepository]-[Lista] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public IEnumerable<Professor> Busca(string nome)
        {
            throw new NotImplementedException();
        }

        public Professor Atualiza(Professor professor)
        {
            throw new NotImplementedException();
        }

        public void Apaga(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
