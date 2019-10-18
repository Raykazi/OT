using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Data;

namespace TrackerInterface
{
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
        public List<Vehicle> Vehicles { get; private set; } //Custom vehicle class array  with the players vehicle info
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
            Vehicles = new List<Vehicle>();

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
        public void AddGear(string gear)
        {
            if (gear.Length <= 2) return;
            gear = gear.Insert(0, "{\"gear\": ");
            gear += "}";
            var equipment = JArray.Parse(JObject.Parse(gear)["gear"].ToString());
            for (var i = 0; i < equipment.Count(); i++)
            {
                if (i < 5 || i > 5 && i < 9)
                {
                    Equipment.Add(TranslateEquipment(equipment[i].ToString()));
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
        public void AddVehicles(DataTable data)
        {
            List<Vehicle> tmpVehicles = new List<Vehicle>();
            foreach (DataRow row in data.Rows)
            {
                JArray mods = JArray.Parse(Helper.ToJson(row["modifications"].ToString()));
                int id = (int)row["id"];
                int insured = Convert.ToInt32(row["insured"]);
                int active = Convert.ToInt32(row["active"]);
                int alive = Convert.ToInt32(row["alive"]);
                int turbo = Convert.ToInt32(mods[0]);
                int trunk = Convert.ToInt32(mods[1]);
                int security = Convert.ToInt32(mods[2]);
                string name = (string)row["classname"];
                List<Item> items = new List<Item>();
                JArray inv_array = JArray.Parse(Helper.ToJson(row["inventory"].ToString()));
                if (inv_array.Count > 0)
                {
                    foreach (JToken item in inv_array[0].Cast<JArray>())
                    {
                        items.Add(new Item { Name = item[0].ToString(), Amount = Convert.ToInt32(item[1]) });
                    }
                }
                tmpVehicles.Add(new Vehicle(id, Vehicle.TranslateName(name), active, insured, turbo, security, trunk, items));
            }
            Vehicles = tmpVehicles;
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
        /// <summary>
        /// Translate arma names 
        /// </summary>
        /// <param name="a3Name"></param>
        /// <returns></returns>
        public string TranslateEquipment(string a3Name)
        {
            var translatedName = "";
            switch (a3Name)
            {
                default:
                    translatedName = a3Name;
                    break;
                case "hgun_P07_khk_F":
                case "hgun_P07_khk_Snds_F":
                    translatedName = "P07 9 mm";
                    break;
                case "hgun_Pistol_01_F":
                    translatedName = "PM 9 mm";
                    break;
                case "SMG_05_F":
                    translatedName = "Protector 9 mm";
                    break;
                case "hgun_Rook40_F":
                    translatedName = "Rook-40 9 mm";
                    break;
                case "hgun_PDW2000_F":
                    translatedName = "PDW2000 9 mm";
                    break;
                case "sgun_HunterShotgun_01_sawedoff_F":
                    translatedName = "Kozlice 12G (Sawed-Off)";
                    break;
                case "sgun_HunterShotgun_01_F":
                    translatedName = "Kozlice 12G";
                    break;
                case "arifle_SDAR_F":
                    translatedName = "SDAR 5.56 mm";
                    break;
                case "SMG_03_TR_camo":
                    translatedName = "ADR-97 TR 5.7 mm";
                    break;
                case "SMG_01_F":
                    translatedName = "Vermin SMG .45 ACP";
                    break;
                case "arifle_Mk20C_F":
                    translatedName = "Mk20C 5.56 mm";
                    break;
                case "arifle_TRG21_F":
                    translatedName = "TRG-21 5.56 mm";
                    break;
                case "arifle_Katiba_F":
                    translatedName = "Katiba 6.5 mm";
                    break;
                case "arifle_MX_khk_F":
                case "arifle_MX_F":
                    translatedName = "MX 6.5 mm";
                    break;
                case "arifle_MSBS65_sand_F":
                case "arifle_MSBS65_camo_F":
                    translatedName = "Promet 6.5 mm";
                    break;
                case "arifle_MXM_F":
                case "arifle_MXM_khk_F":
                    translatedName = "MXM 6.5 mm";
                    break;
                case "arifle_MSBS65_Mark_F":
                case "arifle_MSBS65_Mark_camo_F":
                case "arifle_MSBS65_Mark_sand_F":
                    translatedName = "Promet MR 6.5 mm";
                    break;
                case "arifle_MX_SW_F":
                case "arifle_MX_SW_khk_F":
                    translatedName = "MX SW 6.5 mm";
                    break;
                case "arifle_AK12_arid_F":
                case "arifle_AK12_lush_F":
                    translatedName = "AK-12 7.62 mm";
                    break;
                case "arifle_AK12U_arid_F":
                case "arifle_AK12U_lush_F":
                    translatedName = "AKU-12 7.62 mm";
                    break;
                case "arifle_RPK12_arid_F":
                case "arifle_RPK12_lush_F":
                    translatedName = "RPK-12 7.62 mm";
                    break;
                case "srifle_DMR_07_ghex_F":
                case "srifle_DMR_07_hex_F":
                    translatedName = "CMR-76 6.5 mm";
                    break;
                case "srifle_DMR_03_khaki_F":
                case "srifle_DMR_03_tan_F":
                case "srifle_DMR_03_woodland_F":
                case "srifle_DMR_03_multicam_F":
                case "srifle_DMR_01_F":
                    translatedName = "Mk-I EMR 7.62 mm";
                    break;
                case "srifle_EBR_F":
                    translatedName = "Mk18 ABR 7.62 mm";
                    break;
                case "srifle_DMR_06_camo_F":
                case "srifle_DMR_06_olive_F":
                case "srifle_DMR_06_hunter_F":
                    translatedName = "Mk14 7.62 mm";
                    break;
                case "LMG_Mk200_F":
                case "LMG_Mk200_black_F":
                    translatedName = "Mk200 6.5 mm";
                    break;
                case "LMG_Zafir_F":
                    translatedName = "Zafir 7.62 mm";
                    break;
                case "launch_B_Titan_olive_F":
                case "launch_I_Titan_F":
                case "launch_Titan_F":
                    translatedName = "Titan MPRL";
                    break;
                case "arifle_AKS_F":
                    translatedName = "AKS-74U 5.45 mm";
                    break;
                case "LMG_03_F":
                    translatedName = "LIM-85 5.56 mm";
                    break;
                case "arifle_SPAR_01_blk_F":
                case "arifle_SPAR_01_khk_F":
                case "arifle_SPAR_01_snd_F":
                    translatedName = "SPAR-16 5.56 mm";
                    break;
                case "arifle_SPAR_02_blk_F":
                case "arifle_SPAR_02_khk_F":
                case "arifle_SPAR_02_snd_F":
                    translatedName = "SPAR-16S 5.56 mm";
                    break;
                case "arifle_CTAR_blk_F":
                    translatedName = "CAR-95 5.8 mm";
                    break;
                case "arifle_CTARS_blk_F":
                    translatedName = "CAR-95-1 5.8 mm";
                    break;
                case "arifle_ARX_blk_F":
                case "arifle_ARX_ghex_F":
                case "arifle_ARX_hex_F":
                    translatedName = "Type 115 6.5 mm";
                    break;
                case "arifle_AK12_F":
                    translatedName = "AK-12 7.62 mm";
                    break;
                case "arifle_AKM_F":
                case "arifle_AKM_FL_F":
                    translatedName = "AKM 7.62 mm";
                    break;
                case "arifle_SPAR_03_blk_F":
                case "arifle_SPAR_03_khk_F":
                case "arifle_SPAR_03_snd_F":
                    translatedName = "SPAR-17 7.62 mm";
                    break;
            }
            return translatedName;

        }
    }
}
