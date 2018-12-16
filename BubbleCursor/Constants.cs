using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace BubbleCursor
{
    public class Constants
    {
        public const int MaxBubbles = 60;
        public const int BubbleSize = 20;
        public const double LargestBubbleScale = 4;
        public const double SmallestBubbleScale = 1;

        public const int NumberOfTimers = 7;

        public static SolidColorBrush CursorColor = Utils.GetSolidColorBrush("#FF52616D");

        public static SolidColorBrush Pink = Utils.GetSolidColorBrush("#FFA83F5C");
        public static SolidColorBrush Purple = Utils.GetSolidColorBrush("#FF5C2849");
        public static SolidColorBrush Teal = Utils.GetSolidColorBrush("#FF70B7BA");
        public static SolidColorBrush Blue = Utils.GetSolidColorBrush("#FF0078A4");

        public static SolidColorBrush[] BubbleColors = new[] { Constants.Pink, Constants.Purple, Constants.Teal, Constants.Blue };
    }
}
