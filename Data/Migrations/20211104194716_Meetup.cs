using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaAPI.Data.Migrations
{
    public partial class Meetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Ativo = table.Column<int>(nullable: false),
                    Cargo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Autor = table.Column<string>(nullable: true),
                    Lancamento = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    EditoraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livros_Editoras_EditoraId",
                        column: x => x.EditoraId,
                        principalTable: "Editoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LivroId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    AluguelFeito = table.Column<DateTime>(nullable: false),
                    PrevisaoEntrega = table.Column<DateTime>(nullable: false),
                    Devolucao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alugueis_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alugueis_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Ativo", "Cargo", "Email", "Nome", "Password", "Username" },
                values: new object[] { 1, 1, "admin", "admin@admin.com", "admin", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "admin" });

            migrationBuilder.InsertData(
                table: "Editoras",
                columns: new[] { "Id", "Cidade", "Nome" },
                values: new object[] { 1, "Rio de Janeiro", "Rocco" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cidade", "Email", "Endereco", "Nome" },
                values: new object[] { 1, "Fortaleza", "teste@teste,com", "Rua X, 123", "David Cavalcante dos Santos" });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Autor", "EditoraId", "Lancamento", "Nome", "Quantidade" },
                values: new object[] { 1, "J.K Rowling", 1, 2001, "Harry Potter", 20 });

            migrationBuilder.InsertData(
                table: "Alugueis",
                columns: new[] { "Id", "AluguelFeito", "Devolucao", "LivroId", "PrevisaoEntrega", "UsuarioId" },
                values: new object[] { 1, new DateTime(2021, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), null, 1, new DateTime(2021, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_LivroId",
                table: "Alugueis",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_UsuarioId",
                table: "Alugueis",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EditoraId",
                table: "Livros",
                column: "EditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Editoras");
        }
    }
}
