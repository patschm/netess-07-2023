using System.Xml.Serialization;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cerials;

class Program
{
    static void Main(string[] args)
    {
        Person[] people = new Person[100];
        for (int i = 0; i < people.Length; i++)
        {
            people[i] = new Person { Id = i, Name = "Jan", Age = 63 };
        }
        //SerializingJson(people);
        //SerializingXml(people);

        DeserializeJson();
        //System.Console.WriteLine(p);
    }

    private static void DeserializeJson()
    {
        FileStream fs = File.OpenRead("data.json");
        StreamReader rdr = new StreamReader(fs);
        JsonReader jrdr = new JsonTextReader(rdr);
         JsonSerializer ser = new JsonSerializer();
        ser.ContractResolver = new CamelCasePropertyNamesContractResolver();
        var people = ser.Deserialize<Person[]>(jrdr);
        foreach(var p in people!) System.Console.WriteLine(p);
    }

    private static void SerializingJson(Person[] x)
    {
        FileStream fs = File.Create("data.json");
        StreamWriter wrt = new StreamWriter(fs);
        JsonSerializer ser = new JsonSerializer();
        ser.ContractResolver = new CamelCasePropertyNamesContractResolver();
        ser.Serialize(wrt, x);
        wrt.Close();
    }
    private static void SerializingXml(Person[] x)
    {
        FileStream fs = File.Create("data.xml");
        StreamWriter wrt = new StreamWriter(fs);
        XmlSerializer ser = new XmlSerializer(typeof(Person[]));
        ser.Serialize(wrt, x);
        wrt.Close();
    }
}
