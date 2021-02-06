using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerostat.Classes
{
    public class Aerostat
    {
        private int totalMass;
        private double insideTemp;
        private double payload;
        private readonly Casing casing = new Casing();
        private readonly Heater heater = new Heater();
        private readonly Basket basket = new Basket();

        public int TotalMass
        {
            get
            {
                return totalMass;
            }
            set
            {
                totalMass = value;
            }
        }
        public double InsideTemp
        {
            get
            {
                return insideTemp;
            }
            set
            {
                insideTemp = value;
            }
        }
        public double Payload
        {
            get
            {
                return payload;
            }
            set
            {
                payload = value;
            }
        }

        public int CasingVolume
        {
            get
            {
                return casing.Volume;
            }
            set
            {
                casing.Volume = value;
            }
        }
        public int CasingMass
        {
            get
            {
                return casing.Mass;
            }
            set
            {
                casing.Mass = value;
            }
        }
        public int CasingMaxTemp
        {
            get
            {
                return casing.MaxTemp;
            }
            set
            {
                casing.MaxTemp = value;
            }
        }
        public double CasingCurrentTemp
        {
            get
            {
                return casing.CurrentTemp;
            }
            set
            {
                casing.CurrentTemp = value;
            }
        }
        public string CasingType
        {
            get
            {
                return casing.Type;
            }
        }
        public uint CasingTypeNumber
        {
            get
            {
                return casing.TypeNumber;
            }
            set
            {
                casing.TypeNumber = value;
            }
        }

        public double HeaterPower
        {
            get
            {
                return heater.Power;
            }
            set
            {
                heater.Power = value;
            }
        }
        public int HeaterMass
        {
            get
            {
                return heater.Mass;
            }
            set
            {
                heater.Mass = value;
            }
        }
        public double HeaterCurrentPower
        {
            get
            {
                return heater.CurrentPower;
            }
            set
            {
                heater.CurrentPower = value;
            }
        }
        public string HeaterType
        {
            get
            {
                return heater.Type;
            }
        }
        public uint HeaterTypeNumber
        {
            get
            {
                return heater.TypeNumber;
            }
            set
            {
                heater.TypeNumber = value;
            }
        }

        public int BasketMass
        {
            get
            {
                return basket.Mass;
            }
            set
            {
                basket.Mass = value;
            }
        }
        public string BasketType
        {
            get
            {
                return basket.Type;
            }
        }
        public uint BasketTypeNumber
        {
            get
            {
                return basket.TypeNumber;
            }
            set
            {
                basket.TypeNumber = value;
            }
        }

        public Aerostat()
        {
            totalMass = casing.Mass + heater.Mass + basket.Mass;
            insideTemp = casing.MaxTemp/2;
        }
        
    }
}
