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
                .HasAnnotation("ProductVersion", "3.1.2")
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

            modelBuilder.Entity("SistemaVendas.Core.Domains.Clientes.Entities.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("CPF")
                        .HasColumnName("CPF")
                        .HasColumnType("bigint");

                    b.Property<string>("Endereco")
                        .HasColumnName("Endereco")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Telefone")
                        .HasColumnName("Telefone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("TB_Cliente");
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

                    b.ToTable("TB_Produto_Fornecedor");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.ItemOrdemCompra", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdOrdemCompra")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("char(36)");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.Property<long>("Quantidade")
                        .HasColumnType("bigint");

                    b.Property<double>("SubTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("IdOrdemCompra");

                    b.HasIndex("IdProduto");

                    b.ToTable("TB_ItemOrdemCompra");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.ItemPedidoVenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdPedido")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdProduto")
                        .HasColumnType("char(36)");

                    b.Property<double>("Preco")
                        .HasColumnType("double");

                    b.Property<long>("Quantidade")
                        .HasColumnType("bigint");

                    b.Property<double>("SubTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProduto");

                    b.ToTable("TB_ItemPedido");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.OrdemCompra", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdFornecedor")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdUsuarioLogado")
                        .HasColumnType("char(36)");

                    b.Property<string>("Nota")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("IdFornecedor");

                    b.HasIndex("IdUsuarioLogado");

                    b.ToTable("TB_OrdemCompra");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.PedidoVenda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("IdCliente")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IdUsuarioLogado")
                        .HasColumnType("char(36)");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdUsuarioLogado");

                    b.ToTable("TB_Pedido");
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Produtos.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("Codigo")
                        .HasColumnType("bigint");

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

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.ItemOrdemCompra", b =>
                {
                    b.HasOne("SistemaVendas.Core.Domains.Pedidos.Entities.OrdemCompra", "OrdemCompra")
                        .WithMany("ItemsOrdemCompra")
                        .HasForeignKey("IdOrdemCompra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVendas.Core.Domains.Produtos.Entities.Produto", "Produto")
                        .WithMany("ItemOrdemCompras")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.ItemPedidoVenda", b =>
                {
                    b.HasOne("SistemaVendas.Core.Domains.Pedidos.Entities.PedidoVenda", "Pedido")
                        .WithMany("ItemPedidos")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaVendas.Core.Domains.Produtos.Entities.Produto", "Produto")
                        .WithMany("ItemPedidos")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.OrdemCompra", b =>
                {
                    b.HasOne("SistemaVendas.Core.Domains.Fornecedores.Entities.Fornecedor", "Fornecedor")
                        .WithMany("OrdemCompras")
                        .HasForeignKey("IdFornecedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SistemaVendas.Core.Domains.Auth.Entities.Usuario", "UsuarioLogado")
                        .WithMany("OrdemCompras")
                        .HasForeignKey("IdUsuarioLogado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SistemaVendas.Core.Domains.Pedidos.Entities.PedidoVenda", b =>
                {
                    b.HasOne("SistemaVendas.Core.Domains.Clientes.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SistemaVendas.Core.Domains.Auth.Entities.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdUsuarioLogado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
