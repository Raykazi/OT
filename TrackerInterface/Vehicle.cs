using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace TrackerInterface
{
    /// <summary>
    /// Class for vehicles owned by the player.
    /// </summary>
    [DataContract]
    public class Vehicle
    {
        //Vehicle ID
        [DataMember]
        public int Id { get; private set; }
        //Vehicle name
        [DataMember]
        public string Name { get; private set; }
        //If the vehicle is alive or not??
        [DataMember]
        public int Alive { get; private set; }
        [DataMember]
        //If the vehicle is out on the map right now
        public int Active { get; private set; }
        [DataMember]
        //1 Basic Insurance 2 Full Insurance
        public int InsuranceLevel { get; private set; }
        [DataMember]
        //Tier 1-4, speed and manueverability level
        public int TurboLevel { get; private set; }
        [DataMember]
        //1 or 2 Security level for the car
        public int SecLevel { get; private set; }
        [DataMember]
        //1-4 Space of the vehicle
        public int StorageLevel { get; private set; }
        [DataMember]
        public List<Item> Inventory { get; private set; } //Custom item class for the palyers virtual items
        //Vehicle Constructor
        public Vehicle(int id, string name, int active, int insuranceLevel, int turboLevel, int secLevel, int storageLevel, List<Item> inventory)
        {
            Id = id;
            Name = name;
            Active = active;
            InsuranceLevel = insuranceLevel;
            TurboLevel = turboLevel;
            SecLevel = secLevel;
            StorageLevel = storageLevel;
            Inventory = inventory;

        }
    }
}
