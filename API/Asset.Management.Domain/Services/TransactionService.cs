using Asset.Management.Domain.DTOs.Transaction;
using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.Entities;
using Asset.Management.Domain.DTOs;

namespace Asset.Management.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly IProductService _productService;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(IProductService productService,
        ITransactionRepository transactionRepository)
    {
        this._productService = productService;
        this._transactionRepository = transactionRepository;
    }

    public async Task<Result<Transaction>> CreateOrderAsync(TransactionRequestDTO request)
    {
        var responseValidateRequest = request.ValidateStructureRequest();
        if (responseValidateRequest.Any())
            return new Result<Transaction>(responseValidateRequest);

        var productResponse = await _productService.GetByIdAsync(request.ProductId);

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

    public async Task<Result<Transaction>> GetByIdAsync(string id) 
        => await _transactionRepository.GetByIdAsync(id);
}