using System;
using System.IO;

/// <summary>
/// If a base class implements IDisposable your class should not have IDisposable in the list of its interfaces.
/// In such cases it is recommended to override the base class's protected virtual void Dispose(bool) method or its equivalent.
/// The class should not implement IDisposable explicitly, e.g. the Dispose() method should be public.
/// The class should contain protected virtual void Dispose(bool) method. This method allows the derived classes to correctly dispose the resources of this class.
/// The content of the Dispose() method should be invocation of Dispose(true) followed by GC.SuppressFinalize(this)
/// If the class has a finalizer, i.e. a destructor, the only code in its body should be a single invocation of Dispose(false).
/// If the class inherits from a class that implements IDisposable it must call the Dispose, 
///     or Dispose(bool) method of the base class from within its own implementation of Dispose or Dispose(bool), respectively.
///     This ensures that all resources from the base class are properly released.
/// </summary>
namespace Disposing
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var foo1 = new FooSealed();
            foo1.GetHashCode();
            // needs to be disposed. That's what the interface is telling you.
            //foo1.Dispose();


            var foo2 = new Foo2();

            try
            {
                // use foo
                foo2.GetHashCode();
            }
            finally
            {
                // failure to 
                foo2.Dispose();
            }


            // a using statement is an easier way to define the above
            using (var foo3 = new Foo3())
            {
                foo3.GetHashCode();
            }


            // even easier way of using a using statement
            using var foo4 = new Foo4();
            foo4.GetHashCode();

            var a = "Do something else";

            foo4.GetHashCode(); // can carry on using 'using'


            // sometimes a dispose will call dispose on an underlying object as well.
            // no need to wrap stream in using here, stream writer will call dispose on the stream passed in
            // wrapping it could be misleading as it would indicate stream was still usable after the inner using
            FileStream stream = new FileStream(Guid.NewGuid().ToString() + ".txt", FileMode.CreateNew);
            // Create a StreamWriter from FileStream  
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write("Hello StreamWriter");
            }                
        }
    }

    // Simple implementation
    public class Foo2 : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // object is safely disposed so no need to do anything in GC
        }

        protected virtual void Dispose(bool disposing) // can be overidden if another class extends this class
        {
            // Cleanup
        }
    }

    // Implementation with a finalizer, safer in case someone fails to call dispose
    public class Foo3 : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // supress so it doesn't happen twice, both should call same method to ensure the same objects are disposed of safely
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }

        // Used a fallback in case Dispose isn't called directly
        // Finalizer can't be called diectly, is called by GC when cleaning up object, will go through inheritance tree and finalize each
        ~Foo3()
        {
            Dispose(false);
        }

        // Finalizer does this behind the scenes
        //protected override void Finalize()
        //{
        //    try
        //    {
        //        // Cleanup statements...  
        //    }
        //    finally
        //    {
        //        base.Finalize();
        //    }
        //}
    }



    // Base disposable class
    public class Foo4 : Foo3
    {
        protected override void Dispose(bool disposing) // overrides virtual dispose method in derived method
        {
            // Cleanup
            // ...

            // Calls derived class's dispose method
            base.Dispose(disposing);
        }
    }

    // Sealed classes
    public sealed class FooSealed : IDisposable // if sealed no one can inherit so no need for virtual method 
    {
        public void Dispose()
        {
            // Cleanup
            // ...
            GC.SuppressFinalize(this);
        }
    }

    public sealed class FooSealedFinalise : IDisposable // sealed no override/virtual but with a backup finalizer 
    {
        public void Dispose()
        {
            Destroy(true);
            GC.SuppressFinalize(this);
        }

        private void Destroy(bool disposing)
        {
            // Cleanup
            // ...
        }

        ~FooSealedFinalise()
        {
            Destroy(false);
        }
    }
}
