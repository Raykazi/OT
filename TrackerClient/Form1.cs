using System;
using System.ServiceModel;
using System.Windows.Forms;
using TrackerInterface;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Collections;

namespace TrackerClient
{

    public partial class frmMain : Form
    {
        ChannelFactory<IWCFTrackerService> channelFactory;
        IWCFTrackerService proxy;
        List<string> aliases = new List<string>();
        List<Player> Players;
        List<string> debugList = new List<string>();
        public HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        object locker = new object();


        ListViewItemComparer _lvwItemComparer = new ListViewItemComparer();
        public frmMain()
        {
            InitializeComponent();
            HtmlNode.ElementsFlags.Remove("form");
            lvVehicleInfo.ListViewItemSorter = _lvwItemComparer;
        }
        public void resetLabels(Control control)
        {

            if (control is Label)
            {
                Label lbl = (Label)control;
                lbl.ResetText();

            }
            else
                foreach (Control child in control.Controls)
                {
                    resetLabels(child);
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupListViews();
        }

        private void SetupListViews()
        {
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

        private void tpPlayerStats_Click(object sender, EventArgs e)
        {

        }

        private void tbStats_TextChanged(object sender, EventArgs e)
        {

        }

        private void pSearch_Paint(object sender, PaintEventArgs e)
        {


        }
        delegate void SetTextCallback(string text, Control c);
        private void SetText(string text, Control c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (c.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text, c });
            }
            else
            {
                c.Text = text;
            }
        }
        private void wkbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            tbURL.Text = wkbMain.Url.ToString();
        }

        private void btnGetPlayers_Click(object sender, EventArgs e)
        {
            string currentURL = wkbMain.Url.ToString();
            if (currentURL.Contains("/home"))
            {
                string[] segments = currentURL.Split('/');
                string steamName = segments[4];
                string targetPage = string.Format("https://steamcommunity.com/id/{0}/friends/players/", steamName);
                wkbMain.Url = new Uri(targetPage);
            }
            else if (currentURL.Contains("/friends/players"))
            {
                wkbMain.Reload();
            }
        }

        private void wkbMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            List<string> steamID = new List<string>();
            if (wkbMain.Url.ToString().Contains("/players"))
            {
                steamID.Clear();
                string src = wkbMain.DocumentText.ToString();
                doc.LoadHtml(src);
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
                timerPLRefresh.Enabled = true;
                timerPLRefresh.Start();
                openConnection();
                proxy.updateDB(steamID);
                Players = proxy.sendPlayers();
                List<Player> sortedPlayers = Players.OrderBy(p => p.name).ToList();
                closeConnection();

                lbPlayersAll.DisplayMember = "name";
                lbPlayersAll.DataSource = sortedPlayers;
                METAGAMETHESENIGGAS();
            }
            else if (timerPLRefresh.Enabled && !wkbMain.Url.ToString().Contains("/friends/players"))
            {
                timerPLRefresh.Enabled = false;
                timerPLRefresh.Stop();
            }
        }

        private void METAGAMETHESENIGGAS()
        {
            List<Player> metaPlayers = new List<Player>();

            string[] watchListItems = { "rock", "salt", "cement", "glass", "iron", "copper", "silver", "platinum", "oilp", "diamond", "diamondc", "marijuana", "frog", "mushroom", "heroinp", "cocaine", "moonshine", "meth", "goldbar", "yeast", "sugar", "corn", "cannabis", "heroinu", "ephedra", "lithium", "phosphorus", "oilu", "heroinp", "ironore" };
            string[] watchListVehicles = { "Tempest (Device)", "Zamak Transport (Covered)", "HEMTT Transport", "HEMTT Box", "Truck Box", "Truck Fuel", "Ifrit", "Taru (Fuel)", "Orca", "Huron" };

            foreach (Player p in Players)
            {
                if (p.Virtuals != null)
                    foreach (VirtualItem v in p.Virtuals)
                    {
                        if (Array.IndexOf(watchListItems, v.name) != -1 && !metaPlayers.Contains(p))
                        {
                            metaPlayers.Add(p);
                            continue;
                        }
                    }
                if (p.civAir != null)
                    foreach (Vehicles vehicle in p.civAir)
                    {
                        if (Array.IndexOf(watchListVehicles, vehicle.name) != -1 && !metaPlayers.Contains(p))
                        {
                            if (vehicle.active == 1)
                            {
                                metaPlayers.Add(p);
                                continue;
                            }
                        }
                    }
                if (p.civCar != null)
                    foreach (Vehicles vehicle in p.civCar)
                    {
                        if (Array.IndexOf(watchListVehicles, vehicle.name) != -1 && !metaPlayers.Contains(p))
                        {
                            if (vehicle.active == 1)
                            {
                                metaPlayers.Add(p);
                                continue;
                            }
                        }
                    }
                if (p.civShip != null)
                    foreach (Vehicles vehicle in p.civShip)
                    {
                        if (Array.IndexOf(watchListVehicles, vehicle.name) != -1 && !metaPlayers.Contains(p))
                        {
                            if (vehicle.active == 1)
                            {
                                metaPlayers.Add(p);
                                continue;
                            }
                        }
                    }

            }
            List<Player> sortedPlayers = metaPlayers.OrderBy(p => p.name).ToList();
            lbPlayersMeta.DisplayMember = "name";
            lbPlayersMeta.DataSource = sortedPlayers;
        }

        private void timerPLRefresh_Tick(object sender, EventArgs e)
        {
            wkbMain.Reload();
        }

        private void lbPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab != tpPI)
            {
                tcMain.SelectTab(1);
                tcPlayerInfo.SelectTab(0);
            }
            Player p = (Player)lbPlayersAll.SelectedValue;
            DisplayPlayer(p);
        }

        private void lbPlayersMeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab != tpPI)
            {
                tcMain.SelectTab(1);
                tcPlayerInfo.SelectTab(0);
            }
            Player p = (Player)lbPlayersMeta.SelectedValue;
            DisplayPlayer(p);

        }

        private void DisplayPlayer(Player p)
        {
            List<Vehicles> sortedList = new List<Vehicles>();
            tbAliases.Clear();
            lvVehicleInfo.Items.Clear();
            lvVirtualItems.Items.Clear();

            this.Text = string.Format("Olympus Tracker - {0} | {1}", p.name, p.steamID);
            lblName.Text = string.Format("Name: {0}", p.name);
            lblCash.Text = string.Format("Cash: {0:0,0}", p.cash);
            lblBounty.Text = string.Format("Bounty: {0:0,0}", p.bountyWanted);
            lblKDR.Text = string.Format("K/D/R: {0:0,0}/{1:0,0}/{2:0.##}", p.kills, p.deaths, Convert.ToDecimal(Convert.ToDecimal(p.kills) / Convert.ToDecimal(p.deaths)));
            lblCopRank.Text = string.Format("APD Rank: {0}", p.copLevel);
            lblCopTime.Text = string.Format("APD Time: {0:0,0}", (p.timeApd.ToString() == "-1") ? "N/A" : p.timeApd.ToString());
            lblCopArrest.Text = string.Format("APD Arrests: {0:0,0}", (p.copArrest.ToString() == "-1") ? "N/A" : p.copArrest.ToString());
            lblGang.Text = string.Format("Gang: {0}", p.gangName == "-1" ? "N/A" : p.gangName);
            lblBank.Text = string.Format("Bank: {0:0,0}", p.bank);
            lblVigiBounty.Text = string.Format("Bounty Collected: {0:0,0}", p.bountyCollected.ToString() == "-1" ? "N/A" : p.bountyCollected.ToString());
            lblCivTime.Text = string.Format("Civ Time: {0:0,0}", p.timeCiv);
            lblMedicRank.Text = string.Format("R&R Rank: {0}", p.medicLevel);
            lblMedicTime.Text = string.Format("R&R Time: {0:0,0}", p.timeMed.ToString() == "-1" ? "N/A" : p.timeMed.ToString());
            lblMedicRevives.Text = string.Format("R&R Revives: {0:0,0}", p.medicRevives.ToString() == "-1" ? "N/A" : p.medicRevives.ToString());
            lblVest.Text = string.Format("Vest: {0}", p.Equipment[1]);
            lblHelmet.Text = string.Format("Helmet: {0}", p.Equipment[4]);
            lblGun.Text = string.Format("Gun: {0}", p.Equipment[5]);
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
                    if (!debugList.Contains(v.name))
                        debugList.Add(v.name);
                }
            if (p.civCar != null)
                foreach (Vehicles v in p.civCar)
                {
                    sortedList.Add(v);
                    if (!debugList.Contains(v.name))
                        debugList.Add(v.name);
                }
            if (p.civShip != null)
                foreach (Vehicles v in p.civShip)
                {
                    sortedList.Add(v);
                    if (!debugList.Contains(v.name))
                        debugList.Add(v.name);
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

        private void lvPlayerList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lvVehicleInfo_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _lvwItemComparer.SortColumn)
            {
                // Reverse the current sort direction for this column.
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
                // Set the column number that is to be sorted; default to ascending.
                _lvwItemComparer.SortColumn = e.Column;
                _lvwItemComparer.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvVehicleInfo.Sort();
        }

    }

}
