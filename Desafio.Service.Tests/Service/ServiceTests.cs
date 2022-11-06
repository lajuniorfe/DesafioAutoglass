using AutoMapper;
using Desafio.Application.Controllers;
using Desafio.Application.Model;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces;
using Desafio.Service.Services;
using Desafio.Service.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Desafio.Service.Tests.Service
{
    public class ServiceTests
    {
        private readonly BaseService<Produtos> _baseProdutoService;
        public ServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdutoModel, Produtos>().AfterMap((src, dest) =>
                {
                    dest.Codigo_Fornecedor = src.CodigoFornecedor;
                    dest.Data_Fabricacao = src.DataFabricacao.Value;
                    dest.Data_Validade = src.DataValidade.Value;
                    dest.Descricao_Fornecedor = src.DescricaoFornecedor;
                    dest.Situacao = src.SituacaoProduto;
                    dest.Cnpj = src.CnpjFornecedor;

                });

                cfg.CreateMap<Produtos, ProdutoModel>().AfterMap((src, dest) =>
                {
                    dest.CodigoFornecedor = src.Codigo_Fornecedor;
                    dest.DataFabricacao = src.Data_Fabricacao;
                    dest.DataValidade = src.Data_Validade;
                    dest.DescricaoFornecedor = src.Descricao_Fornecedor;
                    dest.SituacaoProduto = src.Situacao;
                    dest.CnpjFornecedor = src.Cnpj;
                });
            });

            var mapper = config.CreateMapper();

            _baseProdutoService = new BaseService<Produtos>
                   (new Mock<IBaseRepository<Produtos>>().Object, mapper);
        }

        [Fact]
        public void CriarProdutoIDValida()
        {

            ProdutoModel produtos = new ProdutoModel()
            {
                Codigo = 1234,
                CnpjFornecedor = "12312414534543",
                CodigoFornecedor = 2,
                DataFabricacao = DateTime.Parse("2021-11-04"),
                DataValidade = DateTime.Parse("2022-11-04"),
                Descricao = "teste mock",
                DescricaoFornecedor = "dsdsadas",
                SituacaoProduto = "Ativo"
            };

            Assert.Throws<Exception>(() => _baseProdutoService.Add<ProdutoModel, ProdutoModel, ProdutoValidator>(produtos));
        }

        [Fact]
        public void BuscarProdutoIdZero()
        {
            Assert.Throws<Exception>(() => _baseProdutoService.GetById<ProdutoModel>(0));
        }

        [Fact]
        public void AlterarProdutoIdValida()
        {
            ProdutoModel produtos = new ProdutoModel()
            {
                Codigo = 1234,
                CnpjFornecedor = "12312414534543",
                CodigoFornecedor = 2,
                DataFabricacao = DateTime.Parse("2021-11-04"),
                DataValidade = DateTime.Parse("2022-11-04"),
                Descricao = "teste mock",
                DescricaoFornecedor = "dsdsadas",
                SituacaoProduto = "Ativo"
            };

            Assert.Throws<Exception>(() => _baseProdutoService.Update<ProdutoModel, ProdutoModel, ProdutoValidator>(produtos));
        }
    }


   
}
