using ApplicationLayer;
using Dapper;
using DomainLayer.Interfaces.Infrastructure;
using DomainLayer.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace InfrastructureLayer.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbContext _context;
        private readonly ILogger<AlunoRepository> _logger;

        public AlunoRepository(ILogger<AlunoRepository> logger, IDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Registra(Aluno aluno)
        {
            _logger.LogInformation($"[AlunoRepository]-[Registra] -> [Start]: Payload -> {JsonSerializer.Serialize(aluno)}");

            using var connection = _context.CreateConnection;

            var query = "INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES (@Nome, @Matricula, @DataNascimento);";

            try
            {
                await connection.ExecuteAsync(query, aluno);
            } catch (Exception exception) {
                _logger.LogError($"[AlunoRepository]-[Registra] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Registra] -> [InnerException]: Message -> {exception.InnerException}");

            }

            _logger.LogInformation("[AlunoRepository]-[Registra] -> [Finish]");
        }

        public Task Apaga(Guid id) => throw new NotImplementedException();

        public Task Atualiza(Aluno aluno) => throw new NotImplementedException();

        public Task<IEnumerable<Aluno>> Busca(string nome) => throw new NotImplementedException();

        public Task<IEnumerable<int>> BuscaNotas(int matricula) => throw new NotImplementedException();

        public Task<IEnumerable<Aluno>> Lista() => throw new NotImplementedException();
    }
}
