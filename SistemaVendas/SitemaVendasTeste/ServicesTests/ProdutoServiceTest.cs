using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SistemaVendas.Aplication.Services.Produtos;
using SistemaVendas.Core.Domains.Produtos.Entities;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SitemaVendasTeste.ServicesTests
{
    [TestClass]
    public class ProdutoServiceTest
    {
        private ProdutoService _produtoService;
        private Mock<IProdutoRepository> _mockProdutoRepository;
        private Mock<IMapper> _mockMapper;

        [TestInitialize]
        public void Setup()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [TestMethod]
        public async Task BuscarProdutosTest()
        {
            var listProdutos = new List<Produto> { new Produto { Nome = "Teste" }};

            _mockProdutoRepository.Setup(x => x.BuscarTodos()).ReturnsAsync(listProdutos);
        }
    }
}
