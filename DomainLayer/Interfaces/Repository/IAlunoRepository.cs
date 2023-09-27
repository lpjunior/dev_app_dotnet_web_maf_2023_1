using ApplicationLayer;

namespace DomainLayer.Interfaces.Repository
{
    public interface IAlunoRepository
    {
        Aluno Registra(Aluno aluno);
        IEnumerable<Aluno> Lista();
        IEnumerable<Aluno> Busca(string nome);
        Aluno Atualiza(Aluno aluno);
        void Apaga(Guid id);
        IEnumerable<int> BuscaNotas(int matricula);
    }
}
