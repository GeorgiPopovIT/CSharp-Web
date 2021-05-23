using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private List<string> laps;
        private readonly Stopwatch stopwatch;
        public Chronometer()
        {
            this.stopwatch = new Stopwatch();
            this.laps = new List<string>();
        }
        public string GetTime => stopwatch.Elapsed.ToString("mm':'ss'.'ffff");

        public List<string> Laps => this.laps;

        public string Lap()
        {
            this.laps.Add(this.GetTime);
            return this.GetTime;
        }

        public void Reset()
        {
            stopwatch.Reset();
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }
    }
}
