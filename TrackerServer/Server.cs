using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;
using TrackerInterface;
using TrackerServer.Rest;

namespace TrackerServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Server : IServer
    {
#if DEBUG
        private const string HostOs = "127.0.0.1";
        private const int PortOs = 3307;
        private const string UserOs = "apistorage_web";
        private const string PassOs = "MehG098T9BWNS#edzW&OOO#vR0or*kug";
        private const string SchemaOs = "lc_prod";

        private const string HostOt = "157.230.200.22";
        private const int PortOt = 3306;
        private const string UserOt = "otuser";
        private const string PassOt = "2016againlol";
        private const string SchemaOt = "otdb";
#else
        private const string HostOs = "198.50.177.116";
        private const int PortOs = 3306;
        private const string UserOs = "apistorage_web";
        private const string PassOs = "MehG098T9BWNS#edzW&OOO#vR0or*kug";
        private const string SchemaOs = "lc_prod";

        private const string HostOt = "127.0.0.1";
        private const int PortOs = 3306;
        private const string UserOt = "otuser";
        private const string PassOt = "2016againlol";
        private const string SchemaOt = "otdb";
#endif

        private readonly int[] Servers = new[] {1, 2};

        private const string PlayerSelectQuery =
            "SELECT * FROM players INNER JOIN gangmembers ON players.playerid=gangmembers.playerid WHERE last_active >= NOW() - INTERVAL 15 MINUTE AND last_server = {0} ORDER BY last_active DESC";

        private const string PlayerAliasQuery = "SELECT `aliases` FROM players where playerid = {0};";
        private const string PlayerCountQuery = "SELECT COUNT(*) FROM players WHERE playerid = '{0}'";
        private const string PlayerUpdateQuery = "UPDATE players SET actually_active = 0 WHERE actually_active = 1;";

        private const string VehicleSelectQuery =
            "SELECT id, pid, side, `type`, classname, alive, active, insured, modifications, inventory FROM vehicles WHERE pid = '{0}' AND alive = 1 AND active > 0";

        private const string VehicleDeleteQuery = "DELETE FROM vehicles WHERE pid = '{0}';";
        private readonly string house_query = $"";
        private readonly string house_update_query = $"";

        private readonly string gang_query =
            "SELECT * FROM `gangwars` WHERE (init_gangid = '111' OR acpt_gangid = '111') AND active = 1";

        private List<int> WarTargetGid { get; set; }

        public void ConsoleLog(string msg, string sender)
        {
            Console.WriteLine($"[{DateTime.Now}] ({sender}) {msg}");
        }

        public List<int> FetchWarTargets()
        {
            return WarTargetGid;
        }


    public void PlayerUpdate()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoPlayerUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
        }

        public void HouseUpdate()
        {
            Servers.Select(id =>
            {
                Thread tr = new Thread(() => DoHouseUpdate(id));
                tr.Start();
                return tr;

            }).ToList().ForEach(t => t.Join());
        }

        public void GangWarsUpdate()
        {
            Thread tr = new Thread(DoGangWarUpdate);
            tr.Start();
            tr.Join();
        }

        private async void DoGangWarUpdate()
        {
            OtRestClient client = new OtRestClient();
            client.Headers.Add("Authorization", "Token 94cc74bddddeb5464aa90535451df9ef6bdd1235");
            string endpoint = "https://lc2.olympus-entertainment.com/api/v1/gangs/111/wars/?active=true&limit=1000";
            string offsetqs = "&offset={0}";
            List<int> gangId = new List<int>();
            int offset = 0;
            int pages = 1;
            while (true)
            {
                IRestResponse response = await client.Get(endpoint, client.Headers);
                Results r = JsonConvert.DeserializeObject<Results>(response.Content);
                foreach (War war in r.results)
                {
                    if (!gangId.Contains(war.enemy.id))
                        gangId.Add(war.enemy.id);
                }
                if (r.next != null)
                {
                    offset += 1000;
                    endpoint += string.Format(offsetqs, offset);
                    ConsoleLog($"Page {pages} Completed. {offset}/{r.count} ", "UPDATER");
                    pages++;
                }
                else
                    break;
            }

            WarTargetGid = gangId;
            ConsoleLog($"{gangId.Count} active wars synced.", "UPDATER");
        }

        private void DoPlayerUpdate(int serverId)
        {
            try
            {
                Db os_db = new Db(HostOs, PortOs, SchemaOs, UserOs, PassOs);
                Db ot_db = new Db(HostOt, PortOt, SchemaOt, UserOt, PassOt);
                string t = string.Format(PlayerSelectQuery, serverId);
                if (os_db.LogCheck() == false)
                {
                    ConsoleLog("LOGGING ENABLED!! ABORTING!", "UPDATER");
                    Thread.CurrentThread.Abort();
                }
                DataTable os_dt = os_db.ExecuteReaderDT(t);
                if (os_dt == null) return;
                int pip = 0;
                int pup = 0;
                ConsoleLog($"Server {serverId} started", "UPDATER");
                ot_db.ExecuteNonQuery(PlayerUpdateQuery);
                foreach (DataRow row in os_dt.Rows)
                {
                    int results = 0;
                    ulong playerid = Convert.ToUInt64(row["playerid"]);
                    int count = Convert.ToInt32(ot_db.ExecuteScalar(string.Format(PlayerCountQuery, playerid)));
                    if (count == 0)
                    {
                        string sql = $"INSERT INTO players (`uid`,`name`,`playerid`,`cash`,`bankacc`,`coplevel`,`cop_licenses`,`civ_licenses`,`med_licenses`,`cop_gear`,`med_gear`,`mediclevel`,`arrested`,`adminlevel`,`newdonor`,`donatorlvl`,`civ_gear`,`coordinates`,`player_stats`,`wanted`,`last_active`,`joined`,`last_side`,`last_server`,`newslevel`,`warpts`,`warkills`,`wardeaths`,`supportteam`,`vigiarrests`,`current_title`,`gangID`,`gangName`,`gangRank`, `aliases`) VALUES ('{row["uid"]}', '{row["name"]}', '{row["playerid"]}', '{row["cash"]}', '{row["bankacc"]}', '{row["coplevel"]}', '{row["cop_licenses"]}', '{row["civ_licenses"]}', '{row["med_licenses"]}', '{row["cop_gear"]}', '{row["med_gear"]}', '{row["mediclevel"]}', '{row["arrested"]}', '{row["adminlevel"]}', '{row["newdonor"]}', '{row["donatorlvl"]}', '{row["civ_gear"]}', '{row["coordinates"]}', '{row["player_stats"]}', '{row["wanted"]}', '{((DateTime)row["last_active"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{((DateTime)row["joined"]).ToString("yyyy-MM-dd HH:mm:ss")}', '{row["last_side"]}', '{row["last_server"]}', '{row["newslevel"]}', '{row["warpts"]}', '{row["warkills"]}', '{row["wardeaths"]}', '{row["supportteam"]}', '{row["vigiarrests"]}', '{row["current_title"]}', '{row["gangid"]}', '{row["gangname"]}', '{row["rank"]}', '{row["name"]};');";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pip++ : 0;
                    }
                    else
                    {
                        string alias = ot_db.ExecuteScalar(string.Format(PlayerAliasQuery, playerid)).ToString();
                        if (!alias.Contains(row["name"].ToString()))
                        {
                            alias += $"{row["name"]};";
                        }
                        string sql = $"UPDATE players SET `name`= '{row["name"]}', cash = '{row["cash"]}', bankacc = '{row["bankacc"]}', coplevel = '{row["coplevel"]}', cop_licenses = '{row["cop_licenses"]}', civ_licenses = '{row["civ_licenses"]}', med_licenses = '{row["med_licenses"]}', cop_gear = '{row["cop_gear"]}', med_gear = '{row["med_gear"]}', mediclevel = '{row["mediclevel"]}', arrested = '{row["arrested"]}', adminlevel = '{row["adminlevel"]}', newdonor = '{row["newdonor"]}', donatorlvl = '{row["donatorlvl"]}', civ_gear = '{row["civ_gear"]}', coordinates = '{row["coordinates"]}', player_stats = '{row["player_stats"]}', wanted = '{row["wanted"]}', last_active = '{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', last_side = '{row["last_side"]}', last_server = '{row["last_server"]}', warpts = '{row["warpts"]}', warkills = '{row["warkills"]}', wardeaths = '{row["wardeaths"]}', supportteam = '{row["supportteam"]}', vigiarrests = '{row["vigiarrests"]}', current_title = '{row["current_title"]}', gangID = '{row["gangid"]}', gangName = '{row["gangname"]}', gangRank = '{row["rank"]}',  aliases = '{alias}' WHERE playerid = '{playerid}';";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                    ot_db.ExecuteNonQuery(string.Format(VehicleDeleteQuery, playerid));
                    if (os_db.LogCheck() == false)
                    {
                        ConsoleLog("LOGGING ENABLED!! ABORTING!", "UPDATER");
                        Thread.CurrentThread.Abort();
                    }
                    DataTable os_veh_dt = os_db.ExecuteReaderDT(string.Format(VehicleSelectQuery, playerid));
                    foreach (DataRow row2 in os_veh_dt.Rows)
                    {
                        string sql = $"INSERT INTO `vehicles` (id, pid, side, `type`, classname, alive, active, insured, modifications, inventory) VALUES ('{row2["id"]}', '{row2["pid"]}', '{row2["side"]}', '{row2["type"]}', '{row2["classname"]}', '{Convert.ToInt32(row2["alive"])}', '{Convert.ToInt32(row2["active"])}', '{Convert.ToInt32(row2["insured"])}', '{row2["modifications"]}', '{row2["inventory"]}');";
                        ot_db.ExecuteNonQuery(sql);
                    }
                }
                ConsoleLog($"Server {serverId}: {pip} Added; {pup} Updated;", "UPDATER");
                os_db = null;
                ot_db = null;
            }
            catch (Exception e)
            {
                ConsoleLog(e.Message, "UPDATER");
                ConsoleLog(e.StackTrace, "UPDATER");
            }
        }
        private void DoHouseUpdate(int serverId)
        {
            try
            {
                Db os_db = new Db(HostOs, PortOs, SchemaOs, UserOs, PassOs);
                Db ot_db = new Db(HostOt, PortOt, SchemaOt, UserOt, PassOt);
                if (os_db.LogCheck() == false)
                {
                    ConsoleLog("LOGGING ENABLED!! ABORTING!", "UPDATER");
                    Thread.CurrentThread.Abort();
                }
                string t = $"SELECT * FROM houses{serverId} WHERE last_active >= NOW() - INTERVAL 1 HOUR";
                DataTable os_dt = os_db.ExecuteReaderDT(t);
                if (os_dt == null) return;
                int pip = 0;
                int pup = 0;
                ConsoleLog($"Server {serverId} Houses started", "UPDATER");
                foreach (DataRow row in os_dt.Rows)
                {
                    int results = 0;
                    string pos = row["pos"].ToString();
                    int count = Convert.ToInt32(ot_db.ExecuteScalar($"SELECT COUNT(*) FROM houses WHERE pos = '{pos}' AND server = '{serverId}';"));
                    if (count == 0)
                    {
                        string sql = $"INSERT INTO houses (pid, pos, server, inventory, storageCapacity, owned, last_active, player_keys, inAH, oil, physical_inventory, physicalStorageCapacity) " +
                                     $"VALUES ('{row["pid"]}', '{row["pos"]}', {serverId}, '{row["inventory"]}', {row["storageCapacity"]}, {row["owned"]}, '{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', '{row["player_keys"]}', {row["inAH"]}, {row["oil"]}, '{row["physical_inventory"]}', {row["physicalStorageCapacity"]})";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pip++ : 0;
                    }
                    else
                    {
                        string sql =
                            $"UPDATE houses SET pid='{row["pid"]}', pos='{row["pos"]}', server={serverId}, inventory='{row["inventory"]}', storageCapacity={row["storageCapacity"]}, owned={row["owned"]}, last_active='{(DateTime)row["last_active"]:yyyy-MM-dd HH:mm:ss}', player_keys='{row["player_keys"]}', inAH={row["inAH"]}, oil={row["oil"]}, physical_inventory='{row["physical_inventory"]}', physicalStorageCapacity={row["physicalStorageCapacity"]} WHERE pos='{row["pos"]}' AND server={serverId}";
                        results = ot_db.ExecuteNonQuery(sql);
                        _ = results > 0 ? pup++ : 0;
                    }
                }
                ConsoleLog($"Server {serverId} Houses: {pip} Added; {pup} Updated;", "UPDATER");
                os_db = null;
                ot_db = null;
            }
            catch (Exception e)
            {
                ConsoleLog(e.Message, "UPDATER");
                ConsoleLog(e.StackTrace, "UPDATER");
            }
        }
    }
}
