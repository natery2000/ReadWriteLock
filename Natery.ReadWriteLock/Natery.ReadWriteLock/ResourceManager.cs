namespace Natery.ReadWriteLock
{
  internal class ResourceManager<TResource> : IResourceManager<TResource>
  {
    private TResource _resource = default(TResource);

    public TResource Get()
    {
      return _resource;
    }

    public void Set(TResource resource)
    {
      _resource = resource;
    }
  }
}
