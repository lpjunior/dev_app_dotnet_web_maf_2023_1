using BibliotecaApp.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models;

[Index(nameof(Email), IsUnique = true)]
public class Usuario : IdentityUser
{
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [Display(Name = "Tipo")]
    public UsuarioType Type { get; set; } = UsuarioType.Usuario;
    
    [Display(Name = "Data de Criação")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Data de Modificação")]
    public DateTime? ModifiedAt { get; set; }
    
    [Display(Name = "Criado Por")]
    public Guid? CreationUser { get; set; }

    [Display(Name = "Modificado Por")]
    public Guid? ChangedUser { get; set; }
}