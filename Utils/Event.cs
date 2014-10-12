using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Utils
{
    public class Event
    {

        public int obj;
        public double probability;
        public String name;
        public double effect;
        private double p;
        public Main sys;
        public Event(string n,int o,double p,double e) {
            name = n;
            obj = o;
            probability = p;
            this.p = probability;
            effect = e;
        }        
        public void UpdateTime(double t) {
            p = 1 - System.Math.Pow((1 - p), 2);
            if (p >= 0.75)p=probability ; sys.SimulateEvent(this);            
        }
        

    }
}
