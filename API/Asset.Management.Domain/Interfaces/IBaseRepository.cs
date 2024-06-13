using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;

public interface IBaseRepository<T>
{
    Task<Result<T>> Insert(T knight);
    Task<Result<T>> GetById(string id);
    Task<Result<T>> Delete(T id);
    Task<Result<T>> Update(T knight);
    Task<Result<IEnumerable<T>>> GetList();
}