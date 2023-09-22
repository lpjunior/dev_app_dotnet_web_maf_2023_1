using ApplicationLayer;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;

namespace ServiceLayer
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository) => _repository = repository;

        public void Apaga(Guid id) => _repository.Apaga(id);

        public Aluno Atualiza(Aluno aluno) => _repository.Atualiza(aluno);

        public IEnumerable<Aluno> Busca(string nome) => _repository.Busca(nome);

        public IEnumerable<Aluno> Lista() => _repository.Lista();

        public Aluno Registra(Aluno aluno) => _repository.Registra(aluno);

        public string Situacao(int matricula)
        {
            // busquei todas as notas do aluno cadastradas
            var notas = _repository.BuscaNotas(matricula); // já me entrega a soma das notas
            
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
