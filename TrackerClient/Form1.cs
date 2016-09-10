using System;
using System.ServiceModel;
using System.Windows.Forms;
using TrackerInterface;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using RestSharp;

namespace TrackerClient
{

    public partial class frmMain : Form
    {
        ChannelFactory<IWCFTrackerService> channelFactory;
        IWCFTrackerService server;
        List<Player> onlinePlayers = new List<Player>();
        List<Player> slackPostList = new List<Player>();
        List<string> debugListVeh = new List<string>();
        List<string> debugListEqu = new List<string>();
        bool doingWork = false;
        string serverID = "arma_1";
        internal Map playerMap;
        SlackClient sc = new SlackClient("https://hooks.slack.com/services/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");
        object locker = new object();

        Stopwatch sw = new Stopwatch();
        public int refreshTime = 180000;

        string[] watchListLegeals = {
               "salt","saltr",
                "sand","glass",
                "rock", "cement",
                "copperore", "copperr",
                "silver","silverr",
                "platinum","platinumr",
                "ironore","ironr",
                "oilu", "oilp",
                "diamond", "diamondc"};
        string[] watchListIllegals = {
                "cannabis","marijuana",
                "cocaine",  "cocainep",
                "heroinu","heroinp",
                "frog", "frogp",
                "mushroom","mmushroom",
                "ephedra", "lithium", "phosphorus","crystalmeth",
                "yeast", "sugar", "corn","moonshine",
                "goldbar" };
        string[] watchListVehicles = { "Hellcat", "HEMTT Box", "HEMTT Fuel", "HEMTT Transport", "Hummingbird", "Huron", "Ifrit", "Offroad (Armed)", "Orca", "M900", "Mohawk", "SDV",
                "Taru (Bench)", "Taru (Fuel)", "Taru (Transport)", "Tempest (Device)", "Tempest Fuel", "Tempest Transport", "Tempest Transport (Covered)", "Truck", "Truck Box", "Truck Fuel",
                "Zamak Fuel", "Zamak Transport", "Zamak Transport (Covered)"};


        ListViewItemComparer _lvwItemComparer = new ListViewItemComparer();
        private ListBox activeListbox;

        public frmMain()
        {
            InitializeComponent();
            HtmlNode.ElementsFlags.Remove("form");
            lvVehicleInfo.ListViewItemSorter = _lvwItemComparer;
            timerPLRefresh.Enabled = true;
            timerPLRefresh.Start();
            pbMap.Image = Properties.Resources.Altis3;
        }

        private void closeConnection()
        {
            if (channelFactory.State < CommunicationState.Closing)
            {
                channelFactory.Close();
            }
        }

        private void openConnection()
        {
            channelFactory = new ChannelFactory<IWCFTrackerService>("TrackerClientEndpoint");
            server = channelFactory.CreateChannel();
        }

        private void timerPLRefresh_Tick(object sender, EventArgs e)
        {
            switch (sw.ElapsedMilliseconds)
            {
                default:
                    //if (doingWork == false && refreshToolStripMenuItem.Enabled)
                    if (sw.IsRunning)
                    {
                        tsslStatus.Text = string.Format("Refreshing player list in {0} seconds.", (refreshTime - sw.ElapsedMilliseconds) / 1000);
                        if (sw.ElapsedMilliseconds >= refreshTime)
                            Reset();
                    }
                    break;
            }
        }

        private void Reset()
        {
            try
            {
                doingWork = true;
                tsslStatus.Text = string.Format("Refreshing player list now.");
                bwPlayerListRefresh.RunWorkerAsync();
                sw.Stop();
                sw.Reset();
            }
            catch
            {

            }
        }

        private void BuildTargetList()
        {
            rtbDebugVehicle.Clear();
            List<Player> targetPlayers = new List<Player>();
            foreach (Player p in onlinePlayers)
            {
                string wItem = "";
                string wVehicle = "";
                p.TargetLevel = -1;
                if (p.civAir != null)
                    foreach (Vehicle vehicle in p.civAir)
                    {
                        foreach (string watchItem in watchListVehicles)
                        {
                            if (watchItem == vehicle.name && vehicle.active == 1)
                            {
                                wVehicle += vehicle.name + "\r\n";
                                if (!targetPlayers.Contains(p))
                                    targetPlayers.Add(p);
                                p.TargetLevel = 0;
                            }
                        }
                    }
                if (p.civCar != null)
                    foreach (Vehicle vehicle in p.civCar)
                    {
                        foreach (string watchItem in watchListVehicles)
                        {
                            if (watchItem == vehicle.name && vehicle.active == 1)
                            {
                                wVehicle += vehicle.name + "\r\n";
                                if (!targetPlayers.Contains(p))
                                    targetPlayers.Add(p);
                                p.TargetLevel = 0;
                            }
                        }
                    }
                if (p.civShip != null)
                    foreach (Vehicle vehicle in p.civShip)
                    {
                        foreach (string watchItem in watchListVehicles)
                        {
                            if (watchItem == vehicle.name && vehicle.active == 1)
                            {
                                wVehicle += vehicle.name + "\r\n";
                                if (!targetPlayers.Contains(p))
                                    targetPlayers.Add(p);
                                p.TargetLevel = 0;
                            }
                        }
                    }
                if (p.Virtuals != null)
                    foreach (VirtualItem item in p.Virtuals)
                    {
                        foreach (string watchItem in watchListLegeals)
                        {
                            if (watchItem == item.name)
                            {
                                wItem += item.name + "\r\n";
                                if (!targetPlayers.Contains(p))
                                    targetPlayers.Add(p);
                                p.TargetLevel = 1;
                            }
                        }
                        foreach (string watchItem in watchListIllegals)
                        {
                            if (watchItem == item.name)
                            {
                                wItem += item.name + "\r\n";
                                if (!targetPlayers.Contains(p))
                                    targetPlayers.Add(p);
                                p.TargetLevel = 2;
                            }
                        }
                    }
                if (wItem.Length > 0 || wVehicle.Length > 0)
                {
                    if (!slackPostList.Contains(p))
                    {
                        slackPostList.Add(p);
                        new Thread(() =>
                        {
                            Fields[] temp = {
                                new Fields() { Title = "Item", Value = wItem },
                                new Fields() { Title = "Vehicle", Value = wVehicle }
                            };
                            Attachment attachment = new Attachment()
                            {
                                title = p.name,
                                text = "Last Updated: " + p.lastUpdated.ToString(),
                                fields = temp
                            };
                            //sc.PostMessage(attachment);
                            Thread.Sleep(3000);
                        }).Start();
                    }
                }
            }
            targetPlayers = targetPlayers.OrderBy(p => p.name).ToList();
            lbPlayersTargets.DisplayMember = "name";
            lbPlayersTargets.DataSource = targetPlayers;
        }

        private void ListBox_MouseEnter(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            activeListbox = lb;
        }
        private void lbHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            House h = (House)lb.SelectedValue;
            DisplayHouse(h);
        }
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (tcMain.SelectedTab != tpPlayerInfo)
            {
                tcMain.SelectTab(1);
            }
            if (lb.SelectedIndices.Count == 1)
            {
                Player p = (Player)lb.SelectedValue;
                DisplayPlayer(p);
            }
        }
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            ListBox lb = (ListBox)sender;
            if (e.Index > -1 && e.Index < lb.Items.Count)
            {
                Player p = (Player)lb.Items[e.Index];
                bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
                string text = p.adminLevel == 0 ? p.name : p.name.Insert(p.name.Length, " [ADMIN]");
                switch (p.TargetLevel)
                {
                    case 0:
                        g.FillRectangle(new SolidBrush(Color.LightSlateGray), e.Bounds);
                        break;
                    case 1:
                        g.FillRectangle(new SolidBrush(Color.LawnGreen), e.Bounds);
                        break;
                    case 2:
                        g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
                        break;
                    default:
                        g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                        break;
                }
                if (selected)
                {
                    Color Highlight = SystemColors.MenuHighlight;
                    g.FillRectangle(new SolidBrush(Highlight), e.Bounds);
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                    g.DrawString(text, e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Transparent), e.Bounds);
                    g.DrawRectangle(new Pen(Color.Transparent), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
                    g.DrawString(text, e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
                }
                e.DrawFocusRectangle();
            }
        }

        private void DisplayHouse(House h)
        {
            pbMap.Invalidate();
            lvHVirtuals.Items.Clear();
            if (h.Virtual != null)
                foreach (VirtualItem v in h.Virtual)
                {
                    ListViewItem lviV = new ListViewItem(v.name);
                    lviV.SubItems.Add(v.amount.ToString());
                    lvHVirtuals.Items.Add(lviV);
                }
            if (playerMap != null)
            {
                if (h.Location.Length > 1)
                    pbHouses_CenterPlayer(h.Location);

            }
        }
        internal void pbHouses_CenterPlayer(string[] location)
        {
            float[] newCords = Helper.performCordScale(location, pbMap);
            int X = Convert.ToInt32(newCords[0]);
            int Y = Convert.ToInt32(newCords[1]);
            Point panelcenter = new Point((this.panelPictureBox.Width / 2), (this.panelPictureBox.Height / 2)); // find the centerpoint of the panel
            Point offsetinpicturebox = new Point((this.pbMap.Location.X + X), (this.pbMap.Location.Y + Y)); // find the offset of the mouse click
            Point offsetfromcenter = new Point((panelcenter.X - offsetinpicturebox.X), (panelcenter.Y - offsetinpicturebox.Y)); // find the difference between the mouse click and the center
            this.panelPictureBox.AutoScrollPosition = new Point((Math.Abs(this.panelPictureBox.AutoScrollPosition.X) + (-1 * offsetfromcenter.X)), (Math.Abs(this.panelPictureBox.AutoScrollPosition.Y) + (-1 * offsetfromcenter.Y)));
        }

        private void pbHouses_Paint(object sender, PaintEventArgs e)
        {
            if (lbHouses.SelectedValue == null) return;
            House h = (House)lbHouses.SelectedValue;
            float x = float.Parse(h.Location[0]);
            float y = float.Parse(h.Location[1]);
            float[] newCords = Helper.performCordScale(x, y, pbMap.Height, pbMap.Width);
            e.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(4, 4)));
        }
        private void DisplayPlayer(Player p)
        {
            List<Vehicle> sortedList = new List<Vehicle>();
            List<House> houses = new List<House>();
            tbAliases.Clear();
            lvVehicleInfo.Items.Clear();
            lvVirtualItems.Items.Clear();

            this.Text = string.Format("{0} | {1}", p.name, p.steamID);
            lblName.Text = string.Format("Name: {0}", p.name);
            lblCash.Text = string.Format("Cash: {0:C}", p.cash);
            lblBounty.Text = string.Format("Bounty: {0:C}", p.bountyWanted);
            lblKDR.Text = string.Format("K/D/R: {0:0,0}/{1:0,0}/{2:0.##}", p.kills, p.deaths, Convert.ToDecimal(Convert.ToDecimal(p.kills) / Convert.ToDecimal(p.deaths)));
            lblCopRank.Text = string.Format("APD Rank: {0}", p.copLevel);
            lblCopTime.Text = string.Format("APD Time: {0:0,0}", (p.timeApd.ToString() == "-1") ? "N/A" : p.timeApd.ToString());
            lblGang.Text = string.Format("Gang: {0}", p.gangName == "-1" ? "N/A" : p.gangName);
            lblBank.Text = string.Format("Bank: {0:C}", p.bank);
            lblVigiBounty.Text = string.Format("Bounty Collected: {0:C}", p.bountyCollected == -1 ? 0 : p.bountyCollected);
            lblCivTime.Text = string.Format("Civ Time: {0:0,0}", p.timeCiv);
            lblMedicRank.Text = string.Format("R&R Rank: {0}", p.medicLevel);
            lblMedicTime.Text = string.Format("R&R Time: {0:0,0}", p.timeMed.ToString() == "-1" ? "N/A" : p.timeMed.ToString());
            lblVest.Text = string.Format("Vest: {0}", p.Equipment.Count == 0 ? "None" : p.Equipment[1]);
            lblHelmet.Text = string.Format("Helmet: {0}", p.Equipment.Count == 0 ? "None" : p.Equipment[4]);
            lblGun.Text = string.Format("Gun: {0}", p.Equipment.Count == 0 ? "None" : p.Equipment[5]);
            lblUpdated.Text = string.Format("Last Updated (UTC): {0}", p.lastUpdated);
            if (p.location.Length > 1)
                lblLocation.Text = string.Format("Last Seen @ X:{0} Y:{1}", p.location[0], p.location[1]);
            else
                lblLocation.Text = string.Format("Last Seen @ Unknown");
            foreach (string equip in p.Equipment)
            {
                if (!debugListEqu.Contains(equip) && equip.Length > 0)
                {
                    debugListVeh.Add(equip);
                    rtbDebugEquipment.Text += string.Format("{0}{1}", equip, Environment.NewLine);
                }
            }
            foreach (string alias in p.aliases)
            {
                tbAliases.Text += string.Format("{0}{1}", alias, Environment.NewLine);
            }
            if (p.Virtuals != null)
                foreach (VirtualItem v in p.Virtuals)
                {
                    ListViewItem lviV = new ListViewItem(v.name);
                    lviV.SubItems.Add(v.amount.ToString());
                    lvVirtualItems.Items.Add(lviV);
                }
            if (p.civAir != null)
                foreach (Vehicle v in p.civAir)
                {
                    sortedList.Add(v);
                }
            if (p.civCar != null)
                foreach (Vehicle v in p.civCar)
                {
                    sortedList.Add(v);
                }
            if (p.civShip != null)
                foreach (Vehicle v in p.civShip)
                {
                    sortedList.Add(v);
                }
            sortedList = sortedList.OrderByDescending(v => v.active).ToList();
            foreach (Vehicle v in sortedList)
            {
                ListViewItem lviV = new ListViewItem(v.name);
                lviV.SubItems.Add(v.active.ToString());
                lviV.SubItems.Add(v.turboLevel.ToString());
                lviV.SubItems.Add(v.secLevel.ToString());
                lviV.SubItems.Add(v.storageLevel.ToString());
                lviV.SubItems.Add(v.insuranceLevel.ToString());
                lvVehicleInfo.Items.Add(lviV);
            }
            if (p.houses != null)
                foreach (House h in p.houses)
                {
                    houses.Add(h);
                }
            houses = houses.OrderBy(h => h.ID).ToList();
            lbHouses.DisplayMember = "id";
            lbHouses.DataSource = houses;
            if (playerMap != null)
            {
                if (p.location.Length > 1)
                    playerMap.pbMap_CenterPlayer(p.location);

            }
        }

        private void lvVehicleInfo_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _lvwItemComparer.SortColumn)
            {
                if (_lvwItemComparer.Order == SortOrder.Ascending)
                {
                    _lvwItemComparer.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwItemComparer.Order = SortOrder.Ascending;
                }
            }
            else
            {
                _lvwItemComparer.SortColumn = e.Column;
                _lvwItemComparer.Order = SortOrder.Ascending;
            }
            lvVehicleInfo.Sort();
        }

        private void bwPlayerListRefresh_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            openConnection();
            server.PullPlayers(serverID);
            onlinePlayers = server.GetPlayerList();
            onlinePlayers = onlinePlayers.OrderBy(p => p.name).ToList();
            closeConnection();
            if (playerMap != null)
            {
                playerMap.players = onlinePlayers;
                playerMap.canReset = true;
                playerMap.pbMap.Invalidate();
            }
        }

        private void bwPlayerListRefresh_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            doingWork = false;
            if (!sw.IsRunning)
                sw.Start();
            lbPlayersAll.DisplayMember = "name";
            lbPlayersAll.DataSource = onlinePlayers;
            BuildTargetList();
        }

        private void cmsWatchlist_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (activeListbox != lbWatchlist)
                removeFromWatchlistToolStripMenuItem.Enabled = false;
            else
                removeFromWatchlistToolStripMenuItem.Enabled = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Reset();
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playerMap = new Map();
            playerMap.players = onlinePlayers;
            playerMap.Show();
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem b = (ToolStripMenuItem)sender;
            //foreach
            b.Checked = true;
            switch (b.Text)
            {
                case "Server #1":
                    serverID = "arma_1";
                    break;
                case "Server #2":
                    serverID = "arma_2_blame_poseidon";
                    break;
                case "Server #3":
                    serverID = "arma_3";
                    break;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }

}
