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
        var rulesValidations = new List<string>(); 

        if (responseValidateRequest.Any())
            return new Result<Transaction>(responseValidateRequest);

        var productResponse = await _productService.GetByIdAsync(request.ProductId);

        if ((productResponse?.Success ?? false) == false || productResponse?.Data == null)
            return new Result<Transaction>
            (
                "Erro ao buscar produto ou não encontrado" 
            );

        RulesValidation(productResponse.Data, request.Quantity, ref rulesValidations);

        if (rulesValidations.Any())
            return new Result<Transaction>(rulesValidations);

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

        if (responseInsered.Success)
            await _productService.RecalculateAllowQuantity(request.TypeTransaction, request.Quantity, productResponse.Data);

        return responseInsered; 
    }

    private void RulesValidation(Product product,int quantity, ref List<string> rulesMessage) 
    {
        if (product?.ExpirationDate < DateTime.Now)
            rulesMessage.Add("Produto expirado");
        
        if (product?.AvailableQuantity < quantity)
            rulesMessage.Add("Quantidade de ativos não disponíveis");
    }

    public async Task<Result<Transaction>> GetByIdAsync(string id) 
        => await _transactionRepository.GetByIdAsync(id);
}