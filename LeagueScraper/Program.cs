

using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace OhPeeScraper
{
  internal class Program
  {
    private static string path = Directory.GetCurrentDirectory() + "\\acc.txt";

    private static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("EUW");
        Console.WriteLine("NA");
        Console.WriteLine("EUNE");
        Console.WriteLine("TR");
        Console.WriteLine(" ");
        Console.Write("Enter region: ");
        string str = "http://" + Console.ReadLine() + ".op.gg/ranking/ladder/page=";
        Console.WriteLine("Current divisions:");
        Console.WriteLine("Challenger is from 1 to 2");
        Console.WriteLine("Master is from 3 to 6");
        Console.WriteLine("Diamond is from 6 to 100");
        Console.WriteLine("Platinum is from 100 to 600");
        Console.WriteLine("Gold is from 600 to 2100");
        Console.WriteLine("silver is 2100 until 6000.");
        Console.WriteLine("Bronze is 6000 and more");
        Console.WriteLine("Care! It can change overtime. I am just giving you an idea of what you are scraping.");
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.WriteLine(" ");
        Console.Write("On which page would you like to start? (E.g. '1000') ");
        int int32_1 = Convert.ToInt32(Console.ReadLine());
        Console.Write("On which page would you like to stop? (E.g. '1010') ");
        int int32_2 = Convert.ToInt32(Console.ReadLine());
        using (StreamWriter streamWriter = System.IO.File.AppendText(Program.path))
        {
          for (int index1 = int32_1; index1 < int32_2; ++index1)
          {
            HttpWebResponse response = (HttpWebResponse) (WebRequest.Create(str + (object) index1) as HttpWebRequest).GetResponse();
            WebHeaderCollection headers = response.Headers;
            Encoding utF8 = Encoding.UTF8;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), utF8))
            {
              string end = streamReader.ReadToEnd();
              HtmlDocument htmlDocument = new HtmlDocument();
              htmlDocument.LoadHtml(end);
              HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//tr[contains(@class, 'ranking-table__row')]");
              for (int index2 = 0; index2 < htmlNodeCollection.Count; ++index2)
                streamWriter.WriteLine(htmlNodeCollection[index2].ChildNodes[1].InnerText);
              if (index1 == int32_2 - 1)
                Console.Write("Finished! Please check acc.txt.(Show some love to BleachSlave2)");
            }
            Console.Title = "BleachSlave2 League Scraper. Current page: " + (object) index1;
          }
          Console.ReadKey();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
        Console.ReadLine();
      }
    }
  }
}
