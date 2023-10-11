using ApplicationLayer;
using DomainLayer.ViewModels;

namespace DomainLayer.Interfaces.Repository
{
    public interface IProfessorRepository
    {
        Task Registra(ProfessorCadastroViewModel viewModel);
        Task<IEnumerable<Professor>> Lista();
        IEnumerable<Professor> Busca(string nome);
        Professor Atualiza(Professor professor);
        void Apaga(Guid id);
    }
}
