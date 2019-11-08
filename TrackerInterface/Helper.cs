using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerInterface
{
    //Functions that help the client and server run.
    public static class Helper
    {
        /// <summary>
        /// Convert time to Unix format
        /// </summary>
        /// <param name="date">DateTime object to convert</param>
        /// <returns>Unix formatted time</returns>
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        /// <summary>
        /// Convert Unix time to Readable time
        /// </summary>
        /// <param name="unixTime">Unix time to convert</param>
        /// <returns>DateTime formatted time</returns>
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
        /// <summary>
        /// Decode the location string, format it into X,Y,Z format
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string[] ParseLocation(string location)
        {
            //location = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(location));
            location = location.Remove(0, 2);
            location = location.Remove(location.IndexOf("]"));
            return location.Split(',');
        }
        /// <summary>
        /// Cleanup strings to convert to JSON
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToJson(string data)
        {
            data = data.Trim('"');
            data = data.Replace('`', '"');
            return data;
        }
        /// <summary>
        /// Assumming this converts it into SQL format. Not 100% sure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToSQL<T>(List<T> list)
        {
            return list.Aggregate("", (current, item) => current + (item + ","));
        }
        /// <summary>
        /// Converts string to SQL
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToSQL(string[] list)
        {
            var result = list.Aggregate("", (current, s) => current + (s + ","));
            result.Insert(0, "[");
            result.Insert(result.Length, "]");
            return result;
        }
        /// <summary>
        /// Converts String to Base 64
        /// </summary>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Used to clean up the crate string for houses
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (var index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }
        /// <summary>
        /// Scales given coordinates to match the map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pb"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Scales given coordinates to match the map
        /// </summary>
        /// <param name="location"></param>
        /// <param name="pbMap"></param>
        /// <returns></returns>
        public static float[] performCordScale(string[] location, PictureBox pbMap)
        {
            float coordX = float.Parse(location[0]);
            float coordY = float.Parse(location[1]);
            return performCordScale(coordX, coordY, pbMap);
        }
        /// <summary>
        /// Collection of POI 
        /// </summary>
        /// <returns></returns>
        public static List<Location> BuildPoi()
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location { X = 16014.1F, Y = 16959.6F, Name = "Federal Reserve", Color = Color.Yellow }); ;
            /*HQ*/
            locations.Add(new Location { X = 14166.5F, Y = 16267.8F, Name = "Air HQ", Color = Color.Blue });
            locations.Add(new Location { X = 3268.08F, Y = 12968.4F, Name = "Kavala HQ", Color = Color.Blue });
            locations.Add(new Location { X = 13797.1F, Y = 18947.6F, Name = "Athira HQ", Color = Color.Blue });
            locations.Add(new Location { X = 12436.5F, Y = 14128.8F, Name = "Neochori HQ", Color = Color.Blue });
            locations.Add(new Location { X = 17416.9F, Y = 13177.4F, Name = "Pyrgos HQ", Color = Color.Blue });

            /*Hospitals*/
            locations.Add(new Location { X = 3743.86F, Y = 13002.1F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 15231.5F, Y = 17198.3F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 16951.7F, Y = 12108.6F, Name = "Hospital", Color = Color.Pink });
            locations.Add(new Location { X = 25322.9F, Y = 20997F, Name = "Hospital", Color = Color.Pink });

            /*Dealers*/
            locations.Add(new Location { X = 15170.8F, Y = 18684.4F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 12401.5F, Y = 14822.7F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 17413.6F, Y = 12129.8F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 27858.6F, Y = 21646.3F, Name = "Drug Dealer", Color = Color.Red });
            locations.Add(new Location { X = 3265.43F, Y = 14118.1F, Name = "Drug Dealer", Color = Color.Red });

            locations.Add(new Location { X = 3550.69F, Y = 14003.1F, Name = "Turtle Dealer", Color = Color.Red });
            locations.Add(new Location { X = 16405.5F, Y = 12683.6F, Name = "Turtle Dealer", Color = Color.Red });

            /*Black Markets*/
            locations.Add(new Location { X = 5238.88F, Y = 14790.7F, Name = "Black Market", Color = Color.Red });
            locations.Add(new Location { X = 18398.7F, Y = 15284.9F, Name = "Black Market", Color = Color.Red });
            locations.Add(new Location { X = 11802.2F, Y = 16503.3F, Name = "Black Market", Color = Color.Red });

            /*Rebels */
            locations.Add(new Location { X = 8312.86F, Y = 10067.1F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 27648.3F, Y = 23612.8F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 14191F, Y = 21230.8F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 12291.4F, Y = 8864.7F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 10253.6F, Y = 10368.1F, Name = "Rebel Outpost", Color = Color.Red });
            locations.Add(new Location { X = 21861.5F, Y = 10971.5F, Name = "Rebel Outpost", Color = Color.Red });
            /*Rebels BS*/
            //locations.Add(new Location { X = 27648.8F, Y = 23613.2F, Name = "Rebel Boat Shop", Color = Color.Red });

            /*Fields*/
            locations.Add(new Location { X = 19724.9F, Y = 17009.5F, Name = "Heroin Field", Color = Color.Red });
            locations.Add(new Location { X = 6209.9F, Y = 16852.7F, Name = "Weed Field", Color = Color.Red });
            locations.Add(new Location { X = 8560, Y = 19125.6F, Name = "Cocaine Field", Color = Color.Red });
            locations.Add(new Location { X = 13012.6F, Y = 22375.9F, Name = "Ephedra Field", Color = Color.Red });
            locations.Add(new Location { X = 8064.59F, Y = 22529.9F, Name = "Phosphorus Field", Color = Color.Red });
            locations.Add(new Location { X = 18967.4F, Y = 12337.1F, Name = "Sugar Cane Field", Color = Color.Orange });
            locations.Add(new Location { X = 17564.8F, Y = 10272F, Name = "Yeast Field", Color = Color.Orange });
            locations.Add(new Location { X = 19654.9F, Y = 8093.64F, Name = "Corn Field", Color = Color.Orange });
            locations.Add(new Location { X = 25053.7F, Y = 18857.9F, Name = "Cow Manure Field", Color = Color.Orange });

            /*Processors*/
            locations.Add(new Location { X = 6034.12F, Y = 16186.7F, Name = "Oil Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 5451.21F, Y = 14227.9F, Name = "Copper Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 17327, Y = 17436.5F, Name = "Diamond Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 16100F, Y = 21264.3F, Name = "Silver Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 12543.6F, Y = 16306.3F, Name = "Sand Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 10150.8F, Y = 15050.5F, Name = "Iron Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 23226F, Y = 21854F, Name = "Salt Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 7067.01F, Y = 11511.3F, Name = "Platinum Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 19031.5F, Y = 14567.1F, Name = "Rock Processing", Color = Color.LightGreen });
            locations.Add(new Location { X = 23935.7F, Y = 16076.1F, Name = "Heroin Processing", Color = Color.Red });
            locations.Add(new Location { X = 5391.96F, Y = 17903.1F, Name = "Cocaine Processing", Color = Color.Red });
            locations.Add(new Location { X = 24589.8F, Y = 23780.6F, Name = "Magic Mushroom Processing", Color = Color.Red });
            locations.Add(new Location { X = 6453.96F, Y = 14668F, Name = "Weed Processing", Color = Color.Red });
            locations.Add(new Location { X = 18787.9F, Y = 18207.7F, Name = "Frog Processing", Color = Color.Red });       
            locations.Add(new Location { X = 18700.7F, Y = 6444.88F, Name = "Moonshine Brewery", Color = Color.Red });
            locations.Add(new Location { X = 5515.75F, Y = 19759F, Name = "Crystal Meth Lab", Color = Color.Red });

            /*Mines*/
            locations.Add(new Location { X = 23559.4F, Y = 19351.2F, Name = "Salt Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 5926.82F, Y = 12444.5F, Name = "Copper Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 18508F, Y = 14303.3F, Name = "Diamond Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 16010F, Y = 18160.5F, Name = "Silver Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 14323.6F, Y = 15374.2F, Name = "Sand Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 10551F, Y = 21346.7F, Name = "Lithium Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 4511.84F, Y = 10521.2F, Name = "Platinum Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 9264.14F, Y = 13852.7F, Name = "Oil Tower", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 17049.5F, Y = 11341.7F, Name = "Rock Mine", Color = Color.MediumSeaGreen });
            locations.Add(new Location { X = 8924.56F, Y = 15525.4F, Name = "Iron Mine", Color = Color.MediumSeaGreen });

            /*Tutle Poaching*/
            locations.Add(new Location { X = 1502.52F, Y = 11703.7F, Name = "Turtle Poaching", Color = Color.Red });
            locations.Add(new Location { X = 13758.8F, Y = 11640.9F, Name = "Turtle Poaching", Color = Color.Red });

            /*Traders*/
            locations.Add(new Location { X = 4382.27F, Y = 12612.5F, Name = "Copper Trader", Color = Color.Orange });
            locations.Add(new Location { X = 19293.8F, Y = 16525F, Name = "Cement Trader", Color = Color.Orange });
            locations.Add(new Location { X = 12292.4F, Y = 14132.9F, Name = "Glass Trader", Color = Color.Orange });
            locations.Add(new Location { X = 24561.1F, Y = 21081F, Name = "Salt Trader", Color = Color.Orange });
            locations.Add(new Location { X = 12783F, Y = 15709.9F, Name = "Iron Trader", Color = Color.Orange });
            locations.Add(new Location { X = 9009.23F, Y = 16076.9F, Name = "Oil Trader", Color = Color.Orange });
            locations.Add(new Location { X = 10787.2F, Y = 12375.7F, Name = "Platinum Trader", Color = Color.Orange });
            locations.Add(new Location { X = 13675.4F, Y = 18662.1F, Name = "Silver Trader", Color = Color.Orange });
            locations.Add(new Location { X = 20858.6F, Y = 16876.2F, Name = "Diamond Trader", Color = Color.Orange });
            locations.Add(new Location { X = 16682.8F, Y = 12449.6F, Name = "Salvage Trader", Color = Color.Orange });
            locations.Add(new Location { X = 12659.8F, Y = 14205.1F, Name = "Salvage Trader", Color = Color.Orange });
            locations.Add(new Location { X = 3443.57F, Y = 12660.9F, Name = "Salvage Trader", Color = Color.Orange });
            locations.Add(new Location { X = 22150.8F, Y = 13610.5F, Name = "Moonshine Distributor", Color = Color.Red });
            locations.Add(new Location { X = 18839.3F, Y = 15617.1F, Name = "Gold Trader", Color = Color.Orange });
            locations.Add(new Location { X = 18070.5F, Y = 19188.5F, Name = "Gold Trader", Color = Color.Orange });
            locations.Add(new Location { X = 12553.8F, Y = 16421.1F, Name = "Gold Trader", Color = Color.Orange });

            return locations;
        }
        /// <summary>
        /// Draws the POI on the map
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pbMap"></param>
        public static void PaintPoi(PaintEventArgs e, PictureBox pbMap)
        {
            foreach (Location l in BuildPoi())
            {
                float[] newCords = performCordScale(l.X, l.Y, pbMap);
                e.Graphics.FillEllipse(new SolidBrush(l.Color), new RectangleF(new PointF(newCords[0], newCords[1]), new Size(12, 12)));
                e.Graphics.DrawString(l.Name, new Font("Tahoma", 9F, FontStyle.Bold), new SolidBrush(l.Color), new PointF(newCords[0] + 12, newCords[1]));
            }
        }
        /// <summary>
        /// Logs output with a timestamp
        /// </summary>
        /// <param name="msg">Message to output</param>
        public static void ConsoleLog(string msg)
        {
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now, msg));
        }

    }
    /// <summary>
    /// Class that handles locations of POI
    /// </summary>
    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
    }
}
