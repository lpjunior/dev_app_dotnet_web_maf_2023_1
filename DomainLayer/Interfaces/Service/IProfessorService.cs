using ApplicationLayer;
using DomainLayer.ViewModels;

namespace DomainLayer.Interfaces.Service
{
    public interface IProfessorService
    {
        Task Registra(ProfessorCadastroViewModel viewModel);
        Task<IEnumerable<Professor>> Lista();
        IEnumerable<Professor> Busca(string nome);
        Professor Atualiza(Professor professor);
        void Apaga(Guid id);
    }
}
