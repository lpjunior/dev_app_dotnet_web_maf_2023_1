using ApplicationLayer;

namespace DomainLayer.Interfaces.Service
{
    public interface IAlunoService
    {
        Aluno Registra(Aluno aluno);
        IEnumerable<Aluno> Lista();
        IEnumerable<Aluno> Busca(string nome);
        Aluno Atualiza(Aluno aluno);
        void Apaga(Guid id);
    }
}
