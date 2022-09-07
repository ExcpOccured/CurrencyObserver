using System.Xml.Serialization;
using CurrencyObserver.DAL.Clients.Models;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CurrencyObserver.DAL.Clients;

public class CbrClient : ICbrClient
{
    private readonly CbrClientOptions _options;

    private readonly ILogger<CbrClient> _logger;

    private readonly IHttpClientFactory _httpClientFactory;

    public CbrClient(
        IOptions<CbrClientOptions> options,
        IHttpClientFactory httpClientFactory,
        ILogger<CbrClient> logger)
    {
        _options = options.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<CbrCurrencyQuotesResponse?> GetCurrencyQuotesAsync(CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage? message;
        try
        {
            message = await httpClient.GetAsync(_options.Url, cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError(exception, exception.Message);
            return null;
        }

        await using var stream = await message.Content.ReadAsStreamAsync(cancellationToken);
        using var streamReader = new StreamReader(stream);

        var xmlSerializer = new XmlSerializer(typeof(CbrCurrencyQuotesResponse));

        CbrCurrencyQuotesResponse? quotesResponse;
        try
        {
            quotesResponse = (CbrCurrencyQuotesResponse?)xmlSerializer.Deserialize(streamReader);
        }
        catch (InvalidOperationException exception)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError(exception, exception.Message);
            return null;
        }

        return quotesResponse;
    }
}