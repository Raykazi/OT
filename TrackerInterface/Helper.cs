using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerInterface
{
    //Functions that help the client and server run.
    public static class Helper
    {
        //Convert time to Unix
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
        //Convert to a human readable format
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static string ToJson(string data)
        {
            data = data.Trim('"');
            data = data.Replace('`', '"');
            return data;
        }
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }
        public static float[] performCordScale(float x, float y, PictureBox pb)
        {
            float mapHeight = pb.Height;
            float mapWidth = pb.Width;
            float altisDim = 30720;
            float[] coords = new float[2];
            float coordX = x;
            float coordY = y;
            coords[0] = ((mapWidth * coordX) / altisDim);
            coords[1] = (mapHeight - ((mapHeight * coordY) / altisDim));
            return coords;
        }
        public static float[] performCordScale(string[] location, PictureBox pbMap)
        {
            float coordX = float.Parse(location[0]);
            float coordY = float.Parse(location[1]);
            return Helper.performCordScale(coordX, coordY, pbMap);
        }
        public static List<Location> BuildPoi()
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location { X = 16014.1F, Y = 16959.6F, Name = "Federal Reserve", Color = Color.Yellow }); ;
            /*HQ*/
            locations.Add(new Location { X = 14166.5F, Y = 16267.8F, Name = "Air HQ", Color = Color.Blue });
            locations.Add(new Location { X = 3268.08F, Y = 12968.4F, Name = "Kavala HQ", Color = Color.Blue });
            locations.Add(new Location { X = 13824.8F, Y = 18969F, Name = "Athira HQ", Color = Color.Blue });
            locations.Add(new Location { X = 12409.6F, Y = 14093.9F, Name = "Neochori HQ", Color = Color.Blue });
            locations.Add(new Location { X = 17416.9F, Y = 13177.4F, Name = "Pyrgos HQ", Color = Color.Blue });

            /*Hospitals*/
            locations.Add(new Location { X = 3771.83F, Y = 12985.1F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 15231.5F, Y = 17198.3F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 17522.8F, Y = 13287.8F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 25322F, Y = 20964.6F, Name = "Hospital", Color = Color.Pink });

            /*Black Markets*/
            locations.Add(new Location { X = 11693.3F, Y = 15915.6F, Name = "Black Market", Color = Color.Red });
            locations.Add(new Location { X = 4987.7F, Y = 14552F, Name = "Black Market", Color = Color.Red });
            locations.Add(new Location { X = 18399.6F, Y = 15280.7F, Name = "Black Market", Color = Color.Red });

            /*Rebels */
            locations.Add(new Location { X = 8316.77F, Y = 10065.2F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 15104F, Y = 22619.2F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 21841.4F, Y = 8386.78F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 27648.8F, Y = 23613.2F, Name = "Rebel Outpost", Color = Color.Red });

            /*Fields*/
            locations.Add(new Location { X = 19873F, Y = 17062.1F, Name = "Heroin Field", Color = Color.Red });
            locations.Add(new Location { X = 6210.05F, Y = 16852.6F, Name = "Weed Field", Color = Color.Red });
            locations.Add(new Location { X = 11489.9F, Y = 19568.6F, Name = "Cocaine Field", Color = Color.Red });
            locations.Add(new Location { X = 12339.7F, Y = 22373.4F, Name = "Ephedra Field", Color = Color.Red });
            locations.Add(new Location { X = 8064.69F, Y = 22530.1F, Name = "Phosphorus Field", Color = Color.Red });
            locations.Add(new Location { X = 18967.4F, Y = 12337.1F, Name = "Sugar Cane Field", Color = Color.Orange });
            locations.Add(new Location { X = 17564.8F, Y = 10272F, Name = "Yeast Field", Color = Color.Orange });
            locations.Add(new Location { X = 19654.9F, Y = 8093.64F, Name = "Corn Field", Color = Color.Orange });
            locations.Add(new Location { X = 22400.1F, Y = 13921F, Name = "Cow Manure Field", Color = Color.Orange });

            /*Processors*/
            locations.Add(new Location { X = 9743.13F, Y = 19434.5F, Name = "Oil Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 7855.42F, Y = 16127.6F, Name = "Copper Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 17323.8F, Y = 17434.1F, Name = "Diamond Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 5405.18F, Y = 17905.9F, Name = "Silver Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 12575.1F, Y = 16381.2F, Name = "Sand Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 12833.4F, Y = 15764, Name = "Iron Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 25696.2F, Y = 23554.6F, Name = "Salt Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 9357.22F, Y = 21150.7F, Name = "Platinum Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 19028F, Y = 14565.9F, Name = "Rock Mixing", Color = Color.LightGreen });

            locations.Add(new Location { X = 21257.4F, Y = 11032.1F, Name = "Heroin Processing", Color = Color.Red });
            locations.Add(new Location { X = 8440.54F, Y = 12757.9F, Name = "Cocaine Processing", Color = Color.Red });
            locations.Add(new Location { X = 24600F, Y = 23776.6F, Name = "Magic Mushroom Processing", Color = Color.Red });
            locations.Add(new Location { X = 4202.2F, Y = 11266.7F, Name = "Weed Processing", Color = Color.Red });
            locations.Add(new Location { X = 15407.2F, Y = 15883.5F, Name = "Frog Factory", Color = Color.Red });
            locations.Add(new Location { X = 18700.7F, Y = 6444.88F, Name = "Moonshine Brewery", Color = Color.Red });
            locations.Add(new Location { X = 5506.02F, Y = 19785.8F, Name = "Crystal Meth Lab", Color = Color.Red });

            /*Mines*/
            locations.Add(new Location { X = 24541.3F, Y = 19252.3F, Name = "Salt Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 24541.3F, Y = 19252.3F, Name = "Salt Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 8136.89F, Y = 14452.2F, Name = "Copper Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 18508F, Y = 14303.3F, Name = "Diamond Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 3665.75F, Y = 19775.1F, Name = "Silver Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 16560.9F, Y = 15538F, Name = "Sand Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 10556.7F, Y = 21346.2F, Name = "Lithium Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 4677.2F, Y = 21800.8F, Name = "Platinum Mine", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 7465.58F, Y = 13896.2F, Name = "Oil Tower", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 15515.8F, Y = 14492.5F, Name = "Oil Rig", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 17067.7F, Y = 11353.2F, Name = "Rock Quarry", Color = Color.SaddleBrown });
            locations.Add(new Location { X = 8919.93F, Y = 15523.8F, Name = "Iron Collection", Color = Color.SaddleBrown });

            /*Tutle Poaching*/
            locations.Add(new Location { X = 14856.3F, Y = 12226.4F, Name = "Turtle Poaching", Color = Color.Red });
            locations.Add(new Location { X = 1384.2F, Y = 10635.3F, Name = "Turtle Poaching", Color = Color.Red });
            locations.Add(new Location { X = 14483.2F, Y = 9528.17F, Name = "Turtle Poaching", Color = Color.Red });
            locations.Add(new Location { X = 3597.28F, Y = 8247.33F, Name = "Turtle Poaching", Color = Color.Red });

            /*Traders*/
            locations.Add(new Location { X = 4243.81F, Y = 15026.6F, Name = "Copper Trader", Color = Color.Orange });
            locations.Add(new Location { X = 19294.1F, Y = 16525.6F, Name = "Cement Trader", Color = Color.Orange });
            locations.Add(new Location { X = 10249.8F, Y = 14863.5F, Name = "Glass Trader", Color = Color.Orange });
            locations.Add(new Location { X = 21849.2F, Y = 20924.5F, Name = "Salt Trader", Color = Color.Orange });
            locations.Add(new Location { X = 18371.8F, Y = 15566.4F, Name = "Iron Trader", Color = Color.Orange });
            locations.Add(new Location { X = 6312.38F, Y = 16253.7F, Name = "Oil Trader", Color = Color.Orange });
            locations.Add(new Location { X = 8564.91F, Y = 18169.7F, Name = "Platinum Trader", Color = Color.Orange });
            locations.Add(new Location { X = 9559.76F, Y = 15647.6F, Name = "Silver Trader", Color = Color.Orange });
            locations.Add(new Location { X = 9007.71F, Y = 16057.7F, Name = "Diamond Trader", Color = Color.Orange });
            locations.Add(new Location { X = 17964.1F, Y = 18034.5F, Name = "Moonshine Distributor", Color = Color.Red });

            /*Dealers*/
            locations.Add(new Location { X = 3594.42F, Y = 13829.7F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 14548.4F, Y = 18852.1F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 12642.4F, Y = 14629.4F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 17507.6F, Y = 12324.7F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 26363.7F, Y = 21128.1F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 2627.36F, Y = 9840F, Name = "Turtle Dealer", Color = Color.Red });
            locations.Add(new Location { X = 15034.2F, Y = 11079F, Name = "Turtle Dealer", Color = Color.Red });
            return locations;
        }
        public static void PaintPoi(PaintEventArgs e, PictureBox pbMap)
        {
            foreach (Location l in BuildPoi())
            {
                float[] newCords = performCordScale(l.X, l.Y, pbMap);
                e.Graphics.FillEllipse(new SolidBrush(l.Color), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(12, 12)));
                e.Graphics.DrawString(l.Name, new Font("Tahoma", 9F, FontStyle.Bold), new SolidBrush(l.Color), new PointF(newCords[0] + 12, newCords[1]));
            }
        }
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now, msg));
        }

    }

    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
