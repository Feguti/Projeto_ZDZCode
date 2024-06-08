using APITeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace APITeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {

        private static int ProximoId = 0;
        public static List<DadosProduto> ListaDadosProduto = new List<DadosProduto>();


        [HttpPost("registrar")]
        public DadosProduto Post([FromBody] DadosProduto dadosProduto)
        {
            dadosProduto.Id = ProximoId++;
            ListaDadosProduto.Add(dadosProduto);
            return dadosProduto;
        }

        [HttpGet()]
        public IEnumerable<DadosProduto> Get()
        {
            return ListaDadosProduto.ToList();
        }

        [HttpGet("consultar")]
        public ActionResult<IEnumerable<DadosProduto>> Search([FromQuery] string nome = null, string descricao = null)
        {
            var query = ListaDadosProduto.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(f => f.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(descricao))
            {
                query = query.Where(f => f.Descricao.Contains(descricao, StringComparison.OrdinalIgnoreCase));
            }


            return query.ToList();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DadosProduto dadosProduto)
        {
            var produto = ListaDadosProduto.FirstOrDefault(f => f.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            produto.Nome = dadosProduto.Nome;
            produto.Tipo = dadosProduto.Tipo;
            produto.Categoria = dadosProduto.Categoria;
            produto.Descricao = dadosProduto.Descricao;


            return Ok(produto);
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var produto = ListaDadosProduto.FirstOrDefault(f => f.Id == id);
            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado" });
            }

            ListaDadosProduto.Remove(produto);
            return Ok(new { message = "Produto excluído com sucesso" });
        }

    }
}
