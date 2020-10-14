using System;
using System.Collections.Generic;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {   
            var nonGeneric = new NonGenericList();
            nonGeneric.Add(1);

            var generic = new GenericList<int>();
            generic.Add(1);

            var generic2 = new GenericList<string>();
            generic2.Add("test");
        }
    }

    public class NonGenericList
    {
        public int[] Items { get; set; } = new int[0];

        public void Add(int input)
        {
            int[] result = new int[Items.Length + 1];

            Items.CopyTo(result, 0);

            result[Items.Length] = input;
        }

        public int Count => Items.Length;
    }

    public class GenericList<T>
    {
        public T[] Items { get; set; } = new T[0];

        public void Add(T input)
        {
            T[] result = new T[Items.Length + 1];

            Items.CopyTo(result, 0);

            result[Items.Length] = input;
        }

        public int Count => Items.Length;
    }
}
