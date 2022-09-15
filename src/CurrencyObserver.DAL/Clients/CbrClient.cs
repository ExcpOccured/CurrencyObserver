using System.Diagnostics;
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
        Debug.Assert(!string.IsNullOrEmpty(_options.Url));
        
        using var httpClient = _httpClientFactory.CreateClient();

        HttpResponseMessage? httpMessage;
        try
        {
            httpMessage = await httpClient.GetAsync(_options.Url, cancellationToken);
        }
        catch (HttpRequestException httpRequestException)
        {
            
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError(httpRequestException, httpRequestException.Message);
            return null;
        }

        if (!httpMessage.IsSuccessStatusCode)
        {
            var httpContentStr = await httpMessage.Content.ReadAsStringAsync(cancellationToken);
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError($"Failed to get quotes from CBR API \n{httpContentStr}");
            return null;
        }

        await using var stream = await httpMessage.Content.ReadAsStreamAsync(cancellationToken);

        CbrCurrencyQuotesResponse? quotes;
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(CbrCurrencyQuotesResponse));

            using var streamReader = new StreamReader(stream);
            {
                quotes = (CbrCurrencyQuotesResponse?)xmlSerializer.Deserialize(streamReader);
            }
        }
        catch (InvalidOperationException invalidOperationException)
        {
            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.LogError(invalidOperationException, invalidOperationException.Message);
            return null;
        }

        return quotes;
    }
}