using System;

namespace TrackerClient
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.pPlayerList = new System.Windows.Forms.Panel();
            this.tcPlayerLists = new System.Windows.Forms.TabControl();
            this.tpPlayersAll = new System.Windows.Forms.TabPage();
            this.lbPlayersAll = new System.Windows.Forms.ListBox();
            this.cmsWatchlist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToWatchlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromWatchlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpPlayersTargets = new System.Windows.Forms.TabPage();
            this.lbPlayersTargets = new System.Windows.Forms.ListBox();
            this.tpWatchlist = new System.Windows.Forms.TabPage();
            this.lbWatchlist = new System.Windows.Forms.ListBox();
            this.pMain = new System.Windows.Forms.Panel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpPlayerInfo = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpInfo = new System.Windows.Forms.TabPage();
            this.lblLauncher = new System.Windows.Forms.Label();
            this.lblSecondary = new System.Windows.Forms.Label();
            this.lbVehicles = new System.Windows.Forms.ListBox();
            this.lblVVirtuals = new System.Windows.Forms.Label();
            this.lvVVirtuals = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvVirtualItems = new System.Windows.Forms.ListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbAliases = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblHelmet = new System.Windows.Forms.Label();
            this.lblVest = new System.Windows.Forms.Label();
            this.lblGun = new System.Windows.Forms.Label();
            this.lblVehicles = new System.Windows.Forms.Label();
            this.lblVirtuals = new System.Windows.Forms.Label();
            this.lblVigiBounty = new System.Windows.Forms.Label();
            this.lblMedicRank = new System.Windows.Forms.Label();
            this.lblCopRank = new System.Windows.Forms.Label();
            this.lblGang = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAliases = new System.Windows.Forms.Label();
            this.lblKDR = new System.Windows.Forms.Label();
            this.lblBounty = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.tpHouses = new System.Windows.Forms.TabPage();
            this.panelPictureBox = new System.Windows.Forms.Panel();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.panelHouses = new System.Windows.Forms.Panel();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.lbHouses = new System.Windows.Forms.ListBox();
            this.lvHInventory = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHVirtuals = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.rtbDebugEquipment = new System.Windows.Forms.RichTextBox();
            this.rtbDebugVehicle = new System.Windows.Forms.RichTextBox();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerPLRefresh = new System.Windows.Forms.Timer(this.components);
            this.bwPlayerListRefresh = new System.ComponentModel.BackgroundWorker();
            this.bwPlayerListFilter = new System.ComponentModel.BackgroundWorker();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.server1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.server2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nud_RefreshTimer = new System.Windows.Forms.NumericUpDown();
            this.panelLeft.SuspendLayout();
            this.pPlayerList.SuspendLayout();
            this.tcPlayerLists.SuspendLayout();
            this.tpPlayersAll.SuspendLayout();
            this.cmsWatchlist.SuspendLayout();
            this.tpPlayersTargets.SuspendLayout();
            this.tpWatchlist.SuspendLayout();
            this.pMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpPlayerInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpInfo.SuspendLayout();
            this.tpHouses.SuspendLayout();
            this.panelPictureBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.panelHouses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.tpDebug.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.pPlayerList);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(174, 494);
            this.panelLeft.TabIndex = 26;
            // 
            // pPlayerList
            // 
            this.pPlayerList.Controls.Add(this.tcPlayerLists);
            this.pPlayerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pPlayerList.Location = new System.Drawing.Point(0, 0);
            this.pPlayerList.Name = "pPlayerList";
            this.pPlayerList.Size = new System.Drawing.Size(174, 494);
            this.pPlayerList.TabIndex = 67;
            // 
            // tcPlayerLists
            // 
            this.tcPlayerLists.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcPlayerLists.Controls.Add(this.tpPlayersAll);
            this.tcPlayerLists.Controls.Add(this.tpPlayersTargets);
            this.tcPlayerLists.Controls.Add(this.tpWatchlist);
            this.tcPlayerLists.Location = new System.Drawing.Point(0, 0);
            this.tcPlayerLists.Name = "tcPlayerLists";
            this.tcPlayerLists.SelectedIndex = 0;
            this.tcPlayerLists.Size = new System.Drawing.Size(174, 494);
            this.tcPlayerLists.TabIndex = 36;
            // 
            // tpPlayersAll
            // 
            this.tpPlayersAll.Controls.Add(this.lbPlayersAll);
            this.tpPlayersAll.Location = new System.Drawing.Point(4, 4);
            this.tpPlayersAll.Name = "tpPlayersAll";
            this.tpPlayersAll.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayersAll.Size = new System.Drawing.Size(166, 468);
            this.tpPlayersAll.TabIndex = 0;
            this.tpPlayersAll.Text = "All";
            this.tpPlayersAll.UseVisualStyleBackColor = true;
            // 
            // lbPlayersAll
            // 
            this.lbPlayersAll.ContextMenuStrip = this.cmsWatchlist;
            this.lbPlayersAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersAll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPlayersAll.FormattingEnabled = true;
            this.lbPlayersAll.Location = new System.Drawing.Point(3, 3);
            this.lbPlayersAll.Name = "lbPlayersAll";
            this.lbPlayersAll.Size = new System.Drawing.Size(160, 462);
            this.lbPlayersAll.TabIndex = 35;
            this.lbPlayersAll.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            this.lbPlayersAll.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            this.lbPlayersAll.MouseEnter += new System.EventHandler(this.ListBox_MouseEnter);
            // 
            // cmsWatchlist
            // 
            this.cmsWatchlist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToWatchlistToolStripMenuItem,
            this.removeFromWatchlistToolStripMenuItem});
            this.cmsWatchlist.Name = "cmsWatchlist";
            this.cmsWatchlist.Size = new System.Drawing.Size(201, 48);
            this.cmsWatchlist.Opening += new System.ComponentModel.CancelEventHandler(this.cmsWatchlist_Opening);
            // 
            // addToWatchlistToolStripMenuItem
            // 
            this.addToWatchlistToolStripMenuItem.Name = "addToWatchlistToolStripMenuItem";
            this.addToWatchlistToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.addToWatchlistToolStripMenuItem.Text = "&Add To Watchlist";
            // 
            // removeFromWatchlistToolStripMenuItem
            // 
            this.removeFromWatchlistToolStripMenuItem.Name = "removeFromWatchlistToolStripMenuItem";
            this.removeFromWatchlistToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.removeFromWatchlistToolStripMenuItem.Text = "&Remove From Watchlist";
            // 
            // tpPlayersTargets
            // 
            this.tpPlayersTargets.Controls.Add(this.lbPlayersTargets);
            this.tpPlayersTargets.Location = new System.Drawing.Point(4, 4);
            this.tpPlayersTargets.Name = "tpPlayersTargets";
            this.tpPlayersTargets.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayersTargets.Size = new System.Drawing.Size(166, 468);
            this.tpPlayersTargets.TabIndex = 1;
            this.tpPlayersTargets.Text = "Targets";
            this.tpPlayersTargets.UseVisualStyleBackColor = true;
            // 
            // lbPlayersTargets
            // 
            this.lbPlayersTargets.ContextMenuStrip = this.cmsWatchlist;
            this.lbPlayersTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersTargets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPlayersTargets.FormattingEnabled = true;
            this.lbPlayersTargets.Location = new System.Drawing.Point(3, 3);
            this.lbPlayersTargets.Name = "lbPlayersTargets";
            this.lbPlayersTargets.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbPlayersTargets.Size = new System.Drawing.Size(160, 462);
            this.lbPlayersTargets.TabIndex = 36;
            this.lbPlayersTargets.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            this.lbPlayersTargets.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            this.lbPlayersTargets.MouseEnter += new System.EventHandler(this.ListBox_MouseEnter);
            // 
            // tpWatchlist
            // 
            this.tpWatchlist.Controls.Add(this.lbWatchlist);
            this.tpWatchlist.Location = new System.Drawing.Point(4, 4);
            this.tpWatchlist.Name = "tpWatchlist";
            this.tpWatchlist.Size = new System.Drawing.Size(166, 468);
            this.tpWatchlist.TabIndex = 2;
            this.tpWatchlist.Text = "Watchlist";
            this.tpWatchlist.UseVisualStyleBackColor = true;
            // 
            // lbWatchlist
            // 
            this.lbWatchlist.ContextMenuStrip = this.cmsWatchlist;
            this.lbWatchlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbWatchlist.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbWatchlist.FormattingEnabled = true;
            this.lbWatchlist.Location = new System.Drawing.Point(0, 0);
            this.lbWatchlist.Name = "lbWatchlist";
            this.lbWatchlist.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbWatchlist.Size = new System.Drawing.Size(166, 468);
            this.lbWatchlist.TabIndex = 37;
            this.lbWatchlist.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            this.lbWatchlist.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            this.lbWatchlist.MouseEnter += new System.EventHandler(this.ListBox_MouseEnter);
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.tcMain);
            this.pMain.Controls.Add(this.panelLeft);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 24);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(913, 494);
            this.pMain.TabIndex = 29;
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tcMain.Controls.Add(this.tpPlayerInfo);
            this.tcMain.Controls.Add(this.tpDebug);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(174, 0);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(739, 494);
            this.tcMain.TabIndex = 30;
            // 
            // tpPlayerInfo
            // 
            this.tpPlayerInfo.Controls.Add(this.tabControl1);
            this.tpPlayerInfo.Location = new System.Drawing.Point(4, 4);
            this.tpPlayerInfo.Name = "tpPlayerInfo";
            this.tpPlayerInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayerInfo.Size = new System.Drawing.Size(712, 486);
            this.tpPlayerInfo.TabIndex = 1;
            this.tpPlayerInfo.Text = "Player Info";
            this.tpPlayerInfo.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tpInfo);
            this.tabControl1.Controls.Add(this.tpHouses);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 480);
            this.tabControl1.TabIndex = 66;
            // 
            // tpInfo
            // 
            this.tpInfo.Controls.Add(this.lblLauncher);
            this.tpInfo.Controls.Add(this.lblSecondary);
            this.tpInfo.Controls.Add(this.lbVehicles);
            this.tpInfo.Controls.Add(this.lblVVirtuals);
            this.tpInfo.Controls.Add(this.lvVVirtuals);
            this.tpInfo.Controls.Add(this.lvVirtualItems);
            this.tpInfo.Controls.Add(this.tbAliases);
            this.tpInfo.Controls.Add(this.lblLocation);
            this.tpInfo.Controls.Add(this.lblHelmet);
            this.tpInfo.Controls.Add(this.lblVest);
            this.tpInfo.Controls.Add(this.lblGun);
            this.tpInfo.Controls.Add(this.lblVehicles);
            this.tpInfo.Controls.Add(this.lblVirtuals);
            this.tpInfo.Controls.Add(this.lblVigiBounty);
            this.tpInfo.Controls.Add(this.lblMedicRank);
            this.tpInfo.Controls.Add(this.lblCopRank);
            this.tpInfo.Controls.Add(this.lblGang);
            this.tpInfo.Controls.Add(this.lblName);
            this.tpInfo.Controls.Add(this.lblAliases);
            this.tpInfo.Controls.Add(this.lblKDR);
            this.tpInfo.Controls.Add(this.lblBounty);
            this.tpInfo.Controls.Add(this.lblBank);
            this.tpInfo.Controls.Add(this.lblCash);
            this.tpInfo.Location = new System.Drawing.Point(4, 4);
            this.tpInfo.Name = "tpInfo";
            this.tpInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfo.Size = new System.Drawing.Size(698, 454);
            this.tpInfo.TabIndex = 0;
            this.tpInfo.Text = "Info";
            this.tpInfo.UseVisualStyleBackColor = true;
            // 
            // lblLauncher
            // 
            this.lblLauncher.AutoSize = true;
            this.lblLauncher.Location = new System.Drawing.Point(453, 57);
            this.lblLauncher.Name = "lblLauncher";
            this.lblLauncher.Size = new System.Drawing.Size(58, 13);
            this.lblLauncher.TabIndex = 93;
            this.lblLauncher.Text = "Launcher: ";
            // 
            // lblSecondary
            // 
            this.lblSecondary.AutoSize = true;
            this.lblSecondary.Location = new System.Drawing.Point(450, 30);
            this.lblSecondary.Name = "lblSecondary";
            this.lblSecondary.Size = new System.Drawing.Size(61, 13);
            this.lblSecondary.TabIndex = 92;
            this.lblSecondary.Text = "Secondary:";
            // 
            // lbVehicles
            // 
            this.lbVehicles.ContextMenuStrip = this.cmsWatchlist;
            this.lbVehicles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbVehicles.FormattingEnabled = true;
            this.lbVehicles.Location = new System.Drawing.Point(340, 184);
            this.lbVehicles.Name = "lbVehicles";
            this.lbVehicles.Size = new System.Drawing.Size(160, 264);
            this.lbVehicles.TabIndex = 91;
            this.lbVehicles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LbVehicles_DrawItem);
            this.lbVehicles.SelectedIndexChanged += new System.EventHandler(this.LbVehicles_SelectedIndexChanged);
            // 
            // lblVVirtuals
            // 
            this.lblVVirtuals.AutoSize = true;
            this.lblVVirtuals.Location = new System.Drawing.Point(503, 168);
            this.lblVVirtuals.Name = "lblVVirtuals";
            this.lblVVirtuals.Size = new System.Drawing.Size(73, 13);
            this.lblVVirtuals.TabIndex = 90;
            this.lblVVirtuals.Text = "Vehicle Items:";
            // 
            // lvVVirtuals
            // 
            this.lvVVirtuals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvVVirtuals.HideSelection = false;
            this.lvVVirtuals.Location = new System.Drawing.Point(506, 184);
            this.lvVVirtuals.Name = "lvVVirtuals";
            this.lvVVirtuals.Size = new System.Drawing.Size(173, 264);
            this.lvVVirtuals.TabIndex = 89;
            this.lvVVirtuals.UseCompatibleStateImageBehavior = false;
            this.lvVVirtuals.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Item";
            this.columnHeader5.Width = 91;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Amount";
            // 
            // lvVirtualItems
            // 
            this.lvVirtualItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chAmount});
            this.lvVirtualItems.HideSelection = false;
            this.lvVirtualItems.Location = new System.Drawing.Point(179, 184);
            this.lvVirtualItems.Name = "lvVirtualItems";
            this.lvVirtualItems.Size = new System.Drawing.Size(155, 264);
            this.lvVirtualItems.TabIndex = 81;
            this.lvVirtualItems.UseCompatibleStateImageBehavior = false;
            this.lvVirtualItems.View = System.Windows.Forms.View.Details;
            // 
            // chItem
            // 
            this.chItem.Text = "Item";
            this.chItem.Width = 91;
            // 
            // chAmount
            // 
            this.chAmount.Text = "Amount";
            // 
            // tbAliases
            // 
            this.tbAliases.Location = new System.Drawing.Point(9, 184);
            this.tbAliases.Multiline = true;
            this.tbAliases.Name = "tbAliases";
            this.tbAliases.ReadOnly = true;
            this.tbAliases.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAliases.Size = new System.Drawing.Size(150, 264);
            this.tbAliases.TabIndex = 66;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(220, 84);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(46, 13);
            this.lblLocation.TabIndex = 88;
            this.lblLocation.Text = "Coords: ";
            // 
            // lblHelmet
            // 
            this.lblHelmet.AutoSize = true;
            this.lblHelmet.Location = new System.Drawing.Point(465, 108);
            this.lblHelmet.Name = "lblHelmet";
            this.lblHelmet.Size = new System.Drawing.Size(46, 13);
            this.lblHelmet.TabIndex = 86;
            this.lblHelmet.Text = "Helmet: ";
            // 
            // lblVest
            // 
            this.lblVest.AutoSize = true;
            this.lblVest.Location = new System.Drawing.Point(480, 81);
            this.lblVest.Name = "lblVest";
            this.lblVest.Size = new System.Drawing.Size(31, 13);
            this.lblVest.TabIndex = 85;
            this.lblVest.Text = "Vest:";
            // 
            // lblGun
            // 
            this.lblGun.AutoSize = true;
            this.lblGun.Location = new System.Drawing.Point(464, 3);
            this.lblGun.Name = "lblGun";
            this.lblGun.Size = new System.Drawing.Size(47, 13);
            this.lblGun.TabIndex = 84;
            this.lblGun.Text = "Primary: ";
            // 
            // lblVehicles
            // 
            this.lblVehicles.AutoSize = true;
            this.lblVehicles.Location = new System.Drawing.Point(337, 168);
            this.lblVehicles.Name = "lblVehicles";
            this.lblVehicles.Size = new System.Drawing.Size(50, 13);
            this.lblVehicles.TabIndex = 82;
            this.lblVehicles.Text = "Vehicles:";
            // 
            // lblVirtuals
            // 
            this.lblVirtuals.AutoSize = true;
            this.lblVirtuals.Location = new System.Drawing.Point(176, 168);
            this.lblVirtuals.Name = "lblVirtuals";
            this.lblVirtuals.Size = new System.Drawing.Size(35, 13);
            this.lblVirtuals.TabIndex = 80;
            this.lblVirtuals.Text = "Items:";
            // 
            // lblVigiBounty
            // 
            this.lblVigiBounty.AutoSize = true;
            this.lblVigiBounty.Location = new System.Drawing.Point(176, 57);
            this.lblVigiBounty.Name = "lblVigiBounty";
            this.lblVigiBounty.Size = new System.Drawing.Size(90, 13);
            this.lblVigiBounty.TabIndex = 79;
            this.lblVigiBounty.Text = "Bounty Collected:";
            // 
            // lblMedicRank
            // 
            this.lblMedicRank.AutoSize = true;
            this.lblMedicRank.Location = new System.Drawing.Point(9, 135);
            this.lblMedicRank.Name = "lblMedicRank";
            this.lblMedicRank.Size = new System.Drawing.Size(61, 13);
            this.lblMedicRank.TabIndex = 75;
            this.lblMedicRank.Text = "R&&R Rank:";
            // 
            // lblCopRank
            // 
            this.lblCopRank.AutoSize = true;
            this.lblCopRank.Location = new System.Drawing.Point(9, 108);
            this.lblCopRank.Name = "lblCopRank";
            this.lblCopRank.Size = new System.Drawing.Size(61, 13);
            this.lblCopRank.TabIndex = 74;
            this.lblCopRank.Text = "APD Rank:";
            // 
            // lblGang
            // 
            this.lblGang.AutoSize = true;
            this.lblGang.Location = new System.Drawing.Point(230, 3);
            this.lblGang.Name = "lblGang";
            this.lblGang.Size = new System.Drawing.Size(36, 13);
            this.lblGang.TabIndex = 73;
            this.lblGang.Text = "Gang:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(32, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 68;
            this.lblName.Text = "Name:";
            // 
            // lblAliases
            // 
            this.lblAliases.AutoSize = true;
            this.lblAliases.Location = new System.Drawing.Point(9, 168);
            this.lblAliases.Name = "lblAliases";
            this.lblAliases.Size = new System.Drawing.Size(43, 13);
            this.lblAliases.TabIndex = 72;
            this.lblAliases.Text = "Aliases:";
            // 
            // lblKDR
            // 
            this.lblKDR.AutoSize = true;
            this.lblKDR.Location = new System.Drawing.Point(24, 84);
            this.lblKDR.Name = "lblKDR";
            this.lblKDR.Size = new System.Drawing.Size(46, 13);
            this.lblKDR.TabIndex = 67;
            this.lblKDR.Text = "K/D/R: ";
            // 
            // lblBounty
            // 
            this.lblBounty.AutoSize = true;
            this.lblBounty.Location = new System.Drawing.Point(27, 57);
            this.lblBounty.Name = "lblBounty";
            this.lblBounty.Size = new System.Drawing.Size(43, 13);
            this.lblBounty.TabIndex = 71;
            this.lblBounty.Text = "Bounty:";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(231, 30);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(35, 13);
            this.lblBank.TabIndex = 70;
            this.lblBank.Text = "Bank:";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(36, 30);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(34, 13);
            this.lblCash.TabIndex = 69;
            this.lblCash.Text = "Cash:";
            // 
            // tpHouses
            // 
            this.tpHouses.Controls.Add(this.panelPictureBox);
            this.tpHouses.Controls.Add(this.panelHouses);
            this.tpHouses.Location = new System.Drawing.Point(4, 4);
            this.tpHouses.Name = "tpHouses";
            this.tpHouses.Padding = new System.Windows.Forms.Padding(3);
            this.tpHouses.Size = new System.Drawing.Size(698, 454);
            this.tpHouses.TabIndex = 1;
            this.tpHouses.Text = "Houses";
            this.tpHouses.UseVisualStyleBackColor = true;
            // 
            // panelPictureBox
            // 
            this.panelPictureBox.AutoScroll = true;
            this.panelPictureBox.Controls.Add(this.pbMap);
            this.panelPictureBox.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.panelPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPictureBox.Location = new System.Drawing.Point(346, 3);
            this.panelPictureBox.Name = "panelPictureBox";
            this.panelPictureBox.Size = new System.Drawing.Size(349, 448);
            this.panelPictureBox.TabIndex = 86;
            this.panelPictureBox.MouseEnter += new System.EventHandler(this.panelPictureBox_MouseEnter);
            this.panelPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelPictureBox_MouseWheel);
            // 
            // pbMap
            // 
            this.pbMap.Image = global::TrackerClient.Properties.Resources.Altis3;
            this.pbMap.InitialImage = global::TrackerClient.Properties.Resources.Altis3;
            this.pbMap.Location = new System.Drawing.Point(0, 0);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(3370, 3370);
            this.pbMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMap.TabIndex = 1;
            this.pbMap.TabStop = false;
            this.pbMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pbHouses_Paint);
            this.pbMap.MouseEnter += new System.EventHandler(this.panelPictureBox_MouseEnter);
            // 
            // panelHouses
            // 
            this.panelHouses.AutoScroll = true;
            this.panelHouses.Controls.Add(this.tbZoom);
            this.panelHouses.Controls.Add(this.lbHouses);
            this.panelHouses.Controls.Add(this.lvHInventory);
            this.panelHouses.Controls.Add(this.lvHVirtuals);
            this.panelHouses.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelHouses.Location = new System.Drawing.Point(3, 3);
            this.panelHouses.Name = "panelHouses";
            this.panelHouses.Size = new System.Drawing.Size(343, 448);
            this.panelHouses.TabIndex = 85;
            // 
            // tbZoom
            // 
            this.tbZoom.Location = new System.Drawing.Point(164, 66);
            this.tbZoom.Maximum = 300;
            this.tbZoom.Minimum = 100;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(152, 45);
            this.tbZoom.TabIndex = 86;
            this.tbZoom.Value = 100;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // lbHouses
            // 
            this.lbHouses.FormattingEnabled = true;
            this.lbHouses.Location = new System.Drawing.Point(3, 3);
            this.lbHouses.Name = "lbHouses";
            this.lbHouses.Size = new System.Drawing.Size(155, 108);
            this.lbHouses.TabIndex = 1;
            this.lbHouses.SelectedIndexChanged += new System.EventHandler(this.lbHouses_SelectedIndexChanged);
            // 
            // lvHInventory
            // 
            this.lvHInventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvHInventory.HideSelection = false;
            this.lvHInventory.Location = new System.Drawing.Point(164, 133);
            this.lvHInventory.Name = "lvHInventory";
            this.lvHInventory.Size = new System.Drawing.Size(152, 329);
            this.lvHInventory.TabIndex = 85;
            this.lvHInventory.UseCompatibleStateImageBehavior = false;
            this.lvHInventory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Item";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Amount";
            // 
            // lvHVirtuals
            // 
            this.lvHVirtuals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvHVirtuals.HideSelection = false;
            this.lvHVirtuals.Location = new System.Drawing.Point(6, 133);
            this.lvHVirtuals.Name = "lvHVirtuals";
            this.lvHVirtuals.Size = new System.Drawing.Size(152, 329);
            this.lvHVirtuals.TabIndex = 83;
            this.lvHVirtuals.UseCompatibleStateImageBehavior = false;
            this.lvHVirtuals.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amount";
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.rtbDebugEquipment);
            this.tpDebug.Controls.Add(this.rtbDebugVehicle);
            this.tpDebug.Location = new System.Drawing.Point(4, 4);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(712, 486);
            this.tpDebug.TabIndex = 2;
            this.tpDebug.Text = "Debug Info";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // rtbDebugEquipment
            // 
            this.rtbDebugEquipment.Location = new System.Drawing.Point(348, 6);
            this.rtbDebugEquipment.Name = "rtbDebugEquipment";
            this.rtbDebugEquipment.Size = new System.Drawing.Size(339, 556);
            this.rtbDebugEquipment.TabIndex = 1;
            this.rtbDebugEquipment.Text = "";
            // 
            // rtbDebugVehicle
            // 
            this.rtbDebugVehicle.Location = new System.Drawing.Point(3, 6);
            this.rtbDebugVehicle.Name = "rtbDebugVehicle";
            this.rtbDebugVehicle.Size = new System.Drawing.Size(339, 556);
            this.rtbDebugVehicle.TabIndex = 0;
            this.rtbDebugVehicle.Text = "";
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 518);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(913, 22);
            this.ssMain.TabIndex = 2;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(39, 17);
            this.tsslStatus.Text = "Status";
            // 
            // timerPLRefresh
            // 
            this.timerPLRefresh.Interval = 1000;
            this.timerPLRefresh.Tick += new System.EventHandler(this.timerPLRefresh_Tick);
            // 
            // bwPlayerListRefresh
            // 
            this.bwPlayerListRefresh.WorkerReportsProgress = true;
            this.bwPlayerListRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPlayerListRefresh_DoWork);
            this.bwPlayerListRefresh.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwPlayerListRefresh_RunWorkerCompleted);
            // 
            // bwPlayerListFilter
            // 
            this.bwPlayerListFilter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPlayerListFilter_DoWork);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(913, 24);
            this.menuStrip.TabIndex = 66;
            this.menuStrip.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.server1ToolStripMenuItem,
            this.server2ToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "&Server";
            // 
            // server1ToolStripMenuItem
            // 
            this.server1ToolStripMenuItem.Checked = true;
            this.server1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.server1ToolStripMenuItem.Name = "server1ToolStripMenuItem";
            this.server1ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.server1ToolStripMenuItem.Text = "Server #1";
            this.server1ToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // server2ToolStripMenuItem
            // 
            this.server2ToolStripMenuItem.Name = "server2ToolStripMenuItem";
            this.server2ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.server2ToolStripMenuItem.Text = "Server #2";
            this.server2ToolStripMenuItem.Click += new System.EventHandler(this.serverToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "&Map";
            this.mapToolStripMenuItem.Click += new System.EventHandler(this.mapToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // nud_RefreshTimer
            // 
            this.nud_RefreshTimer.Location = new System.Drawing.Point(163, 2);
            this.nud_RefreshTimer.Name = "nud_RefreshTimer";
            this.nud_RefreshTimer.Size = new System.Drawing.Size(35, 20);
            this.nud_RefreshTimer.TabIndex = 67;
            this.nud_RefreshTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_RefreshTimer.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nud_RefreshTimer.ValueChanged += new System.EventHandler(this.Nud_RefreshTimer_ValueChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 540);
            this.Controls.Add(this.nud_RefreshTimer);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panelLeft.ResumeLayout(false);
            this.pPlayerList.ResumeLayout(false);
            this.tcPlayerLists.ResumeLayout(false);
            this.tpPlayersAll.ResumeLayout(false);
            this.cmsWatchlist.ResumeLayout(false);
            this.tpPlayersTargets.ResumeLayout(false);
            this.tpWatchlist.ResumeLayout(false);
            this.pMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpPlayerInfo.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpInfo.ResumeLayout(false);
            this.tpInfo.PerformLayout();
            this.tpHouses.ResumeLayout(false);
            this.panelPictureBox.ResumeLayout(false);
            this.panelPictureBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.panelHouses.ResumeLayout(false);
            this.panelHouses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.tpDebug.ResumeLayout(false);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListBox lbPlayersAll;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.Timer timerPLRefresh;
        private System.ComponentModel.BackgroundWorker bwPlayerListRefresh;
        private System.Windows.Forms.TabControl tcPlayerLists;
        private System.Windows.Forms.TabPage tpPlayersAll;
        private System.Windows.Forms.TabPage tpPlayersTargets;
        private System.ComponentModel.BackgroundWorker bwPlayerListFilter;
        private System.Windows.Forms.ListBox lbPlayersTargets;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.TabPage tpWatchlist;
        private System.Windows.Forms.ListBox lbWatchlist;
        private System.Windows.Forms.ContextMenuStrip cmsWatchlist;
        private System.Windows.Forms.ToolStripMenuItem addToWatchlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromWatchlistToolStripMenuItem;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpPlayerInfo;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.RichTextBox rtbDebugEquipment;
        private System.Windows.Forms.RichTextBox rtbDebugVehicle;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem server1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem server2ToolStripMenuItem;
        private System.Windows.Forms.Panel pPlayerList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpInfo;
        private System.Windows.Forms.TabPage tpHouses;
        private System.Windows.Forms.ListBox lbHouses;
        private System.Windows.Forms.ListView lvHVirtuals;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvHInventory;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panelHouses;
        private System.Windows.Forms.Panel panelPictureBox;
        internal System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.NumericUpDown nud_RefreshTimer;
        private System.Windows.Forms.Label lblLauncher;
        private System.Windows.Forms.Label lblSecondary;
        private System.Windows.Forms.ListBox lbVehicles;
        private System.Windows.Forms.Label lblVVirtuals;
        private System.Windows.Forms.ListView lvVVirtuals;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView lvVirtualItems;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.TextBox tbAliases;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblHelmet;
        private System.Windows.Forms.Label lblVest;
        private System.Windows.Forms.Label lblGun;
        private System.Windows.Forms.Label lblVehicles;
        private System.Windows.Forms.Label lblVirtuals;
        private System.Windows.Forms.Label lblVigiBounty;
        private System.Windows.Forms.Label lblMedicRank;
        private System.Windows.Forms.Label lblCopRank;
        private System.Windows.Forms.Label lblGang;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAliases;
        private System.Windows.Forms.Label lblKDR;
        private System.Windows.Forms.Label lblBounty;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblCash;
    }
}

