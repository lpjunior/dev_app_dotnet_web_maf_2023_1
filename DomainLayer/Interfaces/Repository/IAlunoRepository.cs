using ApplicationLayer;

namespace DomainLayer.Interfaces.Repository
{
    public interface IAlunoRepository
    {
        Task Registra(Aluno aluno);
        Task<IEnumerable<Aluno>> Lista();
        Task<IEnumerable<Aluno>> Busca(string nome);
        Task Atualiza(Aluno aluno);
        Task Apaga(Guid id);
        Task<IEnumerable<int>> BuscaNotas(int matricula);
    }
}
