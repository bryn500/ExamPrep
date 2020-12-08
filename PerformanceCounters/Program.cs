using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PerformanceCounters
{
    public static class Program
    {
        public static void Main()
        {
            // Get current values from counters
            var currentProcess = Process.GetCurrentProcess().ProcessName;
            PerformanceCounter privateBytes = new PerformanceCounter(categoryName: "Process", counterName: "Private Bytes", instanceName: currentProcess);
            PerformanceCounter gen2Collections = new PerformanceCounter(categoryName: ".NET CLR Memory", counterName: "# Gen 2 Collections", instanceName: currentProcess);
            Console.WriteLine("private bytes = " + privateBytes.NextValue());
            Console.WriteLine("gen 2 collections = " + gen2Collections.NextValue());

            // lookup counters
            var cats = PerformanceCounterCategory.GetCategories();
            var processorCategory = cats.FirstOrDefault(cat => cat.CategoryName == "Processor");

            var countersInCategory = processorCategory.GetCounters("_Total");

            DisplayCounter(countersInCategory.First(cnt => cnt.CounterName == "% Processor Time"));

            // Custom Counter only works when running VS as admin
            bool exists = PerformanceCounterCategory.Exists("MyTimeCategory");
            if (!exists)
            {
                PerformanceCounterCategory.Create("MyTimeCategory", "My category help",
                    PerformanceCounterCategoryType.SingleInstance, "Current Seconds",
                    "My counter help");
            }
            PerformanceCounter pc = new PerformanceCounter("MyTimeCategory", "Current Seconds", false);
            while (true)
            {
                Thread.Sleep(1000);
                pc.RawValue = DateTime.Now.Second;
                Console.WriteLine("{0}\t{1} = {2}", pc.CategoryName, pc.CounterName, pc.NextValue());
            }
        }

        private static void DisplayCounter(PerformanceCounter performanceCounter)
        {
            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1000);
                Console.WriteLine("{0}\t{1} = {2}", performanceCounter.CategoryName, performanceCounter.CounterName, performanceCounter.NextValue());                
            }
        }
    }
}
