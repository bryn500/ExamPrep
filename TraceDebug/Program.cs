//#define CONDITION1
#define CONDITION2
#define CONDITION3
using System;
using System.Diagnostics;

namespace TraceDebug
{
    public static class Program
    {
        // Initialize the trace source.
        static readonly TraceSource ts = new TraceSource("TraceTest");

        static void Main(string[] args)
        {
            Trace.TraceInformation("Something happened");
            Trace.TraceError("Oh no! Something happened");

            ts.TraceInformation("Hello"); // won't be printed
            ts.TraceEvent(TraceEventType.Error, 0, "!!! Hello !!!");
            ts.Switch.Level = SourceLevels.Verbose; // changing logging level
            ts.TraceInformation("Hello");

            ts.Flush();
            ts.Close();

            Console.ReadLine();


            Console.WriteLine("Calling Method1");
            Method1(3);
            Console.WriteLine("Calling Method2");
            Method2();
#if CONDITION3
            Console.WriteLine("Condition3 is defined");
#endif
        }

        [Conditional("CONDITION1")]
        public static void Method1(int x)
        {
            Console.WriteLine("CONDITION1 is defined");
        }

        [Conditional("CONDITION1"), Conditional("CONDITION2")]
        public static void Method2()
        {
            Console.WriteLine("CONDITION1 or CONDITION2 is defined");
        }

        // must be void
        //[Conditional("CONDITION2")]
        //public static int Method3(int x)
        //{
        //    Console.WriteLine("CONDITION2 is defined");
        //    return 1;
        //}
    }
}
