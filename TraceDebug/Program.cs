using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
