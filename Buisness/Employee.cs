using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Utils;
using Game.CService;

namespace Game.Buisness
{
    public class Employee
    {
        public String name;
        public double moral;       
        public int age;
        public int exp;
        public Game.Utils.Enums.Personality p;
        public Enums.Gender g;
        public double stress;
        public double wage=0f;       
        public double politness;
        public SkillsSet skill;
        public Company job;
        Main game;
        public Enums.Media assignemnt;
        List<Customer> clients=new List<Customer>();
        List<Call> quee=new List<Call>();
        double learningRate;//how fast employee can be trained
        double sleeps;
        double workday;
        #region"properties"
        public double Satisfaction {
            get {
                moral += (wage / game.economy.MaxWage)-stress;
                return moral;
            }
        }
        private double TimeAvailable {
            get {
                return workday - quee.Sum(item => Call.TimeCost(item.type));
            }
        }
        private double MoralDecay {
            get { 
            List<Employee>coworkers=job.workers.FindAll(item=>item.p!=this.p&&item.assignemnt==this.assignemnt);
            return 0.0027* coworkers.Sum(item => (int)item.p)/coworkers.Count;//time constant(percent decay per day)*average worker coherance
            }
        }
        public bool IsFree {
            get {               
                return 480d < quee.Sum(item => Call.TimeCost(item.type)) ;
            }
        }
        #endregion
       public Employee(Main g) {
            //this.name = name;
            this.game = g;   
            this.p = (Enums.Personality)g.ran.Next(-3, 3);
            this.g = (Enums.Gender)(g.ran.Next()%2);
            skill = new SkillsSet(p);
            age = g.ran.Next(21, 65);
            this.exp = g.ran.Next(age - 21);
            politness = g.ran.NextDouble();
            this.moral = g.ran.NextDouble();            
            sleeps = 0f;
            learningRate = g.ran.NextDouble();
            List<String> lnames = g.data.getDataFor(0);
            List<String> fnames = g.data.getDataFor(((int)this.g) + 1);
            this.name = fnames[g.ran.Next(fnames.Count)] + " " + lnames[g.ran.Next(fnames.Count)];
            //this.p = ran.Next(0,4);

        }
       public void Fire() {
           double part = 1 / job.workers.Count;
           for (int i = 0; i < job.workers.Count; i++) {
               job.workers[i].moral -= job.workers[i].moral*part*0.02;
           }
       }
       public void Praise() {
           this.moral += 0.05*this.moral;
           for (int i = 0; i < job.workers.Count; i++) {
               if ((int)job.workers[i].p < 0 && (int)this.p > 0) {
                   job.workers[i].moral -= 0.025 * job.workers[i].moral;
               }
           }        
       
       }   
       public void GiveDayOff(int days) {
           sleeps = days;
           stress -= (days*1440)/480;       
       }
       public bool Hire(Company c,double offer) {           
           bool hstatus=((offer/this.DesiredWage()) * c.Reputation)>0.80f;
           if (hstatus)
           {
               job = c;
               wage = offer;
           }
           return hstatus;
       }
       public void ChangeWage(double w) {
           moral += (w - wage) * 0.002;
           this.wage = w;
       }
       public double DesiredWage() {
           double wage=0f;
           double [] bonusBases = new double[5] { 1.04f, 1.57f, 4.18f, 1.57f, 5.23f };
           for (int i = 0; i < skill.values.Length; i++) {
               wage += bonusBases[i] * skill.values[i];
           }
           double wageBase = (game.economy.MinWage * (0.2+game.economy.Inflation)) + game.economy.MinWage;
           return wageBase+(0.0833*this.exp*wage);
       }
       public void HandleCall(Call c) {
           quee.Sort();
           if (clients.Contains(c.from)||c.type==this.assignemnt) {
               c.ActualT =Convert.ToInt32((job.gameSession.time.Now - c.sentAt) + skill.values[(int)c.type] * Call.TimeCost(c.type));              
               c.from.EndCall(c);
               workday -= c.ActualT;
           }           
       }
       public void Train(Enums.Media s,int newlvl) {
           sleeps = 2 / learningRate;
           skill.values[(int)s] = newlvl / 100d;
       }
       public static double CostForTrainingCourse(Enums.Media s,Employee e,int newlevel) {
           double[] pricelist=new double[5]{300d,350d,500d,800d,1000d};
           int i=(int)s;
           return ((newlevel/100d-e.skill.values[i])/e.skill.values[i])*100*pricelist[i];
       }
       public void EndDay() {
           this.moral += MoralDecay;
           if (this.sleeps > 0) {sleeps -= 1; return;}
           if (Satisfaction < 0) {
               job.GiveResignation(this);
           }           
           while (workday > 0 || quee.Count > 0) {
               if (quee.Count == 0)break; 
               quee[0].ActualT = Convert.ToInt32((job.gameSession.time.Now - quee[0].sentAt) + skill.values[(int)quee[0].type] * Call.TimeCost(quee[0].type));
               quee[0].from.EndCall(quee[0]);
               workday -= quee[0].ActualT;
               quee.RemoveAt(0);
           }
           for (int i = 0; i < quee.Count - 1; i++) quee[i].TimeLeft -= game.time.Now - quee[i].sentAt;
           stress += 1 - (workday / 480);
           workday = 480d;
       }
       public void DropCall(Call c) {
           quee.Remove(c);
       }
       public void AddClient(Customer c) {
           clients.Add(c);
       }
       public void QueeCall(Call c) {
           quee.Add(c);
       }
    }

    public class SkillsSet {     
       public double[] values=new double[5];
       public SkillsSet(Enums.Personality p) {
            Random rand = new Random();           
            for (int i = 0; i < 4; i++) {
                values[i] = rand.NextDouble();
            }
      
        }
       public int BestSkill() {
           double maxVal=0d; 
           int ix=0;
           for (int i = 0; i < values.Length; i++) {
               if (values[i] > maxVal) {
                   ix = i;
                   maxVal = values[i];
               }
           }
           return ix;
       }

    }
}
