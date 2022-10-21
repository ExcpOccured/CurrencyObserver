namespace CurrencyObserver.Common.Utils;

public class AsyncReadWriteLocker
{
    private readonly ReaderWriterLockSlim _lock = new();

    public IDisposable ReadLock() => new ReadLockDisposable(this);

    public IDisposable WriteLock() => new WriteLockDisposable(this);

    private readonly struct WriteLockDisposable : IDisposable
    {
        private readonly AsyncReadWriteLocker _locker;

        public WriteLockDisposable(AsyncReadWriteLocker locker)
        {
            _locker = locker;
            _locker._lock.EnterWriteLock();
        }

        public void Dispose() => _locker._lock.ExitWriteLock();
    }

    private readonly struct ReadLockDisposable : IDisposable
    {
        private readonly AsyncReadWriteLocker _locker;

        public ReadLockDisposable(AsyncReadWriteLocker locker)
        {
            _locker = locker;
            _locker._lock.EnterReadLock();
        }

        public void Dispose() => _locker._lock.ExitReadLock();
    }
}