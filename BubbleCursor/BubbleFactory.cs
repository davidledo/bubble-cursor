using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace BubbleCursor
{
    public class BubbleFactory
    {
        private static Random randomValue = new Random(Guid.NewGuid().GetHashCode());
        private static List<Bubble> currentBubbles;

        private static CompositeTransform BubbleTransformMaker(double width, double height)
        {
            CompositeTransform bubbleTransform = new CompositeTransform();

            double scale = Utils.Map(randomValue.NextDouble(), 0, 1, Constants.SmallestBubbleScale, Constants.LargestBubbleScale);
            bubbleTransform.ScaleX = scale;
            bubbleTransform.ScaleY = scale;
            
            double x = randomValue.NextDouble() * width;
            double y = randomValue.NextDouble() * height;

            double distance = 0;
            while(currentBubbles.Count != 0 
                && distance > 250)
            {
                distance = Utils.DistancesBetweenPointAndBubbles(new Point(x, y), currentBubbles)
                    .First().Value;
                x = randomValue.NextDouble() * width;
                y = randomValue.NextDouble() * height;
            }
            bubbleTransform.TranslateX = x;
            bubbleTransform.TranslateY = y;
            return bubbleTransform;
        }

        private static SolidColorBrush PickRandomColor()
        {
            int index = randomValue.Next(Constants.BubbleColors.Length);
            return Constants.BubbleColors[index];
        }

        public static Bubble CreateBubble(double width, double height, List<Bubble> bubbles)
        {
            currentBubbles = bubbles;
            Ellipse bubbleVisual = new Ellipse()
            {
                Width = Constants.BubbleSize,
                Height = Constants.BubbleSize,
                Fill = PickRandomColor(),
                Opacity = 0.8,
                RenderTransform = BubbleTransformMaker(width, height)
            };
            return new Bubble(bubbleVisual);
        }
    }
}
