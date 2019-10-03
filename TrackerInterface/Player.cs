using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using TrackerServer;

namespace TrackerInterface
{
    /// <summary>
    /// Class that handles  player's virtual Items
    /// </summary>
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
        //Vehicle Constructor
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
    /// <summary>
    /// Class that handles player information
    /// </summary>
    [DataContract]
    public class Player
    {
        [DataMember]
        //Olympus User ID
        public int Uid { get; private set; }
        [DataMember]
        //Player Steam64 ID
        public string SteamId { get; private set; }
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

        public DateTime LastActive { get; private set; }//Last time of login
        [DataMember]

        public DateTime LastUpdated { get; private set; }//Last time they were saved
        [DataMember]
        public List<Vehicle> CivVehicles { get; private set; } //Custom vehicle class array  with the players vehicle info
        [DataMember]
        public List<Vehicle> ApdVehicles { get; private set; } //Custom vehicle class array  with the players vehicle info
        [DataMember]
        public List<Vehicle> MedVehicles { get; private set; } //Custom vehicle class array  with the players vehicle info
        [DataMember]
        public List<House> Houses { get; set; } //Thank you FeDot
        [DataMember]
        public List<string> Equipment { get; private set; }//String list with their physical equipment
        [DataMember]
        public List<Item> Virtuals { get; private set; } //Custom item class for the palyers virtual items
        [DataMember]
        public int TargetLevel = -1; //Because these ***holes wanted colors
        [DataMember]
        public string[] Location; //Thank you FeDot
        [DataMember]
        public string Faction { get; private set; } //Cop,Civ, Medic
        [DataMember]
        public int Server { get; set; } //Server 1 or 2

        private readonly Db _db = new Db();
        /// <summary>
        /// Constructor
        /// </summary>
        public Player(int uid, string steamId, string name, string aliases, string gangN, int gangR, long lastActive, long lastUpdated, string location, string faction)
        {
            // Initialize Lists
            Houses = new List<House>();
            Aliases = new List<string>();
            Equipment = new List<string>();
            CivVehicles = new List<Vehicle>();

            //Setup player object with parameters from the calls
            Uid = uid;
            SteamId = steamId;
            Name = name;
            //Seperate Gang history 
            var alias = aliases.Split(';');
            foreach (var aliasName in alias.Where(aliasName => aliasName.Length > 0))
                Aliases.Add(aliasName);
            GangName = gangN;
            GangRank = gangR;
            //Parse times into human readable format
            LastActive = Helper.FromUnixTime(lastActive);
            LastUpdated = Helper.FromUnixTime(lastUpdated);
            //Parse Location into X,Y format
            Location = Helper.ParseLocation(location);
            Faction = faction;
        }
        /// <summary>
        /// Checks DB for an existing record, if one is found update if not create a record
        /// </summary>
        public void Save(string aliases, string cAir, string cCar, string cShip, string aAir, string aCar, string aShip, string mAir, string mCar, string mShip, string aGear, string cGear, string mGear, int server)
        {
            var location = Helper.Base64Encode(Helper.ToSQL(Location));
            var data = _db.ExecuteReader("SELECT steamID FROM Player WHERE steamID =?", SteamId);
            var sql = "";
            if (data[0].Count == 0)
            {
                sql = "INSERT INTO player (UID, steamID, playerName, cash, bank, copLevel, medicLevel, adminLevel, donatorLevel, kills, deaths, medicRevives, bountyCollected, copArrests, timeCiv, timeApd, timeMed, bountyWanted, aliases, gangName, lastActive, vehApdAir, vehApdCar, vehApdShip, vehCivAir, vehCivCar, vehCivShip, vehMedAir, vehMedCar, vehMedShip, gearApd, gearCiv, gearMed, gangRank, timestamp, location, server) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                _db.ExecuteNonQuery(sql, Uid, SteamId, Name, Cash, Bank, CopLevel, MedicLevel, AdminLevel, DonatorLevel, Kills, Deaths, MedicRevives, BountyCollected, CopArrest, TimeCiv, TimeApd, TimeMed, BountyWanted, Helper.ToSQL(Aliases), GangName, LastActive.ToUnixTime(), aAir, aCar, aShip, cAir, cCar, cShip, mAir, mCar, mShip, aGear, cGear, mGear, GangRank, LastUpdated.ToUnixTime(), location, Server);
            }
            else
            {
                sql = "UPDATE player SET `playerName` = ?, `cash` = ?, `bank` = ?, `copLevel` = ?, `medicLevel` = ?, `adminLevel` = ?, `donatorLevel` = ?, `aliases` = ?, `kills` = ?, `deaths` = ?, `medicRevives` = ?, `bountyCollected` = ?, `copArrests` = ?, `timeCiv` = ?, `timeApd` = ?, `timeMed` = ?, `bountyWanted` = ?, `gangName` = ?, `gangRank` = ?, `lastActive` = ?, `gearApd` = ?, `gearCiv` = ?, `gearMed` = ?, `vehApdAir` = ?, `vehApdCar` = ?, `vehApdShip` = ?, `vehCivAir` = ?, `vehCivCar` = ?, `vehCivShip` = ?, `vehMedAir` = ?, `vehMedCar` = ?, `vehMedShip` = ? , `timestamp` = ? , `location` = ?, `server` = ? WHERE `steamID` = ?";
                _db.ExecuteNonQuery(sql, Name, Cash, Bank, CopLevel, MedicLevel, AdminLevel, DonatorLevel, Helper.ToSQL(Aliases), Kills, Deaths, MedicRevives, BountyCollected, CopArrest, TimeCiv, TimeApd, TimeMed, BountyWanted, GangName, GangRank, LastActive.ToUnixTime(), aGear, cGear, mGear, aAir, aCar, aShip, cAir, cCar, cShip, mAir, mCar, mShip, LastUpdated.ToUnixTime(), location, Server, SteamId);
            }
        }
        /// <summary>
        /// Setup the player object in regards to player stats
        /// </summary>
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
        /// <summary>
        /// Setup the player object in regards to player times
        /// </summary>        
        public void AddTime(int timeCiv, int timeApd, int timeMed)
        {
            TimeCiv = timeCiv;
            TimeApd = timeApd;
            TimeMed = timeMed;
        }
        /// <summary>
        /// Setup the player object in regards to player money
        /// </summary>        
        public void AddMoney(int cash, int bank, int bountyCollected, int bountyWanted)
        {
            Cash = cash;
            Bank = bank;
            BountyCollected = bountyCollected;
            BountyWanted = bountyWanted;
        }
        /// <summary>
        /// Parse the JSON string regarding gear and inventory
        /// </summary>
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
        /// <summary>
        /// Parse the JSON string regarding player's vehicles
        /// </summary>
        /// <param name="vehicles"></param>
        /// <param name="faction"></param>
        public void AddVehicles(string vehicles, string faction)
        {
            //Only care about civ vehicles for now.
            switch (faction)
            {
                case "civ":
                    vehicles = vehicles.Insert(0, "{\"vehicle\": ");
                    vehicles += "}";
                    var v = JArray.Parse(JObject.Parse(vehicles)["vehicle"].ToString());
                    foreach (var jToken in v)
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
                    break;
            }
        }
        /// <summary>
        /// Initialize vehicle lists depending on faction,
        /// Calls function to add player's vehicles to their respective objects
        /// </summary>
        public void AddVehicles(string air, string car, string ship, string faction)
        {
            switch (faction)
            {
                case "civ":
                    CivVehicles = new List<Vehicle>();
                    break;
                case "apd":
                    ApdVehicles = new List<Vehicle>();
                    break;
                case "med":
                    MedVehicles = new List<Vehicle>();
                    break;
            }
            if (air.Length > 2)
                AddVehicles(air, faction);
            if (car.Length > 2)
                AddVehicles(car, faction);
            if (ship.Length > 2)
                AddVehicles(ship, faction);
        }
        /// <summary>
        /// Parse JSON house string array and place information into House object
        /// </summary>

        public void AddHouses(JArray houses, int server)
        {
            foreach (var jToken in houses)
            {
                try
                {
                    var house = (JObject)jToken;
                    var pos = (string)house["pos"];
                    pos = pos.Remove(0, 1);
                    pos = pos.Remove(pos.IndexOf(']'));
                    var lastUsed = DateTime.Parse(house["last_active"].ToString());
                    var virtuals = JArray.Parse(Helper.ToJson(house["inventory"].ToString()));
                    var crate = Helper.ToJson(house["crates"].ToString());
                    var virtualItems = virtuals[0].Select(item => new Item { Name = (string)item[0], Amount = (int)item[1] }).ToList();
                    var remove1 = crate.AllIndexesOf("\"\\\"");
                    var remove2 = crate.AllIndexesOf("\\\"\"");
                    for (var i = 0; i < remove1.Count(); i++)
                        crate = crate.Remove(crate.IndexOf("\"\\\""), 3);
                    for (var i = 0; i < remove2.Count(); i++)
                        crate = crate.Remove(crate.IndexOf("\\\"\""), 3);

                    var crates = JArray.Parse(crate);
                    var crateList = new List<Crate>();
                    foreach (var c in crates)
                    {
                        var crateInventory = JArray.Parse(c["inventory"].ToString());
                        var items = new List<Item>();
                        var count = crateInventory[1].Count();
                        for (var i = 0; i < count; i++)
                        {
                            var iCount = crateInventory[1][i].Count();
                            for (var k = 0; k < iCount; k++)
                            {
                                var kCount = crateInventory[1][i][k].Count();
                                if (kCount <= 0) continue;
                                var iName = "";
                                var amount = "";
                                if (i != 1 && k == 0)
                                {
                                    iName = crateInventory[1][i][0][0].ToString();
                                    amount = crateInventory[1][i][1][0].ToString();
                                }
                                else if (i == 1 && k == 0)
                                {
                                    var debug = crateInventory[1][i].ToString();
                                    iName = crateInventory[1][i][0][0].ToString();
                                    amount = crateInventory[1][i][0][2].ToString();
                                }
                                if (iName.Length > 0)
                                {
                                    //Console.WriteLine(string.Format("Name: {0} Amount: {1}", iName, amount));
                                    items.Add(new Item { Name = iName, Amount = Convert.ToInt32(amount) });
                                }
                            }
                        }
                        crateList.Add(new Crate { Id = (int)c["id"], LastAccessed = Convert.ToDateTime(c["last_active"]), Items = items });
                    }
                    Houses.Add(new House { Id = (int)house["houseid"], Location = pos.Split(','), VirtualCount = (int)virtuals[1], LastAccessed = lastUsed, Storage = (int)house["storage"], Virtual = virtualItems, Server = server });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                //var data2 = _db.ExecuteReader("SELECT houseID FROM houses WHERE houseID =?", hid);
                //if (data2[0].Count == 0)
                //{
                //    //Add the player to the DB
                //    sql = "INSERT INTO houses (`houseID`, `steamID`, `location`, `lastAccessed`, `virtual`, `crates`, `storage`, `server`)  VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                //    resultH += _db.ExecuteNonQuery(sql, hid, steamId, pos, lastUsed, virtuals, crates, maxStorage, _serverName) == 1 ? 1 : 0;
                //}
                //else
                //{
                //    //Update the player's house
                //    sql = "UPDATE houses SET `steamID` = ?, `location` = ?, `lastAccessed` = ?, `crates` = ?, `virtual` = ?, `storage`= ?, `server`=? WHERE `houseID` = ?";
                //    resultH += _db.ExecuteNonQuery(sql, steamId, pos, lastUsed, crates, virtuals, maxStorage, _serverName, hid) == 1 ? 1 : 0;
                //}
            }
        }
    }
}
