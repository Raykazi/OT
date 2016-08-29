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

namespace TrackerClient
{

    public partial class frmMain : Form
    {
        ChannelFactory<IWCFTrackerService> channelFactory;
        IWCFTrackerService proxy;
        List<Player> onlinePlayers = new List<Player>();
        List<Player> slackPostList = new List<Player>();
        List<string> debugListVeh = new List<string>();
        List<string> debugListEqu = new List<string>();
        Map playerMap;
        SlackClient sc = new SlackClient("https://hooks.slack.com/services/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");
        string steamName = null;
        public HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        object locker = new object();
        bool bgwDone = false;
        Stopwatch sw = new Stopwatch();

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
                "ephedra", "lithium", "phosphorus","meth",
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("This release is for an impatient autistic fuck named Zazhi...");
            wkbMain.Focus();
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
            proxy = channelFactory.CreateChannel();
        }

        private void btnGetPlayers_Click(object sender, EventArgs e)
        {
            Reset();
            btnGetPlayers.Enabled = false;
            string currentURL = wkbMain.Url.ToString();
            string targetPage;
            tsslStatus.Text = "Getting players.";
            if (currentURL.Contains("/home"))
            {
                string[] segments = currentURL.Split('/');
                steamName = segments[4];
                targetPage = string.Format("https://steamcommunity.com/id/{0}/friends/players/", steamName);
                wkbMain.Url = new Uri(targetPage);
            }
            else if (currentURL.Contains("/friends/players"))
            {
                wkbMain.Reload();
            }
            else
            {
                if (steamName == null)
                {
                    targetPage = string.Format("https://steamcommunity.com/id/{0}/friends/players/", steamName);
                    wkbMain.Url = new Uri(targetPage);
                }

            }
        }
        private void wkbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            tbURL.Text = wkbMain.Url.ToString();
        }

        private void wkbMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            List<string> steamID = new List<string>();
            if (wkbMain.Url.ToString().Contains("/friends/players/"))
            {
                steamID.Clear();
                bgwDone = false;
                string src = wkbMain.DocumentText.ToString();
                doc.LoadHtml(src);
                if (src.Contains("You are not currently in a Steamworks game with other Steam players."))
                {
                    btnGetPlayers.Enabled = true;
                    tsslStatus.Text = "Not connected to an Olympus Server.";
                    Reset();
                    return;
                }
                HtmlNode node = doc.GetElementbyId("friendListForm");
                if (node != null)
                {
                    IEnumerable<HtmlNode> allInputs = node.Descendants("input");
                    foreach (HtmlNode input in allInputs)
                    {
                        if (input.Attributes.Contains("data-steamid"))
                        {
                            steamID.Add(input.Attributes["data-steamid"].Value);
                        }
                    }
                }
                bwPlayerListRefresh.RunWorkerAsync(steamID);

            }
            else if (!wkbMain.Url.ToString().Contains("/friends/players"))
            {
                //tsslStatus.Text = "Not connected to a server.";
            }
        }

        private void timerPLRefresh_Tick(object sender, EventArgs e)
        {
            switch (sw.ElapsedMilliseconds)
            {
                default:
                    if (sw.IsRunning)
                    {
                        tspbMain.PerformStep();
                        tsslStatus.Text = string.Format("Refreshing player list in {0} seconds.", (tspbMain.Maximum - (sw.ElapsedMilliseconds / 1000)));
                    }
                    if (bgwDone && !btnGetPlayers.Enabled)
                        btnGetPlayers.Enabled = true;
                    break;
            }
            if (tspbMain.ProgressBar.Value == tspbMain.Maximum)
            {
                Reset();
                wkbMain.Reload();
            }
        }

        private void Reset()
        {
            bgwDone = false;
            tspbMain.Value = 0;
            sw.Stop();
            sw.Reset();
        }

        private void BuildTargetList()
        {
            rtbDebugVehicle.Clear();
            List<Player> targetPlayers = new List<Player>();
            foreach (Player p in onlinePlayers)
            {
                PaintMap(p.location);
                string wItem = "";
                string wVehicle = "";
                p.TargetLevel = -1;
                if (p.civAir != null)
                    foreach (Vehicles vehicle in p.civAir)
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
                    foreach (Vehicles vehicle in p.civCar)
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
                    foreach (Vehicles vehicle in p.civShip)
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

        private void PaintMap(string[] coords)
        {



        }

        private void ListBox_MouseEnter(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            activeListbox = lb;
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
        private void DisplayPlayer(Player p)
        {
            List<Vehicles> sortedList = new List<Vehicles>();
            tbAliases.Clear();
            lvVehicleInfo.Items.Clear();
            lvVirtualItems.Items.Clear();

            this.Text = string.Format("Olympus Tracker - {0} | {1}", p.name, p.steamID);
            lblName.Text = string.Format("Name: {0}", p.name);
            lblCash.Text = string.Format("Cash: {0:C}", p.cash);
            lblBounty.Text = string.Format("Bounty: {0:C}", p.bountyWanted);
            lblKDR.Text = string.Format("K/D/R: {0:0,0}/{1:0,0}/{2:0.##}", p.kills, p.deaths, Convert.ToDecimal(Convert.ToDecimal(p.kills) / Convert.ToDecimal(p.deaths)));
            lblCopRank.Text = string.Format("APD Rank: {0}", p.copLevel);
            lblCopTime.Text = string.Format("APD Time: {0:0,0}", (p.timeApd.ToString() == "-1") ? "N/A" : p.timeApd.ToString());
            lblCopArrest.Text = string.Format("APD Arrests: {0:0,0}", (p.copArrest.ToString() == "-1") ? "N/A" : p.copArrest.ToString());
            lblGang.Text = string.Format("Gang: {0}", p.gangName == "-1" ? "N/A" : p.gangName);
            lblBank.Text = string.Format("Bank: {0:C}", p.bank);
            lblVigiBounty.Text = string.Format("Bounty Collected: {0:C}", p.bountyCollected == -1 ? 0 : p.bountyCollected);
            lblCivTime.Text = string.Format("Civ Time: {0:0,0}", p.timeCiv);
            lblMedicRank.Text = string.Format("R&R Rank: {0}", p.medicLevel);
            lblMedicTime.Text = string.Format("R&R Time: {0:0,0}", p.timeMed.ToString() == "-1" ? "N/A" : p.timeMed.ToString());
            lblMedicRevives.Text = string.Format("R&R Revives: {0:0,0}", p.medicRevives.ToString() == "-1" ? "N/A" : p.medicRevives.ToString());
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
                foreach (Vehicles v in p.civAir)
                {
                    sortedList.Add(v);
                }
            if (p.civCar != null)
                foreach (Vehicles v in p.civCar)
                {
                    sortedList.Add(v);
                }
            if (p.civShip != null)
                foreach (Vehicles v in p.civShip)
                {
                    sortedList.Add(v);
                }
            sortedList = sortedList.OrderByDescending(v => v.active).ToList();
            foreach (Vehicles v in sortedList)
            {
                ListViewItem lviV = new ListViewItem(v.name);
                lviV.SubItems.Add(v.active.ToString());
                lviV.SubItems.Add(v.turboLevel.ToString());
                lviV.SubItems.Add(v.secLevel.ToString());
                lviV.SubItems.Add(v.storageLevel.ToString());
                lviV.SubItems.Add(v.insuranceLevel.ToString());
                lvVehicleInfo.Items.Add(lviV);
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
            proxy.updateDB((List<string>)e.Argument);
            onlinePlayers = proxy.sendPlayers();
            onlinePlayers = onlinePlayers.OrderBy(p => p.name).ToList();
            closeConnection();
        }

        private void bwPlayerListRefresh_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!sw.IsRunning)
                sw.Start();
            bgwDone = true;
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

        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            playerMap = new Map();
            playerMap.players = onlinePlayers;
            playerMap.Show();
        }
    }

}
