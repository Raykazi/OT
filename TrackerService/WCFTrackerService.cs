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
        private readonly object _locker = new object();
        private readonly Db _db = new Db();
        /// <summary>
        /// Returns the steam ID of the given steam name
        /// </summary>
        /// <param name="steamName">Steam Name</param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Returns JSON string with player information
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public string GetPlayerInfo(long playerId)
        {
            var client = new RestClient("http://olympusapi.xyz/apiv2");
            var request = new RestRequest("player/{id}", Method.GET);
            request.AddUrlSegment("id", playerId.ToString());
            var response = client.Execute(request);
            var content = response.Content;
            return content;
        }
        /// <summary>
        /// Returns JSON string with player's house information
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public string GetPlayerHouseInfo(long playerId)
        {
            var client = new RestClient("http://olympusapi.xyz/apiv2");
            var request = new RestRequest("house/{id}", Method.GET);
            request.AddUrlSegment("id", playerId.ToString());
            var response = client.Execute(request);
            var content = response.Content;
            return content;
        }
        /// <summary>
        /// Returns object with active players on the server
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns list with player names on a server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns steam ID using the ingame name of a player
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns list of online players on the server
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public List<Player> GetPlayerList(int serverId)
        {
            try
            {
                int serverNum;
                switch (serverId)
                {
                    default:
                        serverNum = 0;
                        break;
                    case 2:
                        serverNum = 1;
                        break;
                }
                return _onlinePlayers[serverNum];
            }
            catch (Exception e)
            {
                Program.ConsoleLog(e.Message);
            }
            return null;
        }
        /// <summary>
        /// Fetches the online players
        /// Parses information and stores it in a Player object
        /// </summary>
        /// <param name="serverId"></param>
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
                    server1Array.Select(p =>
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
                    server2Array.Select(p =>
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
        /// <summary>
        /// New thread method that calls the CreatePlayer method
        /// </summary>
        /// <param name="pToken">Player JSON string</param>
        /// <param name="serverNum">Server #</param>
        /// <param name="tempOnlinePlayers"></param>
        private void DoWork(JToken pToken, int serverNum, ref List<Player>[] tempOnlinePlayers)
        {
            var p = CreatePlayer(pToken, serverNum);
            if (p != null)
                tempOnlinePlayers[serverNum].Add(p);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pInfo"></param>
        /// <param name="serverNum"></param>
        /// <returns></returns>
        private Player CreatePlayer(JToken pInfo, int serverNum)
        {
            try
            {
                var aliases = "";
                aliases = pInfo["aliases"].Aggregate(aliases, (current, pAlias) => current + (pAlias + ";"));
                var cAir = pInfo["vehicle_civ_air"].ToString();
                var cCar = pInfo["vehicle_civ_car"].ToString();
                var cShip = pInfo["vehicle_civ_ship"].ToString();
                var aAir = pInfo["vehicle_apd_air"].ToString();
                var aCar = pInfo["vehicle_apd_car"].ToString();
                var aShip = pInfo["vehicle_apd_ship"].ToString();
                var mAir = pInfo["vehicle_med_air"].ToString();
                var mCar = pInfo["vehicle_med_car"].ToString();
                var mShip = pInfo["vehicle_med_ship"].ToString();
                var civGear = pInfo["civ_gear"].ToString();
                var apdGear = pInfo["cop_gear"].ToString();
                var medGear = pInfo["med_gear"].ToString();
                var gangName = (int)pInfo["gang_id"] == -1 ? "N/A" : (string)pInfo["gang_name"];
                var p = new Player((int)pInfo["uid"], (long)pInfo["playerid"], (string)pInfo["name"], aliases, gangName, (int)pInfo["gang_rank"], Convert.ToDateTime(pInfo["last_active"]).ToUnixTime(), DateTime.UtcNow.ToUnixTime(), (string)pInfo["loc"], (string)pInfo["last_side"]);
                p.AddGear(civGear);
                p.AddMoney((int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["stat_bounties"], (int)pInfo["wanted_total"]);
                p.AddStats((int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_arrests"]);
                p.AddTime((int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"]);
                p.AddVehicles(cAir, cCar, cShip,"civ");
                p.AddVehicles(aAir, aCar, aShip, "apd");
                p.AddVehicles(mAir, mCar, mShip, "med");
                var phInfo = JObject.Parse(GetPlayerHouseInfo((long)pInfo["playerid"]));
                if (phInfo["error"] != null)
                {
                    //Making sure we got a valid json string
                    Program.ConsoleLog("Error[PHInfo]: " + pInfo["error"]);
                    return p;
                }
                var houses1 = JArray.Parse(phInfo["houses1data"].ToString());
                var houses2 = JArray.Parse(phInfo["houses2data"].ToString());
                p.AddHouses(houses1, 1);
                p.AddHouses(houses2, 2);
                p.Save(aliases, cAir, cCar, cShip, aAir,aCar,aShip,mAir,mCar,mShip, apdGear,civGear,medGear, serverNum);
                Program.ConsoleLog($"Server #{serverNum + 1}: {p.Name} {_tempOnlinePlayers[serverNum].Count}/{_onlinePlayers[serverNum].Count}");
                return p;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
            ////_insertCount++;

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

            //        if (resultP == 1)
            //            _insertCount++;
            //    }
            //    else
            //    {
            //        //Update the player

            //        if (resultP == 1)
            //            _updateCount++;
            //    }
            //    var houses = JArray.Parse(phInfo["houses1data"].ToString());
            //    _db.ExecuteNonQuery("UPDATE houses SET `steamID` = 0 WHERE steamID =?", steamId);
            //}
        }
    }
}
