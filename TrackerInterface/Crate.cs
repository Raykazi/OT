using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrackerInterface
{
    /// <summary>
    /// Class that handles player's housing crates
    /// </summary>
    [DataContract]
    public class Crate
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public List<Item> Items { get; set; }
        [DataMember]
        public DateTime LastAccessed { get; set; }
    }
}
