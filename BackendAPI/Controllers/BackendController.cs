using System.Data;
using System.Reflection.Metadata.Ecma335;
using BackendAPI.Models;
using BackendAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace BackendAPI.Controllers;


[ApiController]
[Route("API")]
public class APIController : Controller
{

    private readonly IRepository<Jogo> _db;
    public APIController(IRepository<Jogo> db){
        _db = db;
    }

    [HttpPost("throwpoints")]
    public async Task<IActionResult> ThrowPoints([FromBody] Jogo j){
        try{

            bool recorde = await VerificaRecorde(j);

            if (recorde) 
            {
                await _db.AddRecord(j);
            }

            await _db.Insert(j);
            return Ok(
                new
                {
                    mensagem = "Jogo Lançado!",
                    dados = j,
                }
            );
        }catch(Exception e){
            return BadRequest("Ocorreu um erro ao retornar Dados! " + e.Message);
        }
    }

    public async Task<bool> VerificaRecorde(Jogo j)
    {
        bool recorde = false;
        var JogosDB = await _db.GetAll();
        if(JogosDB.Count() <= 0)
        {
            return false;
        }

        int recordeAtual = CalcBestScore(JogosDB);

        if(j.QuantidadePontos > recordeAtual)
        {
            recorde = true;
        }
        else
        {
            recorde = false;
        }
;
        return recorde;
    }


    [HttpGet("isok")]
    public IActionResult IsOK(){

        return Ok(new { mensagem = "Cliente recebido com sucesso!" });

    }

    public int CalculaTotalPontos(IEnumerable<Jogo> j)
    {
        int total = 0;
        foreach (Jogo jogo in j)
        {
            total += jogo.QuantidadePontos;
        }
        return total;
    }

    public int CalculaMediaPontos(IEnumerable<Jogo> jogos)
    {
        int totalpontos = CalculaTotalPontos(jogos);
        int media = totalpontos / jogos.Count();
        return media;
    }

    public int CalcBestScore(IEnumerable<Jogo> jogos)
    {
        int bestScore = jogos.Max(j => j.QuantidadePontos);
        return bestScore;
    }

    public int CalcWrostScore(IEnumerable<Jogo> jogos)
    {
        int bestScore = jogos.Min(j => j.QuantidadePontos);
        return bestScore;
    }

    [HttpGet("consult")]
    public async Task<IActionResult> Consult()
    {
        Resultados view = new Resultados();
        var jogos = await _db.GetAll();
        var records = await _db.GetAllRecords();

        view.TotalJogos = jogos.Count();
        view.TotalPontos = CalculaTotalPontos(jogos);
        view.MediaJogos = CalculaMediaPontos(jogos);
        view.BestScore = CalcBestScore(jogos);
        view.WrostScore = CalcWrostScore(jogos);
        view.Records = records.Count();
        return Json(view);
    }
}   