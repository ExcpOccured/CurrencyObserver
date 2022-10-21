using CurrencyObserver.Common.Utils;
using CurrencyObserver.DAL.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace CurrencyObserver.DAL.Providers;

public class RedisConnectionProvider : IRedisConnectionProvider
{
    private readonly RedisOptions _options;

    private readonly AsyncReadWriteLocker _locker;

    private ConnectionMultiplexer? _multiplexer;
    private Exception? _raisedException;

    public RedisConnectionProvider(
        IOptions<RedisOptions> options,
        AsyncReadWriteLocker locker)
    {
        _locker = locker;
        _options = options.Value;
    }

    public Task<ConnectionMultiplexer> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        return OpenConnectionInternalAsync(cancellationToken);
    }

    public ConnectionMultiplexer OpenConnection()
    {
        return OpenConnectionInternal();
    }

    private ConnectionMultiplexer OpenConnectionInternal()
    {
        using var _ = _locker.ReadLock();
        {
            if (_raisedException is not null)
            {
                throw _raisedException;
            }

            if (_multiplexer is not null)
            {
                return _multiplexer;
            }
        }

        ConnectionMultiplexer? multiplexer = null;
        try
        {
            multiplexer = ConnectionMultiplexer.Connect(_options.ConnectionString);

            using var __ = _locker.WriteLock();
            {
                _multiplexer = multiplexer;
            }

            return multiplexer;
        }
        catch (Exception exception)
        {
            multiplexer?.Dispose();

            using var __ = _locker.WriteLock();
            {
                _raisedException = exception;
            }

            throw;
        }
    }

    private Task<ConnectionMultiplexer> OpenConnectionInternalAsync(CancellationToken cancellationToken)
    {
        using var _ = _locker.ReadLock();
        {
            if (_raisedException is not null)
            {
                throw _raisedException;
            }

            if (_multiplexer is not null)
            {
                return Task.FromResult(_multiplexer);
            }
        }

        cancellationToken.ThrowIfCancellationRequested();

        ConnectionMultiplexer? multiplexer = null;
        try
        {
            multiplexer = ConnectionMultiplexer.Connect(_options.ConnectionString);

            using var __ = _locker.WriteLock();
            {
                _multiplexer = multiplexer;
            }

            return Task.FromResult(multiplexer);
        }
        catch (Exception exception)
        {
            multiplexer?.Dispose();

            using var __ = _locker.WriteLock();
            {
                _raisedException = exception;
            }

            throw;
        }
    }
}