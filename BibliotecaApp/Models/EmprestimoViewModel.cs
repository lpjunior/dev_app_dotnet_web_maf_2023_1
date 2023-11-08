﻿using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models
{
    public class EmprestimoViewModel
    {
        [Required(ErrorMessage = "O livro é obrigatório.")]
        [Display(Name = "Livro")]
        public Guid LivroId { get; set; }

        [Display(Name = "Titulo do Livro")]
        public string Titulo { get; set; } = default!;

        [Required]
        [Display(Name = "Livro Disponível")]
        public bool LivroDisponivel { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Retirada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DataRetirada { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Prevista de Devolução")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly DataPrevistaDevolucao { get; set; }
    }
}