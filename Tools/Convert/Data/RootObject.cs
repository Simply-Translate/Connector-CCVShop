using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Convert.Data
{

    public class RootObject
    {
        public string title { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public Dictionary<string, Item> properties { get; set; }
        public bool additionalProperties { get; set; }
    }
}
