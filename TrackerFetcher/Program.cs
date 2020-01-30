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
        static readonly string player_query2 = "SELECT * FROM players INNER JOIN gangmembers ON players.playerid=gangmembers.playerid WHERE last_active >= NOW() - INTERVAL 15 MINUTE AND last_server = {0} ORDER BY last_active DESC";
        static readonly string vehicle_query = "SELECT id, pid, side, `type`, classname, alive, active, insured, modifications, inventory FROM vehicles WHERE pid = '{0}' AND alive = 1 AND active > 0";
        static readonly string vehicle_delete_query = "DELETE FROM vehicles WHERE pid = '{0}'";
        static readonly string player_update_query = "UPDATE players SET actually_active = 0 WHERE actually_active = 1";
        private static readonly string house_query = $"";
        private static readonly string house_update_query = $"";
        private static readonly string gang_query = "SELECT * FROM `gangwars` WHERE (init_gangid = '111' OR acpt_gangid = '111') AND active = 1";

        private static readonly Stopwatch Sw = new Stopwatch();
        private const long PlayerRefreshTime = 60000;
        private const long HouseRefreshTime = 1800000;
        private const long GangRefreshTime = 1800000;
        private static long _playerRunTime;
        private static long _houseRunTime;
        private static long _gangRunTime;

        private static readonly int[] Servers = new[] { 1, 2 };

        private static readonly string[] BMUrl = new[] { "https://www.battlemetrics.com/servers/arma3/226557", "https://www.battlemetrics.com/servers/arma3/226555" };

        public static void ConsoleLog(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }
        static void Main(string[] args)
        {
            PlayerUpdate();
            HouseUpdate();
            //GangUpdate();
            while (true)
            {
                if (!Sw.IsRunning) Sw.Start();
                if (Sw.ElapsedMilliseconds - _playerRunTime >= PlayerRefreshTime)
                    PlayerUpdate();
                if (Sw.ElapsedMilliseconds - _houseRunTime >= HouseRefreshTime)
                    HouseUpdate();
                //if (Sw.ElapsedMilliseconds - _gangRunTime >= GangRefreshTime)
                //    GangUpdate();
            }
        }
        private static void PlayerUpdate()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoPlayerUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            _playerRunTime = Sw.ElapsedMilliseconds;
        }
        private static void HouseUpdate()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoHouseUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
            _houseRunTime = Sw.ElapsedMilliseconds;
        }

        private static void GangUpdate()
        {
            Thread tr = new Thread(DoGangUpdate);
            tr.Start();
            tr.Join();
            _gangRunTime = Sw.ElapsedMilliseconds;
        }
        private static void DoPlayerUpdate(int serverId)
        {
            try
            {
#if DEBUG
                Db os_db = new Db("127.0.0.1", 3307, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
                Db ot_db = new Db("157.230.200.22", 3306, "otdb", "otuser", "2016againlol");
#else
                Db os_db = new Db("198.50.177.116", 3306, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
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
                        string sql = $"INSERT INTO players (`uid`,`name`,`playerid`,`cash`,`bankacc`,`coplevel`,`cop_licenses`,`civ_licenses`,`med_licenses`,`cop_gear`,`med_gear`,`mediclevel`,`arrested`,`adminlevel`,`newdonor`,`donatorlvl`,`civ_gear`,`coordinates`,`player_stats`,`wanted`,`last_active`,`joined`,`last_side`,`last_server`,`newslevel`,`warpts`,`warkills`,`wardeaths`,`supportteam`,`vigiarrests`,`current_title`,`gangID`,`gangName`,`gangRank`, `aliases`) VALUES ('{row["uid"]}', '{row["name"]}', '{row["playerid"]}', '{row["cash"]}', '{row["bankacc"]}', '{row["coplevel"]}', '{row["cop_licenses"]}', '{row["civ_licenses"]}', '{row["med_licenses"]}', '{row["cop_gear"]}', '{row["med_gear"]}', '{row["mediclevel"]}', '{row["arrested"]}', '{row["adminlevel"]}', '{row["newdonor"]}', '{row["donatorlvl"]}', '{row["civ_gear"]}', '{row["coordinates"]}', '{row["player_stats"]}', '{row["wanted"]}', '{((DateTime)row["last_active"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{((DateTime)row["joined"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{row["last_side"]}', '{row["last_server"]}', '{row["newslevel"]}', '{row["warpts"]}', '{row["warkills"]}', '{row["wardeaths"]}', '{row["supportteam"]}', '{row["vigiarrests"]}', '{row["current_title"]}', '{row["gangid"]}', '{row["gangname"]}', '{row["rank"]}', '{row["name"]};');";
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
                        string sql = $"UPDATE players SET `name`= '{row["name"]}', cash = '{row["cash"]}', bankacc = '{row["bankacc"]}', coplevel = '{row["coplevel"]}', cop_licenses = '{row["cop_licenses"]}', civ_licenses = '{row["civ_licenses"]}', med_licenses = '{row["med_licenses"]}', cop_gear = '{row["cop_gear"]}', med_gear = '{row["med_gear"]}', mediclevel = '{row["mediclevel"]}', arrested = '{row["arrested"]}', adminlevel = '{row["adminlevel"]}', newdonor = '{row["newdonor"]}', donatorlvl = '{row["donatorlvl"]}', civ_gear = '{row["civ_gear"]}', coordinates = '{row["coordinates"]}', player_stats = '{row["player_stats"]}', wanted = '{row["wanted"]}', last_active = '{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', last_side = '{row["last_side"]}', last_server = '{row["last_server"]}', warpts = '{row["warpts"]}', warkills = '{row["warkills"]}', wardeaths = '{row["wardeaths"]}', supportteam = '{row["supportteam"]}', vigiarrests = '{row["vigiarrests"]}', current_title = '{row["current_title"]}', gangID = '{row["gangid"]}', gangName = '{row["gangname"]}', gangRank = '{row["rank"]}',  aliases = '{alies}' WHERE playerid = '{playerid}';";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                    ot_db.ExecuteNonQuery(string.Format(vehicle_delete_query, playerid));
                    DataTable os_veh_dt = os_db.ExecuteReaderDT(string.Format(vehicle_query, playerid));
                    foreach (DataRow row2 in os_veh_dt.Rows)
                    {
                        string sql = $"INSERT INTO `vehicles` (id, pid, side, `type`, classname, alive, active, insured, modifications, inventory) VALUES ('{row2["id"]}', '{row2["pid"]}', '{row2["side"]}', '{row2["type"]}', '{row2["classname"]}', '{Convert.ToInt32(row2["alive"])}', '{Convert.ToInt32(row2["active"])}', '{Convert.ToInt32(row2["insured"])}', '{row2["modifications"]}', '{row2["inventory"]}');";
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

        private static void DoHouseUpdate(int serverId)
        {
            try
            {
#if DEBUG
                Db osDb = new Db("127.0.0.1", 3307, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
                Db otDb = new Db("157.230.200.22", 3306, "otdb", "otuser", "2016againlol");
#else
                Db osDb = new Db("198.50.177.116", 3306, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
                Db otDb = new Db("127.0.0.1", 3306, "otdb", "otuser", "2016againlol");
#endif
                string t = $"SELECT * FROM houses{serverId} WHERE last_active >= NOW() - INTERVAL 1 HOUR";
                DataTable os_dt = osDb.ExecuteReaderDT(t);
                if (os_dt == null) return;
                int pip = 0;
                int pup = 0;
                ConsoleLog($"Server {serverId} Houses started");
                foreach (DataRow row in os_dt.Rows)
                {
                    int results = 0;
                    string pos = row["pos"].ToString();
                    int count = Convert.ToInt32(otDb.ExecuteScalar($"SELECT COUNT(*) FROM houses WHERE pos = '{pos}' AND server = '{serverId}';"));
                    if (count == 0)
                    {
                        string sql = $"INSERT INTO houses (pid, pos, server, inventory, storageCapacity, owned, last_active, player_keys, inAH, oil, physical_inventory, physicalStorageCapacity) " +
                                     $"VALUES ('{row["pid"]}', '{row["pos"]}', {serverId}, '{row["inventory"]}', {row["storageCapacity"]}, {row["owned"]}, '{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', '{row["player_keys"]}', {row["inAH"]}, {row["oil"]}, '{row["physical_inventory"]}', {row["physicalStorageCapacity"]})";
                        results = otDb.ExecuteNonQuery(sql);
                        _ = results > 0 ? pip++ : 0;
                    }
                    else
                    {
                        string sql =
                            $"UPDATE houses SET pid='{row["pid"]}', pos='{row["pos"]}', server={serverId}, inventory='{row["inventory"]}', storageCapacity={row["storageCapacity"]}, owned={row["owned"]}, last_active='{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', player_keys='{row["player_keys"]}', inAH={row["inAH"]}, oil={row["oil"]}, physical_inventory='{row["physical_inventory"]}', physicalStorageCapacity={row["physicalStorageCapacity"]} WHERE pos='{row["pos"]}' AND server={serverId}";
                        results = otDb.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                }
                ConsoleLog($"Server {serverId} Houses: {pip} Added; {pup} Updated;");

            }
            catch (Exception e)
            {
                ConsoleLog(e.Message);
                ConsoleLog(e.StackTrace);
            }
        }

        private static void DoGangUpdate()
        {
            try
            {
#if DEBUG
                Db osDb = new Db("127.0.0.1", 3307, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
                Db otDb = new Db("157.230.200.22", 3306, "otdb", "otuser", "2016againlol");
#else
                Db osDb = new Db("198.50.177.116", 3306, "lc_prod", "apistorage_web", "MehG098T9BWNS#edzW&OOO#vR0or*kug");
                Db otDb = new Db("127.0.0.1", 3306, "otdb", "otuser", "2016againlol");
#endif
                DataTable os_dt = osDb.ExecuteReaderDT(gang_query);
                if (os_dt == null) return;
                int pip = 0;
                int pup = 0;
                ConsoleLog($"DB Gang wars started");
                otDb.ExecuteNonQuery("UPDATE `gangwars` SET `active` = '0'");
                foreach (DataRow row in os_dt.Rows)
                {

                    int results = 0;
                    ulong gwid = Convert.ToUInt64(row["id"]);
                    int count = Convert.ToInt32(otDb.ExecuteScalar($"SELECT COUNT(*) FROM gangwars WHERE gwid = '{gwid}';"));
                    if (count == 0)
                    {
                        string sql = $"INSERT INTO gangwars (`gwid`, `instigator`, `init_gangid`, `init_gangname`, `acceptor`, `acpt_gangid`, `acpt_gangname`, `active`, `ikills`, `akills`, `ideaths`, `adeaths`, `date`) " +
                                     $"VALUES ({row["id"]}, {row["instigator"]}, {row["init_gangid"]}, '{row["init_gangname"]}', {row["acceptor"]}, {row["acpt_gangid"]}, '{row["acpt_gangname"]}', {row["active"]}, {row["ikills"]}, {row["akills"]}, {row["ideaths"]}, {row["adeaths"]}, '{(DateTime)row["date"]:yyyy-MM-dd HH:mm:ss}');";
                        results = otDb.ExecuteNonQuery(sql);
                        _ = results > 0 ? pip++ : 0;
                    }
                    else
                    {
                        string sql = $"UPDATE gangwars SET `active`= '1',  `ikills` = '{row["ikills"]}', `akills` = '{row["akills"]}', `ideaths` = '{row["ideaths"]}', `adeaths` = '{row["adeaths"]}', `date` = '{(DateTime)row["date"]:yyyy-MM-dd HH:mm:ss}';";
                        results = otDb.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                }
                ConsoleLog($"DB Gangwars: {pip} Added; {pup} Updated;");

            }
            catch (Exception e)
            {
                ConsoleLog(e.Message);
                ConsoleLog(e.StackTrace);
            }
        }
    }
}
