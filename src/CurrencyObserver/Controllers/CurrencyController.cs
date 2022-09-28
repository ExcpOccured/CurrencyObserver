using CurrencyObserver.Common.Models;
using CurrencyObserver.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyObserver.Controllers;

[ApiController]
public class CurrencyController : Controller
{
    private readonly IMediator _mediator;

    public CurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(nameof(GetByDate))]
    public async Task<ActionResult<List<Currency>>> GetByDate(
        [FromQuery] DateTime date)
    {
        var currencies = await _mediator.Send(new CurrenciesFiltrationQuery
        {
            Predicate = currency => DateTime.Equals(currency.AddedAt, date)
        });

        return Ok(currencies);
    }
}