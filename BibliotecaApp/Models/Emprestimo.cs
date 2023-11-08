using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApp.Models
{
    public class Emprestimo
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O ID do livro é obrigatório")]


        // foreign key(livroId) references Livro(id)

        public Guid LivroId { get; set; }
        [ForeignKey("LivroId")]
        public virtual Livro Livro { get; set; } = default!;

        [Required(ErrorMessage = "A data de retirada do livro é obrigatória")]
        [DataType(DataType.Date)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? DataDevolucao { get; set; }

        public DateOnly DataPrevistaDevolucao() => DataRetirada.AddDays(7);
    }
}
