using System;
using System.Collections.Generic;

namespace Delegates
{   
    public class Program
    {
        // a class with an invoke method that calls the method
        public delegate bool NumberChecker(int n);

        public delegate TReturn GenericDelegate<TArgument, TArgument2, TReturn>(TArgument n, TArgument2 x);

        private static bool LessThan5(int n) { 
            return n < 5; 
        }
        private static bool GreaterThan10(int n) { return n > 10; }

        static void Main(string[] args)
        {
            int[] numbers = new[] { 2, 3, 4, 5, 6, 7, 12, 13, 15 };

            IEnumerable<int> result = CheckNumbers(numbers, LessThan5);
            IEnumerable<int> result2 = CheckNumbers(numbers, GreaterThan10);
            IEnumerable<int> result3 = CheckNumbers(numbers, x => x > 3);

            IEnumerable<int> result4 = CheckNumbersWithFunc(numbers, x => x > 3);
            IEnumerable<int> result5 = CheckNumbersWithFunc(numbers, GreaterThan10, x => Console.WriteLine(x));

            foreach (var n in result)
                Console.WriteLine(n);
        }

        private static IEnumerable<int> CheckNumbers(IEnumerable<int> numbers, NumberChecker checker)
        {
            List<int> result = new List<int>();

            foreach(var n in numbers)
            {
                //checker.Invoke(n);
                if (checker(n))
                    result.Add(n);
            }

            return result;
        }

        private static IEnumerable<int> CheckNumbersWithFunc(IEnumerable<int> numbers, Func<int, bool> checker, Action<int> DoSomething = null)
        {
            List<int> result = new List<int>();

            foreach (var n in numbers)
            {
                DoSomething(n);

                if (checker(n))
                    result.Add(n);
            }

            return result;
        }
    }
}
