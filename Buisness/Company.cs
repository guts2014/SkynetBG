using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.CService;

namespace Game.Buisness
{
    public class Company
    {
        public string name;
        double balance;
        double repBase=0.5f;
        double dbalance;
        public List<Employee> workers = new List<Employee>();
        public List<Customer> clients = new List<Customer>();       
        public double[] loans=new double[2]{0,0};//loan[0]=size,loan[1]=doomsday
        public Main gameSession;
        public double techCo;
        public double comQuality;
        public double penalty = 0d;
        private List<double> otherExp = new List<double>();
        #region"properties"
        public double Reputation {
            get {         
                return repBase + (clients.Sum(item=>item.Satisfaction)/clients.Count)*(workers.Sum(item=>item.Satisfaction)/workers.Count);
            }
           set {
               repBase = value;
           }
        }
       public double Expensess {
           get {
               double wages = workers.Sum(item => item.wage)+otherExp.Sum();
               otherExp.Clear();
               return System.Math.Round(wages,2);
           }       
       }
       public double Income {
           get {
               if (penalty > 0d) return 0f;               
               return System.Math.Round(clients.Sum(item=>item.Payment)*Math.Sqrt(techCo*comQuality),2);
           }       
       }
       public double Balance{
           get {
               return System.Math.Round(balance,2);
           }
           set {
               balance=value;
           }
       }
       public double dBalance {
           get {
               return dbalance;
           }
           set {
               dbalance = value;
           }
       }
        #endregion
       public Company(String name,Main game) {
            this.name = name;
            this.gameSession = game;
            Random ran=new Random();
            this.techCo = ran.NextDouble();
            this.comQuality = ran.NextDouble();
            balance = 50000d;
            for (int i = 0; i < 9; i++)
            {
                clients.Add(new Customer(game) { c = this });
                workers.Add(new Employee(game) { job = this, wage = (double)game.ran.Next((int)game.economy.MaxWage) });
            }

        }        
       
        public void EndDay() {
            penalty = System.Math.Max(penalty - 1, 0);
            dBalance = Income - Expensess;
            loans[1] = System.Math.Max(loans[1],loans[1]-1);
            if (loans[1] <= 0 && loans[0]>0) { balance -= loans[0]; loans[0] -= balance; }
            balance += dbalance ;
        }
        public void GiveResignation(Employee e) {
            workers.Remove(e);
        }
        public List<Customer> UpForGrabs() {
            return clients.FindAll(item => !item.IsTakenCareOf).ToList();
        }
        public void Hire(Employee p, double offer) {
            if (p.Hire(this,offer))workers.Add(p);
        }
        public void Fire(Employee e) {
            e.Fire();
            workers.Remove(e);
        }
        public void UpdateTech(int lvl) {
            otherExp.Add(((lvl/100)-techCo)*1000);
            techCo = lvl / 100;
        }
        public void UpdateComs(int lvl) {
            otherExp.Add(((lvl / 100) - comQuality  * 1500));
            comQuality = lvl / 100;
        }
        public void AssignEmployeeTo(Employee e, Customer c) {
            e.AddClient(c);
            c.AssignTo(e);
        }
        public void TakeLoan(double size) {
            loans[0] += size;
            loans[1] = System.Math.Min(loans[1], loans[0] / dbalance);
            balance += size;
        }
        public void PayLoan(double size) {
            loans[0] -= size;
            balance -= size;
        }
        public void AutoAnswer(Call c) {
            Employee e = workers[gameSession.ran.Next(workers.Count)];
            c.handler = e;
            e.QueeCall(c);
        }
    }
}
