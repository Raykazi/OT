using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string name { get; set; }
        //Amount of item on the player
        [DataMember]
        public int amount { get; set; }

    }
    [DataContract]
    public class House
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int VirtualCount { get; set; }
        [DataMember]
        public int Storage { get; set; }
        [DataMember]
        public string LBName { get; set; }

        [DataMember]
        public string[] Location { get; set; }
        [DataMember]
        public Item[] Virtual { get; set; }
        [DataMember]
        public Crate[] Crates { get; set; }
        [DataMember]
        public DateTime LastAccessed { get; set; }
    }
    public class Crate
    {
        [DataMember]
        public int ID { get; set; }
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
        public int ID { get; private set; }
        //Vehicle name
        [DataMember]
        public string name { get; private set; }
        //If the vehicle is alive or not??
        [DataMember]
        public int alive { get; private set; }
        [DataMember]
        //If the vehicle is out on the map right now
        public int active { get; private set; }
        [DataMember]
        //1 Basic Insurance 2 Full Insurance
        public int insuranceLevel { get; private set; }
        [DataMember]
        //Tier 1-4, speed and manueverability level
        public int turboLevel { get; private set; }
        [DataMember]
        //1 or 2 Security level for the car
        public int secLevel { get; private set; }
        [DataMember]
        //1-4 Space of the vehicle
        public int storageLevel { get; private set; }
        public Vehicle(int ID, string name, int alive, int active, int insuranceLevel, int turboLevel, int secLevel, int storageLevel)
        {
            this.ID = ID;
            this.name = name;
            this.alive = alive;
            this.active = active;
            this.insuranceLevel = insuranceLevel;
            this.turboLevel = turboLevel;
            this.secLevel = secLevel;
            this.storageLevel = storageLevel;
        }
    }
    //Class for the player
    [DataContract]
    public class Player
    {
        [DataMember]
        //Olympus User ID
        public int UID { get; private set; }
        [DataMember]
        //Player Steam64 ID
        public long steamID { get; private set; }
        [DataMember]
        //Player Name
        public string name { get; private set; }
        [DataMember]
        //Cash on hand
        public int cash { get; private set; }
        [DataMember]
        //bank amount
        public int bank { get; private set; }
        [DataMember]
        //APD Cop Rank
        public int copLevel { get; private set; }
        [DataMember]
        //R&R Rank
        public int medicLevel { get; private set; }
        [DataMember]
        //Sneaky Admin rank
        public int adminLevel { get; private set; }
        [DataMember]
        //Donator dollar amount
        public int donatorLevel { get; private set; }
        [DataMember]
        //Aliases the player has gone by, tied to either their steamID or GUID
        public List<string> aliases { get; private set; }
        [DataMember]
        //Player Kills
        public int kills { get; private set; }
        [DataMember]
        //Player Deaths
        public int deaths { get; private set; }
        [DataMember]
        //R&R Revives and maybe Epi pens
        public int medicRevives { get; private set; }
        [DataMember]
        //Amount they have received APD Ticketing and maybe vigi-ing
        public int bountyCollected { get; private set; }
        [DataMember]
        //APD Arrest and maybe Vigi Arrest
        public int copArrest { get; private set; }
        [DataMember]
        //Civilian time
        public int timeCiv { get; private set; }
        [DataMember]
        //APD Time
        public int timeApd { get; private set; }
        [DataMember]
        //R&R Time
        public int timeMed { get; private set; }
        [DataMember]
        //How much the player is worth
        public int bountyWanted { get; private set; }
        [DataMember]
        //Gang that the player is in, -1 if not in one
        public string gangName { get; private set; }
        [DataMember]
        //Rank of the player in the gang 1-5, -1 if not in one
        public int gangRank { get; private set; }
        [DataMember]
        //Last time of login
        public DateTime lastActive { get; private set; }
        [DataMember]
        //Last time they were saved
        public DateTime lastUpdated { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicle[] civAir { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicle[] civCar { get; private set; }
        [DataMember]
        //Custom vehicle class array  with the players vehicle info
        public Vehicle[] civShip { get; private set; }
        [DataMember]
        public House[] houses { get; set; } //Thank you FeDot
        [DataMember]
        //String list with their physical equipment
        public List<string> Equipment { get; private set; }
        [DataMember]
        public Item[] Virtuals { get; private set; } //Custom item class for the palyers virtual items
        [DataMember]
        public int TargetLevel = -1; //Because these ***holes wanted colors
        [DataMember]
        public string[] location; //Thank you FeDot
        //Constructor
        public Player(int UID, long steamID, string name, int cash, int bank, int cop, int medic, int admin, int donator, string aliases,
            int kills, int deaths, int revives, int arrests, int timeC, int timeA, int timeM, int bountyC, int bountyW, string gangN, int gangR, long lastActive,
            string vCivAir, string vCivCar, string vCivShip, string gearCiv, long lastUpdated, string location)
        {
            /*Declarations */
            JArray vehiclesAir;
            JArray vehiclesCar;
            JArray vehiclesShip;
            JArray equipment;
            /*Initializing variables*/
            this.aliases = new List<string>();
            this.Equipment = new List<string>();
            this.aliases.Clear();
            /*Assigining variables*/
            this.UID = UID;
            this.steamID = steamID;
            this.name = name;
            this.cash = cash;
            this.bank = bank;
            this.copLevel = cop;
            this.medicLevel = medic;
            this.adminLevel = admin;
            this.donatorLevel = donator;
            this.kills = kills;
            this.deaths = deaths;
            this.medicRevives = revives;
            this.copArrest = arrests;
            this.timeCiv = timeC;
            this.timeApd = timeA;
            this.timeMed = timeM;
            this.bountyCollected = bountyC;
            this.bountyWanted = bountyW;
            this.gangName = gangN;
            this.gangRank = gangR;
            //Convert Unix time to C# DateTime
            this.lastActive = Helper.FromUnixTime(lastActive);
            this.lastUpdated = Helper.FromUnixTime(lastUpdated);
            /* Splitting the data from the DB into segments*/
            string[] alias = aliases.Split(';');
            foreach (string aliasName in alias)
                if (aliasName.Length > 0)
                    this.aliases.Add(aliasName);
            //Parsing last seen location of player
            location = Encoding.ASCII.GetString(Convert.FromBase64String(location));
            location = location.Remove(0, 2);
            location = location.Remove(location.IndexOf("]"));
            this.location = location.Split(',');
            //Parsing JSON Strings from the DB, Biggest pain in my ass. Thank you FeDot
            if (gearCiv.Length > 2)
            {
                gearCiv = gearCiv.Insert(0, "{\"civ_gear\": ");
                gearCiv += "}";
                equipment = JArray.Parse(JObject.Parse(gearCiv)["civ_gear"].ToString());
                for (int i = 0; i < equipment.Count(); i++)
                {
                    if (i < 5 || i > 5 && i < 9)
                    {
                        Equipment.Add(equipment[i].ToString());
                    }//Ammo
                    else if (i == 12)
                    {

                    }
                    else if (i == 15)
                    {
                        if (equipment[15].ToString().Length < 4) continue;
                        string virtualString = equipment[15].ToString();
                        virtualString = virtualString.Insert(0, "{\"Virtuals\": ");
                        virtualString += "}";
                        JArray tempV = JArray.Parse(JObject.Parse(virtualString)["Virtuals"].First.ToString());
                        Virtuals = new Item[tempV.Count];
                        int counter = 0;
                        foreach (JArray vi in tempV)
                        {
                            Virtuals[counter] = new Item { name = vi[0].ToString(), amount = (int)vi[1] };
                            counter++;
                        }
                    }
                }
            }
            if (vCivAir.Length > 2)
            {
                vCivAir = vCivAir.Insert(0, "{\"vehicle_civ_air\": ");
                vCivAir += "}";
                vehiclesAir = JArray.Parse(JObject.Parse(vCivAir)["vehicle_civ_air"].ToString());
                civAir = new Vehicle[vehiclesAir.Count];
                int vaCounter = 0;
                foreach (JObject vehicle in vehiclesAir)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civAir[vaCounter] = new Vehicle(ID, vName, alive, active, insured, turbo, security, storage);
                    vaCounter++;
                }
            }
            if (vCivCar.Length > 2)
            {
                vCivCar = vCivCar.Insert(0, "{\"vehicle_civ_car\": ");
                vCivCar += "}";
                vehiclesCar = JArray.Parse(JObject.Parse(vCivCar)["vehicle_civ_car"].ToString());
                civCar = new Vehicle[vehiclesCar.Count];
                int vcCounter = 0;
                foreach (JObject vehicle in vehiclesCar)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civCar[vcCounter] = new Vehicle(ID, vName, alive, active, insured, turbo, security, storage);
                    vcCounter++;
                }
            }
            if (vCivShip.Length > 2)
            {
                vCivShip = vCivShip.Insert(0, "{\"vehicle_civ_ship\": ");
                vCivShip += "}";
                vehiclesShip = JArray.Parse(JObject.Parse(vCivShip)["vehicle_civ_ship"].ToString());
                civShip = new Vehicle[vehiclesShip.Count];
                int vsCounter = 0;
                foreach (JObject vehicle in vehiclesShip)
                {
                    int ID = (int)vehicle["id"];
                    string vName = (string)vehicle["vehicle"];
                    int alive = (int)vehicle["alive"];
                    int active = (int)vehicle["active"];
                    int insured = (int)vehicle["modifications"]["insured"];
                    int turbo = (int)vehicle["modifications"]["turbo"];
                    int security = (int)vehicle["modifications"]["security"];
                    int storage = (int)vehicle["modifications"]["storage"];
                    civShip[vsCounter] = new Vehicle(ID, vName, alive, active, insured, turbo, security, storage);
                    vsCounter++;
                }
            }


        }
        public void AddHouses(List<string>[] hr, int houseCount)
        {
            //string.Format("{0} ({1}/{2})", ID, VirtualCount, Storage);
            try
            {
                if (houseCount > 0)
                {
                    houses = new House[houseCount];
                    for (int j = 0; j < houseCount; j++)
                    {
                        string[] location = hr[3][j].Split(',');
                        JArray virtuals = JArray.Parse(Helper.ToJson(hr[5][j]));
                        int virtualCount = virtuals[0].Count();
                        int storage = hr[7][j].Length > 0 ? Convert.ToInt32(hr[7][j]) : 0;
                        houses[j] = new House { ID = Convert.ToInt32(hr[1][j]), LastAccessed = Helper.FromUnixTime(Convert.ToInt64(hr[4][j])), Location = location, Storage = storage, VirtualCount = Convert.ToInt32(virtuals[1]) };
                        houses[j].LBName = string.Format("{0} ({1}/{2})", houses[j].ID, houses[j].VirtualCount, houses[j].Storage);

                        string crate = Helper.ToJson(hr[6][j]);
                        IEnumerable<int> remove1 = crate.AllIndexesOf("\"\\\"");
                        IEnumerable<int> remove2 = crate.AllIndexesOf("\\\"\"");
                        for (int i = 0; i < remove1.Count(); i++)
                            crate = crate.Remove(crate.IndexOf("\"\\\""), 3);
                        for (int i = 0; i < remove1.Count(); i++)
                            crate = crate.Remove(crate.IndexOf("\\\"\""), 3);

                        JArray crates = JArray.Parse(crate);
                        int crateCount;
                        crateCount = crates.Count == 0 ? 0 : crates[0].Count();
                        if (virtualCount > 0)
                        {
                            houses[j].Virtual = new Item[virtualCount];
                            int vCounter = 0;
                            foreach (JArray item in virtuals[0])
                            {
                                houses[j].Virtual[vCounter] = new Item { name = (string)item[0], amount = (int)item[1] };
                                vCounter++;
                            }
                        }
                        //if (crateCount > 0)
                        //{
                        //    houses[j].Crates = new Crate[crateCount];
                        //    int cCounter = 0;
                        //    foreach (JObject c in crates)
                        //    {
                        //        houses[j].Crates[cCounter] = new Crate();
                        //        houses[j].Crates[cCounter].ID = (int)c["id"];
                        //        houses[j].Crates[cCounter].LastAccessed = DateTime.Parse((string)c["last_active"]);
                        //        houses[j].Crates[cCounter].Items = new List<Item>();
                        //        string json = c["inventory"].ToString();
                        //        JArray inventory = JArray.Parse(json);
                        //        int count = inventory[1].Count();
                        //        for (int i = 0; i < count; i++)
                        //        {
                        //            int iCount = inventory[1][i].Count();
                        //            for (int k = 0; k < iCount; k++)
                        //            {
                        //                int kCount = inventory[1][i][k].Count();
                        //                if (kCount > 0)
                        //                {
                        //                    string iName = "";
                        //                    string amount = "";
                        //                    if (i != 1 && k == 0)
                        //                    {
                        //                        iName = inventory[1][i][0][0].ToString();
                        //                        amount = inventory[1][i][1][0].ToString();
                        //                    }
                        //                    else if (i == 1 && k == 0)
                        //                    {
                        //                        string debug = inventory[1][i].ToString();
                        //                        iName = inventory[1][i][0][0].ToString();
                        //                        amount = inventory[1][i][0][2].ToString();
                        //                    }
                        //                    if (iName.Length > 0)
                        //                    {
                        //                        Console.WriteLine(string.Format("Name: {0} Amount: {1}", iName, amount));
                        //                        houses[j].Crates[cCounter].Items.Add(new Item { name = iName, amount = Convert.ToInt32(amount) });
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Helper.ConsoleLog(e.Message);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog(e.Message);
            }

        }
    }
}
