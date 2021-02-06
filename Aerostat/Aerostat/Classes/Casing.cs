using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerostat.Classes
{
    public class Casing
    {
        private int mass;
        private int volume;
        private int maxTemp;
        private double currentTemp;
        private string type;
        private uint typeNumber = 1;

        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
            }
        }
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
        public int MaxTemp
        {
            get
            {
                return maxTemp;
            }
            set
            {
                maxTemp = value;
                currentTemp = value / 2;
            }
        }
        public double CurrentTemp
        {
            get
            {
                return currentTemp;
            }
            set
            {
                currentTemp = value;
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
                type = @"Resources/Casings/" + typeNumber.ToString() + ".png";
            }
        }

        public Casing()
        {
            mass = 250;
            volume = 4000;
            maxTemp = 85;
            currentTemp = maxTemp/2;
            type = @"Resources/Casings/" + typeNumber.ToString() + ".png";
        }

        ~Casing()
        { }




    }
}

