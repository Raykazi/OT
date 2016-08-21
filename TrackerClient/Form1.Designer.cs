namespace TrackerClient
{
    partial class frmMain
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
            this.pLeft = new System.Windows.Forms.Panel();
            this.tcPlayerLists = new System.Windows.Forms.TabControl();
            this.tpPlayersAll = new System.Windows.Forms.TabPage();
            this.lbPlayersAll = new System.Windows.Forms.ListBox();
            this.tpPlayersMeta = new System.Windows.Forms.TabPage();
            this.lbPlayersMeta = new System.Windows.Forms.ListBox();
            this.pSearch = new System.Windows.Forms.Panel();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.pMain = new System.Windows.Forms.Panel();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpPL = new System.Windows.Forms.TabPage();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.wkbMain = new WebKit.WebKitBrowser();
            this.panelTop1 = new System.Windows.Forms.Panel();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.btnGetPlayers = new System.Windows.Forms.Button();
            this.tpPI = new System.Windows.Forms.TabPage();
            this.tcPlayerInfo = new System.Windows.Forms.TabControl();
            this.tpPlayerStats = new System.Windows.Forms.TabPage();
            this.lblHelmet = new System.Windows.Forms.Label();
            this.lblVest = new System.Windows.Forms.Label();
            this.lblGun = new System.Windows.Forms.Label();
            this.lvVehicleInfo = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTurbo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSecurity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStorage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chInsurance = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.lvVirtualItems = new System.Windows.Forms.ListView();
            this.chItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblVirtuals = new System.Windows.Forms.Label();
            this.lblMedicRevives = new System.Windows.Forms.Label();
            this.lblVigiBounty = new System.Windows.Forms.Label();
            this.lblCopArrest = new System.Windows.Forms.Label();
            this.lblMedicTime = new System.Windows.Forms.Label();
            this.lblCivTime = new System.Windows.Forms.Label();
            this.lblCopTime = new System.Windows.Forms.Label();
            this.lblMedicRank = new System.Windows.Forms.Label();
            this.lblCopRank = new System.Windows.Forms.Label();
            this.tbAliases = new System.Windows.Forms.TextBox();
            this.lblGang = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAliases = new System.Windows.Forms.Label();
            this.lblKDR = new System.Windows.Forms.Label();
            this.lblBounty = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.tpCrimes = new System.Windows.Forms.TabPage();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.rtbDebugEquipment = new System.Windows.Forms.RichTextBox();
            this.rtbDebugVehicle = new System.Windows.Forms.RichTextBox();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tspbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerPLRefresh = new System.Windows.Forms.Timer(this.components);
            this.bwPlayerListRefresh = new System.ComponentModel.BackgroundWorker();
            this.bwPlayerListFilter = new System.ComponentModel.BackgroundWorker();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.pLeft.SuspendLayout();
            this.tcPlayerLists.SuspendLayout();
            this.tpPlayersAll.SuspendLayout();
            this.tpPlayersMeta.SuspendLayout();
            this.pSearch.SuspendLayout();
            this.pMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpPL.SuspendLayout();
            this.panelBrowser.SuspendLayout();
            this.panelTop1.SuspendLayout();
            this.tpPI.SuspendLayout();
            this.tcPlayerInfo.SuspendLayout();
            this.tpPlayerStats.SuspendLayout();
            this.tpDebug.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.tcPlayerLists);
            this.pLeft.Controls.Add(this.pSearch);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 0);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(158, 576);
            this.pLeft.TabIndex = 26;
            // 
            // tcPlayerLists
            // 
            this.tcPlayerLists.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcPlayerLists.Controls.Add(this.tpPlayersAll);
            this.tcPlayerLists.Controls.Add(this.tpPlayersMeta);
            this.tcPlayerLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPlayerLists.Location = new System.Drawing.Point(0, 34);
            this.tcPlayerLists.Name = "tcPlayerLists";
            this.tcPlayerLists.SelectedIndex = 0;
            this.tcPlayerLists.Size = new System.Drawing.Size(158, 542);
            this.tcPlayerLists.TabIndex = 36;
            // 
            // tpPlayersAll
            // 
            this.tpPlayersAll.Controls.Add(this.lbPlayersAll);
            this.tpPlayersAll.Location = new System.Drawing.Point(4, 4);
            this.tpPlayersAll.Name = "tpPlayersAll";
            this.tpPlayersAll.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayersAll.Size = new System.Drawing.Size(150, 516);
            this.tpPlayersAll.TabIndex = 0;
            this.tpPlayersAll.Text = "All";
            this.tpPlayersAll.UseVisualStyleBackColor = true;
            // 
            // lbPlayersAll
            // 
            this.lbPlayersAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersAll.FormattingEnabled = true;
            this.lbPlayersAll.Location = new System.Drawing.Point(3, 3);
            this.lbPlayersAll.Name = "lbPlayersAll";
            this.lbPlayersAll.Size = new System.Drawing.Size(144, 510);
            this.lbPlayersAll.TabIndex = 35;
            this.lbPlayersAll.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.lbPlayersAll.SelectedIndexChanged += new System.EventHandler(this.lbPlayers_SelectedIndexChanged);
            // 
            // tpPlayersMeta
            // 
            this.tpPlayersMeta.Controls.Add(this.lbPlayersMeta);
            this.tpPlayersMeta.Location = new System.Drawing.Point(4, 4);
            this.tpPlayersMeta.Name = "tpPlayersMeta";
            this.tpPlayersMeta.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayersMeta.Size = new System.Drawing.Size(150, 516);
            this.tpPlayersMeta.TabIndex = 1;
            this.tpPlayersMeta.Text = "Meta";
            this.tpPlayersMeta.UseVisualStyleBackColor = true;
            // 
            // lbPlayersMeta
            // 
            this.lbPlayersMeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersMeta.FormattingEnabled = true;
            this.lbPlayersMeta.Location = new System.Drawing.Point(3, 3);
            this.lbPlayersMeta.Name = "lbPlayersMeta";
            this.lbPlayersMeta.Size = new System.Drawing.Size(144, 510);
            this.lbPlayersMeta.TabIndex = 36;
            this.lbPlayersMeta.SelectedIndexChanged += new System.EventHandler(this.lbPlayersMeta_SelectedIndexChanged);
            // 
            // pSearch
            // 
            this.pSearch.Controls.Add(this.tbFilter);
            this.pSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSearch.Location = new System.Drawing.Point(0, 0);
            this.pSearch.Name = "pSearch";
            this.pSearch.Size = new System.Drawing.Size(158, 34);
            this.pSearch.TabIndex = 35;
            // 
            // tbFilter
            // 
            this.tbFilter.Location = new System.Drawing.Point(12, 5);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(140, 20);
            this.tbFilter.TabIndex = 0;
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.tcMain);
            this.pMain.Controls.Add(this.pLeft);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(991, 576);
            this.pMain.TabIndex = 29;
            // 
            // tcMain
            // 
            this.tcMain.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tcMain.Controls.Add(this.tpPL);
            this.tcMain.Controls.Add(this.tpPI);
            this.tcMain.Controls.Add(this.tpDebug);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(158, 0);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(833, 576);
            this.tcMain.TabIndex = 30;
            // 
            // tpPL
            // 
            this.tpPL.Controls.Add(this.panelBrowser);
            this.tpPL.Controls.Add(this.panelTop1);
            this.tpPL.Location = new System.Drawing.Point(4, 4);
            this.tpPL.Name = "tpPL";
            this.tpPL.Padding = new System.Windows.Forms.Padding(3);
            this.tpPL.Size = new System.Drawing.Size(806, 568);
            this.tpPL.TabIndex = 0;
            this.tpPL.Text = "Player List";
            this.tpPL.UseVisualStyleBackColor = true;
            // 
            // panelBrowser
            // 
            this.panelBrowser.Controls.Add(this.wkbMain);
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(3, 37);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(800, 528);
            this.panelBrowser.TabIndex = 2;
            // 
            // wkbMain
            // 
            this.wkbMain.BackColor = System.Drawing.Color.White;
            this.wkbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wkbMain.Location = new System.Drawing.Point(0, 0);
            this.wkbMain.Name = "wkbMain";
            this.wkbMain.Size = new System.Drawing.Size(800, 528);
            this.wkbMain.TabIndex = 1;
            this.wkbMain.Url = new System.Uri("https://steamcommunity.com/login/home/?goto=0", System.UriKind.Absolute);
            this.wkbMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wkbMain_DocumentCompleted);
            this.wkbMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wkbMain_Navigated);
            // 
            // panelTop1
            // 
            this.panelTop1.Controls.Add(this.tbURL);
            this.panelTop1.Controls.Add(this.btnGetPlayers);
            this.panelTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop1.Location = new System.Drawing.Point(3, 3);
            this.panelTop1.Name = "panelTop1";
            this.panelTop1.Size = new System.Drawing.Size(800, 34);
            this.panelTop1.TabIndex = 3;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(6, 3);
            this.tbURL.Name = "tbURL";
            this.tbURL.ReadOnly = true;
            this.tbURL.Size = new System.Drawing.Size(456, 20);
            this.tbURL.TabIndex = 3;
            // 
            // btnGetPlayers
            // 
            this.btnGetPlayers.Location = new System.Drawing.Point(468, 3);
            this.btnGetPlayers.Name = "btnGetPlayers";
            this.btnGetPlayers.Size = new System.Drawing.Size(75, 23);
            this.btnGetPlayers.TabIndex = 2;
            this.btnGetPlayers.Text = "Get Players";
            this.btnGetPlayers.UseVisualStyleBackColor = true;
            this.btnGetPlayers.Click += new System.EventHandler(this.btnGetPlayers_Click);
            // 
            // tpPI
            // 
            this.tpPI.Controls.Add(this.tcPlayerInfo);
            this.tpPI.Location = new System.Drawing.Point(4, 4);
            this.tpPI.Name = "tpPI";
            this.tpPI.Padding = new System.Windows.Forms.Padding(3);
            this.tpPI.Size = new System.Drawing.Size(806, 568);
            this.tpPI.TabIndex = 1;
            this.tpPI.Text = "Player Info";
            this.tpPI.UseVisualStyleBackColor = true;
            // 
            // tcPlayerInfo
            // 
            this.tcPlayerInfo.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcPlayerInfo.Controls.Add(this.tpPlayerStats);
            this.tcPlayerInfo.Controls.Add(this.tpCrimes);
            this.tcPlayerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPlayerInfo.Location = new System.Drawing.Point(3, 3);
            this.tcPlayerInfo.Multiline = true;
            this.tcPlayerInfo.Name = "tcPlayerInfo";
            this.tcPlayerInfo.SelectedIndex = 0;
            this.tcPlayerInfo.Size = new System.Drawing.Size(800, 562);
            this.tcPlayerInfo.TabIndex = 27;
            // 
            // tpPlayerStats
            // 
            this.tpPlayerStats.Controls.Add(this.lblUpdated);
            this.tpPlayerStats.Controls.Add(this.lblHelmet);
            this.tpPlayerStats.Controls.Add(this.lblVest);
            this.tpPlayerStats.Controls.Add(this.lblGun);
            this.tpPlayerStats.Controls.Add(this.lvVehicleInfo);
            this.tpPlayerStats.Controls.Add(this.label1);
            this.tpPlayerStats.Controls.Add(this.lvVirtualItems);
            this.tpPlayerStats.Controls.Add(this.lblVirtuals);
            this.tpPlayerStats.Controls.Add(this.lblMedicRevives);
            this.tpPlayerStats.Controls.Add(this.lblVigiBounty);
            this.tpPlayerStats.Controls.Add(this.lblCopArrest);
            this.tpPlayerStats.Controls.Add(this.lblMedicTime);
            this.tpPlayerStats.Controls.Add(this.lblCivTime);
            this.tpPlayerStats.Controls.Add(this.lblCopTime);
            this.tpPlayerStats.Controls.Add(this.lblMedicRank);
            this.tpPlayerStats.Controls.Add(this.lblCopRank);
            this.tpPlayerStats.Controls.Add(this.tbAliases);
            this.tpPlayerStats.Controls.Add(this.lblGang);
            this.tpPlayerStats.Controls.Add(this.lblName);
            this.tpPlayerStats.Controls.Add(this.lblAliases);
            this.tpPlayerStats.Controls.Add(this.lblKDR);
            this.tpPlayerStats.Controls.Add(this.lblBounty);
            this.tpPlayerStats.Controls.Add(this.lblBank);
            this.tpPlayerStats.Controls.Add(this.lblCash);
            this.tpPlayerStats.Location = new System.Drawing.Point(4, 4);
            this.tpPlayerStats.Name = "tpPlayerStats";
            this.tpPlayerStats.Padding = new System.Windows.Forms.Padding(3);
            this.tpPlayerStats.Size = new System.Drawing.Size(792, 536);
            this.tpPlayerStats.TabIndex = 0;
            this.tpPlayerStats.Text = "Player Stats";
            this.tpPlayerStats.UseVisualStyleBackColor = true;
            // 
            // lblHelmet
            // 
            this.lblHelmet.AutoSize = true;
            this.lblHelmet.Location = new System.Drawing.Point(434, 57);
            this.lblHelmet.Name = "lblHelmet";
            this.lblHelmet.Size = new System.Drawing.Size(46, 13);
            this.lblHelmet.TabIndex = 40;
            this.lblHelmet.Text = "Helmet: ";
            // 
            // lblVest
            // 
            this.lblVest.AutoSize = true;
            this.lblVest.Location = new System.Drawing.Point(434, 30);
            this.lblVest.Name = "lblVest";
            this.lblVest.Size = new System.Drawing.Size(31, 13);
            this.lblVest.TabIndex = 40;
            this.lblVest.Text = "Vest:";
            // 
            // lblGun
            // 
            this.lblGun.AutoSize = true;
            this.lblGun.Location = new System.Drawing.Point(434, 3);
            this.lblGun.Name = "lblGun";
            this.lblGun.Size = new System.Drawing.Size(33, 13);
            this.lblGun.TabIndex = 40;
            this.lblGun.Text = "Gun: ";
            // 
            // lvVehicleInfo
            // 
            this.lvVehicleInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chActive,
            this.chTurbo,
            this.chSecurity,
            this.chStorage,
            this.chInsurance});
            this.lvVehicleInfo.Location = new System.Drawing.Point(374, 209);
            this.lvVehicleInfo.Name = "lvVehicleInfo";
            this.lvVehicleInfo.Size = new System.Drawing.Size(412, 276);
            this.lvVehicleInfo.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvVehicleInfo.TabIndex = 39;
            this.lvVehicleInfo.UseCompatibleStateImageBehavior = false;
            this.lvVehicleInfo.View = System.Windows.Forms.View.Details;
            this.lvVehicleInfo.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvVehicleInfo_ColumnClick);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 88;
            // 
            // chActive
            // 
            this.chActive.Text = "Active";
            // 
            // chTurbo
            // 
            this.chTurbo.Text = "Turbo";
            // 
            // chSecurity
            // 
            this.chSecurity.Text = "Security";
            // 
            // chStorage
            // 
            this.chStorage.Text = "Storage";
            // 
            // chInsurance
            // 
            this.chInsurance.Text = "Insurance";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(371, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Vehicles:";
            // 
            // lvVirtualItems
            // 
            this.lvVirtualItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItem,
            this.chAmount});
            this.lvVirtualItems.Location = new System.Drawing.Point(213, 209);
            this.lvVirtualItems.Name = "lvVirtualItems";
            this.lvVirtualItems.Size = new System.Drawing.Size(155, 276);
            this.lvVirtualItems.TabIndex = 37;
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
            // lblVirtuals
            // 
            this.lblVirtuals.AutoSize = true;
            this.lblVirtuals.Location = new System.Drawing.Point(213, 193);
            this.lblVirtuals.Name = "lblVirtuals";
            this.lblVirtuals.Size = new System.Drawing.Size(35, 13);
            this.lblVirtuals.TabIndex = 36;
            this.lblVirtuals.Text = "Items:";
            // 
            // lblMedicRevives
            // 
            this.lblMedicRevives.AutoSize = true;
            this.lblMedicRevives.Location = new System.Drawing.Point(257, 165);
            this.lblMedicRevives.Name = "lblMedicRevives";
            this.lblMedicRevives.Size = new System.Drawing.Size(74, 13);
            this.lblMedicRevives.TabIndex = 34;
            this.lblMedicRevives.Text = "R&&R Revives:";
            // 
            // lblVigiBounty
            // 
            this.lblVigiBounty.AutoSize = true;
            this.lblVigiBounty.Location = new System.Drawing.Point(241, 57);
            this.lblVigiBounty.Name = "lblVigiBounty";
            this.lblVigiBounty.Size = new System.Drawing.Size(90, 13);
            this.lblVigiBounty.TabIndex = 34;
            this.lblVigiBounty.Text = "Bounty Collected:";
            // 
            // lblCopArrest
            // 
            this.lblCopArrest.AutoSize = true;
            this.lblCopArrest.Location = new System.Drawing.Point(9, 165);
            this.lblCopArrest.Name = "lblCopArrest";
            this.lblCopArrest.Size = new System.Drawing.Size(67, 13);
            this.lblCopArrest.TabIndex = 34;
            this.lblCopArrest.Text = "APD Arrests:";
            // 
            // lblMedicTime
            // 
            this.lblMedicTime.AutoSize = true;
            this.lblMedicTime.Location = new System.Drawing.Point(273, 138);
            this.lblMedicTime.Name = "lblMedicTime";
            this.lblMedicTime.Size = new System.Drawing.Size(58, 13);
            this.lblMedicTime.TabIndex = 33;
            this.lblMedicTime.Text = "R&&R Time:";
            // 
            // lblCivTime
            // 
            this.lblCivTime.AutoSize = true;
            this.lblCivTime.Location = new System.Drawing.Point(280, 84);
            this.lblCivTime.Name = "lblCivTime";
            this.lblCivTime.Size = new System.Drawing.Size(51, 13);
            this.lblCivTime.TabIndex = 33;
            this.lblCivTime.Text = "Civ Time:";
            // 
            // lblCopTime
            // 
            this.lblCopTime.AutoSize = true;
            this.lblCopTime.Location = new System.Drawing.Point(18, 138);
            this.lblCopTime.Name = "lblCopTime";
            this.lblCopTime.Size = new System.Drawing.Size(58, 13);
            this.lblCopTime.TabIndex = 33;
            this.lblCopTime.Text = "APD Time:";
            // 
            // lblMedicRank
            // 
            this.lblMedicRank.AutoSize = true;
            this.lblMedicRank.Location = new System.Drawing.Point(270, 111);
            this.lblMedicRank.Name = "lblMedicRank";
            this.lblMedicRank.Size = new System.Drawing.Size(61, 13);
            this.lblMedicRank.TabIndex = 32;
            this.lblMedicRank.Text = "R&&R Rank:";
            // 
            // lblCopRank
            // 
            this.lblCopRank.AutoSize = true;
            this.lblCopRank.Location = new System.Drawing.Point(15, 111);
            this.lblCopRank.Name = "lblCopRank";
            this.lblCopRank.Size = new System.Drawing.Size(61, 13);
            this.lblCopRank.TabIndex = 32;
            this.lblCopRank.Text = "APD Rank:";
            // 
            // tbAliases
            // 
            this.tbAliases.Location = new System.Drawing.Point(12, 209);
            this.tbAliases.Multiline = true;
            this.tbAliases.Name = "tbAliases";
            this.tbAliases.ReadOnly = true;
            this.tbAliases.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAliases.Size = new System.Drawing.Size(195, 276);
            this.tbAliases.TabIndex = 5;
            // 
            // lblGang
            // 
            this.lblGang.AutoSize = true;
            this.lblGang.Location = new System.Drawing.Point(264, 3);
            this.lblGang.Name = "lblGang";
            this.lblGang.Size = new System.Drawing.Size(67, 13);
            this.lblGang.TabIndex = 26;
            this.lblGang.Text = "Gang/Rank:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(38, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name:";
            // 
            // lblAliases
            // 
            this.lblAliases.AutoSize = true;
            this.lblAliases.Location = new System.Drawing.Point(12, 193);
            this.lblAliases.Name = "lblAliases";
            this.lblAliases.Size = new System.Drawing.Size(43, 13);
            this.lblAliases.TabIndex = 25;
            this.lblAliases.Text = "Aliases:";
            // 
            // lblKDR
            // 
            this.lblKDR.AutoSize = true;
            this.lblKDR.Location = new System.Drawing.Point(30, 84);
            this.lblKDR.Name = "lblKDR";
            this.lblKDR.Size = new System.Drawing.Size(46, 13);
            this.lblKDR.TabIndex = 14;
            this.lblKDR.Text = "K/D/R: ";
            // 
            // lblBounty
            // 
            this.lblBounty.AutoSize = true;
            this.lblBounty.Location = new System.Drawing.Point(33, 57);
            this.lblBounty.Name = "lblBounty";
            this.lblBounty.Size = new System.Drawing.Size(43, 13);
            this.lblBounty.TabIndex = 22;
            this.lblBounty.Text = "Bounty:";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(296, 30);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(35, 13);
            this.lblBank.TabIndex = 21;
            this.lblBank.Text = "Bank:";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(42, 30);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(34, 13);
            this.lblCash.TabIndex = 20;
            this.lblCash.Text = "Cash:";
            // 
            // tpCrimes
            // 
            this.tpCrimes.Location = new System.Drawing.Point(4, 4);
            this.tpCrimes.Name = "tpCrimes";
            this.tpCrimes.Size = new System.Drawing.Size(792, 536);
            this.tpCrimes.TabIndex = 2;
            this.tpCrimes.Text = "Crimes";
            this.tpCrimes.UseVisualStyleBackColor = true;
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.rtbDebugEquipment);
            this.tpDebug.Controls.Add(this.rtbDebugVehicle);
            this.tpDebug.Location = new System.Drawing.Point(4, 4);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(806, 568);
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
            this.tspbMain,
            this.tsslStatus});
            this.ssMain.Location = new System.Drawing.Point(0, 576);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(991, 22);
            this.ssMain.TabIndex = 2;
            this.ssMain.Text = "statusStrip";
            // 
            // tspbMain
            // 
            this.tspbMain.Maximum = 180;
            this.tspbMain.Name = "tspbMain";
            this.tspbMain.Size = new System.Drawing.Size(100, 16);
            this.tspbMain.Step = 1;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(0, 17);
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
            // lblUpdated
            // 
            this.lblUpdated.AutoSize = true;
            this.lblUpdated.Location = new System.Drawing.Point(12, 511);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(51, 13);
            this.lblUpdated.TabIndex = 40;
            this.lblUpdated.Text = "Updated:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 598);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.ssMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Olympus Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pLeft.ResumeLayout(false);
            this.tcPlayerLists.ResumeLayout(false);
            this.tpPlayersAll.ResumeLayout(false);
            this.tpPlayersMeta.ResumeLayout(false);
            this.pSearch.ResumeLayout(false);
            this.pSearch.PerformLayout();
            this.pMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpPL.ResumeLayout(false);
            this.panelBrowser.ResumeLayout(false);
            this.panelTop1.ResumeLayout(false);
            this.panelTop1.PerformLayout();
            this.tpPI.ResumeLayout(false);
            this.tcPlayerInfo.ResumeLayout(false);
            this.tpPlayerStats.ResumeLayout(false);
            this.tpPlayerStats.PerformLayout();
            this.tpDebug.ResumeLayout(false);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pSearch;
        private System.Windows.Forms.ListBox lbPlayersAll;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpPL;
        private System.Windows.Forms.TabPage tpPI;
        private WebKit.WebKitBrowser wkbMain;
        private System.Windows.Forms.Panel panelBrowser;
        private System.Windows.Forms.Panel panelTop1;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Button btnGetPlayers;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.Timer timerPLRefresh;
        private System.ComponentModel.BackgroundWorker bwPlayerListRefresh;
        private System.Windows.Forms.TabControl tcPlayerLists;
        private System.Windows.Forms.TabPage tpPlayersAll;
        private System.Windows.Forms.TabPage tpPlayersMeta;
        private System.ComponentModel.BackgroundWorker bwPlayerListFilter;
        private System.Windows.Forms.ListBox lbPlayersMeta;
        private System.Windows.Forms.TabControl tcPlayerInfo;
        private System.Windows.Forms.TabPage tpPlayerStats;
        private System.Windows.Forms.ListView lvVehicleInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvVirtualItems;
        private System.Windows.Forms.Label lblVirtuals;
        private System.Windows.Forms.Label lblMedicRevives;
        private System.Windows.Forms.Label lblVigiBounty;
        private System.Windows.Forms.Label lblCopArrest;
        private System.Windows.Forms.Label lblMedicTime;
        private System.Windows.Forms.Label lblCivTime;
        private System.Windows.Forms.Label lblCopTime;
        private System.Windows.Forms.Label lblMedicRank;
        private System.Windows.Forms.Label lblCopRank;
        private System.Windows.Forms.TextBox tbAliases;
        private System.Windows.Forms.Label lblGang;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAliases;
        private System.Windows.Forms.Label lblKDR;
        private System.Windows.Forms.Label lblBounty;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.TabPage tpCrimes;
        private System.Windows.Forms.ColumnHeader chItem;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chActive;
        private System.Windows.Forms.ColumnHeader chTurbo;
        private System.Windows.Forms.ColumnHeader chSecurity;
        private System.Windows.Forms.ColumnHeader chStorage;
        private System.Windows.Forms.ColumnHeader chInsurance;
        private System.Windows.Forms.Label lblGun;
        private System.Windows.Forms.Label lblHelmet;
        private System.Windows.Forms.Label lblVest;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.RichTextBox rtbDebugEquipment;
        private System.Windows.Forms.RichTextBox rtbDebugVehicle;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.ToolStripProgressBar tspbMain;
        private System.Windows.Forms.Label lblUpdated;
    }
}

