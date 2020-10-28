using System;

/// <summary>
/// You write c#/f#/vb
/// This is compiled by a compiler that generates: CIL (common intermidiate language) language-independent and CPU-independent code (previously called MSIL)
/// csc.exe (c# compiler, other languages will have their own)
///
/// The Common Language Runtime (.net framework) CoreCLR (.net core) runs the CIL
/// At run time, CIL runs in the context of .net, which translates CIL into CPU-specific instructions for the processor on the computer running the application.
/// This translation is done by a process called JIT (just in time)
/// 
/// https://docs.microsoft.com/en-us/dotnet/framework/tools/ildasm-exe-il-disassembler
/// https://docs.microsoft.com/en-us/dotnet/framework/tools/ilasm-exe-il-assembler
/// ildasm converts the exe to CIL
/// ilasm converts the CIl to exe
/// exe is a wrapper around the CIL
/// It tells the OS to run the CIL in the context of a specific .net runtime
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
