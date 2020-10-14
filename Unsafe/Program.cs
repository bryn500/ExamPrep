using System;

namespace UnsafeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            // safe here
            test.TestUnsafe();
            // safe here
        }        
    }

    public class Test
    {
        private int managedInt = 201;

        private unsafe void InitArray(int* first, int numelements)
        {
        }

        public unsafe void Go()
        {
            var arrayofints = new int[207];
            fixed (int* firstElement = &arrayofints[0])
            {
                InitArray(firstElement, arrayofints.Length);
            }
        }

        public unsafe void TestUnsafe()
        {
            int test = 101;
            int* testPointer = &test;
            Console.WriteLine(*testPointer);
            *testPointer = 102;
            Console.WriteLine(*testPointer);

            fixed (int* managedPointer = &managedInt)
            {
                Console.WriteLine(*managedPointer);
                *managedPointer = 202;
                Console.WriteLine(*managedPointer);
            }

            //int* managedPointer = &managedInt;

            Console.WriteLine("initial values:");
            Console.WriteLine(test);
            Console.WriteLine(managedInt);
        }
    }
}
