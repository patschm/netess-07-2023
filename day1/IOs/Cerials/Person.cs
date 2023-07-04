using System.Xml.Serialization;

namespace Cerials;

[XmlType("person")]
public class Person
{
    [XmlAttribute("id")]
    public int Id { get; set; }
    [XmlElement("name")]
    public string? Name { get; set; }
    [XmlElement("age")]
    public int Age { get; set; }

    public override string ToString()
    {
        return $"[{Id}] {Name} ({Age})";
    }
}