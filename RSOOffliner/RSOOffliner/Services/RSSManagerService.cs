using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
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
                ParseRssItems(ref xmlDoc, ref rssManagerView);
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

        private static void ParseRssItems(ref XmlDocument xmlDoc, ref RSSManagerViewModel rssManagerView)
        {
//            Collection<RSS> temp = new Collection<RSS>(); 
            rssManagerView.RssItem.Clear();
            XmlNodeList nodes = xmlDoc.SelectNodes("rss/channel/item");

            foreach (XmlNode node in nodes)
            {
                var item = new RSS
                {
                    Title = ParseDocElements(node, "title"),
                    Description = ParseDocElements(node, "description"),
                    Link = ParseDocElements(node, "link")
                };

                string date = null;
                date = ParseDocElements(node, "pubDate");
                DateTime temp;
                DateTime.TryParse(date, out temp);
                item.Date = temp;

                rssManagerView.RssItem.Add(item);

            }
        }
        public static void SaveRssInfo(List<Manager> manager)
        {
            using (StreamWriter file = File.CreateText(@"Data\tempManager.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, manager);
            }
        }
    }
}
