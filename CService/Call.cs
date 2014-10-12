using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Utils;
using Game.Buisness;

namespace Game.CService
{
    public class Call : IComparable<Call>
    {
        public Enums.Media type;
        public double expT;
        public Customer from;
        public int ActualT;
        public int sentAt;
        int timeLeft;
        public Employee handler;
        public double Value() {
            return System.Math.Sqrt((handler.skill.values[(int)type] * Call.TimeCost(type)) * timeLeft);
        }
        public int TimeLeft {
            get {
                return timeLeft;
            }
            set {
                timeLeft = value;
                if (timeLeft <= 0) {
                    ActualT = 0;
                    from.EndCall(this);
                    handler.DropCall(this);
                }
            }
        }
        public Call(Customer from,Enums.Media t,double time,int sent){
            this.type = t;
            this.from = from;
            this.expT = time;
            this.sentAt = sent;
            timeLeft = MaxTimeForReply(t);
        }
        public void PassDown(Employee to) {
            handler = to;
            to.HandleCall(this);
        }       
        private int MaxTimeForReply(Enums.Media t) {
            int val = 60;
            switch (t) { 
                case Enums.Media.EMAIL :
                    val *= 72;
                    break;
                case Enums.Media.HELP_DESK :
                    break;
                case Enums.Media.PHONE :
                    val = 45;
                    break;
                case Enums.Media.POST:
                    val *= 120;
                    break;
                case Enums.Media.SOCIAL_MEDIA :
                    val = 90;
                    break;
            }
            return val;
        }
        /// <summary>
        /// returns normal time it takes to reply to call of type t in mins
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int NormalReplyTime(Enums.Media t) {
            int val = 60;
            switch (t)
            {
                case Enums.Media.EMAIL:
                    val *= 48;
                    break;
                case Enums.Media.HELP_DESK:
                    val = 30;
                    break;
                case Enums.Media.PHONE:
                    val = 30;
                    break;
                case Enums.Media.POST:
                    val *= 72;
                    break;
                case Enums.Media.SOCIAL_MEDIA:
                    break;
            }
            return val;       
        }
        /// <summary>
        /// Time it physically takes an employee to reply in mins
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int TimeCost(Enums.Media t) {
            int val = 5;
            int i = (int)t;
            if (i <= 1) {
                val = val << 1;//10 mins
            }else{
                 val=val<<2;//20 mins
            }
            return val;
        }
        public int CompareTo(Call other)
        {
            return this.Value().CompareTo(other.Value());
            
        }    
    
    }
}
