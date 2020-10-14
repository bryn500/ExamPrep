using System;

namespace WCF_Pub
{
    /// <summary>
    /// WCF is a windows framework for building APIs, possibly considered legacy
    /// APIs are a means for applications to talk to one another
    /// WCF allows talking over http largely by transferring xml with SOAP - a protocol to transfer data between applications
    /// Apparently can be set up to use REST achitecture and even return JSON instead of XML
    /// Relatively simple/low code for what it is
    /// Define a contract (interface with attributes)
    /// Expose as a service using System.ServiceModel
    /// Use svcutil.exe to generate client - working
    /// </summary>
    public class TestWcfService : ITestWcfService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }

            return composite;
        }

        public bool NotAContractmethod()
        {
            return true;
        }
    }

    public class Test
    { 
        public Test()
        {
            var test = new TestWcfService();
            test.NotAContractmethod();

        }
    }

}
