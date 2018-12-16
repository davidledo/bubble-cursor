using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace BubbleCursor
{
    public class Bubble
    { 
        private Ellipse visual;
        private CompositeTransform bubbleTransform;
        private bool isDestroyed = false;

        public bool IsDestroyed
        {
            get
            {
                return this.isDestroyed;
            }
            set
            {
                this.isDestroyed = value;
            }
        }

        public Ellipse Visual
        {
            get
            {
                return this.visual;
            }
            set
            {
                this.visual = value;
            }
        }

        public Point? Position
        {
            get
            {
                if(bubbleTransform != null)
                {
                    double x = (this.bubbleTransform.CenterX + (bubbleTransform.ScaleX * this.visual.Width) / 2) + this.bubbleTransform.TranslateX;
                    double y = (this.bubbleTransform.CenterY + (bubbleTransform.ScaleY * this.visual.Height) / 2) + this.bubbleTransform.TranslateY;
                    return new Point(x, y);
                }
                return null;
            }
        }

        public double Radius
        {
            get
            {
                if (bubbleTransform != null)
                {
                    return (this.visual.ActualWidth / 2) * this.bubbleTransform.ScaleX;
                }
                return -1;
            }
        }

        public Storyboard FadeAnimateBubble(double from, double to)
        {
            DoubleAnimation disappearAnimation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(700)),
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut }
            };
            Storyboard popAnimation = new Storyboard();
            Storyboard.SetTargetProperty(disappearAnimation, "Opacity");
            Storyboard.SetTarget(disappearAnimation, this.visual);
            popAnimation.Children.Add(disappearAnimation);
            return popAnimation;
        }

        public void Destroy()
        {
            FadeAnimateBubble(0.9, 0);
        }

        internal Bubble(Ellipse visual)
        {
            this.visual = visual;
            FadeAnimateBubble(0, 0.9).Begin();
            this.bubbleTransform = this.visual.RenderTransform as CompositeTransform;
        }
    }
}
