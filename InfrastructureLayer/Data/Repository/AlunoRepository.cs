using ApplicationLayer;
using DomainLayer.Interfaces.Repository;

namespace InfrastructureLayer.Data.Repository
{
    internal class AlunoRepository : IAlunoRepository
    {
        private static List<Aluno> _alunos = default!;

        public AlunoRepository() => _alunos = new List<Aluno>();

        public void Apaga(Guid id)
        {
            throw new NotImplementedException();
        }

        public Aluno Atualiza(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> Busca(string nome)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> BuscaNotas(int matricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> Lista()
        {
            throw new NotImplementedException();
        }

        public Aluno Registra(Aluno aluno)
        {
            throw new NotImplementedException();
        }
    }
}
