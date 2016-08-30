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
        internal bool canReset = false;
        public Map()
        {
            InitializeComponent();
        }

        private void Map_Load(object sender, EventArgs e)
        {
            canReset = true;
        }

        internal void Map_Paint(object sender, PaintEventArgs e)
        {
            if (canReset == true)
            {
                e.Graphics.Clear(Color.Transparent);
                BackgroundImage = Properties.Resources.Altis_UltraHigh;
            }
            try
            {
                if (players == null) return;
                foreach (Player p in players)
                {
                    SolidBrush myBrush = new SolidBrush(Color.Red);
                    Font font = new Font(FontFamily.GenericSansSerif, 7F, FontStyle.Regular);
                    string[] coords = p.location;
                    if (coords.Length < 2) continue;
                    double altisDim = 30720;
                    double mapHeight = Size.Height;
                    double mapWidth = Size.Width;
                    double coordX = Convert.ToDouble(coords[0]);
                    double coordY = Convert.ToDouble(coords[1]);
                    double newX = ((mapWidth * coordX) / altisDim);
                    double newY = mapHeight - ((mapHeight * coordY) / altisDim);
                    PointF location = new PointF((float)newX, (float)newY);
                    Graphics formGraphics = CreateGraphics();
                    e.Graphics.FillRectangle(myBrush, new RectangleF(location, new Size(4, 4)));
                    e.Graphics.DrawString(p.name, font, new SolidBrush(Color.White), new PointF((float)newX + 2, (float)newY));
                    myBrush.Dispose();
                    formGraphics.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                canReset = false;
            }

        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            lblCoords.Text = string.Format("X:{0} Y:{1}", e.X, e.Y);
        }
    }
}
