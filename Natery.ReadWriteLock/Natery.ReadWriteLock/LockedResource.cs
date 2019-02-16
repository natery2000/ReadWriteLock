using System.Threading;

namespace Natery.ReadWriteLock
{
  public class LockedResource<TResource>
  {
    private IResourceManager<TResource> _resourceManager;
    private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    public LockedResource() : this(new ResourceManager<TResource>()) { }

    internal LockedResource(IResourceManager<TResource> resourceManager)
    {
      _resourceManager = resourceManager;
    }

    public TResource Read()
    {
      _lock.EnterReadLock();
      try
      {
        return _resourceManager.Get();
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
        _resourceManager.Set(value);
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }
  }
}
