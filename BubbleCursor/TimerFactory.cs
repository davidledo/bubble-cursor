using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BubbleCursor
{
    public class TimerFactoryEventArgs
    {
        public DispatcherTimer Timer { get; private set; }
        public TimerFactoryEventArgs(DispatcherTimer d)
        {
            this.Timer = d;
        }
    }

    public class TimerFactory
    {
        private static List<DispatcherTimer> timers = new List<DispatcherTimer>();
        private static int timerCount = 0;
        private static DispatcherTimer generatorTimer;

        public static event EventHandler<TimerFactoryEventArgs> TimerCreated;

        private static void RaiseTimerCreated(DispatcherTimer d)
        {
            TimerCreated?.Invoke(null, new TimerFactoryEventArgs(d));
        }

        private static DispatcherTimer CreateTimer()
        {
            return new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(800)
            };
        }

        public static void Start()
        {
            generatorTimer = new DispatcherTimer();
            generatorTimer.Interval = TimeSpan.FromMilliseconds(200);
            generatorTimer.Start();
            generatorTimer.Tick += OnGeneratorTimerTick;
        }

        private static void OnGeneratorTimerTick(object sender, object e)
        {
            if(timerCount < Constants.NumberOfTimers)
            {
                RaiseTimerCreated(CreateTimer());
                timerCount++;
            }
            else
            {
                generatorTimer.Stop();
            }
        }
    }
}
