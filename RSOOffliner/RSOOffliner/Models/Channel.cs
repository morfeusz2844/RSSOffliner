using System.Runtime.Serialization;

namespace RSOOffliner.Models
{
    [DataContract]
    public class Channel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}
