using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace TrackerInterface
{
    //Class for the virtual items
    [DataContract]
    public class Item
    {
        //Name of the item on the player
        [DataMember]
        public string Name { get; set; }
        //Amount of item on the player
        [DataMember]
        public int Amount { get; set; }
    }
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
        public Crate[] Crates { get; set; }
        [DataMember]
        public DateTime LastAccessed { get; set; }
        public override string ToString()
        {
            return $"{Id} {VirtualCount}/{Storage}";
        }
    }
    public class Crate
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public List<Item> Items { get; set; }
        [DataMember]
        public DateTime LastAccessed { get; set; }
    }
    //Class for vehicles owned by the player.
    [DataContract]
    public class Vehicle
    {
        //Olympus vehicle ID
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
        public Vehicle(int id, string name, int alive, int active, int insuranceLevel, int turboLevel, int secLevel, int storageLevel)
        {
            Id = id;
            Name = name;
            Alive = alive;
            Active = active;
            InsuranceLevel = insuranceLevel;
            TurboLevel = turboLevel;
            SecLevel = secLevel;
            StorageLevel = storageLevel;
        }
    }
    //Class for the player
    [DataContract]
    public class Player
    {
        [DataMember]
        //Olympus User ID
        public int Uid { get; private set; }
        [DataMember]
        //Player Steam64 ID
        public long SteamId { get; private set; }
        [DataMember]
        //Player Name
        public string Name { get; private set; }
        [DataMember]
        //Cash on hand
        public int Cash { get; private set; }
        [DataMember]
        //bank amount
        public int Bank { get; private set; }
        [DataMember]
        //APD Cop Rank
        public int CopLevel { get; private set; }
        [DataMember]
        //R&R Rank
        public int MedicLevel { get; private set; }
        [DataMember]
        //Sneaky Admin rank
        public int AdminLevel { get; private set; }
        [DataMember]
        //Donator dollar amount
        public int DonatorLevel { get; private set; }
        [DataMember]
        //Aliases the player has gone by, tied to either their steamID or GUID
        public List<string> Aliases { get; private set; }
        [DataMember]
        //Player Kills
        public int Kills { get; private set; }
        [DataMember]
        //Player Deaths
        public int Deaths { get; private set; }
        [DataMember]
        //R&R Revives and maybe Epi pens
        public int MedicRevives { get; private set; }
        [DataMember]
        //Amount they have received APD Ticketing and maybe vigi-ing
        public int BountyCollected { get; private set; }
        [DataMember]
        //APD Arrest and maybe Vigi Arrest
        public int CopArrest { get; private set; }
        [DataMember]
        //Civilian time
        public int TimeCiv { get; private set; }
        [DataMember]
        //APD Time
        public int TimeApd { get; private set; }
        [DataMember]
        //R&R Time
        public int TimeMed { get; private set; }
        [DataMember]
        //How much the player is worth
        public int BountyWanted { get; private set; }
        [DataMember]
        //Gang that the player is in, -1 if not in one
        public string GangName { get; private set; }
        [DataMember]
        //Rank of the player in the gang 1-5, -1 if not in one
        public int GangRank { get; private set; }
        [DataMember]
        //Last time of login
        public DateTime LastActive { get; private set; }
        [DataMember]
        //Last time they were saved
        public DateTime LastUpdated { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public List<Vehicle> CivVehicles { get; private set; }
        [DataMember]
        public List<House> Houses { get; set; } //Thank you FeDot
        [DataMember]
        //String list with their physical equipment
        public List<string> Equipment { get; private set; }
        [DataMember]
        public List<Item> Virtuals { get; private set; } //Custom item class for the palyers virtual items
        [DataMember]
        public int TargetLevel = -1; //Because these ***holes wanted colors
        [DataMember]
        public string[] Location; //Thank you FeDot
        //Constructor
        public Player(int uid, long steamId, string name, string aliases, string gangN, int gangR, long lastActive, long lastUpdated, string location)
        {
            Houses = new List<House>();
            Aliases = new List<string>();
            Equipment = new List<string>();
            CivVehicles = new List<Vehicle>();
            Uid = uid;
            SteamId = steamId;
            Name = name;
            var alias = aliases.Split(';');
            foreach (var aliasName in alias.Where(aliasName => aliasName.Length > 0))
                Aliases.Add(aliasName);
            GangName = gangN;
            GangRank = gangR;
            LastActive = Helper.FromUnixTime(lastActive);
            LastUpdated = Helper.FromUnixTime(lastUpdated);
            location = Encoding.ASCII.GetString(Convert.FromBase64String(location));
            location = location.Remove(0, 2);
            location = location.Remove(location.IndexOf("]"));
            Location = location.Split(',');
        }

        public void Save()
        {

        }
        public void AddStats(int cop, int medic, int admin, int donator, int kills, int deaths, int revives, int arrests)
        {
            CopLevel = cop;
            MedicLevel = medic;
            AdminLevel = admin;
            DonatorLevel = donator;
            Kills = kills;
            Deaths = deaths;
            MedicRevives = revives;
            CopArrest = arrests;
        }
        public void AddTime(int timeCiv, int timeApd, int timeMed)
        {
            TimeCiv = timeCiv;
            TimeApd = timeApd;
            TimeMed = timeMed;
        }
        public void AddMoney(int cash, int bank, int bountyCollected, int bountyWanted)
        {
            Cash = cash;
            Bank = bank;
            BountyCollected = bountyCollected;
            BountyWanted = bountyWanted;
        }
        public void AddGear(string gearCiv)
        {
            if (gearCiv.Length <= 2) return;
            gearCiv = gearCiv.Insert(0, "{\"civ_gear\": ");
            gearCiv += "}";
            var equipment = JArray.Parse(JObject.Parse(gearCiv)["civ_gear"].ToString());
            for (var i = 0; i < equipment.Count(); i++)
            {
                if (i < 5 || i > 5 && i < 9)
                {
                    Equipment.Add(equipment[i].ToString());
                }//Ammo
                else
                    switch (i)
                    {
                        case 12:

                            break;
                        case 15:
                            if (equipment[15].ToString().Length < 4) continue;
                            var virtualRawStr = equipment[15].ToString();
                            virtualRawStr = virtualRawStr.Insert(0, "{\"Virtuals\": ");
                            virtualRawStr += "}";
                            var virtuals = JArray.Parse(JObject.Parse(virtualRawStr)["Virtuals"].First.ToString());
                            Virtuals = new List<Item>();
                            foreach (var vi in virtuals.Cast<JArray>())
                            {
                                Virtuals.Add(new Item { Name = vi[0].ToString(), Amount = (int)vi[1] });
                            }
                            break;
                    }
            }
        }
        public void AddVehicles(string vCivAir, string vCivCar, string vCivShip)
        {
            if (vCivAir.Length > 2)
            {
                vCivAir = vCivAir.Insert(0, "{\"vehicle_civ_air\": ");
                vCivAir += "}";
                var vehiclesAir = JArray.Parse(JObject.Parse(vCivAir)["vehicle_civ_air"].ToString());
                CivVehicles = new List<Vehicle>();
                foreach (var jToken in vehiclesAir)
                {
                    var vehicle = (JObject)jToken;
                    var id = (int)vehicle["id"];
                    var vName = (string)vehicle["vehicle"];
                    var alive = (int)vehicle["alive"];
                    var active = (int)vehicle["active"];
                    var insured = (int)vehicle["modifications"]["insured"];
                    var turbo = (int)vehicle["modifications"]["turbo"];
                    var security = (int)vehicle["modifications"]["security"];
                    var storage = (int)vehicle["modifications"]["storage"];
                    CivVehicles.Add(new Vehicle(id, vName, alive, active, insured, turbo, security, storage));
                }
            }
            if (vCivCar.Length > 2)
            {
                vCivCar = vCivCar.Insert(0, "{\"vehicle_civ_car\": ");
                vCivCar += "}";
                var vehiclesCar = JArray.Parse(JObject.Parse(vCivCar)["vehicle_civ_car"].ToString());
                foreach (var jToken in vehiclesCar)
                {
                    var vehicle = (JObject)jToken;
                    var id = (int)vehicle["id"];
                    var vName = (string)vehicle["vehicle"];
                    var alive = (int)vehicle["alive"];
                    var active = (int)vehicle["active"];
                    var insured = (int)vehicle["modifications"]["insured"];
                    var turbo = (int)vehicle["modifications"]["turbo"];
                    var security = (int)vehicle["modifications"]["security"];
                    var storage = (int)vehicle["modifications"]["storage"];
                    CivVehicles.Add(new Vehicle(id, vName, alive, active, insured, turbo, security, storage));
                }
            }
            if (vCivShip.Length > 2)
            {
                vCivShip = vCivShip.Insert(0, "{\"vehicle_civ_ship\": ");
                vCivShip += "}";
                var vehiclesShip = JArray.Parse(JObject.Parse(vCivShip)["vehicle_civ_ship"].ToString());
                foreach (var jToken in vehiclesShip)
                {
                    var vehicle = (JObject)jToken;
                    var id = (int)vehicle["id"];
                    var vName = (string)vehicle["vehicle"];
                    var alive = (int)vehicle["alive"];
                    var active = (int)vehicle["active"];
                    var insured = (int)vehicle["modifications"]["insured"];
                    var turbo = (int)vehicle["modifications"]["turbo"];
                    var security = (int)vehicle["modifications"]["security"];
                    var storage = (int)vehicle["modifications"]["storage"];
                    CivVehicles.Add(new Vehicle(id, vName, alive, active, insured, turbo, security, storage));
                }
            }

        }

        public void AddHouse(int hid, string location, long lastUsed, string crates, string virtualStr, int maxStorage)
        {
            try
            {
                JArray virtuals = JArray.Parse(Helper.ToJson(virtualStr));
                int vCount = (int)virtuals[1];
                //Houses.Add(new House { VirtualCount = vCount });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public void AddHouse(List<string>[] hr, int houseCount)
        //{
        //    //string.Format("{0} ({1}/{2})", ID, VirtualCount, Storage);
        //    try
        //    {
        //        if (houseCount <= 0) return;
        //        for (int j = 0; j < houseCount; j++)
        //        {
        //            string[] location = hr[3][j].Split(',');
        //            JArray virtuals = JArray.Parse(Helper.ToJson(hr[5][j]));
        //            int virtualCount = virtuals[0].Count();
        //            int storage = hr[7][j].Length > 0 ? Convert.ToInt32(hr[7][j]) : 0;
        //            Houses[j] = new House { Id = Convert.ToInt32(hr[1][j]), LastAccessed = Helper.FromUnixTime(Convert.ToInt64(hr[4][j])), Location = location, Storage = storage, VirtualCount = Convert.ToInt32(virtuals[1]) };
        //            Houses[j].LbName = $"{Houses[j].Id} ({Houses[j].VirtualCount}/{Houses[j].Storage})";

        //            string crate = Helper.ToJson(hr[6][j]);
        //            IEnumerable<int> remove1 = crate.AllIndexesOf("\"\\\"");
        //            IEnumerable<int> remove2 = crate.AllIndexesOf("\\\"\"");
        //            for (int i = 0; i < remove1.Count(); i++)
        //                crate = crate.Remove(crate.IndexOf("\"\\\""), 3);
        //            for (int i = 0; i < remove1.Count(); i++)
        //                crate = crate.Remove(crate.IndexOf("\\\"\""), 3);

        //            JArray crates = JArray.Parse(crate);
        //            int crateCount;
        //            crateCount = crates.Count == 0 ? 0 : crates[0].Count();
        //            if (virtualCount > 0)
        //            {
        //                Houses[j].Virtual = new Item[virtualCount];
        //                int vCounter = 0;
        //                foreach (JArray item in virtuals[0])
        //                {
        //                    Houses[j].Virtual[vCounter] = new Item { Name = (string)item[0], Amount = (int)item[1] };
        //                    vCounter++;
        //                }
        //            }
        //            //if (crateCount > 0)
        //            //{
        //            //    houses[j].Crates = new Crate[crateCount];
        //            //    int cCounter = 0;
        //            //    foreach (JObject c in crates)
        //            //    {
        //            //        houses[j].Crates[cCounter] = new Crate();
        //            //        houses[j].Crates[cCounter].ID = (int)c["id"];
        //            //        houses[j].Crates[cCounter].LastAccessed = DateTime.Parse((string)c["last_active"]);
        //            //        houses[j].Crates[cCounter].Items = new List<Item>();
        //            //        string json = c["inventory"].ToString();
        //            //        JArray inventory = JArray.Parse(json);
        //            //        int count = inventory[1].Count();
        //            //        for (int i = 0; i < count; i++)
        //            //        {
        //            //            int iCount = inventory[1][i].Count();
        //            //            for (int k = 0; k < iCount; k++)
        //            //            {
        //            //                int kCount = inventory[1][i][k].Count();
        //            //                if (kCount > 0)
        //            //                {
        //            //                    string iName = "";
        //            //                    string amount = "";
        //            //                    if (i != 1 && k == 0)
        //            //                    {
        //            //                        iName = inventory[1][i][0][0].ToString();
        //            //                        amount = inventory[1][i][1][0].ToString();
        //            //                    }
        //            //                    else if (i == 1 && k == 0)
        //            //                    {
        //            //                        string debug = inventory[1][i].ToString();
        //            //                        iName = inventory[1][i][0][0].ToString();
        //            //                        amount = inventory[1][i][0][2].ToString();
        //            //                    }
        //            //                    if (iName.Length > 0)
        //            //                    {
        //            //                        Console.WriteLine(string.Format("Name: {0} Amount: {1}", iName, amount));
        //            //                        houses[j].Crates[cCounter].Items.Add(new Item { name = iName, amount = Convert.ToInt32(amount) });
        //            //                    }
        //            //                }
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //        }
        //    }
        //    catch (ArgumentOutOfRangeException e)
        //    {
        //        Helper.ConsoleLog(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Helper.ConsoleLog(e.Message);
        //    }

        //}
    }
}
