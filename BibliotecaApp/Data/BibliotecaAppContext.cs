using BibliotecaApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using System.Data.SqlClient;

namespace BibliotecaApp.Data
{
    public class BibliotecaAppContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public BibliotecaAppContext(DbContextOptions<BibliotecaAppContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Livro> Livros { get; set; } = default!;
        public DbSet<Emprestimo> Emprestimos { get; set; } = default!;
        public DbSet<Reserva> Reservas { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbSettings = _configuration.GetSection("DatabaseSettings");

            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = dbSettings["Server"],
                InitialCatalog = dbSettings["Database"],
                IntegratedSecurity = bool.Parse(dbSettings["TrustedConnection"]!),
                MultipleActiveResultSets = bool.Parse(dbSettings["MultipleActiveResultSets"]!)
            };

            optionsBuilder.UseSqlServer(connectionString.ToString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Conversor para DateOnly
            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                convertToProviderExpression: d => d.ToDateTime(TimeOnly.MinValue),
                convertFromProviderExpression: d => DateOnly.FromDateTime(d));

            // Conversor para DateOnly? (nullable)
            var dateOnlyNullableConverter = new ValueConverter<DateOnly?, DateTime?>(
                convertToProviderExpression: d => d.HasValue ? d.Value.ToDateTime(TimeOnly.MinValue) : null,
                convertFromProviderExpression: d => d.HasValue ? DateOnly.FromDateTime(d.Value) : (DateOnly?)null);

            // Configurar o conversor para cada propriedade individual
            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.DataRetirada)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            modelBuilder.Entity<Emprestimo>()
                .Property(e => e.DataDevolucao)
                .HasConversion(dateOnlyNullableConverter)
                .HasColumnType("date");
        }
    }
}
