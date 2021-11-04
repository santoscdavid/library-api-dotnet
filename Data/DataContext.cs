using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LivrariaAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Aluguel>()
                .HasKey(A => A.Id);

            builder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Usuario>()
                .HasData(new List<Usuario>(){
                    new Usuario(1, "David Cavalcante dos Santos",
                             "teste@teste,com", "Fortaleza",
                             "Rua X, 123"),
                });
            builder.Entity<Editora>()
                .HasData(new List<Editora>(){
                    new Editora(1, "Rocco", "Rio de Janeiro"),
                });
            builder.Entity<Livro>()
                .HasData(new List<Livro>(){
                    new Livro(1, "Harry Potter", "J.K Rowling", 2001, 20, 1),
                });
            builder.Entity<Aluguel>()
                .HasData(new List<Aluguel>(){
                    new Aluguel(1,1,1,DateTime.Parse("2021-02-22"),
                                    DateTime.Parse("2021-02-22"),
                                    DateTime.Parse("2021-03-22"))
                });
            builder.Entity<Admin>()
                .HasData(new List<Admin>(){
                    new Admin(1, "admin", "admin", sha256_hash("admin"), 1, "admin", "admin@admin.com" )
                });
        }
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}