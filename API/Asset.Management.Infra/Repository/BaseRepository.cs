
using Asset.Management.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Asset.Management.Domain.DTOs;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Asset.Management.Infra.Repository;

public abstract class BaseRepository<T> : IBaseRepository<T>
{
    protected IMongoCollection<T> _collection { get; set; }
    public BaseRepository(IConfiguration configuration, string collection)
    {
        var settings = configuration.GetSection("AssetDatabase");
        var client = new MongoClient(settings["AssetConnectionString"]);
        var database = client.GetDatabase(settings["AssetDatabase"]);
        _collection = database.GetCollection<T>(collection);
    }

    public async Task<Result<T>> InsertAsync(T input)
    {
        try
        {
            await _collection.InsertOneAsync(input);
            return new Result<T>(input);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex.Message ?? "Problemas para inserir");
        }
    }

    public async Task<Result<T>> GetByIdAsync(string id)
    {
        try
        {
            if (ObjectId.TryParse(id, out ObjectId obj))
            {
                var builderFilter = Builders<T>.Filter.Eq("_id", obj);

                var response = await _collection
                    .Find(builderFilter).FirstOrDefaultAsync(); 
                    
                return new Result<T>(response);
            }
            else
                return new Result<T>("Erro na conversão do identificador do produto");
        }
        catch (Exception ex)
        {
            return new Result<T>(ex.Message ?? "Problemas encontrar o registro");
        }
    }

    public async Task<Result<T>> DeleteAsync(T obj)
    {
        try
        {
            var prop = typeof(T)?.GetProperty("Id");

            var builder = Builders<T>.Filter;
            var filter = builder.Eq("_id", new ObjectId(prop?.GetValue(obj)?.ToString()));
            var response = await _collection.DeleteOneAsync(filter);

            return new Result<T>(obj);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex.Message ?? "Problemas para deletar");
        }
    }

    public async Task<Result<T>> UpdateAsync(T input)
    {
        try
        {
            var builders = Builders<T>.Filter;
            var filter = builders.Eq("_id", new ObjectId(typeof(T)?.GetProperty("Id")?.GetValue(input)?.ToString()));

            var response = await _collection.ReplaceOneAsync(filter, input);
            return new Result<T>(input);
        }
        catch (Exception ex)
        {
            return new Result<T>(ex.Message ?? "Problemas para inserir");
        }
    }

    public async Task<Result<IEnumerable<T>>> GetListAsync()
    {
        try
        {
            var response = await _collection.FindAsync(Builders<T>.Filter.Empty);
            var list = await response.ToListAsync();
            return new Result<IEnumerable<T>>(list);
        }
        catch (Exception ex)
        {
            return new Result<IEnumerable<T>>(ex.Message ?? "Erro ao listar cadastros");
        }
    }
}