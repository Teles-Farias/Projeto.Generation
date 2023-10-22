
using blogpessoal.Model;
using blogpessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogpessoal.Controllers
{
    [Route("~/alunos")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
        private readonly IValidator<Aluno> _alunoValidator;

        public AlunoController(
          IAlunoService alunoService,
          IValidator<Aluno> alunoValidator
          )
        {
            _alunoService = alunoService;
            _alunoValidator = alunoValidator;

        }

       
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _alunoService.GetAll());
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _alunoService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(Resposta);
        }

         [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByNome(string nome){

            return Ok(await _alunoService.GetByNome(nome));
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> Create([FromBody] Aluno aluno)
        {
            var validarAluno = await _alunoValidator.ValidateAsync(aluno);

            if (!validarAluno.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarAluno);

            var Resposta = await _alunoService.Create(aluno);

            if (Resposta is null)
                return BadRequest("Aluno já cadastrado!");

            return CreatedAtAction(nameof(GetById), new { id = Resposta.Id }, Resposta);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult> Update([FromBody] Aluno aluno)
        {
            if (aluno.Id == 0)
                return BadRequest("O Id do Aluno é inválido!");
            
            var validarAluno = await _alunoValidator.ValidateAsync(aluno);

            if (!validarAluno.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarAluno);

            var AlunoUpdate = await _alunoService.GetById(aluno.Id);

            if ((AlunoUpdate is not null) && (AlunoUpdate.Id != aluno.Id))
                return BadRequest("O Id do Aluno já está cadastrado");

            var Resposta = await _alunoService.Update(aluno);

            if (Resposta is null)
                return BadRequest("Aluno não encontrado!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaAluno = await _alunoService.GetById(id);

            if(BuscaAluno is null)
            {
                return NotFound("Aluni não foi encontrado!");
            }
            await _alunoService.Delete(BuscaAluno);

            return NoContent();
        }

    }
}