using System;

namespace Dynamic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // dynamic bypasses static type checking.
            // In most cases, it functions like it has type object. 
            // At compile time, an element that is typed as dynamic is assumed to support any operation
            // At run time this can either succeed or cause a run-time exception

            ExampleClass ec = new ExampleClass();
            // The following call to exampleMethod1 causes a compiler error
            // if exampleMethod1 has only one parameter. Uncomment the line
            // to see the error.
            //ec.ExampleMethod1(10, 4);

            dynamic dynamic_ec = new ExampleClass();
            // The following line is not identified as an error by the
            // compiler, but it causes a run-time exception.
            //dynamic_ec.ExampleMethod1(10, 4);

            // The following call does not cause compiler errors, but does cause a run-time exception
            //dynamic_ec.NonexistentMethod();

            // the following works
            dynamic_ec.ExampleMethod2("Hello");

            // Can convert to dynamic type implicitly
            dynamic d1 = 7;
            dynamic d2 = "a string";
            dynamic d3 = DateTime.Today;
            // implicit conversion can be dynamically applied 
            int i = d1;
            string str = d2;
            DateTime dt = d3;
        }
    }

    public class ExampleClass
    {
        public ExampleClass() { }
        public ExampleClass(int v) { }

        public void ExampleMethod1(int i)
        {
            Console.WriteLine(i);
        }

        public void ExampleMethod2(string str)
        {
            Console.WriteLine(str);
        }
    }
}
