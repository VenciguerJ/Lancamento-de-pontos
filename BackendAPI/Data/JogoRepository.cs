using Dapper;
using System.Data;
using BackendAPI.Models;


namespace BackendAPI.Repositories;

public class JogoRepository : IRepository<Jogo>{
    private readonly IDbConnection _db;

    public JogoRepository(IDbConnection db){
        _db = db;
    }
    public async Task<IEnumerable<Jogo?>> GetAll(){
        
        var Jogos = await _db.QueryAsync<Jogo>("Select * from Jogo");
        return Jogos;
    }
    public async Task<Jogo?> Get(int id){
        var Jogo = await _db.QueryFirstOrDefaultAsync<Jogo>("Select * from Jogo where id = @Id", new {Id = id});
        return Jogo;
    }
    public async Task Insert(Jogo entity){
        var query = "insert into Jogo (DataJogo, QuantidadePontos) values (@DataJogo, @QuantidadePontos)";
        await _db.ExecuteAsync(query, entity);

    }
    public async Task Update (Jogo entity){
        var query = @"update table Jogo set
                    DataJogo = @DataJogo
                    QuantidadePontos = @QuantidadeJogo
                    where Id = @id";
        await _db.ExecuteAsync(query, entity);
    }
    public async Task Delete (int id){
        await _db.ExecuteAsync("DELETE From Jogo Where Id= @Id", new{Id = id});
    }

    public async Task AddRecord(Jogo entity)
    {
        var query = "insert into Recordes (DataJogo, QuantidadePontos) values (@DataJogo, @QuantidadePontos)";
        await _db.ExecuteAsync(query, entity);
    }
    public async Task<IEnumerable<Jogo>> GetAllRecords()
    {
       return await _db.QueryAsync<Jogo>("Select * from Recordes");
    }
}