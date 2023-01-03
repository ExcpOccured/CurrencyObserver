using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CurrencyObserver.Common.Clients;
using Xunit;
using Xunit.Abstractions;

namespace CurrencyObserver.IntegrationTests.Tests.Clients;

public class CbrClientTests : TestBase
{
    public CbrClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Fact]
    public async Task GetCurrencyQuotesAsync_CanReturnData()
    {
        var cbrClient = ServiceClient.GetService<ICbrClient>();
        var quotes = await cbrClient.GetCurrencyQuotesAsync(CancellationToken.None);

        Assert.NotNull(quotes);
        Assert.NotEmpty(quotes!.Currencies);
        
        TestOutputHelper.WriteLine(JsonSerializer.Serialize(quotes));
    }
}