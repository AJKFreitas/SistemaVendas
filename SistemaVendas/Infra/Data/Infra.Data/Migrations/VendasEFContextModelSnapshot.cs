﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaVendas.Infra.Data;

namespace SistemaVendas.Infra.Data.Migrations
{
    [DbContext(typeof(VendasEFContext))]
    partial class VendasEFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SistemaVendas.Core.Domains.Auth.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAdmin")
                        .HasColumnName("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Role")
                        .HasColumnName("Role")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Senha")
                        .HasColumnName("Senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("TB_Usuario");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Fornecedores.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("CNPJ")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("TB_Fornecedor");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Fornecedores.Entities.ProdutoFornecedor", b =>
                {
                    b.Property<Guid>("IdFornecedor")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.HasKey("IdFornecedor", "IdProduto");

                    b.HasIndex("IdProduto");

                    b.ToTable("ProdutoFornecedor");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Produtos.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("TB_Produto");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Fornecedores.Entities.ProdutoFornecedor", b =>
                {
                    b.HasOne("SistemaVendas.Core.Domains.Fornecedores.Entities.Fornecedor", "Fornecedor")
                        .WithMany("ProdutosFornecidos")
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVendas.Core.Domains.Produtos.Entities.Produto", "Produto")
                        .WithMany("ProdutoFornecedores")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
