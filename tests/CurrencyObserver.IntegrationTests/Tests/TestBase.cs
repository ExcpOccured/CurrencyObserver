using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CurrencyObserver.IntegrationTests.Tests;

public abstract class TestBase : IAsyncLifetime
{
    protected readonly ITestOutputHelper TestOutputHelper;

    protected TestBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;

    public virtual Task DisposeAsync() => Task.CompletedTask;
}
