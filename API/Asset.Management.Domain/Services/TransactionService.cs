using Asset.Management.Domain.DTOs.Transaction;
using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.DTOs;
using Asset.Management.Domain.Entities;

namespace Asset.Management.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly IProductRepository _productRepository;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IProductRepository productRepository,
        ITransactionRepository transactionRepository)
    {
        this._productRepository = productRepository;
        this._transactionRepository = transactionRepository;
    }

    public async Task<Result<Transaction>> CreateOrderAsync(TransactionRequestDTO request)
    {
        var responseValidateRequest = request.ValidateStructureRequest();
        if (responseValidateRequest.Any())
            return new Result<Transaction>(responseValidateRequest);

        var productResponse = await _productRepository.GetByIdAsync(request.ProductId);

        if ((productResponse?.Success ?? false) == false || productResponse?.Data == null)
            return new Result<Transaction>
            (
                productResponse?.MessagesError ??
                new List<string> { "Erro ao consultar produto" }
            );

        var transaction = new Transaction( 
            request.ProductId,
            request.Quantity,
            productResponse?.Data?.Price ?? 0,
            (productResponse?.Data?.Price ?? 0) * request.Quantity,
            request.TypeTransaction,
            DateTime.Now,
            request.ClientId
        );

        var responseInsered = await _transactionRepository.InsertAsync(transaction); 
        return responseInsered; 
    }
}