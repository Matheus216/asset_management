using Asset.Management.Domain.DTOs.Transaction;
using Asset.Management.Domain.Interfaces;
using Asset.Management.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Asset.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult<Result<Transaction>>> CreateTransactionAsync([FromBody] TransactionRequestDTO request)
    {
        return Ok(await _transactionService.CreateOrderAsync(request));
    }
}