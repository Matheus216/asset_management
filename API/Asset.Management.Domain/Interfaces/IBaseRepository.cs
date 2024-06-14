using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;

public interface IBaseRepository<T>
{
    Task<Result<T>> InsertAsync(T transaction);
    Task<Result<T>> GetByIdAsync(string id);
    Task<Result<T>> DeleteAsync(T id);
    Task<Result<T>> UpdateAsync(T transaction);
    Task<Result<IEnumerable<T>>> GetListAsync();
}