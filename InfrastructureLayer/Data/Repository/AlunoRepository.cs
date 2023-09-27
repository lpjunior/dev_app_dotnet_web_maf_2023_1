using ApplicationLayer;
using DomainLayer.Interfaces.Repository;

namespace InfrastructureLayer.Data.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private static List<Aluno> _alunos = default!;

        public AlunoRepository() => _alunos = new List<Aluno>();

        public void Apaga(Guid id)
        {
            var alunoFinded = _alunos.Find(aluno => aluno.Id.ToString().Equals(id.ToString()))! ?? throw new Exception("Aluno não localizado");
            _alunos.Remove(alunoFinded);
        }
        
        public Aluno Atualiza(Aluno aluno)
        {
            var alunoFinded = _alunos.Find(alu => alu.Id.ToString().Equals(aluno.Id.ToString()))!;

            var idx = _alunos.IndexOf(alunoFinded);

            if (idx == -1) {
                throw new Exception("Aluno não localizado");
            }

            _alunos[idx].Nome = aluno.Nome;
            _alunos[idx].DataNascimento = aluno.DataNascimento;
            return _alunos[idx];
        }

        public IEnumerable<Aluno> Busca(string nome)
        {
            return _alunos.FindAll(aluno => aluno.Nome.ToLower().Contains(nome.ToLower()));
        }

        public IEnumerable<int> BuscaNotas(int matricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Aluno> Lista()
        {
            return _alunos;
        }

        public Aluno Registra(Aluno aluno)
        {
            aluno.Id = Guid.NewGuid();
            _alunos.Add(aluno);

            return aluno;
        }
    }
}
