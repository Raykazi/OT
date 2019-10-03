using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using TrackerInterface;

namespace TrackerClient
{

    public partial class FrmMain : Form
    {
        private readonly Db _db = new Db();

        /*List*/
        List<Player> _onlinePlayers = new List<Player>();
        List<Player> _slackPostList = new List<Player>();
        List<string> _debugListVeh = new List<string>();
        List<string> _debugListEqu = new List<string>();
        //private readonly List<Player>[] _onlinePlayers = { new List<Player>(), new List<Player>(), new List<Player>() };
        private List<Player>[] _tempOnlinePlayers = { new List<Player>(), new List<Player>(), new List<Player>() };

        bool _doingWork = false; //Prevents starting a player pull while one is still active
        bool _justRefreshed = false; //TODO may remove this later
        Player _lastSelected = null;
        int _serverId = 1; //Default server to pull from
        internal Map PlayerMap; //Map object to pass to the Map form/user control
        //SlackClient _sc = new SlackClient("https://hooks.slack.com/services/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");
        /*Loop Variables*/
        Stopwatch _sw = new Stopwatch();
        public int RefreshTime = 60000;

        /*More Lists*/
        string[] _watchListLegeals = {
               "salt","saltr",
                "sand","glass",
                "rock", "cement",
                "copperore", "copperr",
                "silver","silverr",
                "platinum","platinumr",
                "ironore","ironr",
                "oilu", "oilp",
                "diamond", "diamondc"};
        string[] _watchListIllegals = {
                "cannabis","marijuana",
                "cocaine",  "cocainep",
                "heroinu","heroinp",
                "frog", "frogp",
                "mushroom","mmushroom",
                "ephedra", "lithium", "phosphorus","crystalmeth",
                "yeast", "sugar", "corn","moonshine",
                "goldbar" };
        string[] _watchListVehicles = { "Hellcat", "HEMTT Box", "HEMTT Fuel", "HEMTT Transport", "Hummingbird", "Huron", "Ifrit", "Offroad (Armed)", "Orca", "M900", "Mohawk", "SDV",
                "Taru (Bench)", "Taru (Fuel)", "Taru (Transport)", "Tempest (Device)", "Tempest Fuel", "Tempest Transport", "Tempest Transport (Covered)", "Truck", "Truck Box", "Truck Fuel",
                "Zamak Fuel", "Zamak Transport", "Zamak Transport (Covered)"};

        ListViewItemComparer _lvwItemComparer = new ListViewItemComparer();
        private ListBox _activeListbox;
        /// <summary>
        /// Initialize the form
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            lvVehicleInfo.ListViewItemSorter = _lvwItemComparer;
            timerPLRefresh.Enabled = true;
            timerPLRefresh.Start();
            pbMap.Image = Properties.Resources.Altis3;
        }
        /// <summary>
        /// En/Disable buttons depending on Background worker status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerPLRefresh_Tick(object sender, EventArgs e)
        {
            switch (_sw.ElapsedMilliseconds)
            {
                default:
                    server1ToolStripMenuItem.Enabled = server2ToolStripMenuItem.Enabled = server3ToolStripMenuItem.Enabled = refreshToolStripMenuItem.Enabled = !_doingWork;
                    if (_sw.IsRunning)
                    {
                        tsslStatus.Text =
                            $"Refreshing player list in {(RefreshTime - _sw.ElapsedMilliseconds) / 1000} seconds.";
                        if (_sw.ElapsedMilliseconds >= RefreshTime)
                            Reset();
                    }
                    break;
            }
        }
        /// <summary>
        /// Starts the BGW method
        /// </summary>
        private void Reset()
        {
            try
            {
                if (bwPlayerListRefresh.IsBusy) return;
                _doingWork = true;
                tsslStatus.Text = @"Refreshing player list now.";
                bwPlayerListRefresh.RunWorkerAsync();
                _sw.Reset();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        /// <summary>
        /// Adds players with targeted items to a special list
        /// </summary>
        private void BuildTargetList()
        {
            var targetPlayers = new List<Player>();
            foreach (var p in _onlinePlayers)
            {

                var wItem = "";
                var wVehicle = "";
                p.TargetLevel = -1;
                if (p.CivVehicles != null)
                    foreach (var vehicle in from vehicle in p.CivVehicles from watchVehicle in _watchListVehicles where watchVehicle == vehicle.Name && vehicle.Active >= 1 select vehicle)
                    {
                        wVehicle += vehicle.Name + "\r\n";
                        if (!targetPlayers.Contains(p))
                            targetPlayers.Add(p);
                        p.TargetLevel = 0;
                    }
                if (p.Virtuals != null)
                    foreach (var item in p.Virtuals)
                    {
                        foreach (var watchItem in _watchListLegeals.Where(watchItem => watchItem == item.Name))
                        {
                            wItem += item.Name + "\r\n";
                            if (!targetPlayers.Contains(p))
                                targetPlayers.Add(p);
                            p.TargetLevel = 1;
                        }
                        foreach (var watchItem in _watchListIllegals.Where(watchItem => watchItem == item.Name))
                        {
                            wItem += item.Name + "\r\n";
                            if (!targetPlayers.Contains(p))
                                targetPlayers.Add(p);
                            p.TargetLevel = 2;
                        }
                    }
                //if (wItem.Length > 0 || wVehicle.Length > 0)
                //{
                //    if (!_slackPostList.Contains(p))
                //    {
                //        _slackPostList.Add(p);
                //        new Thread(() =>
                //        {
                //            Fields[] temp = {
                //                new Fields() { Title = "Item", Value = wItem },
                //                new Fields() { Title = "Vehicle", Value = wVehicle }
                //            };
                //            var attachment = new Attachment()
                //            {
                //                Title = p.Name,
                //                Text = "Last Updated: " + p.LastUpdated,
                //                Fields = temp
                //            };
                //            //sc.PostMessage(attachment);
                //            Thread.Sleep(3000);
                //        }).Start();
                //    }
                //}
            }
            targetPlayers = targetPlayers.OrderBy(p => p.Name).ToList();
            lbPlayersTargets.DisplayMember = "name";
            lbPlayersTargets.DataSource = targetPlayers;
        }
        /// <summary>
        /// Sets the listbox that the mouse enters as the active one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_MouseEnter(object sender, EventArgs e)
        {
            var lb = (ListBox)sender;
            _activeListbox = lb;
        }
        /// <summary>
        /// Displays the information of the selected house
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lb = (ListBox)sender;
            var h = (House)lb.SelectedValue;
            DisplayHouse(h);
        }
        /// <summary>
        /// Displays the information of a selected player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (ListBox)sender;
                if (tcMain.SelectedTab != tpPlayerInfo)
                {
                    tcMain.SelectTab(1);
                }
                if (lb.SelectedIndices.Count != 1) return;
                var p = (Player)lb.SelectedValue;
                _lastSelected = p;
                DisplayPlayer(p);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Draws the listbox manually with corresponding colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var g = e.Graphics;
            var lb = (ListBox)sender;
            if (e.Index <= -1 || e.Index >= lb.Items.Count) return;
            var p = (Player)lb.Items[e.Index];
            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var text = p.AdminLevel == 0 ? p.Name : p.Name.Insert(p.Name.Length, " [" + p.Faction.ToUpper() + "]");
            if (p.AdminLevel > 0)
            {
                g.FillRectangle(new SolidBrush(Color.LightSeaGreen), e.Bounds);
                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            else
            {
                switch (p.Faction)
                {
                    case "cop":
                        var lRank = Color.FromArgb(0, 97, 255);
                        var hRank = Color.FromArgb(0, 46, 122);
                        switch (p.CopLevel)
                        {
                            case 1:
                                text += " [Dep]";
                                g.FillRectangle(new SolidBrush(lRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 2:
                                text += " [PO]";
                                g.FillRectangle(new SolidBrush(lRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 3:
                                text += " [Corp]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 4:
                                text += " [SGT]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 5:
                                text += " [LT]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 6:
                                text += " [DChief]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 7:
                                text += " [Chief]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            default:
                                text += " [CopLev " + p.CopLevel + "]";
                                g.FillRectangle(new SolidBrush(hRank), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                        }
                        break;
                    case "med":
                        g.FillRectangle(new SolidBrush(Color.DarkGreen), e.Bounds);
                        g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                        break;
                    default:
                        switch (p.TargetLevel)
                        {
                            case 0:
                                g.FillRectangle(new SolidBrush(Color.Yellow), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 1:
                                g.FillRectangle(new SolidBrush(Color.DeepPink), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            case 2:
                                g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                            default:
                                g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                                g.DrawString(text, e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
                                break;
                        }
                        break;
                }
            }
            if (selected)
            {
                var highlight = SystemColors.MenuHighlight;
                g.FillRectangle(new SolidBrush(highlight), e.Bounds);
                g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            e.DrawFocusRectangle();
        }
        /// <summary>
        /// Displays the house 
        /// </summary>
        /// <param name="h"></param>
        private void DisplayHouse(House h)
        {
            pbMap.Invalidate();
            lvHVirtuals.Items.Clear();
            lvHInventory.Items.Clear();
            if (h == null) return;
            if (h.Virtual != null)
                foreach (var v in h.Virtual)
                {
                    var lviV = new ListViewItem(v.Name);
                    lviV.SubItems.Add(v.Amount.ToString());
                    lvHVirtuals.Items.Add(lviV);
                }
            if (h.Location.Length > 1)
                pbHouses_CenterPlayer(h.Location);
        }
        /// <summary>
        /// Centers map on the given location
        /// </summary>
        /// <param name="location">Location of the house</param>
        internal void pbHouses_CenterPlayer(string[] location)
        {
            var newCords = Helper.performCordScale(location, pbMap);
            var x = Convert.ToInt32(newCords[0]);
            var y = Convert.ToInt32(newCords[1]);
            var panelcenter = new Point((panelPictureBox.Width / 2), (panelPictureBox.Height / 2)); // find the centerpoint of the panel
            var offsetinpicturebox = new Point((pbMap.Location.X + x), (pbMap.Location.Y + y)); // find the offset of the mouse click
            var offsetfromcenter = new Point((panelcenter.X - offsetinpicturebox.X), (panelcenter.Y - offsetinpicturebox.Y)); // find the difference between the mouse click and the center
            panelPictureBox.AutoScrollPosition = new Point((Math.Abs(panelPictureBox.AutoScrollPosition.X) + (-1 * offsetfromcenter.X)), (Math.Abs(panelPictureBox.AutoScrollPosition.Y) + (-1 * offsetfromcenter.Y)));
        }
        /// <summary>
        /// Draws on the map near the player's house
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbHouses_Paint(object sender, PaintEventArgs e)
        {
            if (lbHouses.SelectedValue == null) return;
            var h = (House)lbHouses.SelectedValue;
            var newCords = Helper.performCordScale(h.Location, pbMap);
            e.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(4, 4)));
        }
        /// <summary>
        /// Displays information from the Player object on the form
        /// </summary>
        /// <param name="p"></param>
        private void DisplayPlayer(Player p)
        {
            var sortedList = new List<Vehicle>();
            var houses = new List<House>();
            tbAliases.Clear();
            lvVehicleInfo.Items.Clear();
            lvVirtualItems.Items.Clear();
            lvHVirtuals.Items.Clear();
            lvHInventory.Items.Clear();
            //lbHouses.DataSource = null;
            //lbHouses.DisplayMember = null;
            //lbHouses.Items.Clear();

            Text = $"{p.Name} | {p.SteamId}";
            lblName.Text = $"Name: {p.Name}";
            lblCash.Text = $"Cash: {p.Cash:C}";
            lblBounty.Text = $"Bounty: {p.BountyWanted:C}";
            lblKDR.Text =
                $"K/D/R: {p.Kills:0,0}/{p.Deaths:0,0}/{Convert.ToDecimal(Convert.ToDecimal(p.Kills) / Convert.ToDecimal(p.Deaths)):0.##}";
            lblCopRank.Text = $"APD Rank: {ParseRank(p.CopLevel, 0)}";
            lblCopTime.Text = $"APD Time: {((p.TimeApd.ToString() == "-1") ? "N/A" : p.TimeApd.ToString()):0,0}";
            lblGang.Text = $"Gang: {(p.GangName == "-1" ? "N/A" : p.GangName)}";
            lblBank.Text = $"Bank: {p.Bank:C}";
            lblVigiBounty.Text = $"Bounty Collected: {(p.BountyCollected == -1 ? 0 : p.BountyCollected):C}";
            lblCivTime.Text = $"Civ Time: {p.TimeCiv:0,0}";
            lblMedicRank.Text = $"R&R Rank: {ParseRank(p.MedicLevel, 1)}";
            lblMedicTime.Text = $"R&R Time: {(p.TimeMed.ToString() == "-1" ? "N/A" : p.TimeMed.ToString()):0,0}";
            lblVest.Text = $"Vest: {(p.Equipment.Count == 0 ? "None" : p.Equipment[1])}";
            lblHelmet.Text = $"Helmet: {(p.Equipment.Count == 0 ? "None" : p.Equipment[4])}";
            lblGun.Text = $"Gun: {(p.Equipment.Count == 0 ? "None" : TranslateWeapons(p.Equipment[5]))}";
            lblUpdated.Text = $"Last Updated (UTC): {p.LastUpdated}";
            lblLocation.Text = p.Location.Length > 1 ? $"Last Seen @ X:{p.Location[0]} Y:{p.Location[1]}" : "Last Seen @ Unknown";
            //foreach (var equip in p.Equipment.Where(equip => !_debugListEqu.Contains(equip) && equip.Length > 0))
            //{
            //    _debugListVeh.Add(equip);
            //    rtbDebugEquipment.Text += $"{equip}{Environment.NewLine}";
            //}
            foreach (var alias in p.Aliases)
            {
                tbAliases.Text += $"{alias}{Environment.NewLine}";
            }
            if (p.Virtuals != null)
                foreach (var v in p.Virtuals)
                {
                    var lviV = new ListViewItem(v.Name);
                    lviV.SubItems.Add(v.Amount.ToString());
                    lvVirtualItems.Items.Add(lviV);
                }
            if (p.CivVehicles != null)
                sortedList.AddRange(p.CivVehicles);
            sortedList = sortedList.OrderByDescending(v => v.Active).ToList();
            foreach (var v in sortedList)
            {
                var lviV = new ListViewItem(v.Name);
                lviV.SubItems.Add(v.Active.ToString());
                lviV.SubItems.Add(v.TurboLevel.ToString());
                lviV.SubItems.Add(v.SecLevel.ToString());
                lviV.SubItems.Add(v.StorageLevel.ToString());
                lviV.SubItems.Add(v.InsuranceLevel.ToString());
                lvVehicleInfo.Items.Add(lviV);
            }
            if (p.Houses != null)
            {
                foreach (var house in p.Houses)
                {
                    if (house.Server == _serverId)
                        houses.Add(house);
                }
            }
            houses = houses.OrderBy(h => h.Id).ToList();
            lbHouses.DisplayMember = houses.ToString();
            lbHouses.DataSource = houses;
            if (PlayerMap == null) return;
            if (_sw.ElapsedMilliseconds <= 1000) return;
            if (p.Location.Length > 1)
                PlayerMap.pbMap_CenterPlayer(p.Location);
        }

        private object ParseRank(int jobLevel, int job)
        {
            var position = "";
            if (job == 0)
            {
                switch (jobLevel)
                {
                    case 0:
                        position = "N/A";
                        break;
                    case 1:
                        position = "Derputy";
                        break;
                    case 2:
                        position = "Patrol Officer";
                        break;
                    case 3:
                        position = "Corporal";
                        break;
                    case 4:
                        position = "Sergeant";
                        break;
                    case 5:
                        position = "Lieutenant";
                        break;
                    case 6:
                        position = "Dep. Chief";
                        break;
                    case 7:
                        position = "Chief of Police";
                        break;
                }
            }
            else
            {
                switch (jobLevel)
                {
                    case 0:
                        position = "N/A";
                        break;
                    case 1:
                        position = "EMT";
                        break;
                    case 2:
                        position = "Paramedic";
                        break;
                    case 3:
                        position = "S && R";
                        break;
                    case 4:
                        position = "Air Responder";
                        break;
                    case 5:
                        position = "Coordinator";
                        break;
                }

            }
            return position;
        }
        List<Player>[] onlineP = new List<Player>[3];
        /// <summary>
        /// Connects to the server and pulls player information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwPlayerListRefresh_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                
                //OpenConnection();
                //_onlinePlayers = _server.GetPlayerList(_serverId);
                //List<string>[] tmp = _db.ExecuteReader($"SELECT * FROM players  WHERE last_Server = '{_serverId}' AND last_active >= NOW() - INTERVAL 7.5 MINUTE ORDER BY `name` ASC");
                DataTable tempPlayers = _db.ExecuteReaderDT($"SELECT * FROM players  WHERE last_active >= NOW() - INTERVAL 7.5 MINUTE ORDER BY `name` ASC");
                foreach(var playerList in _tempOnlinePlayers)
                {
                    playerList.Clear();
                }
                foreach (DataRow row in tempPlayers.Rows)
                {
                    int server = (int)row["last_server"];
                    Player p = CreatePlayer(row, (int)row["last_server"]);
                    _tempOnlinePlayers[server].Add(p);

                }
                _onlinePlayers = _tempOnlinePlayers[_serverId].OrderBy(p => p.Name).ToList();
                //CloseConnection();
                if (PlayerMap == null) return;
                PlayerMap.Players = _onlinePlayers;
                PlayerMap.CanReset = true;
                PlayerMap.pbMap.Invalidate();
            }
            catch (EndpointNotFoundException)
            {
                tsslStatus.Text = "Unable to connect to server";
            }
            catch (TimeoutException)
            {

            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Player CreatePlayer(DataRow row, int serverNum)
        {
            int uid = (int)row["uid"];
            string steamID = (string)row["playerid"];
            string name = (string)row["name"];
            string gangName = (int)row["gangId"] == -1 ? "N/A" : (string)row["gangName"];
            int gangRank = Convert.ToInt32(row["gangRank"]);
            DateTime lastActive = Convert.ToDateTime(row["last_active"].ToString());

            var aliases = "";
            aliases =  JToken.Parse(Helper.ToJson(row["aliases"].ToString())).Aggregate(aliases, (current, pAlias) => current + (pAlias + ";"));
            Player p = new Player(uid, steamID, name, aliases, gangName, gangRank, lastActive.ToUnixTime(), DateTime.UtcNow.ToUnixTime(), (string)row["coordinates"], (string)row["last_side"]);
            p.AddMoney((int)row["cash"], (int)row["bankacc"], 0, 0);
            return p;

        }

        /// <summary>
        /// Runs tasks after pulling player information from the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwPlayerListRefresh_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            _doingWork = false;
            _justRefreshed = true;
            if (!_sw.IsRunning)
                _sw.Start();
            lbPlayersAll.DisplayMember = "name";
            lbPlayersAll.DataSource = _onlinePlayers;
            BuildTargetList();
        }

        private void cmsWatchlist_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            removeFromWatchlistToolStripMenuItem.Enabled = _activeListbox == lbWatchlist;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Reset();
        }
        /// <summary>
        /// Displays map with listed player location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerMap = new Map { Players = _onlinePlayers };
            PlayerMap.Show();
        }
        /// <summary>
        /// Handles the selection of server to pull from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var b = (ToolStripMenuItem)sender;
            foreach (ToolStripMenuItem item in serverToolStripMenuItem.DropDownItems)
                item.Checked = item == b ? true : false;
            switch (b.Text)
            {
                case "Server #1":
                    _serverId = 1;
                    break;
                case "Server #2":
                    _serverId = 2;
                    break;
            }
            Reset();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }
        /// <summary>
        /// Zoom zoom zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZoom_Scroll(object sender, EventArgs e)
        {
            var percent = (double)tbZoom.Value / 100;
            var original = Properties.Resources.Altis3;
            var newSize = new Size((int)(original.Width * percent), (int)(original.Height * percent));
            var picture = new Bitmap(original, newSize);
            pbMap.Image = picture;
            original.Dispose();
            GC.Collect();
        }
        //TODO Remove this
        private void panelPictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (pbMap.Focused == false)
            {
                //pbMap.Focus();
            }
        }
        /// <summary>
        /// Catches the mousewheel event and zooms appropriately
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int _minmax = 10;
            double _zoomfactor = 1.25;
            //Zoom In
            if (e.Delta < 0)
            {
                if ((pbMap.Width < (_minmax * panelHouses.Width)) && (pbMap.Height < (_minmax * panelHouses.Height)))
                {
                    pbMap.Width = Convert.ToInt32(pbMap.Width * _zoomfactor);
                    pbMap.Height = Convert.ToInt32(pbMap.Height * _zoomfactor);
                    pbMap.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            //Zoom out
            else
            {
                if ((pbMap.Width > (panelHouses.Width / _minmax)) && (pbMap.Height > (panelHouses.Height / _minmax)))
                {
                    pbMap.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbMap.Width = Convert.ToInt32(pbMap.Width / _zoomfactor);
                    pbMap.Height = Convert.ToInt32(pbMap.Height / _zoomfactor);
                }
            }
        }
        /// <summary>
        /// Translate arma weapon names 
        /// </summary>
        /// <param name="a3Name"></param>
        /// <returns></returns>
        public string TranslateWeapons(string a3Name)
        {
            var translatedName = "";
            switch (a3Name)
            {
                default:
                    translatedName = a3Name;
                    if (!_debugListEqu.Contains(a3Name))
                    {
                        _debugListEqu.Add(a3Name);
                        rtbDebugEquipment.Text += $"{a3Name}{Environment.NewLine}";

                    }
                    break;
                case "launch_B_Titan_tna_F":
                case "launch_O_Titan_ghex_F":
                    translatedName = "Titan MPRL";
                    break;
                case "launch_B_Titan_short_tna_F":
                case "launch_O_Titan_short_ghex_F":
                    translatedName = "Titan MPRL Compact (Tropic)";
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
                case "arifle_CTAR_hex_F":
                case "arifle_CTAR_ghex_F":
                    translatedName = "CAR-95 5.8 mm";
                    break;

                case "arifle_CTARS_blk_F":
                case "arifle_CTARS_hex_F":
                case "arifle_CTARS_ghex_F":
                    translatedName = "CAR-95-1 5.8 mm";
                    break;

                case "LMG_Mk200_BI_F":
                case "LMG_Mk200_LP_BI_F":
                    translatedName = "Mk200 6.5 mm";
                    break;

                case "arifle_ARX_blk_F":
                case "arifle_ARX_ghex_F":
                case "arifle_ARX_hex_F":
                    //case "arifle_ARX_hex_ARCO_Pointer_Snds_F":
                    //case "arifle_ARX_ghex_ARCO_Pointer_Snds_F":
                    //case "arifle_ARX_hex_ACO_Pointer_Snds_F":
                    //case "arifle_ARX_ghex_ACO_Pointer_Snds_F":
                    //case "arifle_ARX_hex_DMS_Pointer_Snds_Bipod_F":
                    //case "arifle_ARX_ghex_DMS_Pointer_Snds_Bipod_F":
                    //case "arifle_ARX_Viper_F":
                    //case "arifle_ARX_Viper_hex_F":
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
