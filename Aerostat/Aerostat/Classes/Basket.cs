using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aerostat.Classes
{
    public class Basket
    {
       
        private string type;
        private int mass;
        private uint typeNumber = 1;

        public int Mass
        {
            get
            {
                return mass;
            }
            set
            {
                mass = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
        }
        public uint TypeNumber
        {
            get
            {
                return typeNumber;
            }
            set
            {
                typeNumber = value;
                type = @"Resources/Baskets/" + typeNumber.ToString() + ".png";
            }
            
    }

        public Basket()
        {
            mass = 90;
            type = @"Resources/Baskets/" + typeNumber.ToString() + ".png";
        }
    }
}
