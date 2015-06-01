using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Xml;
using Newtonsoft.Json;
using RSOOffliner.Models;
using RSOOffliner.ViewModels;
using Formatting = Newtonsoft.Json.Formatting;

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

            string json = JsonConvert.SerializeObject(manager, Formatting.Indented);


            using (StreamWriter file = File.CreateText(@"Data\tempManager.json"))
            {
                file.Write(json);
            }
        }
        public static List<Manager> GetRssInfo()
        {
            List<Manager> result;
            using (StreamReader reader = File.OpenText(@"Data\tempManager.json"))
            {
                result = JsonConvert.DeserializeObject<List<Manager>>(reader.ReadToEnd());
            }
            return result;
        }
    }
}
