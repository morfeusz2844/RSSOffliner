using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSOOffliner.Models
{
    [Serializable]
    public class RSS
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        //public struct Items
        //{
        //    public DateTime Date;
        //    public string Title;
        //    public string Description;
        //    public string Link;
        //}
    }
}
