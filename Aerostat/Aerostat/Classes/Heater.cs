using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerostat.Classes
{
    class Heater
    {
        private int mass;
        private double power;
        private double currentPower;
        private string type;
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
        public double Power
        {
            get
            {
                return power;
            }
            set
            {
                power = value;
            }
        }
        public double CurrentPower
        {
            get
            {
                return currentPower;
            }
            set
            {
                currentPower = value;
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
                type = @"Resources/Heaters/" + typeNumber.ToString() + ".png";
            }
        }

        public Heater()
        {
            mass = 25;
            power = 5;
            currentPower = power/2;
            type = @"Resources/Heaters/" + typeNumber.ToString() + ".png";
        }
    }
}