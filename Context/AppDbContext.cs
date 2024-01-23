using APIAlunos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIAlunos.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>().HasData(
            new Aluno
            {
                Id = 1,
                Nome = "Lucas Mol",
                Email = "lucasmolcms@gmail.com",
                Idade = 20
            },
            new Aluno
            {
                Id = 2,
                Nome = "Felipe Domingues",
                Email = "felimedm@debarry.cloud",
                Idade = 21
            }
        );
    }
}
