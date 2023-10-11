using ApplicationLayer;
using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;
using DomainLayer.ViewModels;

namespace ServiceLayer
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _repository;

        public ProfessorService(IProfessorRepository repository) => _repository = repository;

        /// <summary>
        /// Método responsável por salvar os dados de um professor
        /// </summary>
        /// <param name="professor"></param>
        /// <returns>professor</returns>
        public async Task Registra(ProfessorCadastroViewModel viewModel) => await _repository.Registra(viewModel);

        public async Task<IEnumerable<Professor>> Lista() => await _repository.Lista();

        public IEnumerable<Professor> Busca(string nome) => _repository.Busca(nome);

        Professor IProfessorService.Atualiza(Professor professor) => _repository.Atualiza(professor);

        void IProfessorService.Apaga(Guid id) => _repository.Apaga(id);
    }
}
