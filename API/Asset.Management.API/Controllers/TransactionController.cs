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
        try
        {
            var response = await _transactionService.CreateOrderAsync(request);

            if ((response?.Success ?? false) == false)
                return BadRequest(response);

            return Created(string.Empty, response);
        }
        catch (Exception ex)
        {
            return BadRequest(new Result<Transaction>(ex.Message));
        }
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<Result<Transaction>>> GetTransactionById(string id)
    {
        try
        {
            var response = await _transactionService.GetByIdAsync(id);

            if ((response?.Success ?? false) == false)
                return BadRequest(response);
            
            if (response?.Success == true && response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new Result<Transaction>(ex.Message));
        }
    }
}