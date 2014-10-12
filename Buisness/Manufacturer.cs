using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Buisness
{
    class Manufacturer:Company 
    {
        double k;//efficiency coefficient shows how efficient the factory is
        int capacity;//how much stuff they can make per day
        public Manufacturer() {
            Random r = new Random();
            k = r.NextDouble();
            capacity = r.Next();
        }

    }
}
