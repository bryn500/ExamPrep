using System;
using System.Collections.Generic;

/// <summary>
/// https://csharpindepth.com/Articles/Closures
/// closures allow you to encapsulate some behaviour, pass it around like any other object, and still have access to the context in which they were first declared
/// </summary>
namespace Closure
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = new List<string>() { "1", "12", "123", "1234", "12345", "12346", "1234567" };

            Console.Write("Maximum length of string to include? ");
            int userMaxLength = int.Parse(Console.ReadLine()); // userMaxLength scope is in Main
            MyPredicate<string> predicate = item => item.Length < userMaxLength; // we create an anonymous method that uses a variable decalred outside of itself
            IList<string> shortWords = ListUtil.Filter(words, predicate); // this method is passed to another method
            ListUtil.Dump(shortWords); // and userMaxLength int is still available

            Console.WriteLine("Changing the value of the scoped variable within the predicate:");
            userMaxLength = 0;
            MyPredicate<string> predicate2 = item => { userMaxLength++; return item.Length <= userMaxLength; };
            IList<string> shortWords2 = ListUtil.Filter(words, predicate2);
            ListUtil.Dump(shortWords2);
        }

        public delegate bool MyPredicate<T>(T obj);

        public static class ListUtil
        {
            public static IList<T> Filter<T>(IList<T> source, MyPredicate<T> predicate)
            {
                List<T> ret = new List<T>();
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        ret.Add(item);
                    }
                }
                return ret;
            }

            public static IList<T> Dump<T>(IList<T> source)
            {
                List<T> ret = new List<T>();
                foreach (T item in source)
                {
                    Console.WriteLine(item);
                }
                return ret;
            }
        }
    }
}
