using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BubbleCursor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<Bubble> bubbles = new List<Bubble>();
        Ellipse bubbleCursor;
        CompositeTransform cursorTransform = new CompositeTransform();

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += OnProgramLoaded;
        }

        private void CreateBubble()
        {
            Bubble bubble = BubbleFactory.CreateBubble(this.ActualWidth, this.ActualHeight, this.bubbles);
            this.bubbles.Add(bubble);
            this.TargetContainer.Children.Add(bubble.Visual);
        }

        private void InitializeBubbles()
        {
            for(int i = 0; i < Constants.MaxBubbles; i++)
            {
                CreateBubble();
            }
        }

        private void OnProgramLoaded(object sender, RoutedEventArgs e)
        {
            InitializeBubbles();

            TimerFactory.Start();
            TimerFactory.TimerCreated += TimerFactory_TimerCreated;

            //this.timer.Interval = TimeSpan.FromMilliseconds(800);

            InitializeBubbleCursor();
            this.PointerMoved += OnMouseMove;
        }

        private void TimerFactory_TimerCreated(object sender, TimerFactoryEventArgs e)
        {
            e.Timer.Tick += OnTimerTick;
            e.Timer.Start();
        }

        public void InitializeBubbleCursor()
        {
            this.bubbleCursor = new Ellipse()
            {
                Fill = Constants.CursorColor,
                Width = 1, Height = 1
            };
            this.cursorTransform = new CompositeTransform();
            this.bubbleCursor.RenderTransform = this.cursorTransform;
            this.BubbleCursorCanvas.Children.Add(this.bubbleCursor);
        }


        private void UpdateBubbleCursor(Point cursorPosition)
        {
            var x = Utils.DistancesBetweenPointAndBubbles(cursorPosition, this.bubbles);
            Bubble closest = x.First().Key;
            Bubble second = x.ElementAt(1).Key;

            double containmentDistanceClosest = x.First().Value + closest.Radius;
            double intersectingDistanceSecond = x.ElementAt(1).Value - second.Radius;
            
            this.cursorTransform.ScaleX = 2 * Math.Min(containmentDistanceClosest, intersectingDistanceSecond);
            this.cursorTransform.ScaleY = this.cursorTransform.ScaleX;
           
        }

        private void OnMouseMove(object sender, PointerRoutedEventArgs e)
        {
            Point position = e.GetCurrentPoint(this.TargetContainer).Position;
            UpdateBubbleCursor(position);

            this.cursorTransform.TranslateX = position.X - cursorTransform.ScaleX / 2;
            this.cursorTransform.TranslateY = position.Y - cursorTransform.ScaleY / 2;

        }

        private void OnTimerTick(object sender, object e)
        {
            // make a bubble
            CreateBubble();

            // pop a bubble
            Bubble poppingBubble = this.bubbles.Where(b => b.IsDestroyed == false).First();
            poppingBubble.IsDestroyed = true;
            Storyboard popAnimation = poppingBubble.FadeAnimateBubble(0.9, 0);
            popAnimation.Completed += delegate
            {
                this.TargetContainer.Children.Remove(poppingBubble.Visual);
                this.bubbles.Remove(poppingBubble);
            };
            popAnimation.Begin();
        }
    }
}
