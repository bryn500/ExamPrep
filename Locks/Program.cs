using System;
using System.Threading;

namespace Locks
{
    class Program
    {
        private static int Count = 0;
        private static object lockObject = new object();
        // lock doesn't stop others from using, only those that also check if it's locked
        // A lock allows only one thread to enter the part that's locked and the lock is not shared with any other processes.

        private static Mutex mut = new Mutex();
        // A Mutex is the same as a lock but it can be system wide (shared by multiple processes). slower

        private static Semaphore sem = new Semaphore(2, 2);
        // A Semaphore does the same as a mutex but allows x number of threads to enter, this can be used for example to limit the number of cpu, io or ram intensive tasks running at the same time.

        private static SemaphoreSlim semSlim = new SemaphoreSlim(0, 2);
        // SemaphoreSlim is a version of semaphore used for this process only, not system wide

        // Semaphore is weird, if initial count is set to 0, nothing can access until there is a release      


        static void Main(string[] args)
        {   
            var thread1 = new Thread(IncrementBad);
            var thread2 = new Thread(IncrementBad);
            thread1.Start();
            Thread.Sleep(500);
            thread2.Start();

            var thread3 = new Thread(IncrementLocked);
            var thread4 = new Thread(IncrementLocked);
            thread3.Start();
            Thread.Sleep(500);
            thread4.Start();
        }

        static void IncrementBad()
        {
            while (true)
            {
                int temp = Count;
                Thread.Sleep(1000);
                Count = temp + 1;
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} incremented to {Count}");
                Thread.Sleep(1000);
            }
        }

        static void IncrementLocked()
        {
            while (true)
            {
                lock (lockObject)
                {
                    int temp = Count;
                    Thread.Sleep(1000);
                    Count = temp + 1;
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} incremented to {Count}");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
