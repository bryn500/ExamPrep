using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Copying
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var test = new MyClass();
            var directCopy = test; // just a reference to the same object

            var shallowCopy = test.ShallowCopy(); // copies of values, links to reference

            var serialized = JsonSerializer.Serialize(test);
            var deepCopy = JsonSerializer.Deserialize<MyClass>(serialized); // copy of everything            

            test.MyInt = 2;
            test.MyList = new List<int>() { 3, 4 };
            test.MyString = "Changed";
            test.MySubClass.MyInt = 3;
            test.MySubClass.MyList = new List<int>() { 5, 5 };
            test.MySubClass.MyString = "Changed sub";

            Console.WriteLine("Done");
        }
    }

    public class MyClass
    {
        public int MyInt { get; set; }
        public List<int> MyList { get; set; }
        public string MyString { get; set; }

        public SubClass MySubClass { get; set; }

        public MyClass()
        {
            MyInt = 1;
            MyString = "Test";
            MyList = new List<int>() { 1, 2 };
            MySubClass = new SubClass();
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }

    public class SubClass
    {
        public int MyInt { get; set; }
        public List<int> MyList { get; set; }
        public string MyString { get; set; }

        public SubClass()
        {
            MyInt = 1;
            MyString = "Test";
            MyList = new List<int>() { 1, 2 };
        }
    }
}
