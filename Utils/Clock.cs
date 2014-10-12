using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Game.Utils
{
    public class Clock
    {
        private Timer t;
        private int cycle=1380;
        private double speed;
        private int year;
        private Main sys;
        public int Now {
            get {
              return cycle;
            }
        }
        public int Year {
            get {
                return year;
            }
        }
        public int Day {
            get {
                return (int)(cycle / 1440) % 7;
            }
        }
        public int Hours {
            get {
                return (cycle / 60) % 24; 
            }
        }
        public int Mins
        {
            get
            {
                return cycle % 60;
            }
        }
        public double Speed {
            get {
                return speed;
            }
            set {
                this.speed =System.Math.Max(value,0);
                if (speed == 0) {
                    Stop();
                }
                else if (speed == 1)
                {
                    t.Interval = 100;
                    Start();
                }
                else {
                    t.Interval = speed*100;                    
                }
           
            }
        }
        
        
        public Clock(Main c,double speed=1d) {
            this.speed = speed;
            t = new Timer(speed*100);
            t.Elapsed += this.OnTimedEvent;  
            this.sys = c;
           // t.Enabled = true;
        }
        public void Start() {
            t.Start();
        }
        public void Stop() {
            t.Stop(); 
        }   
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            cycle = System.Math.Min(cycle+1, 525948);
            if (cycle % 525948 == 0) { cycle = 0; year++; }
            if (Hours == 0) {                
                DayEnded();
                cycle += 60;
            }
            Tick();
        }
        public override string ToString() {
            return Year + " y: " + Day + " d: " + Hours + " h: " + Mins + " m x "+(1/speed);
        }

        public event TickHandler Tick;      
        public delegate void TickHandler();
        public event DayEndHandler DayEnded;
        public delegate void DayEndHandler();
    }
}
