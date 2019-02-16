namespace Natery.ReadWriteLock
{
  internal interface IResourceManager<TResource>
  {
    TResource Get();
    void Set(TResource resource);
  }
}