namespace BibliotecaApp.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string Autor { get; set; } = default!;
        public int AnoPublicacao { get; set; }
        public string ISBN { get; set; } = default!;
        public int QuantidadeDisponivel { get; set; }

    }
}
