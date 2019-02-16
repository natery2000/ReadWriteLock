using System;

namespace Natery.ReadWriteLock.Test
{
  public class MockResourceManager<TResource> : IResourceManager<TResource>
  {
    private Func<TResource> _get;
    private Action<TResource> _set;

    public MockResourceManager(Func<TResource> get, Action<TResource> set)
    {
      _get = get;
      _set = set;
    }

    public TResource Get()
    {
      return _get();
    }

    public void Set(TResource resource)
    {
      _set(resource);
    }
  }
}
