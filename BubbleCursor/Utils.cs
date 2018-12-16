using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace BubbleCursor
{
    public class Utils
    {
        public static double Map(double value, double min, double max, double newMin, double newMax)
        {
            if (value == min)
            {
                return newMin;
            }
            return (((newMax - newMin) / (max - min)) * (value - min)) + newMin;
        }

        /// <summary>
        /// Code source: http://joeljoseph.net/converting-hex-to-color-in-universal-windows-platform-uwp/
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }

        public static double DistanceBetweenTwoPoints(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static Dictionary<Bubble, double> DistancesBetweenPointAndBubbles(Point point, List<Bubble> bubbles)
        {
            var x = bubbles
                .ToDictionary(b => b, d => Utils.DistanceBetweenTwoPoints(d.Position.Value, point))
                .OrderBy(n => n.Value).ToDictionary(key => key.Key, value => value.Value);
            return x;
        }
    }
}
