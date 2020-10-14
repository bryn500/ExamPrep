using System;
using System.Collections.Generic;
using System.Linq;

namespace Anon
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var test = new
            {
                Message = "Hello",
                Nested = new
                {
                    Numbers = new List<int>() { 1, 2 }
                }
            };

            // can't cast
            //var testCast = (Top)test;

            // readonly so can't do the following            
            //test.Message = "New string";

            // but can do this
            test.Nested.Numbers.Add(3);

            Console.WriteLine(test.Message);
            Console.WriteLine(test.Nested.Numbers[0]);
            Console.WriteLine(CanRead(test));
            CantDo(test);

            // often used in LINQ             
            var result = test.Nested.Numbers.Select(x => new 
            {
                Key = Guid.NewGuid(),
                Value = x + 1
            }).ToList();

            Console.WriteLine(result[0]);
        }

        private static bool CanRead(dynamic data)
        {
            if (data.Message == "Hello" && data.Nested.Numbers.Count == 3)
                return true;
            else
                return false;
        }

        private static void CantDo(dynamic data)
        {
            try
            {
                data.Message = 1;
            }
            catch
            {
                Console.WriteLine("type resolved at runtime, can't assign an int to a string");
            }

            try
            {
                data.NewField = "New data";
            }
            catch
            {
                Console.WriteLine("type resolved at runtime, can't assign new fields");
            }
        }

        private class Top
        {
            public string Message { get; set; }
            public Nested Nested { get; set; }
        }

        private class Nested
        {
            public List<int> Numbers { get; set; }
        }
    }

}
