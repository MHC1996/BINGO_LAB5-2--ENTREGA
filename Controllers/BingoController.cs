using BingoVS.Services;
using Methods.BingoVS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BingoVS.Controllers;

[ApiController]
[Route("api/bingo")]
public class BingoController : ControllerBase
{
    private readonly IBingoService _bingoService;

    public BingoController(IBingoService bingoService)
    {
        _bingoService = bingoService;
    }

    [HttpGet("adicionar/apostador")]
    public ActionResult<string> AdicionarApostador()
    {
        try
        {
            string adiciona = _bingoService.Adiciona();

            return Ok(adiciona);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

    [HttpDelete("remover/{id}")]
    public ActionResult<string> RemoverApostador(int id)
    {
        try
        {
            _bingoService.Remove(id);
            return Ok("Apostador com Id " + id + " excluído com sucesso.");
        }
        catch (InvalidDataException)
        {
            return StatusCode(404, "O id inserido não pôde ser encontrado");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

    [HttpGet("adicionar/cartela/{id}")]
    public ActionResult<bool> AdicionarCartela(int id)
    {
        try
        {
            Cartela cartelaAdicionada = _bingoService.AddCartela(id);
            if (cartelaAdicionada != null)
            {
                return StatusCode(200, "Cartela adicionada com sucesso ao apostador de ID " + id.ToString() +
                    "\nNumeros da cartela: " + cartelaAdicionada.ToString());
            }
            return StatusCode(404, "Cartela não adicionada. ID " + id.ToString() + " não encontrado");
        }
        catch (IndexOutOfRangeException)
        {
            return StatusCode(404, "Apostador não encontrado com o Id informado.");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

    [HttpGet("ranking")]
    public ActionResult<string> RankingDeAcertos()
    {
        try
        {
            var ranking = _bingoService.Ranking();
            if (ranking != null)
            {
                return StatusCode(200, ranking);
            }
            else
            {
                return StatusCode(404, "Impossivel formar ranking sem ter iniciado bingo.");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

    [HttpGet("probabilidade")]
    public ActionResult<string> Probabilidade()
    {
        try
        {
            return StatusCode(200, _bingoService.Prob());
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

    [HttpGet("sorteio")]
    public ActionResult<string> SorteioNumero()
    {
        try
        {
            var numerosSorteados = _bingoService.SorteioNum();
            if (numerosSorteados != null)
            {
                return StatusCode(200, numerosSorteados);
            }
            else
            {
                return StatusCode(404, "Impossivel iniciar bingo. Verifique a existencia de apostadores e se todos tem cartela(s).");
            }
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro ao processar requisição");
        }
    }

}

