using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCF_Pub;

namespace WCF_Host
{
    public static class Program
    {
        private const string ClientAddress = "http://localhost:8000/GettingStarted/"; //TestWcfService
        
        static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.
            Uri baseAddress = new Uri(ClientAddress); 

            // Step 2: Create a ServiceHost instance.
            var selfHost = new ServiceHost(typeof(TestWcfService), baseAddress);

            try
            {
                // Step 3: Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(ITestWcfService), new WSHttpBinding(), "TestWcfService");

                // Step 4: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };

                selfHost.Description.Behaviors.Add(smb);

                // Step 5: Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
