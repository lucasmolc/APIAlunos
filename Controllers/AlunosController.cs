using APIAlunos.Models;
using APIAlunos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAlunos.Controllers;

[Route("[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private IAlunoService _alunoService;

    public AlunosController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
    {
        try
        {
            var alunos = await _alunoService.GetAlunos();
            return Ok(alunos);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("alunosPorNome")]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
    {
        try
        {
            var alunos = await _alunoService.GetAlunosByName(nome);
            if (alunos is null)
                return NotFound();

            return Ok(alunos);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet("{id:int}", Name = "GetAluno")]
    public async Task<ActionResult<Aluno>> GetAlunosById(int id)
    {
        try
        {
            var aluno = await _alunoService.GetAluno(id);
            if (aluno is null)
                return NotFound();

            return Ok(aluno);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(Aluno aluno)
    {
        try
        {
            await _alunoService.CreateAluno(aluno);
            return CreatedAtRoute(nameof(GetAlunosById), new { id = aluno.Id }, aluno);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Edit(int Id, [FromBody] Aluno aluno)
    {
        try
        {
            if (aluno.Id == Id)
            {
                await _alunoService.UpdateAluno(aluno);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int Id)
    {
        try
        {
            var aluno = await _alunoService.GetAluno(Id);

            if (aluno is null)
                return NotFound();

            await _alunoService.DeleteAluno(aluno);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
