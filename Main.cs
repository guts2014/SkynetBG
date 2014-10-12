using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Utils;
using Game.Buisness;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using game_gui;

namespace Game
{
    public class Main
    {
        public Clock time;
        public Economy economy;
        public List<Event> eventpool = new List<Event>();
        public List<Employee> jobseekers = new List<Employee>();
        public Company c;
        public DataProvider data;
        public  Random ran;
        public Main(){
           data = new DataProvider();
           ran = new Random();
           time = new Clock(this);
           economy = new Economy();
           eventpool = DataProvider.data2Event(data.getDataFor(3));           
           for (int i = 0; i < 9; i++) jobseekers.Add(new Employee(this));          
           eventpool.ForEach(item => item.sys = this);           
           create cr=new create(this);
           cr.Show();
           time.DayEnded += new Clock.DayEndHandler(EndDay);
          // time.Start();
                   
        }   
        public void EndDay() {
            economy.SimEconomy();
            CollectCalls();
            c.clients.ForEach(item => item.EndDay());
            c.workers.ForEach(item => item.EndDay());
            c.EndDay();
            AttractNewCustomers();
            eventpool.ForEach(item => item.UpdateTime(time.Now));
            Update();
        }
        public void CollectCalls() {
            List<Customer> free = new List<Customer>();
            free=c.clients.Take(c.clients.Count).ToList();
            if (free.Count == 0)return;
            int callers = Convert.ToInt32(0.6 * free.Count);
            while (callers > 0 && free.Count>0) {
                Random ran = new Random();
                Customer cl = free[ran.Next(free.Count)];
                if (!cl.called) {
                    cl.GiveCall();
                    free.Remove(cl);
                    callers--;
                }
            }
        
        }
        public void AttractNewCustomers() { 
            int count=Convert.ToInt32(c.clients.Count*c.Reputation*economy.GDP_Growth);
            for (int i = 0; i < count; i++) c.clients.Add(new Customer(this));
        } 
        
        public void SimulateEvent(Event e) {
             Random ran=new Random();
            switch (e.obj) { 
                case 0:
                    c.Balance -= c.dBalance * e.effect;
                    break;
                case 1:
                    c.Balance -= c.dBalance * e.effect;
                    break;
                case 2:
                    //a customer no like us :(                   
                    c.clients[ran.Next(c.clients.Count)].relation -= e.effect;
                    break;
                case 3:
                    //worker wants something and aint happy that we ignore him
                    c.workers[ran.Next(c.workers.Count)].moral -= e.effect;
                    break;
                case 4:
                    //well its economics' faut agaaaaaaaaaaaain
                    economy.Inflation += 0.4;
                    break;
                case 5:
                    //BUGS IN THE SERVER!
                    c.Balance -= c.dBalance * (c.techCo - e.effect);
                    break;
                case 6:
                    //sorry phone's broken we need to fix it
                    c.Balance -= c.dBalance *(c.comQuality - e.effect);
                    break;
                case 7:
                    //cant work sorry we have other things to take care of today
                    this.c.penalty = e.effect;
                    break;
            
            }
        }
        public void SaveGame() {
            FileStream fs = new FileStream(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+c.name, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            this.time.Stop();
            try
            {
                formatter.Serialize(fs, this);
            }
            catch (SerializationException e)
            {
             }
            finally
            {
                fs.Close();
            }
        }
        public static Main Load() {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
            Main game=null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();            
                game = (Main)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {               
            }
            finally
            {
                fs.Close();
                
            }
            return game;
        }
        public event UpdateUI Update;
        public delegate void UpdateUI();
    
    }
}
