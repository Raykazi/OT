using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TrackerInterface;

namespace TrackerClient
{
    public partial class Map : Form
    {
        internal List<Player> players;
        private Font font = new Font("Tahoma", 7F, FontStyle.Regular);
        internal bool canReset = false;
        public Map()
        {
            InitializeComponent();
        }
        private void Map_Load(object sender, EventArgs e)
        {
            canReset = true;
        }

        private void pbMap_Paint(object sender, PaintEventArgs e)
        {
            Point point = panel1.AutoScrollPosition;
            if (canReset == true)
            {
                e.Graphics.Clear(Color.Transparent);
                pbMap.Image = Properties.Resources.Altis3;
            }
            Helper.PaintPOI(e, pbMap);
            try
            {
                if (players == null) return;
                foreach (Player p in players)
                {
                    if (p.location.Length < 2) continue;
                    float[] newCords = Helper.performCordScale(p.location, pbMap);
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(4, 4)));
                    e.Graphics.DrawString(p.name, font, new SolidBrush(Color.White), new PointF(newCords[0] + 2, newCords[1]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                canReset = false;
            }
        }
        private void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            //Text = string.Format("Map X:{0} Y:{1} Size: {2},{3} ASP {4} {5}", e.X, e.Y, panel1.Size.Height, panel1.Size.Width, panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);

        }

        private void pbMap_MouseClick(object sender, MouseEventArgs e)
        {

        }
        internal void pbMap_CenterPlayer(string[] location)
        {
            float[] newCords = Helper.performCordScale(location, pbMap);
            int X = Convert.ToInt32(newCords[0]);
            int Y = Convert.ToInt32(newCords[1]);
            Point panelcenter = new Point((this.panel1.Width / 2), (this.panel1.Height / 2)); // find the centerpoint of the panel
            Point offsetinpicturebox = new Point((this.pbMap.Location.X + X), (this.pbMap.Location.Y + Y)); // find the offset of the mouse click
            Point offsetfromcenter = new Point((panelcenter.X - offsetinpicturebox.X), (panelcenter.Y - offsetinpicturebox.Y)); // find the difference between the mouse click and the center
            this.panel1.AutoScrollPosition = new Point((Math.Abs(this.panel1.AutoScrollPosition.X) + (-1 * offsetfromcenter.X)), (Math.Abs(this.panel1.AutoScrollPosition.Y) + (-1 * offsetfromcenter.Y)));
        }
    }
}
