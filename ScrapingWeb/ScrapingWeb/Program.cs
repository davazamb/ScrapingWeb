using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ScrapingWeb
{
    class Program
    {

        static string path = @"C:\Users\davaz\Pictures\Web\";
        static List<string> lista = new List<string>();
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            string url = "https://github.com/davazamb/ScrapingWeb";
            HtmlDocument doc = web.Load(url);

            //-----------Obtenr Titles en HTML

            //HtmlNode body = doc.DocumentNode.CssSelect("Body").First();
            //string htmBody = body.InnerHtml;

            //foreach (var item in doc.DocumentNode.CssSelect(".pr-3"))
            //{
            //    var nodoAn = item.CssSelect("a").First();
            //    lista.Add(nodoAn.InnerHtml);
            //}

            //-------Obtener Imagenes por URL
            //string path = @"C:\Users\davaz\Pictures\Web\";
            //int i = 0;
            //foreach (var item in doc.DocumentNode.CssSelect("img"))
            //{

            //    Console.WriteLine("Iniciando copia " + i);
            //    using (WebClient webClient = new WebClient())
            //    {
            //        string imagen = item.GetAttributeValue("src");
            //        webClient.DownloadFile(new Uri(imagen), path + i + ".jpg");
            //    }
            //    i++;
            //}
            //Console.WriteLine("Finalizado!");

            //-------Obtener todas las Imagenes a URL
            foreach (var item in doc.DocumentNode.CssSelect("img"))
            {

                Console.WriteLine("Start copy");
                using (WebClient webClient = new WebClient())
                {
                    string image = item.GetAttributeValue("src");
                    string[] arrayImage = image.Split('/');
                    string name = path + arrayImage[arrayImage.Length - 1];
                    if (!lista.Contains(arrayImage[arrayImage.Length - 1]))
                    {
                        webClient.DownloadFile(new Uri(image), name);
                        lista.Add(arrayImage[arrayImage.Length - 1]);
                    }
                }

            }

            foreach (var item in doc.DocumentNode.CssSelect("a"))
            {
                string href = item.GetAttributeValue("href");
                if (!href.StartsWith("https://github.com/davazamb/ScrapingWeb") && !lista.Contains(href))
                {
                    lista.Add(href);
                    GetImages(href);
                }

            }
            Console.WriteLine("Finish!");

        }

        private static void GetImages (string url, string tab="")
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            foreach (var item in doc.DocumentNode.CssSelect("img"))
            {
                using (WebClient webClient = new WebClient())
                {
                    string image = item.GetAttributeValue("src");
                    string[] arrayImage = image.Split('/');
                    string name = path + arrayImage[arrayImage.Length - 1];
                    if (!lista.Contains(arrayImage[arrayImage.Length - 1]))
                    {
                        webClient.DownloadFile(new Uri(image), name);
                        lista.Add(arrayImage[arrayImage.Length - 1]);
                    }
                }

            }

            foreach (var item in doc.DocumentNode.CssSelect("a"))
            {
                string href = item.GetAttributeValue("href");
                if (!href.StartsWith("https://github.com/davazamb/ScrapingWeb") && !lista.Contains(href))
                {
                    lista.Add(href);
                    GetImages(href, tab + ">");
                }
            }

        }
    }
}
