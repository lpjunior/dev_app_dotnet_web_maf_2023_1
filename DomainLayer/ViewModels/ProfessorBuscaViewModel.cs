namespace DomainLayer.ViewModels
{
    public class ProfessorBuscaViewModel
    {
        public Guid Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Conhecimento { get; set; } = string.Empty;
    }
}
