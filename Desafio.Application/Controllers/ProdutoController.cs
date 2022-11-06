using Desafio.Application.Model;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces;
using Desafio.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IBaseService<Produtos> _baseProdutoService;

        public ProdutoController(IBaseService<Produtos> baseProdutoService)
        {
            _baseProdutoService = baseProdutoService;
        }

        [HttpPost]
        public IActionResult Criar([FromBody] ProdutoModel produtos)
        {
            try
            {
                if (produtos is null)
                    return NotFound();

                if (produtos.DataFabricacao.Value.Date >= produtos.DataValidade.Value.Date)
                    return BadRequest("Data da fabricação não pode ser maior ou igual a data de validade.");

                return Execute(() => _baseProdutoService.Add<ProdutoModel, ProdutoModel, ProdutoValidator>(produtos));
            }
            catch
            {
                throw new Exception();
            }
        }


        [HttpPut]
        public IActionResult Alterar([FromBody] ProdutoModel produtos)
        {

            if (produtos is null)
                return NotFound();

            if (produtos.DataFabricacao.Value.Date >= produtos.DataValidade.Value.Date)
                return BadRequest("Data da fabricação não pode ser maior ou igual a data de validade.");

            return Execute(() => _baseProdutoService.Update<ProdutoModel, ProdutoModel, ProdutoValidator>(produtos));
        }

        [HttpDelete("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            try
            {
                if (codigo == 0)
                    return NotFound();


                var retorno = _baseProdutoService.GetById<ProdutoModel>(codigo);
                retorno.SituacaoProduto = "Inativo";

                return Execute(() =>
                {
                    _baseProdutoService.Update<ProdutoModel, ProdutoModel, ProdutoValidator>(retorno);
                    return NoContent();
                });

            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{codigo}")]
        public IActionResult GetProdutosCodigo(int codigo)
        {
            if (codigo == 0)
                return NotFound();

            return Execute(() => _baseProdutoService.GetById<ProdutoModel>(codigo));
        }


        [HttpGet("{numeropagina}/{quantidaderegistro}")]
        public IActionResult GetProdutos(int numeroPagina, int quantidadeRegistro, [FromQuery] ProdutoModel produtos)
        {
            try
            {
                return Execute(() =>
                {
                    var retornoConsulta = _baseProdutoService.Get<ProdutoModel>(numeroPagina, quantidadeRegistro);
                   
                    return Filtro(produtos, retornoConsulta);
                 
                });

            }
            catch
            {
                return BadRequest();
            }

        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IEnumerable<ProdutoModel> Filtro(ProdutoModel produtos, IEnumerable<ProdutoModel> retornoConsulta)
        {

            IEnumerable<ProdutoModel> ListaProdutos = new List<ProdutoModel>();
            bool PercorreFiltro = false;

            if (produtos.CnpjFornecedor != "" && produtos.CnpjFornecedor != null)
            {
                if (ListaProdutos.Any())
                {
                    ListaProdutos = ListaProdutos.Where(a => a.CnpjFornecedor == produtos.CnpjFornecedor).ToList();
                }
                else
                {
                    ListaProdutos = retornoConsulta.Where(a => a.CnpjFornecedor == produtos.CnpjFornecedor).ToList();
                }

                PercorreFiltro = true;
            }

            if (produtos.DataValidade != null)
            {
                if (ListaProdutos.Any())
                {
                    ListaProdutos = ListaProdutos.Where(a => a.DataValidade.Value.Date >= produtos.DataValidade.Value.Date).ToList();
                }
                else
                {
                    ListaProdutos = retornoConsulta.Where(a => a.DataValidade.Value.Date >= produtos.DataValidade.Value.Date).ToList();
                }

                PercorreFiltro = true;
            }

            if (produtos.SituacaoProduto != null && produtos.SituacaoProduto != "")
            {
                if (ListaProdutos.Any())
                {
                    ListaProdutos = ListaProdutos.Where(a => a.SituacaoProduto == produtos.SituacaoProduto).ToList();
                }
                else
                {
                    ListaProdutos = retornoConsulta.Where(a => a.SituacaoProduto == produtos.SituacaoProduto).ToList();

                }

                PercorreFiltro = true;
            }


            if (ListaProdutos.Any())
            {
                return ListaProdutos;
            }
            else if (PercorreFiltro is true)
            {
                return ListaProdutos;
            }
            else
            {
                return retornoConsulta;
            }
        }
    }
}
