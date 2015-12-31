using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Convert.Data
{
    public class Item
    {      
        public string description { get; set; }
        public string type { get; set; }
        public string format { get; set; }
        public bool? required { get; set; }
        public int? minimum { get; set; }
        public int? maximum { get; set; }
        public int? maxlength { get; set; }
        [JsonProperty("enum")]
        public List<string> Enum { get; set; }
        public Dictionary<string, Item> properties { get; set; }
    }
}
