using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using Newtonsoft.Json;
using RSOOffliner.Models;
using RSOOffliner.ViewModels;

namespace RSOOffliner.Services
{
    public static class ChannelService
    {
        public static List<Channel> GetChannels()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(3));
            List<Channel> result;
            var serializer = new DataContractJsonSerializer(typeof(Channel[]));
            using (StreamReader reader = File.OpenText(@"Data\channelsData.json"))
            {
                var temp = serializer.ReadObject(reader.BaseStream);
                result = ((Channel[])temp).ToList();
            }
//            Thread.Sleep(TimeSpan.FromSeconds(3));
            return result;
        }

        public static void SaveChannels(List<Channel> channels)
        {
            using (StreamWriter file = File.CreateText(@"Data\channelsData.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file,channels);
            }
        }
    }
}
