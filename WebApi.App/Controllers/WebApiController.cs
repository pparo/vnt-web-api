using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiController : ControllerBase
    {
        private static List<Pessoa> Pessoas = new List<Pessoa>{
            new Pessoa{
                Id = 1,
                Nome = "Pedro",
                Sobrenome = "Paro",
                DataNascimento = new DateOnly(1986, 8, 18),
                Profissao = "Dev"
            },
            new Pessoa{
                Id = 2,
                Nome = "Pedro",
                Sobrenome = "Junior",
                DataNascimento = new DateOnly(2018, 8, 18),
                Profissao = "Dev"
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> Get()
        {
            return Ok(Pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetById(int id)
        {
            var pessoa = Pessoas.Find(p => p.Id == id);

            if(pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<List<Pessoa>>> AddPessoa(Pessoa pessoa)
        {
            Pessoas.Add(pessoa);

            return Ok(Pessoas);
        }

        [HttpPut]
        public async Task<ActionResult<List<Pessoa>>> UpdatePessoa(Pessoa pessoaUpdated)
        {
            var pessoa = Pessoas.Find(p => p.Id == pessoaUpdated.Id);

            if(pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }

            pessoa.DataNascimento = pessoaUpdated.DataNascimento;
            pessoa.Nome = pessoaUpdated.Nome;
            pessoa.Profissao = pessoaUpdated.Profissao;
            pessoa.Sobrenome = pessoaUpdated.Sobrenome;
            
            return Ok(Pessoas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> DeletePessoa(int id)
        {
            var pessoa = Pessoas.Find(p => p.Id == id);

            if(pessoa == null)
            {
                return BadRequest("Pessoa não encontrada");
            }

            Pessoas.Remove(pessoa);

            return Ok(Pessoas);
        }
    }
}