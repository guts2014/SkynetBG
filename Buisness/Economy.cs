using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Buisness
{
   public class Economy
    {
        double min;//minimum income
        double max;//maximum income
        double sph;//spending habbits
        double inf;//inflation rate
        double demand;//demand for stuff
        double capacity;//capacity of the economy to produce stuff
        double pg;//population growth
        double gdpG;//growth in the economy
        double price;       
        private double slope ;//slope of the supply/demand graph
        public double MinWage {
            get
            {
                return min;
            }
            set
            {
                SpendingHabbit += sph * ((value - min)/min);
                min = value;
            }
        }
        public double MaxWage {
            get
            {
                return max;
            }
            set{
                max = value;
            }
        }
        public double SpendingHabbit {
            get
            {
                return sph;
            }
            set
            {
                sph = value;
                if (sph < 0.4) IncreaseWages();
            }
        }
        public double SavingHabbit {
            get
            {
                return 1d-sph;
            }
            set
            {
                sph=1d-value;
            }
        }
        public double Inflation {
            get {
                return inf;
            }
            set {
                inf = value;
                SpendingHabbit -= inf;
            }
        }
        private double Price {
            get {               
                return price;
            }
            set {
                Inflation = (price - value)/price;
                price = value;
            }
        }
        private double Demand{
            get { 
            
                return demand;
            }
            set {
                demand = value;
            }
        }
        private double Supply {
            get {
                return capacity;
            }
            set {
                gdpG = value - capacity;
                capacity = value;
            }
        
        }
        private double PopulationGrowth {
            get {                
                return pg;
            }
            set{
                pg = value;
            }
        }
        public double GDP_Growth {
            get
            {
                gdpG = (pg + inf)*slope;
                return gdpG;
            }
            set
            {
                gdpG = value;
            }
        }
        public Economy() {
            min = 7f;
            max = 20f;
            sph = 0.6;
            inf = 0.02;
            pg = 0.0011d;//population growth
            capacity = 4000;
            demand = 8000;
            slope = -0.085;//slope of the price/demand graph
            gdpG = 0.02;//gdp growth
            price = (0.2 * (min / capacity)) + min;
        }
        public void SimEconomy() {
            Demand += demand * PopulationGrowth;//Natural growth of the demand based on increase in the population         
            int c = Demand.CompareTo(Supply);
            double newp = 0f;
            switch(c){
                case 1:
                    newp = (0.2 * (min / capacity)) + min;//new price old price+20%
                    double b = price - (slope * capacity);
                    Demand += (slope * capacity) - b;
                    capacity += capacity * GDP_Growth;
                    Inflation = (newp-price)/price;
                    price = newp;
                    break;
                case 0:

                    break;
                case -1:
                    newp = System.Math.Max(price - (0.1 * price),(min/capacity)+min);
                    Inflation = newp;
                    capacity -= capacity * GDP_Growth;
                    break;
            }
            pg -= -(gdpG * slope);
            StandardWageGrowth();        
        }
        private void IncreaseWages() {
            MinWage += (min * Inflation);
            MaxWage += (max * Inflation);           
        }
        private void StandardWageGrowth() {
            MinWage += (min * GDP_Growth);
            MaxWage += (min * GDP_Growth);
        }
    }
}
