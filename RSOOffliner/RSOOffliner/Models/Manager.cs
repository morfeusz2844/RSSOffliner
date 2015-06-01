using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml;

namespace RSOOffliner.Models
{
    [DataContract]
    public class Manager
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public ObservableCollection<RSS> RssItem { get; set; }
        [DataMember]
        public string FeedTitle { get; set; }
        [DataMember]
        public string FeedDescription { get; set; }
    }
}
