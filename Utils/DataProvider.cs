using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Utils
{
    public class DataProvider
    {
        public DataProvider() { 



        }
        //0-names,1-names male,2-names fem,3-events
        /// <summary>
        /// Get data for object
        /// </summary>
        /// <param name="obj">0-last names,1-male names,2-fem names,3-event</param>
        /// <returns></returns>
        public List<String> getDataFor(int obj) {
            string sysPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string[] lines;
            switch (obj) { 
                case 0:
                    lines = System.IO.File.ReadAllLines(sysPath + "\\Last.txt");
                    return lines.ToList();                    
                case 1:
                    lines = System.IO.File.ReadAllLines(sysPath + "\\Male.txt");
                    return lines.ToList();
                case 2:
                    lines = System.IO.File.ReadAllLines(sysPath + "\\Female.txt");
                    return lines.ToList();
                case 3:
                    lines = System.IO.File.ReadAllLines(sysPath + "\\Events.txt");
                    return lines.ToList();
            
            
            }
            return null;
        }
        public static List<Event> data2Event(List<String>data) {
            data = data.FindAll(item => item != "");
            List<Event> el = new List<Event>();
            for (int i = 0; i < data.Count; i++) { 
                string[]line=data[i].Split('/');                
                el.Add(new Event(line[0],int.Parse(line[1]),double.Parse(line[3]),double.Parse(line[2])));
            }
                return el;
        }

    }
}
