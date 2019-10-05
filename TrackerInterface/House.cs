using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TrackerInterface
{
    /// <summary>
    /// Class that handles player's houses
    /// </summary>
    [DataContract]
    public class House
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int VirtualCount { get; set; }
        [DataMember]
        public int Storage { get; set; }
        [DataMember]
        public string[] Location { get; set; }
        [DataMember]
        public List<Item> Virtual { get; set; }
        [DataMember]
        public List<Crate> Crates { get; set; }
        [DataMember]
        public DateTime LastAccessed { get; set; }
        [DataMember]
        public int Server { get; set; }
        public override string ToString()
        {
            return $"{Id} {VirtualCount}/{Storage}";
        }
    }
}
