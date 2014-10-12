using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Utils;
using Game.CService;

namespace Game.Buisness
{
    public class Customer:IComparable<Customer>
    {
        public String name;
        public int age;     
        double patience;
        Employee handler;
        int totalCalls;
        int goodCalls;
        public Enums.Media pref;
        double income;
        Main par;
        public Company c;
        public double relation;//measures the client's amount of trust towards the employee that handles his calls
        double pHabit;
        public bool called;
        public double Satisfaction
        {
            get
            {
                if (totalCalls == 0) return 1d;
                return System.Math.Sqrt((goodCalls/totalCalls)*relation);
            }
        }
        public bool IsTakenCareOf {
            get {
                return handler != null;
            }        
        }
        public double SpendingHabbits {
            get {
                return System.Math.Sqrt(par.economy.SpendingHabbit * pHabit);
            }
        }
        public double Payment {
            get {
                return income*SpendingHabbits*Satisfaction;
            }
        }
        public Customer(Main game) {                       
            patience = game.ran.NextDouble();
            income = game.ran.Next(Convert.ToInt32(game.economy.MinWage),Convert.ToInt32(game.economy.MaxWage))/1d;
            par = game;
            pref = (Enums.Media)game.ran.Next(4);
            handler = null;
            pHabit = game.ran.NextDouble();
            List<String>dat=game.data.getDataFor(0);
            name = dat[game.ran.Next(dat.Count)];
            goodCalls = 0;
            totalCalls = 0;
            called = false;
        }
        public void AssignTo(Employee w) {
            if (handler == null)
            {
                relation = 0.5;
            }
            else {
                relation = 0.5 - relation;
            }
            handler = w;
        }
        public void GiveCall() {
            double ExpT=Call.NormalReplyTime(pref)*patience;
            called = true;
            Call c = new Call(this, Enums.Media.EMAIL, ExpT,this.par.time.Now);
            this.totalCalls+=1;
            if (handler != null)
            {
                c.PassDown(handler);
            }
            else {
                this.c.AutoAnswer(c);
            }           
        }  
        public void EndCall(Call c) {
            called = false;
            double a = 1 - ((c.ActualT- c.expT)/c.expT);
            if (a >= 0.5)
            {
                goodCalls+=1;
                double reIncr = 0d;
                if (this.handler == null) return;
                if (c.handler == this.handler)
                {
                    reIncr = c.handler.politness;
                }
                else
                {
                    reIncr -= this.handler.politness - c.handler.politness;
                }
                relation += 0.0002 * (reIncr);
            }
        }   
        public void Leave() {
            c.Reputation -= 1 / c.clients.Count;
        }
        public void EndDay() {
            income += par.economy.GDP_Growth * income;  
        }
        public int CompareTo(Customer other)
        {
            return other.Payment.CompareTo(this.Payment);

        }   
    }
}
