namespace Natery.ReadWriteLock.Test
{
  public class Resource<T>
  {
    public T Value { get; }

    public Resource(T i)
    {
      Value = i;
    }
  }
}
