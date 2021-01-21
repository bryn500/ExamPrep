using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace Serialization
{
    public class Program
    {
        static void Main(string[] args)
        {
            // create some data
            SomethingToTransfer transferData = new SomethingToTransfer()
            {
                Id = 1,
                Name = "A name",
                PrivateStuffDontSerialize = "Password",
                SomeStuff = new List<Inner>() {
                    new Inner() {
                        Name = "inner name"
                    },
                    new Inner() {
                        Name = "inner name 2"
                    }
                }
            };

            SomethingToTransfer deserilizeResult;

            // JSON
            Console.WriteLine(Environment.NewLine + "JSON" + Environment.NewLine);

            var json = JsonConvert.SerializeObject(transferData, Formatting.Indented);
            Console.WriteLine(json);
            deserilizeResult = JsonConvert.DeserializeObject<SomethingToTransfer>(json);
            Console.WriteLine("Should be empty:" + deserilizeResult.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + deserilizeResult.SomeStuff.First().Name);

            // XML
            Console.WriteLine(Environment.NewLine + "XML" + Environment.NewLine);
            var ser = new XmlSerializer(typeof(SomethingToTransfer));
            var writer = new StringWriter();
            ser.Serialize(writer, transferData);
            Console.WriteLine(writer.ToString());

            var reader = new StringReader(writer.ToString());
            deserilizeResult = (SomethingToTransfer)ser.Deserialize(reader);
            Console.WriteLine("Should be empty:" + deserilizeResult.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + deserilizeResult.SomeStuff.First().Name);

            writer.Close();
            writer.Dispose();
            reader.Close();
            reader.Dispose();

            // DataContract
            Console.WriteLine(Environment.NewLine + "XML - DataContract" + Environment.NewLine);
            var dcSer = new DataContractSerializer(typeof(SomethingToTransfer));
            var ms = new MemoryStream();
            dcSer.WriteObject(ms, transferData);
            Console.WriteLine(Encoding.Default.GetString(ms.ToArray()));
            ms.Seek(0, SeekOrigin.Begin);
            deserilizeResult = (SomethingToTransfer)dcSer.ReadObject(ms);
            Console.WriteLine("Should be empty:" + deserilizeResult.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + deserilizeResult.SomeStuff.First().Name);
            ms.Dispose();

            // Json
            Console.WriteLine(Environment.NewLine + "JSON - DataContract" + Environment.NewLine);
            var jsonSer = new DataContractJsonSerializer(typeof(SomethingToTransfer));
            var jsonMs = new MemoryStream();
            jsonSer.WriteObject(jsonMs, transferData);
            Console.WriteLine(Encoding.Default.GetString(jsonMs.ToArray()));
            jsonMs.Seek(0, SeekOrigin.Begin);
            deserilizeResult = (SomethingToTransfer)jsonSer.ReadObject(jsonMs);
            Console.WriteLine("Should be empty:" + deserilizeResult.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + deserilizeResult.SomeStuff.First().Name);
            jsonMs.Dispose();

            // Binary
            // a security risk avoid
            Console.WriteLine(Environment.NewLine + "Binary" + Environment.NewLine);
            var bf = new BinaryFormatter();
            var binMS = new MemoryStream();
            bf.Serialize(binMS, transferData);
            Console.WriteLine(Encoding.Default.GetString(binMS.ToArray()));
            binMS.Seek(0, SeekOrigin.Begin);
            deserilizeResult = (SomethingToTransfer)bf.Deserialize(binMS);
            Console.WriteLine("Should be empty:" + deserilizeResult.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + deserilizeResult.SomeStuff.First().Name);
            binMS.Dispose();

            // Custom
            Console.WriteLine(Environment.NewLine + "Custom" + Environment.NewLine);
            var customData = new Custom()
            {
                Id = 1,
                Name = "Test",
                PrivateStuffDontSerialize = "Password"
            };
            var json2 = JsonConvert.SerializeObject(customData, Formatting.Indented);
            Console.WriteLine(json2);
            var result = JsonConvert.DeserializeObject<Custom>(json2);
            Console.WriteLine("Should be empty:" + result.PrivateStuffDontSerialize);
            Console.WriteLine("Should be populated:" + result.Name);
        }
    }

    [DataContract] // DataContractSerializers (newtonsoft uses these as well)
    [Serializable] // BinaryFormatter
    public class SomethingToTransfer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<Inner> SomeStuff { get; set; }

        [DataMember]
        public string DynamicData
        {
            get { return Id + Name; }
            set { } // needed for data contract
        }

        // lack of data member attribute is used by datacontract and newtonsoft
        [XmlIgnore] // for XmlSerializer
        [field: NonSerialized] // for binary formatter - for older versions of .net would need to use a backing field
        public string PrivateStuffDontSerialize { get; set; }

        [DataMember]
        public string AField = "Hello";

        public string AMethod()
        {
            return "return value of this method";
        }
    }

    [Serializable] // only needed for binary formatter
    public class Inner
    {
        public string Name { get; set; }
    }


    [Serializable]
    public class Custom : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivateStuffDontSerialize { get; set; }


        public Custom()
        {
            // ...
        }

        protected Custom(SerializationInfo info, StreamingContext context)
        {
            // Deserialize
            Id = info.GetInt32("id");
            Name = info.GetString("NAME");
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize
            info.AddValue("id", Id);
            info.AddValue("NAME", Name);
        }
    }
}
