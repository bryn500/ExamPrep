using System;
using System.Collections;
using System.Collections.Generic;

namespace Boxing
{
    public class Program
    {
        // https://www.youtube.com/watch?v=Fxs_GRiSR5g&list=PLRwVmtr-pp07XP8UBiUJ0cyORVCmCgkdA&index=12
        public static void Main(string[] args)
        {
            // This happens to all value types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types

            // Create int on stack
            int i = 123;

            // Box it onto heap
            object o = i;

            // o is a reference to a boxed copy of the original value of i
            i = 20;
            Console.WriteLine(i);
            Console.WriteLine(o);

            // this works but rather than just overwriting the value, it creates a new object on heap with an int in it
            // more work + garbage collection requried
            o = 100;
            Console.WriteLine(o);

            // Can't do this
            // Can't write to this as it's the value within the box, not a variable of type int.
            //((int)o)++;

            Console.WriteLine(o);
            // You can unbox it
            // Moves to stack, adds 1, reboxes
            // inefficient
            i = (int)o;
            i++;
            o = i;
            Console.WriteLine(o);

            object o2 = 5;
            //long l = o2;
            // long l = (long)o2;
            long l = (int)o2;
            Console.WriteLine(l);

            // Before generics
            // has object[] behind the scenes
            // would be an array of boxed references in the heap
            // ineffcient
            var arrList = new ArrayList();
            arrList.Add(1);
            arrList.Add("test");
            // With generics
            // has int[] behind the scenes
            // no boxing/unboxing, just the value
            var list = new List<int>();
            list.Add(1);
            //list.Add("test");


        }
    }
}
