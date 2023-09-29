using ApplicationLayer;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;

namespace ServiceLayer
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository) => _repository = repository;

        public async Task Apaga(Guid id) => await _repository.Apaga(id);

        public async Task Atualiza(Aluno aluno) => await _repository.Atualiza(aluno);

        public async Task<IEnumerable<Aluno>> Busca(string nome) => await _repository.Busca(nome);

        public Task<IEnumerable<int>> BuscaNotas(int matricula) => throw new NotImplementedException();

        public async Task<IEnumerable<Aluno>> Lista() => await _repository.Lista();

        public async Task Registra(Aluno aluno) => await _repository.Registra(aluno);

        public async Task<string> SituacaoAsync(int matricula)
        {
            // busquei todas as notas do aluno cadastradas
            var notas = await _repository.BuscaNotas(matricula); // já me entrega a soma das notas
            
            // define a quantidade de exercicios propostos durante a UC
            var totalExercicios = 13;

            var somaNotas = notas.Sum(nota => nota);

            var media = somaNotas / totalExercicios;

            if(media < 4) { return "repovado"; }
            else if (media < 7) { return "recuperação"; }
            else { return "aprovado"; }
        }
    }
}
