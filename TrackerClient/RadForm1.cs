using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using TrackerInterface;

namespace TrackerClient
{
    public partial class rfMain : Telerik.WinControls.UI.RadForm
    {
        private readonly Db _db = new Db();

        /*List*/
        List<Player> _onlinePlayers = new List<Player>();
        List<Player> _tmpOnlinePlayers = new List<Player>();
        List<string> _debugListEqu = new List<string>();

        readonly Color _defaultBgColor = Color.FromArgb(255, 37, 37, 38);
        bool _doingWork = false; //Prevents starting a player pull while one is still active
        Player _lastSelectedPayer = null;
        Player _selectedPlayer = null;
        int _serverId = 1; //Default server to pull from
        Stopwatch _sw = new Stopwatch();
        int RefreshTime = 15000;
        ListBox _activeListbox;


        internal Map PlayerMap; //Map object to pass to the Map form/user control
        internal bool CanReset = false;
        Bitmap map = Properties.Resources.Altis3;

        private RadListControl _activeListControl;

        readonly string[] _watchListLegeals = {
            "salt","saltr",
            "sand","glass",
            "rock", "cement",
            "copperore", "copperr",
            "silver","silverr",
            "platinum","platinumr",
            "ironore","ironr",
            "oilu", "oilp",
            "diamond", "diamondc",
            "excavationtools"};

        readonly string[] _watchListIllegals = {
            "cannabis","marijuana","hash",
            "cocaine",  "cocainep","crack","ccocaine",
            "heroinu","heroinp","pheroin",
            "frog", "frogp","acid",
            "mushroomu","mmushroom","mmushroom", "mmushroomp",
            "ephedra", "lithium", "phosphorus","crystalmeth","methu",
            "yeast", "sugar", "corn","moonshine",
            "goldbar", "moneybag" };

        readonly string[] _watchListRunVehicles = { "Van (Cargo)", "CH-67 Huron", "SDV", "HEMTT", "V-44 X Blackfish", "Truck", "CH-49 Mohawk", "Zamak", "Tempest", "Mi-290 Taru (Fuel)" };
        readonly string[] _watchListMiscVehicles = { "Y-32 Xi'an", "Qilin (Minigun)", "Ifrit", "Strider", "Offroad (AT)", "MB 4WD (LMG)", "Prowler (HMG)", "Hunter", "UH-80 Ghost Hawk" };

        public rfMain()
        {
            InitializeComponent();
            timerMain.Enabled = true;
            timerMain.Start();
            this.lbPlayersTargets.BackColor = lbPlayersAll.BackColor = rlvVirItems.BackColor = rlcVehicles.BackColor = rbtbAliases.BackColor = rtebVehicleInfo.BackColor = _defaultBgColor;
            this.lbPlayersTargets.ForeColor = lbPlayersAll.ForeColor = Color.White;
            pbMap.Image = Properties.Resources.Altis3;
            _activeListbox = lbPlayersAll;
            rtrbZoom.Value = 100;
        }
        /// <summary>
        /// Connects to the server and pulls player information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwPlayerTabRefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _tmpOnlinePlayers.Clear();
                DataTable tempPlayers = _db.ExecuteReaderDT($"SELECT * FROM players  WHERE last_active >= NOW() - INTERVAL 7.5 MINUTE AND last_server = '{_serverId}' ORDER BY `name` ASC");
                tempPlayers.DefaultView.Sort = "name asc";
                foreach (DataRow row in tempPlayers.Rows)
                {
                    int server = (int)row["last_server"];
                    Player p = CreatePlayer(row, (int)row["last_server"]);
                    _tmpOnlinePlayers.Add(p);
                }
                _onlinePlayers = _tmpOnlinePlayers.OrderBy(p => p.Name).ToList();
                if (PlayerMap == null) return;
                PlayerMap.Players = _onlinePlayers;
                PlayerMap.CanReset = true;
                PlayerMap.pbMap.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            switch (_sw.ElapsedMilliseconds)
            {
                default:
                    rmiServer1.Enabled = rmiServer2.Enabled = rbRefresh.Enabled = !_doingWork;
                    if (_sw.IsRunning)
                    {
                        rlRefresh.Text = $@"Refreshing player list in {(RefreshTime - _sw.ElapsedMilliseconds) / 1000} seconds.";
                        if (_sw.ElapsedMilliseconds >= RefreshTime)
                        {
                            Reset();
                        }
                    }
                    break;
            }

        }
        private void Reset()
        {
            try
            {
                if (bwPlayerTabRefresh.IsBusy) return;
                _doingWork = true;
                rlRefresh.Text = @"Refreshing player list now.";
                if (_activeListbox.SelectedValue != null)
                {
                    _lastSelectedPayer = (Player)_activeListbox.SelectedItem;
                }
                bwPlayerTabRefresh.RunWorkerAsync();
                _sw.Reset();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void bwPlayerTabRefresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _doingWork = false;
            if (!_sw.IsRunning)
                _sw.Start();
            lbPlayersAll.DataSource = _onlinePlayers;
            lbPlayersAll.DisplayMember = "name";
            BuildTargetList();
            if (_activeListbox == null)
                _activeListbox = lbPlayersAll;
            for (int i = 0; i < _activeListbox.Items.Count; i++)
            {
                Player p = (Player)_activeListbox.Items[i];
                if (_lastSelectedPayer == null || p.Name != _lastSelectedPayer.Name) continue;
                _activeListbox.SelectedIndex = i;
            }
            _activeListbox.Refresh();
        }

        private void rfMain_Load(object sender, EventArgs e)
        {
            Reset();
        }
        /// <summary>
        /// Adds players with targeted items to a special list
        /// </summary>
        private void BuildTargetList()
        {
            var targetPlayers = new List<Player>();
            foreach (var p in _onlinePlayers)
            {
                p.TargetLevel = -1;
                if (p.Vehicles != null)
                {
                    foreach (var vehicle in from vehicle in p.Vehicles from watchVehicle in _watchListRunVehicles where vehicle.Name.Contains(watchVehicle) && vehicle.Active >= 1 select vehicle)
                    {
                        if (!targetPlayers.Contains(p))
                            targetPlayers.Add(p);
                        p.TargetLevel = 0;
                        vehicle.TargetLevel = 0;
                    }
                    foreach (var vehicle in from vehicle in p.Vehicles from watchVehicle in _watchListMiscVehicles where vehicle.Name.Contains(watchVehicle) && vehicle.Active >= 1 select vehicle)
                    {
                        if (!targetPlayers.Contains(p))
                            targetPlayers.Add(p);
                        p.TargetLevel = 3;
                        vehicle.TargetLevel = 3;
                    }
                }
                if (p.Virtuals != null)
                    foreach (var item in p.Virtuals)
                    {
                        foreach (var watchItem in _watchListLegeals.Where(watchItem => watchItem == item.Name))
                        {
                            if (!targetPlayers.Contains(p))
                                targetPlayers.Add(p);
                            p.TargetLevel = 1;
                        }
                        foreach (var watchItem in _watchListIllegals.Where(watchItem => watchItem == item.Name))
                        {
                            if (!targetPlayers.Contains(p))
                                targetPlayers.Add(p);
                            p.TargetLevel = 2;
                        }
                    }
                if (p.Equipment.Count >= 8)
                    if (p.Equipment[7].Contains("Titan"))
                    {
                        p.TargetLevel = 4;
                        targetPlayers.Add(p);
                    }
            }
            targetPlayers = targetPlayers.OrderBy(p => p.Name).ToList();
            lbPlayersTargets.DataSource = targetPlayers;
            lbPlayersTargets.DisplayMember = "name";
        }
        /// <summary>
        /// Displays information from the Player object on the form
        /// </summary>
        /// <param name="p"></param>
        private void DisplayPlayer(Player p)
        {
            var sortedList = new List<Vehicle>();
            var houses = new List<House>();
            var vehicles = new List<Vehicle>();
            rbtbAliases.Clear();
            rlvVirItems.Items.Clear();

            Text = p.Name;
            lblName.Text = $@"Name: {p.Name}";
            lblCash.Text = $@"Cash: {p.Cash:C}";
            lblBounty.Text = $@"Bounty: {p.BountyWanted:C}";
            lblKDR.Text = $@"K/D/R: {p.Kills}/{p.Deaths}/{Convert.ToDecimal(Convert.ToDecimal(p.Kills) / Convert.ToDecimal(p.Deaths)):0.##}";
            lblCopRank.Text = $@"APD Rank: {p.CopRank}";
            lblGang.Text = $@"Gang: {(p.GangName == "-1" ? "N/A" : p.GangName)}";
            lblBank.Text = $@"Bank: {p.Bank:C}";
            lblMedicRank.Text = $@"R&&R Rank: {p.MedicRank}";
            lblVest.Text = $@"Vest: {(p.Equipment.Count == 0 ? "None" : p.Equipment[1])}";
            lblHelmet.Text = $@"Helmet: {(p.Equipment.Count == 0 ? "None" : p.Equipment[4])}";
            lblGun.Text = $@"Primary: {(p.Equipment.Count == 0 ? "None" : (p.Equipment[5]))}";
            lblSecondary.Text = $@"Secondary: {(p.Equipment.Count == 0 ? "None" : (p.Equipment[6]))}";
            lblLauncher.Text = $@"Launcher: {(p.Equipment.Count == 0 ? "None" : (p.Equipment[7]))}";
            lblLocation.Text = p.Location.Length > 1 ? $"Last Seen @ X:{p.Location[0]} Y:{p.Location[1]}" : "Last Seen @ Unknown";
            lblSteamID.Text = $@"Steam ID: {p.SteamId}";
            lblBM.Text = $@"BattleMetrics ID: {p.BMId}";
            foreach (var alias in p.Aliases)
            {
                rbtbAliases.Text += $@"{alias}{Environment.NewLine}";
            }
            if (p.Virtuals != null)
                foreach (var v in p.Virtuals)
                {
                    ListViewDataItem item = new ListViewDataItem();
                    rlvVirItems.Items.Add(item);
                    item[0] = v.Name;
                    item[1] = v.Amount;
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
            rlcVehicles.DataSource = p.Vehicles.OrderByDescending(v => v.Active).ToList();
            rlcVehicles.DisplayMember = "name";
            if (p.Location.Length > 1 && p.Faction == "civ")
                pbMap_CenterPlayer(p.Location);
        }

        private Player CreatePlayer(DataRow row, int serverNum)
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

            string aliases = "";
            aliases = row["aliases"].ToString();
            Player p = new Player(uid, steamID, name, aliases, gangName, gangRank, lastActive.ToUnixTime(), DateTime.UtcNow.ToUnixTime(), (string)row["coordinates"], (string)row["last_side"], row["bm_id"].ToString());
            DataTable player_vehicles = _db.ExecuteReaderDT($"SELECT * FROM vehicles WHERE `pid` = '{p.SteamId}' AND `side` = '{p.Faction}' AND `active` = '{serverNum}' AND `alive` = '1' ORDER BY  active DESC, type");
            p.AddMoney((int)row["cash"], (int)row["bankacc"], 0, bounty);
            p.AddStats(coplvl, medlvl, admlvl, donlvl, kills, deaths, revives, arrests);
            string gear = "";
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
            p.AddVehicles(player_vehicles);
            foreach (string item in p.Equipment)
            {
                if (item.Contains("_") && !_debugListEqu.Contains(item))
                {
                    _debugListEqu.Add(item);
                    //SetText($"{item}{Environment.NewLine}");
                }
            }
            return p;
        }

        private void rsbServer_Click(object sender, EventArgs e)
        {
            switch (((RadMenuItem)sender).Text)
            {
                case "Server 1":
                    _serverId = 1;
                    break;
                case "Server 2":
                    _serverId = 2;
                    break;
            }
            Reset();
        }


        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var lb = (ListBox)sender;
            var g = e.Graphics;
            if (e.Index <= -1 || e.Index >= lb.Items.Count) return;
            Player p = (Player)lb.Items[e.Index];
            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var text = p.AdminLevel == 0 ? p.Name : p.Name.Insert(p.Name.Length, $" [{p.Faction.ToUpper()}] ({p.AdminLevel})");
            Color textColor = Color.White;
            Color backColor = new Color();
            if (p.AdminLevel > 0)
            {
                backColor = Color.LightSeaGreen;
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
                                backColor = lRank;
                                break;
                            case 2:
                                text += " [PO]";
                                backColor = lRank;
                                break;
                            case 3:
                                text += " [Corp]";
                                backColor = hRank;
                                break;
                            case 4:
                                text += " [SGT]";
                                backColor = hRank;
                                break;
                            case 5:
                                text += " [LT]";
                                backColor = hRank;
                                break;
                            case 6:
                                text += " [DChief]";
                                backColor = hRank;
                                break;
                            case 7:
                                text += " [Chief]";
                                backColor = hRank;
                                break;
                            default:
                                text += " [CopLev " + p.CopLevel + "]";
                                backColor = hRank;
                                break;
                        }
                        break;
                    case "med":
                        backColor = Color.DarkGreen;
                        break;
                    default:
                        switch (p.TargetLevel)
                        {
                            case 0:
                                backColor = Color.Yellow;
                                textColor = Color.Black;
                                break;
                            case 1:
                                backColor = Color.DeepPink;
                                break;
                            case 2:
                                backColor = Color.Red;
                                break;
                            case 3:
                                backColor = Color.IndianRed;
                                break;
                            case 4:
                                backColor = Color.White;
                                textColor = Color.Black;
                                break;
                            default:
                                textColor = Color.White;
                                break;
                        }
                        break;
                }
            }
            if (selected)
            {
                backColor = SystemColors.MenuHighlight;
            }
            else if (!selected && backColor.IsEmpty)
            {
                backColor = _defaultBgColor;
            }

            if (!backColor.IsEmpty)
                g.FillRectangle(new SolidBrush(backColor), e.Bounds);
            g.DrawString(text, e.Font, new SolidBrush(textColor), new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (ListBox)sender;
                if (lb.SelectedIndices.Count != 1) return;
                var p = (Player)lb.SelectedValue;
                _selectedPlayer = p;
                DisplayPlayer(p);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void ListBox_MouseEnter(object sender, EventArgs e)
        {
            _activeListbox = (ListBox)sender;
        }

        private void rbRefresh_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void rtbRefresh_ValueChanged(object sender, EventArgs e)
        {
            var value = ((RadTrackBarElement)sender).Value;
            rlRefresh.Text = $@"Refreshing every {value}s";
            RefreshTime = Convert.ToInt32(value) * 1000;
        }

        private void lblBM_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Player p = (Player)_activeListbox.SelectedValue;
            string url = $"https://www.battlemetrics.com/players/{p.BMId}";
            var si = new ProcessStartInfo(url);
            Process.Start(si);
            lblBM.LinkVisited = true;
        }

        private void lblSteamID_Click(object sender, EventArgs e)
        {
            Player p = (Player)_activeListbox.SelectedValue;
            string url = "";
            if (Form.ModifierKeys == Keys.Control)
            {
                url = $"https://www.battlemetrics.com/players/{p.SteamId}";
            }
            else
            {
                url = $"https://olympus-entertainment.com/olympus-stats/index.php?pid={p.SteamId}";
            }
            var si = new ProcessStartInfo(url);
            Process.Start(si);
            lblSteamID.LinkVisited = true;
        }

        private void radListControl1_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
        {
            Player p = null;
            var selected = (args.VisualItem.Selected);
            foreach (Player player in _onlinePlayers)
            {
                if (args.VisualItem.Data.Value.ToString() == player.SteamId)
                    p = player;
            }
            if (p == null) return;
            var text = p.AdminLevel == 0 ? p.Name : p.Name.Insert(p.Name.Length, $" [{p.Faction.ToUpper()}] ({p.AdminLevel})");
            Color textColor = Color.White;
            Color backColor = new Color();
            if (p.AdminLevel > 0)
            {
                backColor = Color.LightSeaGreen;
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
                                backColor = lRank;
                                break;
                            case 2:
                                text += " [PO]";
                                backColor = lRank;
                                break;
                            case 3:
                                text += " [Corp]";
                                backColor = hRank;
                                break;
                            case 4:
                                text += " [SGT]";
                                backColor = hRank;
                                break;
                            case 5:
                                text += " [LT]";
                                backColor = hRank;
                                break;
                            case 6:
                                text += " [DChief]";
                                backColor = hRank;
                                break;
                            case 7:
                                text += " [Chief]";
                                backColor = hRank;
                                break;
                            default:
                                text += " [CopLev " + p.CopLevel + "]";
                                backColor = hRank;
                                break;
                        }
                        break;
                    case "med":
                        backColor = Color.DarkGreen;
                        break;
                    default:
                        switch (p.TargetLevel)
                        {
                            case 0:
                                backColor = Color.Yellow;
                                textColor = Color.Black;
                                break;
                            case 1:
                                backColor = Color.DeepPink;
                                break;
                            case 2:
                                backColor = Color.Red;
                                break;
                            case 3:
                                backColor = Color.IndianRed;
                                break;
                            case 4:
                                backColor = Color.Black;
                                break;
                            default:
                                textColor = Color.White;
                                break;
                        }
                        break;
                }
            }
            if (selected)
            {
                backColor = SystemColors.MenuHighlight;
            }
            else if (!selected && backColor.IsEmpty)
            {
                backColor = _defaultBgColor;
            }

            if (!backColor.IsEmpty)
                args.VisualItem.BackColor = backColor;
            args.VisualItem.ForeColor = textColor;
        }



        private void pbMap_Paint(object sender, PaintEventArgs e)
        {
            Font _font = new Font("Tahoma", 7F, FontStyle.Regular); //Default font
            Point point = rpMap.AutoScrollPosition;
            if (CanReset == true)
            {
                e.Graphics.Clear(Color.Transparent);
                pbMap.Image = map;
            }

            Helper.PaintPoi(e, pbMap);
            try
            {
                if (_onlinePlayers == null) return;
                foreach (Player p in _onlinePlayers)
                {
                    if (p.Location.Length < 2) continue;
                    float[] newCords = Helper.performCordScale(p.Location, pbMap);
                    Color mapColor = new Color();
                    switch (p.TargetLevel)
                    {
                        case -1:
                            mapColor = Color.White;
                            break;
                        case 0:
                            mapColor = Color.Yellow;
                            break;
                        case 1:
                            mapColor = Color.Green;
                            break;
                        case 2:
                            mapColor = Color.Red;
                            break;
                        case 3:
                            mapColor = Color.IndianRed;
                            break;
                        case 4:
                            mapColor = Color.Black;
                            break;
                    }

                    e.Graphics.FillRectangle(new SolidBrush(mapColor),
                        new RectangleF(new PointF(newCords[0], newCords[1]), new Size(4, 4)));
                    e.Graphics.DrawString(p.Name, _font, new SolidBrush(Color.White),
                        new PointF(newCords[0] + 2, newCords[1]));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CanReset = false;
            }
        }
        private int _xPos;
        private int _yPos;
        private bool _dragging;
        private double currentZoom = 100;
        /// <summary>
        /// Centers map on selected player location
        /// </summary>
        /// <param name="location"></param>
        private void pbMap_CenterPlayer(string[] location)
        {
            float[] newCords = Helper.performCordScale(location, pbMap);
            int x = Convert.ToInt32(newCords[0]);
            int y = Convert.ToInt32(newCords[1]);
            Point panelcenter = new Point((rpMap.Width / 2), (rpMap.Height / 2)); // find the centerpoint of the panel
            Point offsetinpicturebox = new Point((pbMap.Location.X + x), (pbMap.Location.Y + y)); // find the offset of the mouse click
            Point offsetfromcenter = new Point((panelcenter.X - offsetinpicturebox.X), (panelcenter.Y - offsetinpicturebox.Y)); // find the difference between the mouse click and the center
            rpMap.AutoScrollPosition = new Point((Math.Abs(rpMap.AutoScrollPosition.X) + (-1 * offsetfromcenter.X)), (Math.Abs(rpMap.AutoScrollPosition.Y) + (-1 * offsetfromcenter.Y)));
        }

        private void pbMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragging = true;
            _xPos = e.X;
            _yPos = e.Y;
        }

        private void pbMap_MouseUp(object sender, MouseEventArgs e)
        {
            var c = sender as PictureBox;
            if (null == c) return;
            _dragging = false;
        }

        private void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            var c = sender as PictureBox;
            if (!_dragging || null == c) return;
            c.Top = e.Y + c.Top - _yPos;
            c.Left = e.X + c.Left - _xPos;
            GC.Collect();
        }

        private void rtrbZoom_ValueChanged(object sender, EventArgs e)
        {
            var original = Properties.Resources.Altis3;
            var percent = (double)rtrbZoom.Value / 100;
            var newSize = new Size((int)(original.Width * percent), (int)(original.Height * percent));
            var picture = new Bitmap(original, newSize);
            pbMap.Image = picture;
            map = picture;
            original.Dispose();
            GC.Collect();
            if(_selectedPlayer != null)
            pbMap_CenterPlayer(_selectedPlayer.Location);
            //Point panelCenter = new Point((rpMap.Width / 2), (rpMap.Height / 2)); // find the centerpoint of the panel
            //Point pbOffset = new Point((pbMap.Location.X + e.X), (pbMap.Location.Y + e.Y)); // find the offset of the mouse click
            //Point centerOffset = new Point((panelCenter.X - pbOffset.X), (panelCenter.Y - pbOffset.Y)); // find the difference between the mouse click and the center
        }
    }
}
