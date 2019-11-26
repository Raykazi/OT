namespace TrackerClient
{
    partial class rfMain
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
            Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn1 = new Telerik.WinControls.UI.ListViewDetailColumn("Column 0", "Item");
            Telerik.WinControls.UI.ListViewDetailColumn listViewDetailColumn2 = new Telerik.WinControls.UI.ListViewDetailColumn("Column 1", "Amount");
            this.rtVSDark = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            this.rpvMain = new Telerik.WinControls.UI.RadPageView();
            this.rpvpPlayers = new Telerik.WinControls.UI.RadPageViewPage();
            this.rlvVirItems = new Telerik.WinControls.UI.RadListView();
            this.rlcVehicles = new Telerik.WinControls.UI.RadListControl();
            this.rbtbAliases = new Telerik.WinControls.UI.RadButtonTextBox();
            this.rgbLocation = new Telerik.WinControls.UI.RadGroupBox();
            this.rtrbZoom = new Telerik.WinControls.UI.RadTrackBar();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblVVirtuals = new System.Windows.Forms.Label();
            this.lblVehicles = new System.Windows.Forms.Label();
            this.lblVirtuals = new System.Windows.Forms.Label();
            this.lblAliases = new System.Windows.Forms.Label();
            this.lblLauncher = new System.Windows.Forms.Label();
            this.lblSecondary = new System.Windows.Forms.Label();
            this.lblHelmet = new System.Windows.Forms.Label();
            this.lblVest = new System.Windows.Forms.Label();
            this.lblGun = new System.Windows.Forms.Label();
            this.lblMedicRank = new System.Windows.Forms.Label();
            this.lblCopRank = new System.Windows.Forms.Label();
            this.lblGang = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblKDR = new System.Windows.Forms.Label();
            this.lblBounty = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.rpPlayerLeft = new Telerik.WinControls.UI.RadPanel();
            this.rpvPlayers = new Telerik.WinControls.UI.RadPageView();
            this.rpvpAll = new Telerik.WinControls.UI.RadPageViewPage();
            this.lbPlayersAll = new System.Windows.Forms.ListBox();
            this.rpvpTargets = new Telerik.WinControls.UI.RadPageViewPage();
            this.lbPlayersTargets = new System.Windows.Forms.ListBox();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.radPageViewPage3 = new Telerik.WinControls.UI.RadPageViewPage();
            this.rpBottom = new Telerik.WinControls.UI.RadPanel();
            this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
            this.rbRefresh = new Telerik.WinControls.UI.RadButtonElement();
            this.rtbRefresh = new Telerik.WinControls.UI.RadTrackBarElement();
            this.rlRefresh = new Telerik.WinControls.UI.RadLabelElement();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.rsbServer = new Telerik.WinControls.UI.RadSplitButtonElement();
            this.rmiServer1 = new Telerik.WinControls.UI.RadMenuItem();
            this.rmiServer2 = new Telerik.WinControls.UI.RadMenuItem();
            this.rbMap = new Telerik.WinControls.UI.RadButtonElement();
            this.rpMain = new Telerik.WinControls.UI.RadPanel();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.bwPlayerTabRefresh = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.rtebVehicleInfo = new Telerik.WinControls.UI.RadTextBox();
            this.lblSteamID = new System.Windows.Forms.LinkLabel();
            this.lblBM = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.rpvMain)).BeginInit();
            this.rpvMain.SuspendLayout();
            this.rpvpPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvVirItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlcVehicles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtbAliases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbLocation)).BeginInit();
            this.rgbLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtrbZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlayerLeft)).BeginInit();
            this.rpPlayerLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpvPlayers)).BeginInit();
            this.rpvPlayers.SuspendLayout();
            this.rpvpAll.SuspendLayout();
            this.rpvpTargets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rpBottom)).BeginInit();
            this.rpBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMain)).BeginInit();
            this.rpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtebVehicleInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rpvMain
            // 
            this.rpvMain.Controls.Add(this.rpvpPlayers);
            this.rpvMain.Controls.Add(this.radPageViewPage2);
            this.rpvMain.Controls.Add(this.radPageViewPage3);
            this.rpvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvMain.Location = new System.Drawing.Point(0, 0);
            this.rpvMain.Name = "rpvMain";
            this.rpvMain.SelectedPage = this.rpvpPlayers;
            this.rpvMain.Size = new System.Drawing.Size(1046, 592);
            this.rpvMain.TabIndex = 0;
            this.rpvMain.ThemeName = "VisualStudio2012Dark";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpvMain.GetChildAt(0))).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Left;
            // 
            // rpvpPlayers
            // 
            this.rpvpPlayers.Controls.Add(this.lblBM);
            this.rpvpPlayers.Controls.Add(this.lblSteamID);
            this.rpvpPlayers.Controls.Add(this.rtebVehicleInfo);
            this.rpvpPlayers.Controls.Add(this.rlvVirItems);
            this.rpvpPlayers.Controls.Add(this.rlcVehicles);
            this.rpvpPlayers.Controls.Add(this.rbtbAliases);
            this.rpvpPlayers.Controls.Add(this.rgbLocation);
            this.rpvpPlayers.Controls.Add(this.lblVVirtuals);
            this.rpvpPlayers.Controls.Add(this.lblVehicles);
            this.rpvpPlayers.Controls.Add(this.lblVirtuals);
            this.rpvpPlayers.Controls.Add(this.lblAliases);
            this.rpvpPlayers.Controls.Add(this.lblLauncher);
            this.rpvpPlayers.Controls.Add(this.lblSecondary);
            this.rpvpPlayers.Controls.Add(this.lblHelmet);
            this.rpvpPlayers.Controls.Add(this.lblVest);
            this.rpvpPlayers.Controls.Add(this.lblGun);
            this.rpvpPlayers.Controls.Add(this.lblMedicRank);
            this.rpvpPlayers.Controls.Add(this.lblCopRank);
            this.rpvpPlayers.Controls.Add(this.lblGang);
            this.rpvpPlayers.Controls.Add(this.lblName);
            this.rpvpPlayers.Controls.Add(this.lblKDR);
            this.rpvpPlayers.Controls.Add(this.lblBounty);
            this.rpvpPlayers.Controls.Add(this.lblBank);
            this.rpvpPlayers.Controls.Add(this.lblCash);
            this.rpvpPlayers.Controls.Add(this.rpPlayerLeft);
            this.rpvpPlayers.ItemSize = new System.Drawing.SizeF(24F, 108F);
            this.rpvpPlayers.Location = new System.Drawing.Point(30, 5);
            this.rpvpPlayers.Name = "rpvpPlayers";
            this.rpvpPlayers.Size = new System.Drawing.Size(1011, 582);
            this.rpvpPlayers.Text = "radPageViewPage1";
            // 
            // rlvVirItems
            // 
            this.rlvVirItems.AllowColumnResize = false;
            listViewDetailColumn1.HeaderText = "Item";
            listViewDetailColumn1.Width = 100F;
            listViewDetailColumn2.HeaderText = "Amount";
            listViewDetailColumn2.Width = 50F;
            this.rlvVirItems.Columns.AddRange(new Telerik.WinControls.UI.ListViewDetailColumn[] {
            listViewDetailColumn1,
            listViewDetailColumn2});
            this.rlvVirItems.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysHide;
            this.rlvVirItems.ItemSpacing = -1;
            this.rlvVirItems.Location = new System.Drawing.Point(188, 339);
            this.rlvVirItems.Name = "rlvVirItems";
            this.rlvVirItems.Size = new System.Drawing.Size(155, 226);
            this.rlvVirItems.TabIndex = 120;
            this.rlvVirItems.ThemeName = "VisualStudio2012Dark";
            this.rlvVirItems.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            // 
            // rlcVehicles
            // 
            this.rlcVehicles.Location = new System.Drawing.Point(372, 169);
            this.rlcVehicles.Name = "rlcVehicles";
            this.rlcVehicles.Size = new System.Drawing.Size(160, 140);
            this.rlcVehicles.TabIndex = 119;
            this.rlcVehicles.ThemeName = "VisualStudio2012Dark";
            // 
            // rbtbAliases
            // 
            this.rbtbAliases.Location = new System.Drawing.Point(188, 169);
            this.rbtbAliases.Multiline = true;
            this.rbtbAliases.Name = "rbtbAliases";
            this.rbtbAliases.ReadOnly = true;
            // 
            // 
            // 
            this.rbtbAliases.RootElement.StretchVertically = true;
            this.rbtbAliases.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rbtbAliases.Size = new System.Drawing.Size(150, 140);
            this.rbtbAliases.TabIndex = 118;
            this.rbtbAliases.ThemeName = "VisualStudio2012Dark";
            // 
            // rgbLocation
            // 
            this.rgbLocation.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.rgbLocation.Controls.Add(this.rtrbZoom);
            this.rgbLocation.Controls.Add(this.radGroupBox2);
            this.rgbLocation.Controls.Add(this.lblLocation);
            this.rgbLocation.HeaderText = "Location";
            this.rgbLocation.Location = new System.Drawing.Point(566, 156);
            this.rgbLocation.Name = "rgbLocation";
            this.rgbLocation.Size = new System.Drawing.Size(438, 409);
            this.rgbLocation.TabIndex = 117;
            this.rgbLocation.Text = "Location";
            this.rgbLocation.ThemeName = "VisualStudio2012Dark";
            // 
            // rtrbZoom
            // 
            this.rtrbZoom.AllowKeyNavigation = true;
            this.rtrbZoom.Location = new System.Drawing.Point(283, 13);
            this.rtrbZoom.Maximum = 300F;
            this.rtrbZoom.Minimum = 50F;
            this.rtrbZoom.Name = "rtrbZoom";
            this.rtrbZoom.Size = new System.Drawing.Size(150, 32);
            this.rtrbZoom.SnapMode = Telerik.WinControls.UI.TrackBarSnapModes.None;
            this.rtrbZoom.TabIndex = 107;
            this.rtrbZoom.ThemeName = "VisualStudio2012Dark";
            this.rtrbZoom.Value = 100F;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radGroupBox2.HeaderText = "";
            this.radGroupBox2.Location = new System.Drawing.Point(2, 51);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Size = new System.Drawing.Size(434, 356);
            this.radGroupBox2.TabIndex = 3;
            this.radGroupBox2.ThemeName = "VisualStudio2012Dark";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(28, 26);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(50, 13);
            this.lblLocation.TabIndex = 106;
            this.lblLocation.Text = "Coords: ";
            // 
            // lblVVirtuals
            // 
            this.lblVVirtuals.AutoSize = true;
            this.lblVVirtuals.Location = new System.Drawing.Point(369, 323);
            this.lblVVirtuals.Name = "lblVVirtuals";
            this.lblVVirtuals.Size = new System.Drawing.Size(76, 13);
            this.lblVVirtuals.TabIndex = 115;
            this.lblVVirtuals.Text = "Vehicle Items:";
            // 
            // lblVehicles
            // 
            this.lblVehicles.AutoSize = true;
            this.lblVehicles.Location = new System.Drawing.Point(369, 153);
            this.lblVehicles.Name = "lblVehicles";
            this.lblVehicles.Size = new System.Drawing.Size(51, 13);
            this.lblVehicles.TabIndex = 113;
            this.lblVehicles.Text = "Vehicles:";
            this.lblVehicles.Click += new System.EventHandler(this.lblVehicles_Click);
            // 
            // lblVirtuals
            // 
            this.lblVirtuals.AutoSize = true;
            this.lblVirtuals.Location = new System.Drawing.Point(185, 323);
            this.lblVirtuals.Name = "lblVirtuals";
            this.lblVirtuals.Size = new System.Drawing.Size(37, 13);
            this.lblVirtuals.TabIndex = 111;
            this.lblVirtuals.Text = "Items:";
            // 
            // lblAliases
            // 
            this.lblAliases.AutoSize = true;
            this.lblAliases.Location = new System.Drawing.Point(185, 153);
            this.lblAliases.Name = "lblAliases";
            this.lblAliases.Size = new System.Drawing.Size(45, 13);
            this.lblAliases.TabIndex = 110;
            this.lblAliases.Text = "Aliases:";
            // 
            // lblLauncher
            // 
            this.lblLauncher.AutoSize = true;
            this.lblLauncher.Location = new System.Drawing.Point(583, 66);
            this.lblLauncher.Name = "lblLauncher";
            this.lblLauncher.Size = new System.Drawing.Size(60, 13);
            this.lblLauncher.TabIndex = 108;
            this.lblLauncher.Text = "Launcher: ";
            // 
            // lblSecondary
            // 
            this.lblSecondary.AutoSize = true;
            this.lblSecondary.Location = new System.Drawing.Point(580, 40);
            this.lblSecondary.Name = "lblSecondary";
            this.lblSecondary.Size = new System.Drawing.Size(63, 13);
            this.lblSecondary.TabIndex = 107;
            this.lblSecondary.Text = "Secondary:";
            // 
            // lblHelmet
            // 
            this.lblHelmet.AutoSize = true;
            this.lblHelmet.Location = new System.Drawing.Point(595, 118);
            this.lblHelmet.Name = "lblHelmet";
            this.lblHelmet.Size = new System.Drawing.Size(49, 13);
            this.lblHelmet.TabIndex = 105;
            this.lblHelmet.Text = "Helmet: ";
            // 
            // lblVest
            // 
            this.lblVest.AutoSize = true;
            this.lblVest.Location = new System.Drawing.Point(610, 92);
            this.lblVest.Name = "lblVest";
            this.lblVest.Size = new System.Drawing.Size(31, 13);
            this.lblVest.TabIndex = 104;
            this.lblVest.Text = "Vest:";
            // 
            // lblGun
            // 
            this.lblGun.AutoSize = true;
            this.lblGun.Location = new System.Drawing.Point(594, 14);
            this.lblGun.Name = "lblGun";
            this.lblGun.Size = new System.Drawing.Size(50, 13);
            this.lblGun.TabIndex = 103;
            this.lblGun.Text = "Primary: ";
            // 
            // lblMedicRank
            // 
            this.lblMedicRank.AutoSize = true;
            this.lblMedicRank.Location = new System.Drawing.Point(358, 92);
            this.lblMedicRank.Name = "lblMedicRank";
            this.lblMedicRank.Size = new System.Drawing.Size(62, 13);
            this.lblMedicRank.TabIndex = 101;
            this.lblMedicRank.Text = "R&&R Rank:";
            // 
            // lblCopRank
            // 
            this.lblCopRank.AutoSize = true;
            this.lblCopRank.Location = new System.Drawing.Point(170, 92);
            this.lblCopRank.Name = "lblCopRank";
            this.lblCopRank.Size = new System.Drawing.Size(60, 13);
            this.lblCopRank.TabIndex = 100;
            this.lblCopRank.Text = "APD Rank:";
            // 
            // lblGang
            // 
            this.lblGang.AutoSize = true;
            this.lblGang.Location = new System.Drawing.Point(382, 14);
            this.lblGang.Name = "lblGang";
            this.lblGang.Size = new System.Drawing.Size(38, 13);
            this.lblGang.TabIndex = 99;
            this.lblGang.Text = "Gang:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(191, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 95;
            this.lblName.Text = "Name:";
            // 
            // lblKDR
            // 
            this.lblKDR.AutoSize = true;
            this.lblKDR.Location = new System.Drawing.Point(378, 66);
            this.lblKDR.Name = "lblKDR";
            this.lblKDR.Size = new System.Drawing.Size(42, 13);
            this.lblKDR.TabIndex = 94;
            this.lblKDR.Text = "K/D/R: ";
            // 
            // lblBounty
            // 
            this.lblBounty.AutoSize = true;
            this.lblBounty.Location = new System.Drawing.Point(184, 66);
            this.lblBounty.Name = "lblBounty";
            this.lblBounty.Size = new System.Drawing.Size(46, 13);
            this.lblBounty.TabIndex = 98;
            this.lblBounty.Text = "Bounty:";
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(385, 40);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(35, 13);
            this.lblBank.TabIndex = 97;
            this.lblBank.Text = "Bank:";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Location = new System.Drawing.Point(195, 40);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(35, 13);
            this.lblCash.TabIndex = 96;
            this.lblCash.Text = "Cash:";
            // 
            // rpPlayerLeft
            // 
            this.rpPlayerLeft.Controls.Add(this.rpvPlayers);
            this.rpPlayerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.rpPlayerLeft.Location = new System.Drawing.Point(0, 0);
            this.rpPlayerLeft.Name = "rpPlayerLeft";
            this.rpPlayerLeft.Size = new System.Drawing.Size(164, 582);
            this.rpPlayerLeft.TabIndex = 0;
            this.rpPlayerLeft.ThemeName = "VisualStudio2012Dark";
            // 
            // rpvPlayers
            // 
            this.rpvPlayers.Controls.Add(this.rpvpAll);
            this.rpvPlayers.Controls.Add(this.rpvpTargets);
            this.rpvPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvPlayers.Location = new System.Drawing.Point(0, 0);
            this.rpvPlayers.Name = "rpvPlayers";
            this.rpvPlayers.SelectedPage = this.rpvpAll;
            this.rpvPlayers.Size = new System.Drawing.Size(164, 582);
            this.rpvPlayers.TabIndex = 0;
            this.rpvPlayers.ThemeName = "VisualStudio2012Dark";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpvPlayers.GetChildAt(0))).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Bottom;
            // 
            // rpvpAll
            // 
            this.rpvpAll.Controls.Add(this.lbPlayersAll);
            this.rpvpAll.ItemSize = new System.Drawing.SizeF(25F, 24F);
            this.rpvpAll.Location = new System.Drawing.Point(5, 5);
            this.rpvpAll.Name = "rpvpAll";
            this.rpvpAll.Size = new System.Drawing.Size(154, 547);
            this.rpvpAll.Text = "All";
            // 
            // lbPlayersAll
            // 
            this.lbPlayersAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersAll.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPlayersAll.FormattingEnabled = true;
            this.lbPlayersAll.Location = new System.Drawing.Point(0, 0);
            this.lbPlayersAll.Name = "lbPlayersAll";
            this.lbPlayersAll.Size = new System.Drawing.Size(154, 547);
            this.lbPlayersAll.TabIndex = 1;
            this.lbPlayersAll.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            this.lbPlayersAll.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            this.lbPlayersAll.MouseEnter += new System.EventHandler(this.ListBox_MouseEnter);
            // 
            // rpvpTargets
            // 
            this.rpvpTargets.Controls.Add(this.lbPlayersTargets);
            this.rpvpTargets.ItemSize = new System.Drawing.SizeF(49F, 24F);
            this.rpvpTargets.Location = new System.Drawing.Point(5, 5);
            this.rpvpTargets.Name = "rpvpTargets";
            this.rpvpTargets.Size = new System.Drawing.Size(154, 547);
            this.rpvpTargets.Text = "Targets";
            // 
            // lbPlayersTargets
            // 
            this.lbPlayersTargets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPlayersTargets.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPlayersTargets.FormattingEnabled = true;
            this.lbPlayersTargets.Location = new System.Drawing.Point(0, 0);
            this.lbPlayersTargets.Name = "lbPlayersTargets";
            this.lbPlayersTargets.Size = new System.Drawing.Size(154, 547);
            this.lbPlayersTargets.TabIndex = 0;
            this.lbPlayersTargets.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
            this.lbPlayersTargets.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.ItemSize = new System.Drawing.SizeF(24F, 108F);
            this.radPageViewPage2.Location = new System.Drawing.Point(30, 5);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(1001, 547);
            this.radPageViewPage2.Text = "radPageViewPage2";
            // 
            // radPageViewPage3
            // 
            this.radPageViewPage3.ItemSize = new System.Drawing.SizeF(24F, 108F);
            this.radPageViewPage3.Location = new System.Drawing.Point(30, 5);
            this.radPageViewPage3.Name = "radPageViewPage3";
            this.radPageViewPage3.Size = new System.Drawing.Size(1001, 547);
            this.radPageViewPage3.Text = "radPageViewPage3";
            // 
            // rpBottom
            // 
            this.rpBottom.Controls.Add(this.radStatusStrip1);
            this.rpBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rpBottom.Location = new System.Drawing.Point(0, 592);
            this.rpBottom.Name = "rpBottom";
            this.rpBottom.Size = new System.Drawing.Size(1046, 29);
            this.rpBottom.TabIndex = 1;
            this.rpBottom.ThemeName = "VisualStudio2012Dark";
            // 
            // radStatusStrip1
            // 
            this.radStatusStrip1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rbRefresh,
            this.rtbRefresh,
            this.rlRefresh,
            this.commandBarSeparator1,
            this.rsbServer,
            this.rbMap});
            this.radStatusStrip1.Location = new System.Drawing.Point(0, 3);
            this.radStatusStrip1.Name = "radStatusStrip1";
            this.radStatusStrip1.Padding = new System.Windows.Forms.Padding(2);
            this.radStatusStrip1.Size = new System.Drawing.Size(1046, 26);
            this.radStatusStrip1.TabIndex = 0;
            this.radStatusStrip1.ThemeName = "VisualStudio2012Dark";
            // 
            // rbRefresh
            // 
            this.rbRefresh.AccessibleName = "rbRefresh";
            this.rbRefresh.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.rbRefresh.Name = "rbRefresh";
            this.radStatusStrip1.SetSpring(this.rbRefresh, false);
            this.rbRefresh.Text = "Refresh";
            this.rbRefresh.Click += new System.EventHandler(this.rbRefresh_Click);
            // 
            // rtbRefresh
            // 
            this.rtbRefresh.AutoSize = false;
            this.rtbRefresh.AutoToolTip = true;
            this.rtbRefresh.Bounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.rtbRefresh.Comparer = null;
            this.rtbRefresh.FitInAvailableSize = true;
            this.rtbRefresh.Maximum = 180F;
            this.rtbRefresh.MinSize = new System.Drawing.Size(100, 0);
            this.rtbRefresh.Name = "rtbRefresh";
            this.rtbRefresh.SmallChange = 5;
            this.rtbRefresh.SmallTickFrequency = 5;
            this.radStatusStrip1.SetSpring(this.rtbRefresh, false);
            this.rtbRefresh.Text = "radTrackBarElement1";
            this.rtbRefresh.ToolTipText = "radTrackBarElement1";
            this.rtbRefresh.Value = 15F;
            this.rtbRefresh.ValueChanged += new System.EventHandler(this.rtbRefresh_ValueChanged);
            // 
            // rlRefresh
            // 
            this.rlRefresh.AutoSize = false;
            this.rlRefresh.Bounds = new System.Drawing.Rectangle(0, 0, 200, 18);
            this.rlRefresh.Name = "rlRefresh";
            this.radStatusStrip1.SetSpring(this.rlRefresh, false);
            this.rlRefresh.Text = "radLabelElement1";
            this.rlRefresh.TextWrap = true;
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.Margin = new System.Windows.Forms.Padding(3);
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.radStatusStrip1.SetSpring(this.commandBarSeparator1, false);
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // rsbServer
            // 
            this.rsbServer.ArrowButtonMinSize = new System.Drawing.Size(12, 12);
            this.rsbServer.AutoSize = true;
            this.rsbServer.DefaultItem = null;
            this.rsbServer.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.rsbServer.DropDownDirection = Telerik.WinControls.UI.RadDirection.Up;
            this.rsbServer.ExpandArrowButton = false;
            this.rsbServer.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.rmiServer1,
            this.rmiServer2});
            this.rsbServer.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.rsbServer.Name = "rsbServer";
            this.rsbServer.Padding = new System.Windows.Forms.Padding(-1);
            this.radStatusStrip1.SetSpring(this.rsbServer, false);
            this.rsbServer.Text = "Server";
            // 
            // rmiServer1
            // 
            this.rmiServer1.AccessibleName = "rmiServer1";
            this.rmiServer1.Name = "rmiServer1";
            this.rmiServer1.Text = "Server 1";
            this.rmiServer1.Click += new System.EventHandler(this.rsbServer_Click);
            // 
            // rmiServer2
            // 
            this.rmiServer2.AccessibleName = "rmiServer2";
            this.rmiServer2.Name = "rmiServer2";
            this.rmiServer2.Text = "Server 2";
            this.rmiServer2.Click += new System.EventHandler(this.rsbServer_Click);
            // 
            // rbMap
            // 
            this.rbMap.Name = "rbMap";
            this.radStatusStrip1.SetSpring(this.rbMap, false);
            this.rbMap.Text = "Map";
            // 
            // rpMain
            // 
            this.rpMain.Controls.Add(this.rpvMain);
            this.rpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpMain.Location = new System.Drawing.Point(0, 0);
            this.rpMain.Name = "rpMain";
            this.rpMain.Size = new System.Drawing.Size(1046, 592);
            this.rpMain.TabIndex = 2;
            this.rpMain.ThemeName = "VisualStudio2012Dark";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // bwPlayerTabRefresh
            // 
            this.bwPlayerTabRefresh.WorkerReportsProgress = true;
            this.bwPlayerTabRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwPlayerTabRefresh_DoWork);
            this.bwPlayerTabRefresh.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwPlayerTabRefresh_RunWorkerCompleted);
            // 
            // rtebVehicleInfo
            // 
            this.rtebVehicleInfo.Location = new System.Drawing.Point(372, 339);
            this.rtebVehicleInfo.Multiline = true;
            this.rtebVehicleInfo.Name = "rtebVehicleInfo";
            this.rtebVehicleInfo.ReadOnly = true;
            // 
            // 
            // 
            this.rtebVehicleInfo.RootElement.StretchVertically = true;
            this.rtebVehicleInfo.Size = new System.Drawing.Size(160, 226);
            this.rtebVehicleInfo.TabIndex = 123;
            this.rtebVehicleInfo.ThemeName = "VisualStudio2012Dark";
            // 
            // lblSteamID
            // 
            this.lblSteamID.AutoSize = true;
            this.lblSteamID.LinkColor = System.Drawing.Color.White;
            this.lblSteamID.Location = new System.Drawing.Point(175, 118);
            this.lblSteamID.Name = "lblSteamID";
            this.lblSteamID.Size = new System.Drawing.Size(55, 13);
            this.lblSteamID.TabIndex = 124;
            this.lblSteamID.TabStop = true;
            this.lblSteamID.Text = "Steam ID:";
            this.lblSteamID.VisitedLinkColor = System.Drawing.Color.Gray;
            this.lblSteamID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSteamID_LinkClicked);
            this.lblSteamID.Click += new System.EventHandler(this.lblSteamID_Click);
            // 
            // lblBM
            // 
            this.lblBM.AutoSize = true;
            this.lblBM.LinkColor = System.Drawing.Color.White;
            this.lblBM.Location = new System.Drawing.Point(342, 118);
            this.lblBM.Name = "lblBM";
            this.lblBM.Size = new System.Drawing.Size(93, 13);
            this.lblBM.TabIndex = 125;
            this.lblBM.TabStop = true;
            this.lblBM.Text = "BattleMetrics ID: ";
            this.lblBM.VisitedLinkColor = System.Drawing.Color.Gray;
            this.lblBM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBM_LinkClicked);
            // 
            // rfMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 621);
            this.Controls.Add(this.rpMain);
            this.Controls.Add(this.rpBottom);
            this.Name = "rfMain";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "";
            this.ThemeName = "VisualStudio2012Dark";
            this.Load += new System.EventHandler(this.rfMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rpvMain)).EndInit();
            this.rpvMain.ResumeLayout(false);
            this.rpvpPlayers.ResumeLayout(false);
            this.rpvpPlayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rlvVirItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlcVehicles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtbAliases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgbLocation)).EndInit();
            this.rgbLocation.ResumeLayout(false);
            this.rgbLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtrbZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpPlayerLeft)).EndInit();
            this.rpPlayerLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rpvPlayers)).EndInit();
            this.rpvPlayers.ResumeLayout(false);
            this.rpvpAll.ResumeLayout(false);
            this.rpvpTargets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rpBottom)).EndInit();
            this.rpBottom.ResumeLayout(false);
            this.rpBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rpMain)).EndInit();
            this.rpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rtebVehicleInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme rtVSDark;
        private Telerik.WinControls.UI.RadPageView rpvMain;
        private Telerik.WinControls.UI.RadPanel rpBottom;
        private Telerik.WinControls.UI.RadPanel rpMain;
        private Telerik.WinControls.UI.RadPageViewPage rpvpPlayers;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage3;
        private Telerik.WinControls.UI.RadPanel rpPlayerLeft;
        private System.Windows.Forms.Label lblLauncher;
        private System.Windows.Forms.Label lblSecondary;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblHelmet;
        private System.Windows.Forms.Label lblVest;
        private System.Windows.Forms.Label lblGun;
        private System.Windows.Forms.Label lblMedicRank;
        private System.Windows.Forms.Label lblCopRank;
        private System.Windows.Forms.Label lblGang;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblKDR;
        private System.Windows.Forms.Label lblBounty;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Label lblVVirtuals;
        private System.Windows.Forms.Label lblVehicles;
        private System.Windows.Forms.Label lblVirtuals;
        private System.Windows.Forms.Label lblAliases;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
        private Telerik.WinControls.UI.RadPageView rpvPlayers;
        private Telerik.WinControls.UI.RadPageViewPage rpvpTargets;
        private Telerik.WinControls.UI.RadListView rlvVirItems;
        private Telerik.WinControls.UI.RadListControl rlcVehicles;
        private Telerik.WinControls.UI.RadButtonTextBox rbtbAliases;
        private Telerik.WinControls.UI.RadGroupBox rgbLocation;
        private Telerik.WinControls.UI.RadTrackBar rtrbZoom;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private System.Windows.Forms.Timer timerMain;
        private System.ComponentModel.BackgroundWorker bwPlayerTabRefresh;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private Telerik.WinControls.UI.RadButtonElement rbRefresh;
        private Telerik.WinControls.UI.RadLabelElement rlRefresh;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.RadSplitButtonElement rsbServer;
        private Telerik.WinControls.UI.RadButtonElement rbMap;
        private Telerik.WinControls.UI.RadTrackBarElement rtbRefresh;
        private Telerik.WinControls.UI.RadMenuItem rmiServer1;
        private Telerik.WinControls.UI.RadMenuItem rmiServer2;
        private Telerik.WinControls.UI.RadPageViewPage rpvpAll;
        private System.Windows.Forms.ListBox lbPlayersAll;
        private System.Windows.Forms.ListBox lbPlayersTargets;
        private Telerik.WinControls.UI.RadTextBox rtebVehicleInfo;
        private System.Windows.Forms.LinkLabel lblSteamID;
        private System.Windows.Forms.LinkLabel lblBM;
    }
}
