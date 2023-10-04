namespace DomainLayer.Models
{
    public class Pessoa
    {
        public string Nome { get; set; } = default!;
        public string Cpf { get; set; } = default!;
        public string Rg { get; set; } = default!;
        public DateOnly DataNascimeneto { get; set; }
        public string Sexo { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}