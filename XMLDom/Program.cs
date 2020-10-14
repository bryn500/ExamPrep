using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XMLDom
{
    /// <summary>
    /// Document Object Model (DOM) - in-memory representation of an XML document
    /// Javascript in browser works with DOM, for the HTML file
    /// </summary>
    public static class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string TestFileLocation = $"{path}\\test.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(MyXmlData));

            using (FileStream fileStream = new FileStream(TestFileLocation, FileMode.Open))
            {
                MyXmlData result = (MyXmlData)serializer.Deserialize(fileStream);
                Console.WriteLine($"{result.Books.Count} - {result.Books[0].Author}");
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(TestFileLocation);
            Console.WriteLine(doc.ChildNodes[0].InnerText);
        }
    }
}
