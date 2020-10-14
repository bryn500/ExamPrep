using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLDom
{
    [XmlRoot(ElementName = "books")]
    public class MyXmlData
    {
        [XmlElement(ElementName = "book")]
        public List<Book> Books { get; set; }
    }

    [XmlRoot(ElementName = "book")]
    public class Book
    {
        [XmlElement(ElementName = "author")]
        public string Author { get; set; }
        [XmlElement(ElementName = "price")]
        public Price Price { get; set; }
        [XmlElement(ElementName = "pubdate")]
        public string Pubdate { get; set; }
        [XmlElement(ElementName = "pubinfo")]
        public Pubinfo Pubinfo { get; set; }
    }

    [XmlRoot(ElementName = "price")]
    public class Price
    {
        [XmlAttribute(AttributeName = "format")]
        public string Format { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "pubinfo")]
    public class Pubinfo
    {
        [XmlElement(ElementName = "publisher")]
        public string Publisher { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
    }
}
