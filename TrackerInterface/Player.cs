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
        public string BMId { get; private set; }
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
        public string CopRank { get; set; }
        [DataMember]
        //R&R Rank
        public int MedicLevel { get; private set; }
        public string MedicRank { get; set; }
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
        public List<Vehicle> Vehicles { get; set; } //Custom vehicle class array  with the players vehicle info
        [DataMember]
        public List<House> Houses { get; set; } //Thank you FeDot
        [DataMember]
        public List<string> Equipment { get; private set; }//String list with their physical equipment
        [DataMember]
        public List<string> CivLicenses { get; private set; }
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
        [DataMember]
        public bool WarTarget { get; set; } = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public Player(int uid, string steamId, string name, string aliases, string gangN, int gangR, long lastActive, long lastUpdated, string location, string faction, string bmId)
        {
            // Initialize Lists
            Houses = new List<House>();
            Aliases = new List<string>();
            Equipment = new List<string>();
            Vehicles = new List<Vehicle>();
            CivLicenses = new List<string>();

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
            BMId = bmId;
        }
        public static Player CreatePlayer(DataRow row, int serverNum)
        {
            int bounty = 0;
            int uid = (int)row["uid"];
            string steamID = (string)row["playerid"];
            string name = (string)row["name"];
            string gangName = (int)row["gangId"] == -1 ? "N/A" : (string)row["gangName"];
            int gangRank = Convert.ToInt32(row["gangRank"]);
            DateTime lastActive = Convert.ToDateTime(row["last_active"].ToString());
            int coplvl = Convert.ToInt32(row["coplevel"]);
            int medlvl = Convert.ToInt32(row["mediclevel"]);
            int admlvl = Convert.ToInt32(row["adminlevel"]);
            int donlvl = Convert.ToInt32(row["donatorlvl"]);

            JArray stats = JArray.Parse(Helper.ToJson(row["player_stats"].ToString()));
            int kills = (int)stats[0];
            int deaths = (int)stats[1];
            int revives = (int)stats[2];
            int arrests = (int)stats[6];

            JArray wanted = JArray.Parse(Helper.ToJson(row["wanted"].ToString()));
            if (wanted.Count > 0)
                bounty = (int)wanted[0];

            string aliases = row["aliases"].ToString();
            Player p = new Player(uid, steamID, name, aliases, gangName, gangRank, lastActive.ToUnixTime(), DateTime.UtcNow.ToUnixTime(), (string)row["coordinates"], (string)row["last_side"], row["bm_id"].ToString());
            p.AddStats(coplvl, medlvl, admlvl, donlvl, kills, deaths, revives, arrests, (int)row["cash"], (int)row["bankacc"], 0, bounty);
            string gear = "";
            string licenses = Helper.ToJson(row["civ_licenses"].ToString());
            switch (p.Faction)
            {
                case "civ":
                    gear = Helper.ToJson(row["civ_gear"].ToString());
                    break;
                case "cop":
                    gear = Helper.ToJson(row["cop_gear"].ToString());
                    break;
                case "med":
                    gear = Helper.ToJson(row["med_gear"].ToString());
                    break;
            }
            p.AddGear(gear);
            p.AddLicenses(licenses);
            //foreach (string item in p.Equipment)
            //{
            //    if (item.Contains("_") && !_debugListEqu.Contains(item))
            //    {
            //        _debugListEqu.Add(item);
            //        //SetText($"{item}{Environment.NewLine}");
            //    }
            //}
            //if (_gangWarId.Contains((int)row["gangid"]) && p.Faction == "civ")
            //{
            //    p.WarTarget = true;
            //}
            return p;
        }
        /// <summary>
        /// Setup the player object in regards to player stats
        /// </summary>
        public void AddStats(int cop, int medic, int admin, int donator, int kills, int deaths, int revives, int arrests, int cash, int bank, int bountyCollected, int bountyWanted)
        {
            CopLevel = cop;
            MedicLevel = medic;
            AdminLevel = admin;
            DonatorLevel = donator;
            Kills = kills;
            Deaths = deaths;
            MedicRevives = revives;
            CopArrest = arrests;
            Cash = cash;
            Bank = bank;
            BountyCollected = bountyCollected;
            BountyWanted = bountyWanted;
            ParseLevel();
        }

        private void ParseLevel()
        {
            switch (CopLevel)
            {
                case 0:
                    CopRank = "N/A";
                    break;
                case 1:
                    CopRank = "Derputy";
                    break;
                case 2:
                    CopRank = "Patrol Officer";
                    break;
                case 3:
                    CopRank = "Corporal";
                    break;
                case 4:
                    CopRank = "Staff Sgt.";
                    break;
                case 5:
                    CopRank = "Sergeant";
                    break;
                case 6:
                    CopRank = "Lieutenant";
                    break;
                case 7:
                    CopRank = "Dep. Chief";
                    break;
                case 8:
                    CopRank = "Chief";
                    break;
            }
            switch (MedicLevel)
            {
                case 0:
                    MedicRank = "N/A";
                    break;
                case 1:
                    MedicRank = "EMT";
                    break;
                case 2:
                    MedicRank = "Basic Paramedic";
                    break;
                case 3:
                    MedicRank = "Adv Paramedic";
                    break;
                case 4:
                    MedicRank = "S && R";
                    break;
                case 5:
                    MedicRank = "Supervisor";
                    break;
                case 6:
                    MedicRank = "Coordinator";
                    break;
                case 7:
                    MedicRank = "Director";
                    break;
            }
        }
        /// <summary>
        /// Parse the JSON string regarding gear and inventory
        /// </summary>
        public bool VigiGun { get; private set; }

        private readonly List<string> VigiGuns = new List<string>()
        {
            "hgun_P07_F", "hgun_ACPC2_F", "SMG_02_F", "arifle_SPAR_01_snd_F" ,
            "arifle_MX_Black_F","hgun_Pistol_heavy_02_F","hgun_Pistol_heavy_01_F","arifle_MX_GL_Black_F","arifle_MXM_Black_F",
            "arifle_MX_SW_Black_F","srifle_DMR_03_F","srifle_DMR_02_F","arifle_SPAR_01_GL_blk_F","arifle_SPAR_02_blk_F","arifle_SPAR_03_blk_F","arifle_ARX_blk_F","srifle_DMR_07_blk_F"
        };
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
                    if (VigiGuns.Contains(equipment[i].ToString()))
                        VigiGun = true;
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

        public void AddLicenses(string licenses)
        {
            if (licenses.Length <= 2) return;
            licenses = licenses.Insert(0, "{\"licenses\": ");
            licenses += "}";
            var licenseJa = JArray.Parse(JObject.Parse(licenses)["licenses"].ToString());
            foreach (var license in licenseJa)
            {
                if (license[1].ToString() == "1")
                    CivLicenses.Add(license[0].ToString());
            }

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
