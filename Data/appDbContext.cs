
using blogpessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace blogpessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("tb_alunos");
        }

        // Registrar DbSet - Objeto responsável por manipular a tabela
        public DbSet<Aluno> Alunos { get; set; } = null!;
    }
}