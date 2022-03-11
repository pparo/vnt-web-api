using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.App.Data;
using WebApi.Models;

namespace WebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiController : ControllerBase
    {
        private readonly WebApiDbContext _context;

        public WebApiController(WebApiDbContext context)
        {
            _context = context;
        }        

        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> Get()
        {
            var pessoas = await _context.Pessoas.ToListAsync();

            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetById(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if(pessoa == null)
            {
                return NotFound("Pessoa não encontrada");
            }

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> AddPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        [HttpPut]
        public async Task<ActionResult<Pessoa>> UpdatePessoa(Pessoa pessoaUpdated)
        {
            var pessoa = await _context.Pessoas.FindAsync(pessoaUpdated.Id);

            if(pessoa == null)
            {
                return NotFound("Pessoa não encontrada");
            }

            pessoa.Nome = pessoaUpdated.Nome;
            pessoa.Sobrenome = pessoaUpdated.Sobrenome;
            pessoa.Profissao = pessoaUpdated.Profissao;
            pessoa.DataNascimento = pessoaUpdated.DataNascimento;

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.Pessoas.Any(p => p.Id == pessoaUpdated.Id))
            {
                return NotFound("Pessoa não encontrada");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if(pessoa == null)
            {
                return NotFound("Pessoa não encontrada");
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}