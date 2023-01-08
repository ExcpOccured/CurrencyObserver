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
        var currencies = await _mediator.Send(new GetCurrenciesOnDateQuery(onDate));

        if (currencies.IsEmpty())
        {
            return NotFound($"Currencies not found on date {onDate.ToString(CultureInfo.InvariantCulture)}");
        }

        return Ok(currencies);
    }

    [HttpGet(nameof(GetByCode))]
    public async Task<ActionResult<List<Currency>>> GetByCode(
        [FromQuery] DateTime onDate,
        [FromQuery] CurrencyCode currencyCode)
    {
        var currency = await _mediator.Send(new GetCurrencyByCodeQuery(onDate, currencyCode));

        if (currency is null)
        {
            return NotFound($"Currency not found by code {currencyCode}");
        }

        return Ok(currency);
    }
}