using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// https://www.youtube.com/playlist?list=PLRwVmtr-pp05brRDYXh-OTAIi-9kYcw3r
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            //Find, execute, and create types at runtime by using reflection.
            //This objective may include but is not limited to:
            //Create and apply attributes; read attributes;
            //generate code at runtime by using CodeDom and lambda expressions;
            //use types from the System.Reflection namespace (Assembly, PropertyInfo, MethodInfo, Type)


            IEnumerable<Type> testSuites = Assembly
                .GetExecutingAssembly()
                .GetTypes() // get all types in this assembly/exe
                .Where(x =>
                    x.GetCustomAttributes(false)
                        .Any(z => z is TestClassAttribute)); // where that type has an attribute: TestClassAttribute 

            foreach (Type testSuite in testSuites)
            {
                Console.WriteLine(testSuite.Name);
                object attribute = testSuite.GetCustomAttributes(false).FirstOrDefault(x => x is TestClassAttribute);
                TestClassAttribute testAttribute = attribute as TestClassAttribute;

                // getting property value from attribute
                Console.WriteLine(testAttribute.MyData);

                // gettting property value via reflection
                PropertyInfo prop = testAttribute.GetType().GetProperty("MyData");
                Console.WriteLine(prop.GetValue(testAttribute));

                // All methods in MyTests class
                MethodInfo[] suiteMethods = testSuite.GetMethods();

                // all methods with the TestMethodAttribute
                IEnumerable<MethodInfo> testMethods = suiteMethods
                        .Where(x => x.GetCustomAttributes(false)
                            .Any(z => z is TestMethodAttribute));

                // Create instance of our test suite
                object testSuiteIntance = Activator.CreateInstance(testSuite);

                foreach (MethodInfo method in testMethods)
                {
                    method.Invoke(testSuiteIntance, new object[0]);
                }
            }

            //var testClass = new LambdaCreation();
            //testClass.DoStuff();
        }
    }

    [TestClass("Hello")]
    public class MyTests
    {
        [TestMethod]
        public void DoATest()
        {
            Console.WriteLine("DoATest: Success");
        }

        [TestMethod]
        public void DoAnotherTest()
        {
            Console.WriteLine("DoAnotherTest: Success");
        }

        public void DontDoThis()
        {
            Console.WriteLine("Don't call this method");
        }
    }
}
