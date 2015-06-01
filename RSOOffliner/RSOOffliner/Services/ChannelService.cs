using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using RSOOffliner.Models;

namespace RSOOffliner.Services
{
    public static class ChannelService
    {
        public static List<Channel> GetChannels()
        {
            List<Channel> result;
            using (StreamReader reader = File.OpenText(@"Data\channelsData.json"))
            {
                result = JsonConvert.DeserializeObject<List<Channel>>(reader.ReadToEnd());
            }
            return result;
        }

        public static void SaveChannels(List<Channel> channels)
        {
            string json = JsonConvert.SerializeObject(channels, Formatting.Indented);
            using (StreamWriter file = File.CreateText(@"Data\channelsData.json"))
            {
                file.Write(json);
            }
        }
    }
}
