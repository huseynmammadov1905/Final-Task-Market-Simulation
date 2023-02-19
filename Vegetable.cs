using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTaskMarketSimulation
{
    public class Vegetable
    {
        public Vegetable(string name, double price,int VegsSit)
        {
            this.name = name;
            this.price = price;
            vegetableSituaion = VegsSit;

        }

        public Vegetable() { }



        public string name { get; set; }

        public double price { get; set; }

        public int count = 1;





        public int vegetableSituaion { get; set; } = 1;//1 - 3 -> teze
                                                       //4 - 8 -> normal
                                                       //9 - 10 -> curuk
                                                       //10+ -> curuk



    }
}
