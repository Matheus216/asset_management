using Asset.Management.Domain.DTOs.Transaction;
using Asset.Management.Domain.Entities;
using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Interfaces;
public interface ITransactionService
{
    Task<Result<Transaction>> CreateOrderAsync(TransactionRequestDTO request);
}