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
        private List<Location> Locations = new List<Location>();
        private Font font = new Font("Tahoma", 7F, FontStyle.Regular);
        internal bool canReset = false;
        public Map()
        {
            InitializeComponent();
            Locations.Add(new Location { X = 16014.1F, Y = 16959.6F, Name = "Federal Reserve", color = Color.Yellow }); ;
            /*HQ*/
            Locations.Add(new Location { X = 14166.5F, Y = 16267.8F, Name = "Air HQ", color = Color.Blue });
            Locations.Add(new Location { X = 3268.08F, Y = 12968.4F, Name = "Kavala HQ", color = Color.Blue });
            Locations.Add(new Location { X = 13824.8F, Y = 18969F, Name = "Athira HQ", color = Color.Blue });
            Locations.Add(new Location { X = 12409.6F, Y = 14093.9F, Name = "Neochori HQ", color = Color.Blue });
            Locations.Add(new Location { X = 17416.9F, Y = 13177.4F, Name = "Pyrgos HQ", color = Color.Blue });

            /*Hospitals*/
            Locations.Add(new Location { X = 3771.83F, Y = 12985.1F, Name = "Hospital", color = Color.Pink });
            Locations.Add(new Location { X = 15231.5F, Y = 17198.3F, Name = "Hospital", color = Color.Pink });
            Locations.Add(new Location { X = 17522.8F, Y = 13287.8F, Name = "Hospital", color = Color.Pink });
            Locations.Add(new Location { X = 25322F, Y = 20964.6F, Name = "Hospital", color = Color.Pink });

            /*Black Markets*/
            Locations.Add(new Location { X = 11693.3F, Y = 15915.6F, Name = "Black Market", color = Color.Red });
            Locations.Add(new Location { X = 4987.7F, Y = 14552F, Name = "Black Market", color = Color.Red });
            Locations.Add(new Location { X = 18399.6F, Y = 15280.7F, Name = "Black Market", color = Color.Red });

            /*Rebels */
            Locations.Add(new Location { X = 8316.77F, Y = 10065.2F, Name = "Rebel Outpost", color = Color.Red });
            Locations.Add(new Location { X = 15104F, Y = 22619.2F, Name = "Rebel Outpost", color = Color.Red });
            Locations.Add(new Location { X = 21841.4F, Y = 8386.78F, Name = "Rebel Outpost", color = Color.Red });
            Locations.Add(new Location { X = 27648.8F, Y = 23613.2F, Name = "Rebel Outpost", color = Color.Red });

            /*Fields*/
            Locations.Add(new Location { X = 19873F, Y = 17062.1F, Name = "Heroin Field", color = Color.Red });
            Locations.Add(new Location { X = 6210.05F, Y = 16852.6F, Name = "Weed Field", color = Color.Red });
            Locations.Add(new Location { X = 11489.9F, Y = 19568.6F, Name = "Cocaine Field", color = Color.Red });
            Locations.Add(new Location { X = 12339.7F, Y = 22373.4F, Name = "Ephedra Field", color = Color.Red });
            Locations.Add(new Location { X = 8064.69F, Y = 22530.1F, Name = "Phosphorus Field", color = Color.Red });
            Locations.Add(new Location { X = 18967.4F, Y = 12337.1F, Name = "Sugar Cane Field", color = Color.Orange });
            Locations.Add(new Location { X = 17564.8F, Y = 10272F, Name = "Yeast Field", color = Color.Orange });
            Locations.Add(new Location { X = 19654.9F, Y = 8093.64F, Name = "Corn Field", color = Color.Orange });
            Locations.Add(new Location { X = 22400.1F, Y = 13921F, Name = "Cow Manure Field", color = Color.Orange });

            /*Processors*/
            Locations.Add(new Location { X = 9743.13F, Y = 19434.5F, Name = "Oil Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 7855.42F, Y = 16127.6F, Name = "Copper Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 17323.8F, Y = 17434.1F, Name = "Diamond Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 5405.18F, Y = 17905.9F, Name = "Silver Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 12575.1F, Y = 16381.2F, Name = "Sand Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 12833.4F, Y = 15764, Name = "Iron Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 25696.2F, Y = 23554.6F, Name = "Salt Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 9357.22F, Y = 21150.7F, Name = "Platinum Processing", color = Color.LightGreen });
            Locations.Add(new Location { X = 19028F, Y = 14565.9F, Name = "Rock Mixing", color = Color.LightGreen });
            Locations.Add(new Location { X = 21257.4F, Y = 11032.1F, Name = "Heroin Processing", color = Color.Red });
            Locations.Add(new Location { X = 8440.54F, Y = 12757.9F, Name = "Cocaine Processing", color = Color.Red });
            Locations.Add(new Location { X = 24600F, Y = 23776.6F, Name = "Magic Mushroom Processing", color = Color.Red });
            Locations.Add(new Location { X = 4202.2F, Y = 11266.7F, Name = "Weed Processing", color = Color.Red });
            Locations.Add(new Location { X = 15407.2F, Y = 15883.5F, Name = "Frog Factory", color = Color.Red });
            Locations.Add(new Location { X = 18700.7F, Y = 6444.88F, Name = "Moonshine Brewery", color = Color.Red });
            Locations.Add(new Location { X = 5506.02F, Y = 19785.8F, Name = "Crystal Meth Lab", color = Color.Red });

            /*Mines*/
            Locations.Add(new Location { X = 24541.3F, Y = 19252.3F, Name = "Salt Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 24541.3F, Y = 19252.3F, Name = "Salt Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 8136.89F, Y = 14452.2F, Name = "Copper Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 18508F, Y = 14303.3F, Name = "Diamond Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 3665.75F, Y = 19775.1F, Name = "Silver Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 16560.9F, Y = 15538F, Name = "Sand Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 10556.7F, Y = 21346.2F, Name = "Lithium Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 4677.2F, Y = 21800.8F, Name = "Platinum Mine", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 7465.58F, Y = 13896.2F, Name = "Oil Tower", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 15515.8F, Y = 14492.5F, Name = "Oil Rig", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 17067.7F, Y = 11353.2F, Name = "Rock Quarry", color = Color.SaddleBrown });
            Locations.Add(new Location { X = 8919.93F, Y = 15523.8F, Name = "Iron Collection", color = Color.SaddleBrown });

            /*Tutle Poaching*/
            Locations.Add(new Location { X = 14856.3F, Y = 12226.4F, Name = "Turtle Poaching", color = Color.Red });
            Locations.Add(new Location { X = 1384.2F, Y = 10635.3F, Name = "Turtle Poaching", color = Color.Red });
            Locations.Add(new Location { X = 14483.2F, Y = 9528.17F, Name = "Turtle Poaching", color = Color.Red });
            Locations.Add(new Location { X = 3597.28F, Y = 8247.33F, Name = "Turtle Poaching", color = Color.Red });   

            /*Traders*/
            Locations.Add(new Location { X = 4243.81F, Y = 15026.6F, Name = "Copper Trader", color = Color.Orange });
            Locations.Add(new Location { X = 19294.1F, Y = 16525.6F, Name = "Cement Trader", color = Color.Orange });
            Locations.Add(new Location { X = 10249.8F, Y = 14863.5F, Name = "Glass Trader", color = Color.Orange });
            Locations.Add(new Location { X = 21849.2F, Y = 20924.5F, Name = "Salt Trader", color = Color.Orange });
            Locations.Add(new Location { X = 18371.8F, Y = 15566.4F, Name = "Iron Trader", color = Color.Orange });
            Locations.Add(new Location { X = 6312.38F, Y = 16253.7F, Name = "Oil Trader", color = Color.Orange });
            Locations.Add(new Location { X = 8564.91F, Y = 18169.7F, Name = "Platinum Trader", color = Color.Orange });
            Locations.Add(new Location { X = 9559.76F, Y = 15647.6F, Name = "Silver Trader", color = Color.Orange });
            Locations.Add(new Location { X = 9007.71F, Y = 16057.7F, Name = "Diamond Trader", color = Color.Orange });
            Locations.Add(new Location { X = 17964.1F, Y = 18034.5F, Name = "Moonshine Distributor", color = Color.Red });

            /*Dealers*/
            Locations.Add(new Location { X = 3594.42F, Y = 13829.7F, Name = "Drug Dealer", color = Color.Red });
            Locations.Add(new Location { X = 14548.4F, Y = 18852.1F, Name = "Drug Dealer", color = Color.Red });
            Locations.Add(new Location { X = 12642.4F, Y = 14629.4F, Name = "Drug Dealer", color = Color.Red });
            Locations.Add(new Location { X = 17507.6F, Y = 12324.7F, Name = "Drug Dealer", color = Color.Red });
            Locations.Add(new Location { X = 26363.7F, Y = 21128.1F, Name = "Drug Dealer", color = Color.Red });
            Locations.Add(new Location { X = 2627.36F, Y = 9840F, Name = "Turtle Dealer", color = Color.Red });
            Locations.Add(new Location { X = 15034.2F, Y = 11079F, Name = "Turtle Dealer", color = Color.Red });
        }
        private void Map_Load(object sender, EventArgs e)
        {
            canReset = true;
        }
        public float[] performCordScale(string[] location)
        {
            float coordX = float.Parse(location[0]);
            float coordY = float.Parse(location[1]);
            return performCordScale(coordX, coordY);
        }
        public float[] performCordScale(float x, float y)
        {
            float mapHeight = pbMap.Height;
            float mapWidth = pbMap.Width;
            float altisDim = 30720;
            float[] coords = new float[2];
            float coordX = x;
            float coordY = y;
            coords[0] = ((mapWidth * coordX) / altisDim);
            coords[1] = (mapHeight - ((mapHeight * coordY) / altisDim));
            return coords;
        }
        private void PaintLocations(PaintEventArgs e)
        {
            foreach (Location l in Locations)
            {
                float[] newCords = performCordScale(l.X, l.Y);
                e.Graphics.FillEllipse(new SolidBrush(l.color), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(12, 12)));
                e.Graphics.DrawString(l.Name, new Font("Tahoma", 9F, FontStyle.Bold), new SolidBrush(l.color), new PointF(newCords[0] + 12, newCords[1]));
            }
        }

        private void pbMap_Paint(object sender, PaintEventArgs e)
        {
            if (canReset == true)
            {
                e.Graphics.Clear(Color.Transparent);
                pbMap.Image = Properties.Resources.Altis3;
            }
            PaintLocations(e);
            try
            {
                if (players == null) return;
                foreach (Player p in players)
                {
                    if (p.location.Length < 2) continue;
                    float[] newCords = performCordScale(p.location);
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
            Text = string.Format("Map X:{0} Y:{1}", e.X, e.Y);

        }
    }
    class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; set; }
        public Color color { get; set; }
    }
}
