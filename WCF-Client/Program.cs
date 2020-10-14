using System;

namespace WCF_Client
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Step 1: Create an instance of the WCF proxy.
            TestWcfServiceClient client = new TestWcfServiceClient();

            // Step 2: Call the service operations.
            var result = client.GetData(1);
            Console.WriteLine($"GetData(1): {result}");

            // Call the Subtract service operation.

            var result2 = client.GetDataUsingDataContract(new WCF_Pub.CompositeType() { BoolValue = true });
            Console.WriteLine($"GetDataUsingDataContract(): {result2.BoolValue}|{result2.StringValue}");

            //var notValid = result2.NotADataMember;
            //var notValid = client.NotAContractmethod;

            // Step 3: Close the client to gracefully close the connection and clean up resources.
            Console.WriteLine("\nPress <Enter> to terminate the client.");
            Console.ReadLine();
            client.Close();
        }
    }
}
