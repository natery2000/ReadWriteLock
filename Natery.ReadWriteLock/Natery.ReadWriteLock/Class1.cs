using System.Threading;

namespace Natery.ReadWriteLock
{
  public class LockedResource<TResource>
  {
    private TResource _resource;
    private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public LockedResource(TResource resource)
    {
      _resource = resource;
    }

    public TResource Read()
    {
      _lock.EnterReadLock();
      try
      {
        return _resource;
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    public void Write(TResource value)
    {
      _lock.EnterWriteLock();
      try
      {
        _resource = value;
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }
  }
}
