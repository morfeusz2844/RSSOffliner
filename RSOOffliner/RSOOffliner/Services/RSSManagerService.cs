using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RSOOffliner.Models;
using RSOOffliner.ViewModels;

namespace RSOOffliner.Services
{
    public static class RSSManagerService
    {
        public static void GetFeed(RSSManagerViewModel rssManagerView)
        {
            if (String.IsNullOrEmpty(rssManagerView.Url))
            {
                throw new ArgumentException("You must provide a feed URL");
            }
            using (XmlReader reader = XmlReader.Create(rssManagerView.Url))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
                rssManagerView.FeedTitle = ParseDocElements(xmlDoc.SelectSingleNode("//channel"), "title");
                rssManagerView.FeedDescription = ParseDocElements(xmlDoc.SelectSingleNode("//channel"), "description");
                ParseRssItems(xmlDoc, rssManagerView);
            }
        }

        private static string ParseDocElements(XmlNode parent, string xPath)
        {
            XmlNode node = parent.SelectSingleNode(xPath);
            if (node != null)
                return node.InnerText;
            else
                return "Unresolvable";
        }

        private static void ParseRssItems(XmlDocument xmlDoc, RSSManagerViewModel rssManagerView)
        {
            rssManagerView.RssItem.Clear();
            XmlNodeList nodes = xmlDoc.SelectNodes("rss/channel/item");

            foreach (XmlNode node in nodes)
            {
                RSS item = new RSS();
                item.Title = ParseDocElements(node, "title");
                item.Description = ParseDocElements(node, "description");
                item.Link = ParseDocElements(node, "link");

                string date = null;
                date = ParseDocElements(node, "pubDate");
                //DateTime.TryParse(date, out item.Date);

                rssManagerView.RssItem.Add(item);

            }
        }

        public static string readHtml(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            var data = String.Empty;;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return data;
        }
    }
}
