using System.Text;
using System.Xml;

namespace Stromingen;
class Program
{
    static void Main(string[] args)
    {
        //SpartaansSchrijven();
        //SpartaansLezen();
        //ModerneMensSchrijven();
        //ModerneMensLezen();
        //XmlSchrijven();
        XmlLezen();
    }

    private static void XmlLezen()
    {
         FileStream fs = File.OpenRead("modern.xml");
         XmlReader rdr = XmlReader.Create(fs);
        //  while(rdr.ReadToFollowing("eerste"))
        //  {
        //     System.Console.WriteLine(rdr.ReadElementContentAsString());
        //  }
        //  while(rdr.ReadToFollowing("greet"))
        //  {
        //     rdr.MoveToAttribute("id");
        //     System.Console.WriteLine(rdr.Value);
        //  }
        string result = "";
        while(rdr.ReadToFollowing("greet"))
            result = rdr.ReadOuterXml();
        System.Console.WriteLine(result);
    }

    private static void XmlSchrijven()
    {
         FileStream fs = File.Create("modern.xml");
         XmlWriter writer = XmlWriter.Create(fs);
         writer.WriteStartDocument();
         writer.WriteStartElement("root");
         for(int i = 0; i < 50000000; i++)
         {
            writer.WriteStartElement("greet");
            writer.WriteAttributeString("id", i.ToString());

                writer.WriteStartElement("eerste");
                writer.WriteString("Hello");
                writer.WriteEndElement();

                writer.WriteElementString("tweede", "World");

            writer.WriteEndElement();
         }
         writer.WriteEndElement();

        writer.Flush();
        writer.Close();
    }

    private static void ModerneMensLezen()
    {
       FileStream fs = File.OpenRead("modern.txt");
       StreamReader rdr = new StreamReader(fs);

       var data = string.Empty;
       while ((data = rdr.ReadLine()) != null)
            System.Console.WriteLine(data);

    }

    private static void ModerneMensSchrijven()
    {
        string text = "Hello World ";
        FileStream fs = File.Create("modern.txt");
        StreamWriter writer = new StreamWriter(fs);
        for (int i = 0; i < 1000; i++)
        { 
            writer.WriteLine(text + i);
        }
        writer.Flush();
        fs.Close();
    }

    private static void SpartaansLezen()
    {
        FileStream fs = File.OpenRead("spartaans.txt");

        byte[] buffer = new byte[100];


        while (fs.Read(buffer, 0, buffer.Length) > 0)
        {         
            string txt = Encoding.UTF8.GetString(buffer);
            System.Console.Write(txt);
            Array.Clear(buffer, 0, buffer.Length);
        }
        Console.Write("Hoi");
    }

    private static void SpartaansSchrijven()
    {
        string text = "Hello World ";
        FileStream fs = File.Create("spartaans.txt");
        for (int i = 0; i < 1000; i++)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text + i + "\r\n");
            fs.Write(buffer, 0, buffer.Length);
        }
        fs.Close();
    }
}
