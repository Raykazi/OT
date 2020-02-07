using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerServer
{
    public class Enemy
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }
    }

    public class War
    {
        public DateTime date { get; set; }
        public bool is_active { get; set; }
        public Enemy enemy { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
    }

    public class Results
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<War> results { get; set; }
    }
}
