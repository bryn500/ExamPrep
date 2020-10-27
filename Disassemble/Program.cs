using System;

/// <summary>
/// You write c#
/// This is compiled by a c# compiler Generates: IL (intermidiate language) language-independent and CPU-independent code
/// This goes through JIT at runtime: generates native instructions for cpu 
/// At run time, IL runs in the context of .net, which translates IL into CPU-specific instructions for the processor on the computer running the application.
/// JIT compiler does this
/// 
/// https://docs.microsoft.com/en-us/dotnet/framework/tools/ildasm-exe-il-disassembler
/// https://docs.microsoft.com/en-us/dotnet/framework/tools/ilasm-exe-il-assembler
/// ildasm converts the exe to IL
/// ilasm converts the Il to exe
/// exe is a wrapper around the IL
/// It tells the OS to run the IL in the context of a specific .net runtime
/// </summary>
namespace Disassemble
{
    /// Unsure how this works in .net core in comparisson to .net4
    /// To play around with ildasm/idasm use the developer command prompt for visual studio
    /// csc.exe Program.cs 
    /// ildasm /out=mycode.txt Program.exe
    /// Look at generated file to see what the compiler generated
    /// ildasm /out=mycode.il Program.exe
    /// ilasm /out=recompiled.exe mycode.il 
    public static class Program
    {
        public static void Main(string[] args)
        {
            var test = new MyClass();
            test.DoSomething();
            Console.ReadLine();
        }
    }

    public class MyClass
    {
        public int DoSomething()
        {
            Console.WriteLine("Hello World");
            return 9999;
        }
    }
}
