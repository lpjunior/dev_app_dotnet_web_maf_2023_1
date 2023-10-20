using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApp.Models
{
    public class Livro
    {
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Titulo { get; set; } = default!;
        [StringLength(100)]
        public string Autor { get; set; } = default!;
        public int AnoPublicacao { get; set; }
        [StringLength(13)]
        public string ISBN { get; set; } = default!;
        public int QuantidadeDisponivel { get; set; }
    }
}