using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models
{
    public class Reserva
    {
        [Key]
        [Required]
        public Guid LivroId { get; set; }
    }
}