using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Natery.ReadWriteLock.Test
{
  [TestClass]
  public partial class LockedResourceTests
  {
    [TestMethod]
    public void Constructor_ReferenceType()
    {
      var locked = new LockedResource<Func<int>>();

      Assert.IsNull(locked.Read());
    }
    
    [TestMethod]
    public void Constructor_ValueType()
    {
      var locked = new LockedResource<int>();

      Assert.AreEqual(default(int), locked.Read());
    }

    [TestMethod]
    public void Write_ReferenceType()
    {
      var locked = new LockedResource<Func<int>>();
      Func<int> func = () => 1;

      locked.Write(func);

      Assert.IsNotNull(locked.Read());
      Assert.ReferenceEquals(func, locked.Read());
    }

    [TestMethod]
    public void Write_ValueType()
    {
      var locked = new LockedResource<int>();
      var i = 1;

      locked.Write(i);

      Assert.AreEqual(i, locked.Read());
    }

    [TestMethod]
    public void Multithreading()
    {
      var res = new Resource<int>(1);
      var res1 = new Resource<int>(1);
      var res2 = new Resource<int>(2);

      Func<Resource<int>> get = () => res;
      Action<Resource<int>> set = r => { Thread.Sleep(1200); res = r; };

      var locked = new LockedResource<Resource<int>>(new MockResourceManager<Resource<int>>(get, set));
      locked.Write(res);

      var order = new List<int>();

      var first = new Task(() =>
      {
        order.Add(res1.Value);
        locked.Write(res1);
        order.Add(res1.Value);
      });
      var second = new Task(() =>
      {
        Thread.Sleep(100);
        order.Add(res2.Value);
        locked.Write(res2);
        order.Add(res2.Value);
      });

      first.Start();
      second.Start();
      Task.WaitAll(new[] { first, second });

      Assert.ReferenceEquals(res2, res);
      CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 1, 2 }, order);
    }
  }
}
