using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ScrapingWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lista = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://github.com/davazamb/ScrapingWeb");

            HtmlNode body = doc.DocumentNode.CssSelect("Body").First();
            string htmBody = body.InnerHtml;

            foreach (var item in doc.DocumentNode.CssSelect(".pr-3"))
            {
                var nodoAn = item.CssSelect("a").First();
                lista.Add(nodoAn.InnerHtml);
            }
        }
    }
}
