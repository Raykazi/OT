using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Newtonsoft.Json;
using TrackerInterface;

namespace TrackerServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFTrackerService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WcfTrackerService : IWcfTrackerService
    {
        private readonly List<Player>[] _onlinePlayers = { new List<Player>(), new List<Player>(), new List<Player>() };
        private List<Player>[] _tempOnlinePlayers = { new List<Player>(), new List<Player>(), new List<Player>() };
        public string GetMySteamId(string steamName)
        {
            var client = new RestClient("http://api.steampowered.com");
            var request = new RestRequest("ISteamUser/ResolveVanityURL/v0001/?key={key}&vanityurl={vanityurl}", Method.GET);
            request.AddUrlSegment("key", "095A87F4340E4F2F21C22397D0E1376C"); // replaces matching token in request.Resource
            request.AddUrlSegment("vanityurl", steamName); // replaces matching token in request.Resource
            var response = client.Execute(request);
            var content = response.Content; // raw content as string
            var o = JObject.Parse(content);
            return (string)o["response"]["steamid"];
        }
        //Curl method to fetch data from the API
        public string GetPlayerInfo(long playerId)
        {
            var client = new RestClient("http://olympusapi.xyz/apiv2");
            var request = new RestRequest("player/{id}", Method.GET);
            request.AddUrlSegment("id", playerId.ToString());
            var response = client.Execute(request);
            var content = response.Content;
            return content;
        }
        public string GetPlayerHouseInfo(long playerId)
        {
            var client = new RestClient("http://olympusapi.xyz/apiv2");
            var request = new RestRequest("house/{id}", Method.GET);
            request.AddUrlSegment("id", playerId.ToString());
            var response = client.Execute(request);
            var content = response.Content;
            return content;
        }

        public JObject GetActivePlayers()
        {
            var content = "";
            try
            {
                var client = new RestClient("http://olympusapi.xyz/apiv2");
                var request = new RestRequest("active", Method.GET);
                var response = client.Execute(request);
                content = response.Content;
                var jsonObject = JObject.Parse(content);
                return jsonObject;
            }
            catch (JsonReaderException e)
            {
                if (!content.Contains("Could not make contact with Server"))
                    Program.ConsoleLog(e.Message);
            }
            return null;

        }
        public List<string> GetPlayers(string serverId)
        {
            var content = "";
            try
            {
                var playerNames = new List<string>();
                var client = new RestClient("http://olympusapi.xyz/apiv2");
                var request = new RestRequest("query/{serverNum}", Method.GET);
                request.AddUrlSegment("serverNum", serverId);
                var response = client.Execute(request);
                content = response.Content;
                var jsonObject = JObject.Parse(content);
                var jsonArray = jsonObject["players"] as JArray;
                dynamic playerArray = jsonArray;
                if (playerArray == null) return playerNames;
                foreach (var player in playerArray)
                {
                    string name = player["Name"];
                    playerNames.Add(name);
                }
                return playerNames;

            }
            catch (JsonReaderException e)
            {
                if (!content.Contains("Could not make contact with Server"))
                    Program.ConsoleLog(e.Message);
            }
            return new List<string>();
        }
        public long GetSteamId(string name)
        {
            long steamId = 0;
            IRestResponse response = null;
            try
            {
                Retry:
                if (name.Length <= 0) return 0;
                var client = new RestClient("http://olympusapi.xyz/apiv2");
                var request = new RestRequest("?a=aliases&b={name}", Method.GET);
                request.AddUrlSegment("name", name); // replaces matching token in request.Resource
                //Program.ConsoleLog(String.Format("Fetching alias for {0}", name));
                response = client.Execute(request);
                var content = response.Content;
                var startIndex = content.IndexOf("Player ID:");
                if (startIndex == -1)
                {
                    name = name.Remove(name.LastIndexOf("[")).Trim();
                    goto Retry;
                }
                var id = content.Substring(startIndex + 11, 17);
                if (long.TryParse(id, out steamId))
                    content = null;
                else
                {
                    Program.ConsoleLog($"Failed to get ID {id}");
                    steamId = -1;
                }
            }
            catch (FormatException)
            {
                Program.ConsoleLog("Skipping player: " + name + " Reason" + response.Content);
                steamId = -1;
            }
            catch (ArgumentOutOfRangeException)
            {

            }
            return steamId;

        }
        public List<Player> GetPlayerList(string serverId)
        {
            try
            {
                int serverNum;
                switch (serverId)
                {
                    default:
                        serverNum = 0;
                        break;
                    case "arma_2_blame_poseidon":
                        serverNum = 1;
                        break;
                    case "arma_3":
                        serverNum = 2;
                        break;
                }
                //var sql = "SELECT * FROM servers WHERE `serverName` = ? ";
                //List<string>[] sr = db.ExecuteReader(sql, serverID);
                //playerCount = Convert.ToInt32(sr[2][0]);
                //Player[] players = new Player[playerCount];
                //Player player;
                //var playerList = new List<Player>();
                //sql = "SELECT * FROM player WHERE `server` = ? ORDER BY lastActive DESC LIMIT ?";
                //var pr = _db.ExecuteReader(sql, serverId, _playerCount);
                //for (var i = 0; i < _playerCount; i++)
                //{
                //    player = new Player(Convert.ToInt32(pr[0][i]), Convert.ToInt64(pr[1][i]), pr[2][i], Convert.ToInt32(pr[3][i]), Convert.ToInt32(pr[4][i]), Convert.ToInt32(pr[5][i]), Convert.ToInt32(pr[6][i]), Convert.ToInt32(pr[7][i]), Convert.ToInt32(pr[8][i]), pr[9][i], Convert.ToInt32(pr[10][i]), Convert.ToInt32(pr[11][i]), Convert.ToInt32(pr[12][i]), Convert.ToInt32(pr[14][i]), Convert.ToInt32(pr[15][i]), Convert.ToInt32(pr[16][i]), Convert.ToInt32(pr[17][i]), Convert.ToInt32(pr[13][i]), Convert.ToInt32(pr[18][i]), pr[19][i], Convert.ToInt32(pr[20][i]), Convert.ToInt64(pr[21][i]), pr[28][i], pr[29][i], pr[30][i], pr[23][i], Convert.ToInt64(pr[34][i]), pr[35][i]);
                //    sql = "SELECT * FROM houses WHERE steamID = ?";
                //    var hr = _db.ExecuteReader(sql, Convert.ToInt64(pr[1][i]));
                //    player.AddHouses(hr, hr[0].Count);
                //    playerList.Add(player);
                //    player = null;
                //}
                return _onlinePlayers[serverNum];
            }
            catch (Exception e)
            {
                Program.ConsoleLog(e.Message);
            }
            return null;
        }
        //Updates the players in the database
        public void PullPlayers(string serverId)
        {
            try
            {
                var players = GetActivePlayers();
                var server1Array = players["1"] as JArray;
                var server2Array = players["2"] as JArray;
                if (server1Array != null)
                {
                    Program.ConsoleLog($"Server 1: {server1Array.Count} Online");
                    _tempOnlinePlayers[0].Clear();
                    server1Array?.Select(p =>
                    {
                        var tr = new Thread(() => DoWork(p, 0, ref _tempOnlinePlayers));
                        tr.Start();
                        return tr;
                    }).ToList().ForEach(t => t.Join());
                    _onlinePlayers[0].Clear();
                    _onlinePlayers[0].AddRange(_tempOnlinePlayers[0]);
                    Program.ConsoleLog("Server 1 Processed");
                }
                if (server2Array != null)
                {
                    Program.ConsoleLog($"Server 2: {server2Array.Count} Online");
                    _tempOnlinePlayers[1].Clear();
                    server2Array?.Select(p =>
                    {
                        var tr = new Thread(() => DoWork(p, 1, ref _tempOnlinePlayers));
                        tr.Start();
                        return tr;
                    }).ToList().ForEach(t => t.Join());
                    _onlinePlayers[1].Clear();
                    _onlinePlayers[1].AddRange(_tempOnlinePlayers[1]);
                    Program.ConsoleLog("Server 2 Processed");
                }
            }
            catch (Exception e)
            {
                Program.ConsoleLog(e.Message);
            }
        }

        private void DoWork(JToken pToken, int serverNum, ref List<Player>[] tempOnlinePlayers)
        {
            var p = CreatePlayer(pToken, serverNum);
            if (p != null)
                tempOnlinePlayers[serverNum].Add(p);
        }

        private void DoWork(long steamId, int serverNum, ref List<Player>[] tempOnlinePlayers)
        {
            var p = CreatePlayer(steamId, serverNum);
            if (p != null)
                tempOnlinePlayers[serverNum].Add(p);
        }

        private Player CreatePlayer(JToken pInfo, int serverNum)
        {
            var aliases = "";
            var resultH = 0;
            var resultP = 0;
            //var phInfo = JObject.Parse(GetPlayerHouseInfo((long)pInfo["playerid"]));
            //var houses1 = JArray.Parse(phInfo["houses1data"].ToString());
            //var houses2 = JArray.Parse(phInfo["houses2data"].ToString());
            //if (phInfo["error"] != null)
            //{
            //    //Making sure we got a valid json string
            //    Program.ConsoleLog("Error[PHInfo]: " + pInfo["error"]);
            //    return null;

            //}
            aliases = pInfo["aliases"].Aggregate(aliases, (current, pAlias) => current + (pAlias + ";"));
            var gangName = (int)pInfo["gang_id"] == -1 ? "N/A" : (string)pInfo["gang_name"];
            var p = new Player((int)pInfo["uid"], (long)pInfo["playerid"], (string)pInfo["name"], aliases, gangName, (int)pInfo["gang_rank"], Convert.ToDateTime(pInfo["last_active"]).ToUnixTime(), DateTime.UtcNow.ToUnixTime(), (string)pInfo["loc"], (string)pInfo["last_side"]);
            p.AddGear(pInfo["civ_gear"].ToString());
            p.AddMoney((int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["stat_bounties"], (int)pInfo["wanted_total"]);
            p.AddStats((int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_arrests"]);
            p.AddTime((int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"]);
            p.AddVehicles(pInfo["vehicle_civ_air"].ToString(), pInfo["vehicle_civ_car"].ToString(), pInfo["vehicle_civ_ship"].ToString());

            Program.ConsoleLog($"Server #{serverNum}: {p.Name} {_tempOnlinePlayers[serverNum].Count}/{_onlinePlayers[serverNum].Count}");
            ////_insertCount++;
            //foreach (var jToken in houses1)
            //{
            //    var house = (JObject)jToken;
            //    var hid = (int)house["houseid"];
            //    var pos = (string)house["pos"];
            //    pos = pos.Remove(0, 1);
            //    pos = pos.Remove(pos.IndexOf(']'));
            //    var lastUsed = DateTime.Parse(house["last_active"].ToString()).ToUnixTime();
            //    var crates = Helper.ToJson(house["crates"].ToString());
            //    var virtuals = house["inventory"].ToString();
            //    var maxStorage = (int)house["storage"];
            //    p.AddHouse(hid, pos, lastUsed, crates, virtuals, maxStorage);

            //    //var data2 = _db.ExecuteReader("SELECT houseID FROM houses WHERE houseID =?", hid);
            //    //if (data2[0].Count == 0)
            //    //{
            //    //    //Add the player to the DB
            //    //    sql = "INSERT INTO houses (`houseID`, `steamID`, `location`, `lastAccessed`, `virtual`, `crates`, `storage`, `server`)  VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            //    //    resultH += _db.ExecuteNonQuery(sql, hid, steamId, pos, lastUsed, virtuals, crates, maxStorage, _serverName) == 1 ? 1 : 0;
            //    //}
            //    //else
            //    //{
            //    //    //Update the player's house
            //    //    sql = "UPDATE houses SET `steamID` = ?, `location` = ?, `lastAccessed` = ?, `crates` = ?, `virtual` = ?, `storage`= ?, `server`=? WHERE `houseID` = ?";
            //    //    resultH += _db.ExecuteNonQuery(sql, steamId, pos, lastUsed, crates, virtuals, maxStorage, _serverName, hid) == 1 ? 1 : 0;
            //    //}
            //}
            return p;
            //Lock the table
            //lock (_locker)
            //{
            //    var sql = "";
            //    //Parse the aliases into one string
            //    //if (pInfo.Count > 0)
            //    //Determine if we are adding a player or updating
            //    var data = _db.ExecuteReader("SELECT steamID FROM Player WHERE steamID =?", steamId);
            //    if (data[0].Count == 0)
            //    {
            //        //Add the player to the DB
            //        sql = "INSERT INTO player (UID, steamID, playerName, cash, bank, copLevel, medicLevel, adminLevel, donatorLevel, kills, deaths, medicRevives, bountyCollected, copArrests, timeCiv, timeApd, timeMed, bountyWanted, aliases, gangName, lastActive, vehApdAir, vehApdCar, vehApdShip, vehCivAir, vehCivCar, vehCivShip, vehMedAir, vehMedCar, vehMedShip, gearApd, gearCiv, gearMed, gangRank, timestamp, location, server) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            //        resultP += _db.ExecuteNonQuery(sql, (int)pInfo["uid"], (long)pInfo["playerid"], pInfo["name"], (int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_bounties"], (int)pInfo["stat_arrests"], (int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"], (int)pInfo["wanted_total"], aliases, pInfo["gang_name"], Convert.ToDateTime(pInfo["last_active"]).ToUnixTime(), pInfo["vehicle_apd_air"], pInfo["vehicle_apd_car"], pInfo["vehicle_apd_ship"], pInfo["vehicle_civ_air"], pInfo["vehicle_civ_car"], pInfo["vehicle_civ_ship"], pInfo["vehicle_med_air"], pInfo["vehicle_med_car"], pInfo["vehicle_med_ship"], pInfo["cop_gear"], pInfo["civ_gear"], pInfo["med_gear"], pInfo["gang_rank"], pInfo["time"], pInfo["raybeam"], _serverName) == 1 ? 1 : 0;
            //        if (resultP == 1)
            //            _insertCount++;
            //    }
            //    else
            //    {
            //        //Update the player
            //        sql = "UPDATE player SET `playerName` = ?, `cash` = ?, `bank` = ?, `copLevel` = ?, `medicLevel` = ?, `adminLevel` = ?, `donatorLevel` = ?, `aliases` = ?, `kills` = ?, `deaths` = ?, `medicRevives` = ?, `bountyCollected` = ?, `copArrests` = ?, `timeCiv` = ?, `timeApd` = ?, `timeMed` = ?, `bountyWanted` = ?, `gangName` = ?, `gangRank` = ?, `lastActive` = ?, `gearApd` = ?, `gearCiv` = ?, `gearMed` = ?, `vehApdAir` = ?, `vehApdCar` = ?, `vehApdShip` = ?, `vehCivAir` = ?, `vehCivCar` = ?, `vehCivShip` = ?, `vehMedAir` = ?, `vehMedCar` = ?, `vehMedShip` = ? , `timestamp` = ? , `location` = ?, `server` = ? WHERE `steamID` = ?";
            //        resultP = _db.ExecuteNonQuery(sql, pInfo["name"], (int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], aliases, (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_bounties"], (int)pInfo["stat_arrests"], (int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"], (int)pInfo["wanted_total"], pInfo["gang_name"], pInfo["gang_rank"], Helper.ToUnixTime(Convert.ToDateTime(pInfo["last_active"])), pInfo["cop_gear"], pInfo["civ_gear"], pInfo["med_gear"], pInfo["vehicle_apd_air"], pInfo["vehicle_apd_car"], pInfo["vehicle_apd_ship"], pInfo["vehicle_civ_air"], pInfo["vehicle_civ_car"], pInfo["vehicle_civ_ship"], pInfo["vehicle_med_air"], pInfo["vehicle_med_car"], pInfo["vehicle_med_ship"], pInfo["time"], pInfo["raybeam"], _serverName, steamId);
            //        if (resultP == 1)
            //            _updateCount++;
            //    }
            //    var houses = JArray.Parse(phInfo["houses1data"].ToString());
            //    _db.ExecuteNonQuery("UPDATE houses SET `steamID` = 0 WHERE steamID =?", steamId);
            //}
        }
    }
}
