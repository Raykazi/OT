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

        public static float[] performCordScale(float x, float y, int height, int width)
        {
            float mapHeight = height;
            float mapWidth = width;
            float altisDim = 30720;
            float[] coords = new float[2];
            float coordX = x;
            float coordY = y;
            coords[0] = ((mapWidth * coordX) / altisDim);
            coords[1] = (mapHeight - ((mapHeight * coordY) / altisDim));
            return coords;
        }
    }

    public class Location
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; set; }
        public Color color { get; set; }
    }
}
