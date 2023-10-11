namespace DomainLayer.ViewModels
{
    public class ProfessorCadastroViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public int Matricula { get; set; }
        public IEnumerable<string> Conhecimentos { get; set; } = default!;
    }
}
