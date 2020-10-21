using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Multi threading allows program to do multiple strands of work at the same time
/// Stops one strand of work having to wait for another to complete first
/// Can allow more responsive programs (one part of prgram doesn't freeze because another is working)
/// Can speed things up as well depending on scenario
/// https://docs.microsoft.com/en-us/dotnet/standard/threading/managed-threading-best-practices
/// Multithreading solves problems with throughput and responsiveness, but in doing so it introduces new problems: deadlocks and race conditions.
/// A deadlock occurs when each of two threads tries to lock a resource the other has already locked. Neither thread can make any further progress.
/// A race condition is a bug that occurs when the outcome of a program depends on which of two or more threads reaches a particular block of code first. 
/// Running the program many times produces different results, and the result of any given run cannot be predicted.
/// </summary>
namespace MultiThreading
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var worker = new Worker();
            await worker.DoTheWork();
        }
    }

    public class Worker
    {
        public async Task DoTheWork()
        {
            var printer = new Printer(); // race condition
            var monitored = new MonitoredPrinter(); // using monitor
            var locked = new LockedPrinter(); // using lock    
            var asyncPrinter = new AsyncPrinter();

            var task = asyncPrinter.PrintLetters();
            //ThreadWork(printer);
            //ThreadPoolWork(printer);
            //ParallelForEachWork(printer);
            //ParallelInvoke(printer);
            //TasksWork(printer);
            //PlinqWork(printer);
            await task;
        }

        private void ThreadWork(Printer p)
        {
            // create an array of ten threads that will each print the numbers out
            Thread[] threads = new Thread[10];
            for (var i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers));
            }

            foreach (var thread in threads)
                thread.Start();
        }

        private void ThreadPoolWork(Printer p)
        {
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            for (var i = 0; i < 10; i++)
                ThreadPool.QueueUserWorkItem(workItem, p);

            Console.ReadLine(); // would need a way to wait

            void PrintTheNumbers(object state)
            {
                Printer task = (Printer)state;
                task.PrintNumbers();
            }
        }

        private void ParallelForEachWork(Printer printer)
        {
            var list = Enumerable.Range(0, 10);
            Parallel.ForEach(list, (item) =>
            {
                printer.PrintNumbers();
            });
        }

        private void ParallelInvoke(Printer printer)
        {
            Parallel.Invoke(() =>
            {
                printer.PrintNumbers();
            }, () =>
            {
                printer.PrintNumbers();
            });
        }

        private void TasksWork(Printer p)
        {
            var list = Enumerable.Range(0, 10);
            var tasks = new List<Task>();
            foreach (var item in list)
            {
                var task = new Task(p.PrintNumbers);
                tasks.Add(task);
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());

            // otherwise need a way to wait
            //foreach (var item in list)
            //{
            //    Task.Factory.StartNew(p.PrintNumbers);
            //}
            //Console.ReadLine(); 
        }

        private void PlinqWork(Printer printer)
        {
            var list = Enumerable.Range(0, 10);

            list.AsParallel()
                .ForAll((int i) =>
                {
                    printer.PrintNumbers();
                });
        }
    }

    public class Printer
    {
        public virtual void PrintNumbers()
        {
            for (var i = 0; i < 10; i++)
            {
                Random r = new Random();
                Thread.Sleep(300 * r.Next(5));
                Console.Write(i);
            }
            Console.WriteLine();
        }
    }

    public class LockedPrinter : Printer
    {
        private readonly object lockObject = new object();

        public override void PrintNumbers()
        {
            lock (lockObject)
            {
                for (var i = 0; i < 10; i++)
                {
                    Random r = new Random();
                    Thread.Sleep(300 * r.Next(5));
                    Console.Write(i);
                }
                Console.WriteLine();
            }
        }
    }

    public class MonitoredPrinter : Printer
    {
        private readonly object lockObject = new object();
        public override void PrintNumbers()
        {
            Monitor.Enter(lockObject);
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    Random r = new Random();
                    Thread.Sleep(300 * r.Next(5));
                    Console.Write(i);
                }
                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
    }

    public class AsyncPrinter
    {
        public async Task PrintLetters()
        {
            for (var i = 65; i < 75; i++)
            {
                Random r = new Random();
                await Task.Run(() =>
                {
                    Thread.Sleep(300 * r.Next(5));
                });
                Console.Write((char)i);
            }
            Console.WriteLine("Finished printing letters");
        }
    }
}
