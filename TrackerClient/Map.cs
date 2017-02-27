using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TrackerInterface;

namespace TrackerClient
{
    public partial class Map : Form
    {
        internal List<Player> Players; //List of Players
        private Font _font = new Font("Tahoma", 7F, FontStyle.Regular); //Default font
        internal bool CanReset = false;
        public Map()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Event to handle after main form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_Load(object sender, EventArgs e)
        {
            CanReset = true;
        }
        /// <summary>
        /// Draws map with online players and corresponding colors 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbMap_Paint(object sender, PaintEventArgs e)
        {
            Point point = panel1.AutoScrollPosition;
            if (CanReset == true)
            {
                e.Graphics.Clear(Color.Transparent);
                pbMap.Image = Properties.Resources.Altis3;
            }
            Helper.PaintPoi(e, pbMap);
            try
            {
                if (Players == null) return;
                foreach (Player p in Players)
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
                    }
                    e.Graphics.FillRectangle(new SolidBrush(mapColor), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(4, 4)));
                    e.Graphics.DrawString(p.Name, _font, new SolidBrush(Color.White), new PointF(newCords[0] + 2, newCords[1]));
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
        //TODO Will probably remove this
        private void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            //Text = string.Format("Map X:{0} Y:{1} Size: {2},{3} ASP {4} {5}", e.X, e.Y, panel1.Size.Height, panel1.Size.Width, panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);

        }
        /// <summary>
        /// Centers map on selected player location
        /// </summary>
        /// <param name="location"></param>
        internal void pbMap_CenterPlayer(string[] location)
        {
            float[] newCords = Helper.performCordScale(location, pbMap);
            int x = Convert.ToInt32(newCords[0]);
            int y = Convert.ToInt32(newCords[1]);
            Point panelcenter = new Point((panel1.Width / 2), (panel1.Height / 2)); // find the centerpoint of the panel
            Point offsetinpicturebox = new Point((pbMap.Location.X + x), (pbMap.Location.Y + y)); // find the offset of the mouse click
            Point offsetfromcenter = new Point((panelcenter.X - offsetinpicturebox.X), (panelcenter.Y - offsetinpicturebox.Y)); // find the difference between the mouse click and the center
            panel1.AutoScrollPosition = new Point((Math.Abs(panel1.AutoScrollPosition.X) + (-1 * offsetfromcenter.X)), (Math.Abs(panel1.AutoScrollPosition.Y) + (-1 * offsetfromcenter.Y)));
        }
    }
}
