using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Buisness
{
    class Product
    {
        String name;
        float price;
        float cost;
        Manufacturer m;
        Company owner;
        public Product(String name,Company owner) {
            this.name = name;
            this.owner = owner;            
        }
        public void MakeIn(Company p) {
            //this.m = p;
        }
    }
}
