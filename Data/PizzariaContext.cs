using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
namespace Pizzaria.Data;

public partial class PizzariaContext : DbContext
{
    public PizzariaContext()
    {
    }

    public PizzariaContext(DbContextOptions<PizzariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Pizzas> Pizzas { get; set; }

    public virtual DbSet<PizzasVenda> PizzasVenda { get; set; }

    public virtual DbSet<Venda> Venda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Cpf);

            entity.Property(e => e.Cpf).HasColumnName("CPF");
        });

        modelBuilder.Entity<Pizzas>(entity =>
        {
            entity.HasKey(e => e.IdPizza);

            entity.HasIndex(e => e.Sabor, "IX_Pizzas_Sabor").IsUnique();

            entity.Property(e => e.IdPizza).HasColumnName("idPizza");
        });

        modelBuilder.Entity<PizzasVenda>(entity =>
        {
            entity.HasKey(e => new { e.IdPizza, e.IdVenda });

            entity.Property(e => e.IdPizza).HasColumnName("idPizza");
            entity.Property(e => e.IdVenda).HasColumnName("idVenda");

            entity.HasOne(d => d.IdPizzaNavigation).WithMany(p => p.PizzasVenda).HasForeignKey(d => d.IdPizza);

            entity.HasOne(d => d.IdVendaNavigation).WithMany(p => p.PizzasVenda).HasForeignKey(d => d.IdVenda);
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.IdVenda);

            entity.Property(e => e.IdVenda)
                .ValueGeneratedNever()
                .HasColumnName("idVenda");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venda)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
