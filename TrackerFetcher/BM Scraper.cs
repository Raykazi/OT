using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackerFetcher
{
    internal class BM_Scraper
    {
        public List<string> BMIDs = new List<string>();
        private string Url { get; set; }
        public BM_Scraper(string url)
        {
            Url = url;
        }
        public async Task ScrapeAsync()
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(Url);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            Parse(document);
        }
        //css-1dpmhly
        private void Parse(IHtmlDocument document)
        {
            BMIDs.Clear();
            foreach (var x in document.All)
            {
                if (x.ClassName == "css-1dpmhly")
                {
                    IHtmlAnchorElement anchor = (IHtmlAnchorElement)x;
                    string path = anchor.PathName;
                    string BM_ID = path.Substring(path.LastIndexOf("/") + 1);
                    BMIDs.Add(BM_ID);
                    Console.WriteLine($"{x.TextContent} {BM_ID}");
                }
            }
        }
    }
}
