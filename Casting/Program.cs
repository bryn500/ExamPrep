using System;
using System.Collections.Generic;

namespace Casting
{
    public static class Program
    {
        static void Main(string[] args)
        {
            List<IBase> tests = GetTests();

            foreach (var test in tests)
            {
                var typeTest = test.GetType();

                // Type comparisson
                // Will only work on exact type
                // Can't be used for interfaces/extended classes
                Console.WriteLine($"Type comparison tests for {typeTest.Name}...");
                TypeTest<IBase>(typeTest);
                TypeTest<ITest>(typeTest);
                TypeTest<Test1>(typeTest);
                TypeTest<Test2>(typeTest);
                TypeTest<Test3>(typeTest);
                TypeTest<SubClass>(typeTest);
                Console.WriteLine("");
            }

            Console.WriteLine("----------------");
            Console.WriteLine("");

            foreach (var test in tests)
            {
                var typeTest = test.GetType();

                // Casting
                // Can cast to self
                // Can cast to inheritted type
                // Throws excpetion when failing to cast
                Console.WriteLine($"Casting tests for {typeTest.Name}...");
                CastTest<IBase>(test, typeTest.Name);
                CastTest<ITest>(test, typeTest.Name);
                CastTest<Test1>(test, typeTest.Name);
                CastTest<Test2>(test, typeTest.Name);
                CastTest<Test3>(test, typeTest.Name);
                CastTest<SubClass>(test, typeTest.Name);
                Console.WriteLine("");
            }

            Console.WriteLine("----------------");
            Console.WriteLine("");

            foreach (var test in tests)
            {
                var typeTest = test.GetType();

                // is 
                // compares inheritted types and returns a boolean
                // can then cast after checking with is if needed
                Console.WriteLine($"Is tests for {typeTest.Name}...");
                IsTest<IBase>(test, typeTest.Name);
                IsTest<ITest>(test, typeTest.Name);
                IsTest<Test1>(test, typeTest.Name);
                IsTest<Test2>(test, typeTest.Name);
                IsTest<Test3>(test, typeTest.Name);
                IsTest<SubClass>(test, typeTest.Name);
                Console.WriteLine("");
            }

            Console.WriteLine("----------------");
            Console.WriteLine("");

            foreach (var test in tests)
            {
                var typeTest = test.GetType();

                // as
                // attempts to cast to inheritted type
                // returns null if it fails
                Console.WriteLine($"As tests for {typeTest.Name}...");
                AsTest<IBase>(test, typeTest.Name);
                AsTest<ITest>(test, typeTest.Name);
                AsTest<Test1>(test, typeTest.Name);
                AsTest<Test2>(test, typeTest.Name);
                AsTest<Test3>(test, typeTest.Name);
                AsTest<SubClass>(test, typeTest.Name);
                Console.WriteLine("");
            }


            // don't use as/is for value types:
            float f = 10.01f;
            Console.WriteLine((long)f); // cast directly
            Console.WriteLine(Convert.ToInt64(f)); // or use Convert.To...
        }

        public static void TypeTest<TResult>(Type type)
        {
            var typeOfGeneric = typeof(TResult);

            if (type == typeOfGeneric)
                Console.WriteLine($"{type.Name} TYPE is: {typeOfGeneric.Name}");
            else
                Console.WriteLine($"{type.Name} TYPE is NOT: {typeOfGeneric.Name}");
        }

        public static void CastTest<TResult>(IBase item, string typeName)
        {
            var typeOfGeneric = typeof(TResult).Name;

            try
            {
                var result = (TResult)item;

                Console.WriteLine($"Successfully cast {typeName} into {typeOfGeneric}");
            }
            catch
            {
                Console.WriteLine($"Failed to cast {typeName} into {typeOfGeneric}");
            }
        }

        public static void IsTest<TResult>(IBase item, string typeName)
        {
            var typeOfGeneric = typeof(TResult).Name;

            if (item is TResult)
                Console.WriteLine($"{typeName} IS a {typeOfGeneric}");
            else
                Console.WriteLine($"{typeName} is NOT a {typeOfGeneric}");
        }

        public static void AsTest<TResult>(IBase item, string typeName) where TResult : class
        {
            var typeOfGeneric = typeof(TResult).Name;

            var asResult = item as TResult;

            if (asResult == null)
                Console.WriteLine($"AS was NULL for: {typeName} as {typeOfGeneric}");
            else
                Console.WriteLine($"AS was successful for: {typeName} as {typeOfGeneric}");
        }

        static List<IBase> GetTests()
        {
            return new List<IBase>() {
                new Test1(),
                new Test2(),
                new Test3(),
                new SubClass()
            };
        }
    }

    public interface IBase
    {
        void DoSomething();
    }

    public interface ITest : IBase
    {
        int ID { get; set; }
    }

    public class Test1 : ITest
    {
        public int ID { get; set; }
        public string Different { get; set; }

        public void DoSomething()
        {
            Console.WriteLine("Working on Test1");
        }
    }

    public class Test2 : ITest
    {
        public int ID { get; set; }
        public DateTime Different { get; set; }

        public void DoSomething()
        {
            Console.WriteLine("Working on Test2");
        }
    }

    public class Test3 : IBase
    {
        public int ID { get; set; }

        public virtual void DoSomething()
        {
            Console.WriteLine("Working on Test3");
        }
    }

    public class SubClass : Test3
    {
        public override void DoSomething()
        {
            Console.WriteLine("Working on SubClass");
        }
    }
}
