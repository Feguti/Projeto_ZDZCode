using APITeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace APITeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {

        private static int ProximoId = 0;
        public static List<DadosFuncionario> ListaDadosFuncionarios = new List<DadosFuncionario>();


        [HttpPost()]
        public DadosFuncionario Post([FromBody] DadosFuncionario dadosfuncionario)
        {
            dadosfuncionario.Id = ProximoId++;
            ListaDadosFuncionarios.Add(dadosfuncionario);
            return dadosfuncionario;
        }

        [HttpGet()]
        public IEnumerable<DadosFuncionario> Get()
        {
            return ListaDadosFuncionarios.ToList(); 
        }

        [HttpGet("consultar")]
        public ActionResult<IEnumerable<DadosFuncionario>> Search([FromQuery] string nome = null, [FromQuery] string funcao = null)
        {
            var query = ListaDadosFuncionarios.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(f => f.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(funcao))
            {
                query = query.Where(f => f.Funcao.Contains(funcao, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DadosFuncionario dadosfuncionario)
        {
            var funcionario = ListaDadosFuncionarios.FirstOrDefault(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound(); 
            }

            funcionario.Nome = dadosfuncionario.Nome;
            funcionario.Idade = dadosfuncionario.Idade;
            funcionario.Funcao = dadosfuncionario.Funcao;
            
            return Ok(funcionario); 
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var funcionario = ListaDadosFuncionarios.FirstOrDefault(f => f.Id == id);
            if (funcionario == null)
            {
                return NotFound(new { message = "Funcionário não encontrado" });
            }

            ListaDadosFuncionarios.Remove(funcionario);
            return Ok(new { message = "Funcionário excluído com sucesso" });
        }

    }
}
