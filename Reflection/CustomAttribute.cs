using System;

namespace Reflection
{
    /// <summary>
    /// An attribute 
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Class)] // you can limit where the attribute can be applied, with an attribute
    public class TestClassAttribute : Attribute
    {
        public string MyData { get; set; }

        // This constructor defines a required parameters: name.
        public TestClassAttribute(string myData)
        {
            MyData = myData;
        }
    }

    [AttributeUsage(AttributeTargets.Method)] // you can limit where the attribute can be applied, with an attribute
    public class TestMethodAttribute : Attribute
    {
    }
}
