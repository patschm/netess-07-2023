using System.Xml;
using System.Xml.Serialization;

namespace FeedReader;
class Program
{
    static HttpClient client = new HttpClient
    {
        BaseAddress = new Uri("https://www.nu.nl/rss/")
    };
    static async Task Main(string[] args)
    {
        await foreach (Item item in GetFeed())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(item.Category);
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine(item.Title);
            Console.ResetColor();
            System.Console.WriteLine(item.Description);
            System.Console.WriteLine("===================================================");
        }
    }

    private static async IAsyncEnumerable<Item> GetFeed()
    {
        XmlSerializer ser = new XmlSerializer(typeof(Item));
        var response = await client.GetAsync("algemeen");
        if (response.IsSuccessStatusCode)
        {
            var str = await response.Content.ReadAsStreamAsync();
            var rdr = XmlReader.Create(str);
            while (rdr.ReadToFollowing("item"))
            {
                var item = ser.Deserialize(rdr) as Item;
                if (item != null)
                    yield return item;
            }
        }
    }
}
