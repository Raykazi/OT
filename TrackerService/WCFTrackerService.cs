using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TrackerInterface;

namespace TrackerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFTrackerService" in both code and config file together.
    public class WCFTrackerService : IWCFTrackerService
    {

        object locker = new object();
        DB db = new DB();
        int updateCount = 0;
        int insertCount = 0;
        int playerCount = 0;
        public string getPlayerIfno(long playerID)
        {
            var client = new RestClient("http://olympusapi.xyz/apiv2");
            var request = new RestRequest("player/{id}", Method.GET);
            request.AddUrlSegment("id", playerID.ToString()); // replaces matching token in request.Resource
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            return content;
        }
        public string getPlayers(string IP, int port)
        {
            var client = new RestClient("http://162.243.235.105/");
            var request = new RestRequest("getPlayers.php", Method.POST);

            request.AddParameter("IP", IP); // replaces matching token in request.Resource
            request.AddParameter("Port", port); // replaces matching token in request.Resource
            Program.ConsoleLog(String.Format("Fetching players on server {0}:{1}", IP, port));
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            content = content.Replace(@"\""", "s");
            content = content.Replace(@"\", "");
            JObject o = JObject.Parse(content);
            JArray a = (JArray)o["Players"];
            return content;
        }
        public long getSteamID(string name)
        {
            long steamID = 0;
            IRestResponse response = null;
            try
            {

                var client = new RestClient("http://162.243.235.105/");
                var request = new RestRequest("getAlias.php", Method.POST);
                request.AddParameter("Name", name); // replaces matching token in request.Resource
                Program.ConsoleLog(String.Format("Fetching alias for {0}", name));
                response = client.Execute(request);
                string content = response.Content;
                if (long.TryParse(content, out steamID))
                    content = null;
                else
                    steamID = -1;
            }
            catch (FormatException)
            {
                Program.ConsoleLog("Skipping player: " + name + " Reason" + response.Content.ToString());
                steamID = -1;
            }
            return steamID;

        }
        public List<Player> sendPlayers()
        {
            //Player[] players = new Player[playerCount];
            Player player;
            List<Player> playerList = new List<Player>();
            string sql = "SELECT * FROM player ORDER BY lastActive DESC LIMIT ?";
            List<string>[] r = db.ExecuteReader(sql, playerCount);
            for (int i = 0; i < playerCount; i++)
            {
                player = new Player(Convert.ToInt32(r[0][i]), Convert.ToInt64(r[1][i]), r[2][i], Convert.ToInt32(r[3][i]), Convert.ToInt32(r[4][i]), Convert.ToInt32(r[5][i]), Convert.ToInt32(r[6][i]), Convert.ToInt32(r[7][i]), Convert.ToInt32(r[8][i]), r[9][i], Convert.ToInt32(r[10][i]), Convert.ToInt32(r[11][i]), Convert.ToInt32(r[12][i]), Convert.ToInt32(r[14][i]), Convert.ToInt32(r[15][i]), Convert.ToInt32(r[16][i]), Convert.ToInt32(r[17][i]), Convert.ToInt32(r[13][i]), Convert.ToInt32(r[18][i]), r[19][i], Convert.ToInt32(r[20][i]), Convert.ToInt64(r[21][i]), r[28][i], r[29][i], r[30][i], r[23][i], Convert.ToInt64(r[34][i]));
                playerList.Add(player);
                player = null;
            }
            return playerList;
        }

        public void updateDB(List<string> steamIDs)
        {
            playerCount = steamIDs.Count;
            steamIDs.Select(id =>
            {
                Thread tr = new Thread(() => storeInfo(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join()); ;
            Program.ConsoleLog(string.Format("{0} player added. {1} player updated", insertCount, updateCount));
            //return playerCount;

        }

        private void storeInfo(string id)
        {
            string aliases = "";
            int result = 0;
            long steamID = Convert.ToInt64(id);
            if (steamID > 1)
            {
                JObject pInfo = JObject.Parse(getPlayerIfno(steamID));
                if (pInfo["error"] != null)
                {
                    Program.ConsoleLog("Error: " + pInfo["error"].ToString());
                    return;
                }
                lock (locker)
                {
                    string sql = "";
                    if (pInfo.Count > 0)
                        foreach (var pAlias in pInfo["aliases"])
                        {
                            aliases += pAlias + ";";
                        }
                    sql = "";
                    List<string>[] data = db.ExecuteReader("SELECT steamID FROM Player WHERE steamID =?", steamID);
                    if (data[0].Count == 0)
                    {
                        sql = "INSERT INTO player (UID, steamID, playerName, cash, bank, copLevel, medicLevel, adminLevel, donatorLevel, kills, deaths, medicRevives, bountyCollected, copArrests, timeCiv, timeApd, timeMed, bountyWanted, aliases, gangName, lastActive, vehApdAir, vehApdCar, vehApdShip, vehCivAir, vehCivCar, vehCivShip, vehMedAir, vehMedCar, vehMedShip, gearApd, gearCiv, gearMed, gangRank, timestamp) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        result = db.ExecuteNonQuery(sql, (int)pInfo["uid"], (long)pInfo["playerid"], pInfo["name"], (int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_bounties"], (int)pInfo["stat_arrests"], (int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"], (int)pInfo["wanted_total"], aliases, pInfo["gang_name"], Helper.ToUnixTime(Convert.ToDateTime(pInfo["last_active"])), pInfo["vehicle_apd_air"], pInfo["vehicle_apd_car"], pInfo["vehicle_apd_ship"], pInfo["vehicle_civ_air"], pInfo["vehicle_civ_car"], pInfo["vehicle_civ_ship"], pInfo["vehicle_med_air"], pInfo["vehicle_med_car"], pInfo["vehicle_med_ship"], pInfo["cop_gear"], pInfo["civ_gear"], pInfo["med_gear"], pInfo["gang_rank"], pInfo["time"]);
                        if (result == 1)
                            insertCount++;
                    }
                    else
                    {
                        sql = "UPDATE player SET `playerName` = ?, `cash` = ?, `bank` = ?, `copLevel` = ?, `medicLevel` = ?, `adminLevel` = ?, `donatorLevel` = ?, `aliases` = ?, `kills` = ?, `deaths` = ?, `medicRevives` = ?, `bountyCollected` = ?, `copArrests` = ?, `timeCiv` = ?, `timeApd` = ?, `timeMed` = ?, `bountyWanted` = ?, `gangName` = ?, `gangRank` = ?, `lastActive` = ?, `gearApd` = ?, `gearCiv` = ?, `gearMed` = ?, `vehApdAir` = ?, `vehApdCar` = ?, `vehApdShip` = ?, `vehCivAir` = ?, `vehCivCar` = ?, `vehCivShip` = ?, `vehMedAir` = ?, `vehMedCar` = ?, `vehMedShip` = ? , `timestamp` = ? WHERE `steamID` = ?";
                        result = db.ExecuteNonQuery(sql, pInfo["name"], (int)pInfo["cash"], (int)pInfo["bank"], (int)pInfo["coplevel"], (int)pInfo["mediclevel"], (int)pInfo["adminlevel"], (int)pInfo["donatorlevel"], aliases, (int)pInfo["stat_kills"], (int)pInfo["stat_deaths"], (int)pInfo["stat_revives"], (int)pInfo["stat_bounties"], (int)pInfo["stat_arrests"], (int)pInfo["stat_time_civ"], (int)pInfo["stat_time_apd"], (int)pInfo["stat_time_med"], (int)pInfo["wanted_total"], pInfo["gang_name"], pInfo["gang_rank"], Helper.ToUnixTime(Convert.ToDateTime(pInfo["last_active"])), pInfo["cop_gear"], pInfo["civ_gear"], pInfo["med_gear"], pInfo["vehicle_apd_air"], pInfo["vehicle_apd_car"], pInfo["vehicle_apd_ship"], pInfo["vehicle_civ_air"], pInfo["vehicle_civ_car"], pInfo["vehicle_civ_ship"], pInfo["vehicle_med_air"], pInfo["vehicle_med_car"], pInfo["vehicle_med_ship"], pInfo["time"], steamID);
                        if (result == 1)
                            updateCount++;
                    }
                }
            }
        }
    }
}
