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

            const string query = "INSERT INTO aluno(Nome, Matricula, DataNascimento) VALUES (@Nome, @Matricula, @DataNascimento);";
            var parameters = new
            {
                aluno.Nome,
                aluno.Matricula,
                DataNascimento = aluno.DataNascimento.ToString("yyyy-MM-dd")
            };
            
            try
            {
                await connection.ExecuteAsync(query, parameters);
            } catch (Exception exception) {
                _logger.LogError($"[AlunoRepository]-[Registra] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Registra] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Registra] -> [Finish]");
        }
        public async Task<IEnumerable<Aluno>> Lista()
        {
            _logger.LogInformation($"[AlunoRepository]-[Lista] -> [Start]");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM aluno;";

            try
            {
                var result = await connection.QueryAsync<Aluno>(query);
             
                _logger.LogInformation("[AlunoRepository]-[Lista] -> [Finish]");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Lista] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Lista] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public async Task Apaga(Guid id)
        {
            _logger.LogInformation($"[AlunoRepository]-[Apaga] -> [Start]: GUID -> {id}");

            using var connection = _context.CreateConnection;

            const string query = "DELETE FROM aluno WHERE Id = @Id";
            var param = new
            {
                Id = id
            };

            try
            {
                await connection.ExecuteAsync(query, param);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Apaga] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Apaga] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Apaga] -> [Finish]");
        }

        public async Task Atualiza(Aluno aluno)
        {
            _logger.LogInformation($"[AlunoRepository]-[Atualiza] -> [Start]: Payload -> {JsonSerializer.Serialize(aluno)}");

            using var connection = _context.CreateConnection;

            const string query = "UPDATE aluno SET Nome = @Nome, DataNascimento = @DataNascimento WHERE Matricula = @Matricula;";
            var parameters = new
            {
                aluno.Nome,
                aluno.Matricula,
                DataNascimento = aluno.DataNascimento.ToString("yyyy-MM-dd")
            };

            try
            {
                await connection.ExecuteAsync(query, parameters);
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Atualiza] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Atualiza] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }

            _logger.LogInformation("[AlunoRepository]-[Atualiza] -> [Finish]");
        }

        public async Task<IEnumerable<Aluno>> Busca(string nome)
        {
            _logger.LogInformation($"[AlunoRepository]-[Busca] -> [Start]: Nome -> {nome}");

            using var connection = _context.CreateConnection;

            const string query = "SELECT * FROM aluno WHERE nome LIKE @Nome;";
            var param = new {
                Nome = $"%{nome}%"
            };

            try
            {
                var result = await connection.QueryAsync<Aluno>(query, param);

                _logger.LogInformation("[AlunoRepository]-[Busca] -> [Finish]");
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError($"[AlunoRepository]-[Busca] -> [Exception]: Message -> {exception.Message}");
                _logger.LogError($"[AlunoRepository]-[Busca] -> [InnerException]: Message -> {exception.InnerException}");
                throw;
            }
        }

        public async Task<IEnumerable<int>> BuscaNotas(int matricula) => throw new NotImplementedException();
    }
}
