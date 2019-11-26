using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackerFetcher
{
    class Program
    {
        //static string player_query = "SELECT * FROM players INNER JOIN gangmembers ON players.playerid=gangmembers.playerid INNER JOIN battlemetrics ON players.playerid=battlemetrics.playerid WHERE {0} ORDER BY last_active DESC";
        static string player_query2 = "SELECT * FROM players INNER JOIN gangmembers ON players.playerid=gangmembers.playerid WHERE last_active >= NOW() - INTERVAL 15 MINUTE AND last_server = {0} ORDER BY last_active DESC";
        static string vehicle_query = "SELECT id, pid, side, `type`, classname, alive, active, insured, modifications, inventory FROM vehicles WHERE pid = '{0}' AND alive = 1 AND active > 0";
        static string vehicle_delete_query = "DELETE FROM vehicles WHERE pid = '{0}'";
        static string player_update_query = "UPDATE players SET actually_active = 0 WHERE actually_active = 1";

        private static readonly Stopwatch Sw = new Stopwatch();
        private const long RefreshTime = 60000;

        private static readonly int[] Servers = new int[] {1, 2 };
        private static readonly string[] BMUrl = new string[] { "https://www.battlemetrics.com/servers/arma3/226557", "https://www.battlemetrics.com/servers/arma3/226555" };

        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
        static void Main(string[] args)
        {
            Update();
            while (true)
            {
                if (!Sw.IsRunning) Sw.Start();
                switch (Sw.ElapsedMilliseconds)
                {
                    default:
                        if (Sw.IsRunning)
                        {
                            if (Sw.ElapsedMilliseconds >= RefreshTime)
                                Update();
                        }
                        break;
                }
            }
        }
        private static void Update()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            Sw.Reset();
        }
        private static void DoUpdate(int serverId)
        {
            try
            {
#if DEBUG
                Db os_db = new Db("127.0.0.1", 3307, "lc_prod", "apistorage_web", "tzHdFBi!LIJ5&cmeezX3rlR5Jajgukg");
                Db ot_db = new Db("157.230.200.22", 3306, "otdb", "otuser", "2016againlol");
#else
                Db os_db = new Db("198.50.177.116", 3306, "lc_prod", "apistorage_web", "tzHdFBi!LIJ5&cmeezX3rlR5Jajgukg");
                Db ot_db = new Db("127.0.0.1", 3306, "otdb", "otuser", "2016againlol");
#endif

                string t = string.Format(player_query2, serverId);
                DataTable os_dt = os_db.ExecuteReaderDT(t);
                if (os_dt == null) return;
                int pip = 0;
                int pup = 0;
                ConsoleLog($"Server {serverId} started");
                ot_db.ExecuteNonQuery(player_update_query);
                foreach (DataRow row in os_dt.Rows)
                {
                    int results = 0;
                    ulong playerid = Convert.ToUInt64(row["playerid"]);
                    int count = Convert.ToInt32(ot_db.ExecuteScalar($"SELECT COUNT(*) FROM players WHERE playerid = '{playerid}';"));
                    if (count == 0)
                    {
                        string sql = $"INSERT INTO players (`uid`,`name`,`playerid`,`cash`,`bankacc`,`coplevel`,`cop_licenses`,`civ_licenses`,`med_licenses`,`cop_gear`,`med_gear`,`mediclevel`,`arrested`,`adminlevel`,`newdonor`,`donatorlvl`,`civ_gear`,`coordinates`,`player_stats`,`wanted`,`last_active`,`joined`,`last_side`,`last_server`,`newslevel`,`warpts`,`warkills`,`wardeaths`,`supportteam`,`vigiarrests`,`current_title`,`gangID`,`gangName`,`gangRank`, `aliases`) VALUES ('{row["uid"]}', '{row["name"]}', '{row["playerid"]}', '{row["cash"]}', '{row["bankacc"]}', '{row["coplevel"]}', '{row["cop_licenses"]}', '{row["civ_licenses"]}', '{row["med_licenses"]}', '{row["cop_gear"]}', '{row["med_gear"]}', '{row["mediclevel"]}', '{row["arrested"]}', '{row["adminlevel"]}', '{row["newdonor"]}', '{row["donatorlvl"]}', '{row["civ_gear"]}', '{row["coordinates"]}', '{row["player_stats"]}', '{row["wanted"]}', '{((DateTime) row["last_active"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{((DateTime) row["joined"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{row["last_side"]}', '{row["last_server"]}', '{row["newslevel"]}', '{row["warpts"]}', '{row["warkills"]}', '{row["wardeaths"]}', '{row["supportteam"]}', '{row["vigiarrests"]}', '{row["current_title"]}', '{row["gangid"]}', '{row["gangname"]}', '{row["rank"]}', '{row["name"]};');";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pip++ : 0;
                    }
                    else
                    {
                        string alies = ot_db.ExecuteScalar($"SELECT `aliases` FROM players where playerid = {playerid};").ToString();
                        if (!alies.Contains(row["name"].ToString()))
                        {
                            alies += $"{row["name"]};";
                        }
                        string sql = $"UPDATE players SET `name`= '{row["name"]}', cash = '{row["cash"]}', bankacc = '{row["bankacc"]}', coplevel = '{row["coplevel"]}', cop_licenses = '{row["cop_licenses"]}', civ_licenses = '{row["civ_licenses"]}', med_licenses = '{row["med_licenses"]}', cop_gear = '{row["cop_gear"]}', med_gear = '{row["med_gear"]}', mediclevel = '{row["mediclevel"]}', arrested = '{row["arrested"]}', adminlevel = '{row["adminlevel"]}', newdonor = '{row["newdonor"]}', donatorlvl = '{row["donatorlvl"]}', civ_gear = '{row["civ_gear"]}', coordinates = '{row["coordinates"]}', player_stats = '{row["player_stats"]}', wanted = '{row["wanted"]}', last_active = '{((DateTime) row["last_active"]).ToString("yyyy-MM-dd HH:mm:ss")}', last_side = '{row["last_side"]}', last_server = '{row["last_server"]}', warpts = '{row["warpts"]}', warkills = '{row["warkills"]}', wardeaths = '{row["wardeaths"]}', supportteam = '{row["supportteam"]}', vigiarrests = '{row["vigiarrests"]}', current_title = '{row["current_title"]}', gangID = '{row["gangid"]}', gangName = '{row["gangname"]}', gangRank = '{row["rank"]}',  aliases = '{alies}' WHERE playerid = '{playerid}';";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                    ot_db.ExecuteNonQuery(string.Format(vehicle_delete_query, playerid));
                    DataTable os_veh_dt = os_db.ExecuteReaderDT(string.Format(vehicle_query, playerid));
                    foreach (DataRow row2 in os_veh_dt.Rows)
                    {
                        string sql = string.Format("INSERT INTO `vehicles` (id, pid, side, `type`, classname, alive, active, insured, modifications, inventory) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}');", row2["id"], row2["pid"], row2["side"], row2["type"], row2["classname"], Convert.ToInt32(row2["alive"]), Convert.ToInt32(row2["active"]), Convert.ToInt32(row2["insured"]), row2["modifications"], row2["inventory"]);
                        ot_db.ExecuteNonQuery(sql);
                    }
                }
                ConsoleLog($"Server {serverId}: {pip} Added; {pup} Updated;");
                os_db = null;
                ot_db = null;
            }
            catch (Exception e)
            {
                ConsoleLog(e.Message);
                ConsoleLog(e.StackTrace);
            }
        }
    }
}
