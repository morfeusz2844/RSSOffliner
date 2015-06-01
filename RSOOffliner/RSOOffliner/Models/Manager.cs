using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace RSOOffliner.Models
{
    public class Manager
    {
        private string _url;
        private Collection<RSS> _rssItems = new Collection<RSS>();

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        private string _feedTitle ;
        private string _feedDescription;

        public Manager()
        {
            _url=String.Empty;
        }

        public Manager(string feedUrl)
        {
            _url = feedUrl;
        }
        public Collection<RSS> RSSItems
        {
            get { return _rssItems; }
        }

        public string FeedTitle
        {
            get { return _feedTitle; }
        }

        public string FeedDescription
        {
            get { return _feedDescription; }
        }
    }
}
