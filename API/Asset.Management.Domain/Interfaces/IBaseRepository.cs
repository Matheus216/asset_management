using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;

public interface IBaseRepository<T>
{
    Task<Result<T>> InsertAsync(T knight);
    Task<Result<T>> GetByIdAsync(string id);
    Task<Result<T>> DeleteAsync(T id);
    Task<Result<T>> UpdateAsync(T knight);
    Task<Result<IEnumerable<T>>> GetListAsync();
}