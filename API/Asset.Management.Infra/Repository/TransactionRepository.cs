using Asset.Management.Domain.Entities;
using Asset.Management.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Asset.Management.Infra.Repository;

public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(IConfiguration _configuration) 
        : base(_configuration, nameof(Transaction))
    {
        
    }
}