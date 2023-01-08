using System.Globalization;
using CurrencyObserver.Common.Extensions;
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

    [HttpGet(nameof(GetOnDate))]
    public async Task<ActionResult<List<Currency>>> GetOnDate(
        [FromQuery] DateTime onDate)
    {
        var currencies = await _mediator.Send(new GetCurrenciesByDateQuery
        {
            OnDateTime = onDate,
        });

        if (currencies.IsEmpty())
        {
            return NotFound($"Not found on date {onDate.ToString(CultureInfo.InvariantCulture)}");
        }

        return Ok(currencies);
    }
}