using System;
using System.Threading;

namespace Aerostat.Models
{
    public class MainModel
    {
        private readonly Classes.Aerostat aerostat = new Classes.Aerostat();
        private readonly double accelerationOfGravity = 9.81;
        private int atmosphericPressure;
        private double outsideTemp;
        private double aerostatAcceleration;
        private double currentHeight;
        private double ascendingSpeed;
        private int pixelsUp;
        private double deltaHeight = 0;
        private double deltaTemp;
        private double airCoolingCoefficient;
        private bool overheat = false;
        private double airMass;
        private double airDensity;
        private double energyNeeded; // Q = c * m * dt - энергия на нагрев воздуха в оболочке на 1 градус
        private double energyGiven; // Энергия, которую передает горелка аэростата
        private Timer timer;

        public int CasingVolume
        {
            get
            {
                return aerostat.CasingVolume;
            }
            set
            {
                aerostat.CasingVolume = value;
            }
        }
        public int CasingMass
        {
            get
            {
                return aerostat.CasingMass;
            }
            set
            {
                aerostat.CasingMass = value;
                RecountTotalMass();
            }
        }
        public int CasingMaxTemp
        {
            get
            {
                return aerostat.CasingMaxTemp;
            }
            set
            {
                aerostat.CasingMaxTemp = value;
            }
        }
        public double CasingCurrentTemp
        {
            get
            {
                return aerostat.CasingCurrentTemp;
            }
            set
            {
                aerostat.CasingCurrentTemp = value;
            }
        }
        public string CasingType
        {
            get
            {
                return aerostat.CasingType;
            }
        }
        public uint CasingTypeNumber
        {
            get
            {
                return aerostat.CasingTypeNumber;
            }

            set
            {
                aerostat.CasingTypeNumber = value;

            }
        }

        public double HeaterPower
        {
            get
            {
                return aerostat.HeaterPower;
            }
            set
            {
                aerostat.HeaterPower = value;
            }
        }
        public double HeaterCurrentPower
        {
            get
            {
                return aerostat.HeaterCurrentPower;
            }
            set
            {
                aerostat.HeaterCurrentPower = value;
            }
        }
        public int HeaterMass
        {
            get
            {
                return aerostat.HeaterMass;
            }
            set
            {
                aerostat.HeaterMass = value;
                RecountTotalMass();
            }
        }
        public string HeaterType
        {
            get
            {
                return aerostat.HeaterType;
            }
        }
        public uint HeaterTypeNumber
        {
            get
            {
                return aerostat.HeaterTypeNumber;
            }
            set
            {
                aerostat.HeaterTypeNumber = value;
            }
        }

        public int BasketMass
        {
            get
            {
                return aerostat.BasketMass;
            }
            set
            {
                aerostat.BasketMass = value;
                RecountTotalMass();
            }
        }
        public string BasketType
        {
            get
            {
                return aerostat.BasketType;
            }
        }
        public uint BasketTypeNumber
        {
            get
            {
                return aerostat.BasketTypeNumber;
            }
            set
            {
                aerostat.BasketTypeNumber = value;
            }
        }

        public int AerostatTotalMass
        {
            get
            {
                return aerostat.TotalMass;
            }
            set
            {
                aerostat.TotalMass = value;
            }
        }
        public int AtmosphericPressure
        {
            get
            {
                return atmosphericPressure;
            }

        }
        public double OutsideTemp
        {
            get
            {
                return outsideTemp;
            }
        }
        public double InsideTemp
        {
            get
            {
                return aerostat.InsideTemp;
            }
            set
            {
                aerostat.InsideTemp = value;
            }
        }
        public int UpForce
        {
            get
            {
                return CountUpForce();
            }
        }
        public double CurrentHeight
        {
            get
            {
                return currentHeight;
            }
        }
        public int PixelsUp
        {
            get
            {
                return pixelsUp;
            }
        }
        public bool Overheat
        {
            get
            {
                return overheat;
            }
            set
            {
                overheat = value;
            }
        }

        public MainModel()
        {
            SetDefaultParameters();
        }

        public void SetDefaultParameters()
        {
            double densityСoefficient;

            currentHeight = 0;
            pixelsUp = 0;
            outsideTemp = 20;
            atmosphericPressure = 760;
            ascendingSpeed = 0;
            aerostatAcceleration = 0;

            densityСoefficient = ((100 - CasingCurrentTemp)) / 10;
            InsideTemp = CasingMaxTemp * 0.65;
            CasingCurrentTemp = InsideTemp - 5;
            HeaterCurrentPower = HeaterPower / 2.0;
            airDensity = 0.946 + densityСoefficient * 0.03;
            energyGiven = 0;
            energyNeeded = 0;
            deltaHeight = 0;
            deltaTemp = 0;
            airCoolingCoefficient = 0.0;
        }

        public void ChangeCasingImg(uint direction) // Функция смены изображения оболочки
        {
            uint CasingImageQuantity = 3;

            if (direction == 1) //Если нажата кнопка выбора следующей оболочки
            {
                if (CasingTypeNumber < CasingImageQuantity)
                {
                    CasingTypeNumber++;
                }
                else
                {
                    CasingTypeNumber = 1;
                }
            }
            if (direction == 0) // Если нажата кнопка выбора предыдущей оболочки
            {
                if (CasingTypeNumber > 1)
                {
                    CasingTypeNumber--;
                }
                else
                {
                    CasingTypeNumber = CasingImageQuantity;
                }
            }
        }
        public void ChangeHeaterImg(uint direction) // Функция смены изображения горелки
        {
            uint HeaterImageQuantity = 3;

            if (direction == 1) // Если нажата кнопка выбора следующей горелки
            {
                if (HeaterTypeNumber < HeaterImageQuantity)
                {
                    HeaterTypeNumber++;
                }
                else
                {
                    HeaterTypeNumber = 1;
                }
            }

            if (direction == 0) // Если нажата кнопка выбора предыдущей горелки
            {
                if (HeaterTypeNumber > 1)
                {
                    HeaterTypeNumber--;
                }
                else
                {
                    HeaterTypeNumber = HeaterImageQuantity;
                }
            }
        }
        public void ChangeBasketImg(uint direction) // Функция смены изображения корзины
        {
            uint BasketImageQuantity = 3;

            if (direction == 1) // Если нажата кнопка выбора следующей корзины
            {
                if (BasketTypeNumber < BasketImageQuantity)
                {
                    BasketTypeNumber++;
                }
                else
                {
                    BasketTypeNumber = 1;
                }
            }

            if (direction == 0) // Если нажата кнопка выбора предыдущей горелки
            {
                if (BasketTypeNumber > 1)
                {
                    BasketTypeNumber--;
                }
                else
                {
                    BasketTypeNumber = BasketImageQuantity;
                }
            }
        }

        public void RecountTotalMass()
        {
            AerostatTotalMass = CasingMass + HeaterMass + BasketMass;
        }
        public int CountGravityForce()
        {
            int GravityForce;

            GravityForce = (int)Math.Round(AerostatTotalMass * accelerationOfGravity);

            return GravityForce;
        }
        public int CountUpForce()   
        {
            int upforce;
            double otk = (double)outsideTemp + 273.15; //Пересчет температуры в кельвинах
            double oik = (double)InsideTemp + 273.15;

            upforce = (int)Math.Round(aerostat.CasingVolume * 4.0 * atmosphericPressure * (1.0 / otk - 1.0 / oik)); // Формула подъемной силы шара (согласно вики)
            
            aerostatAcceleration = ((double)(upforce - CountGravityForce() ) / (double)AerostatTotalMass); // F = ma => a = F/m
            if (currentHeight <= 1 && aerostatAcceleration <=0)
            {
                aerostatAcceleration = 0;
                return upforce;
            }

            return upforce;
        }
        public int CountPayload()
        {
            aerostat.Payload = (CountUpForce() - CountGravityForce()) / accelerationOfGravity;

            return (int)Math.Round(aerostat.Payload);
        }

        public void StartTimer()
        {
            timer = new System.Threading.Timer(RecountFlightParameters, null, 0, 1000);
        }
        public void Restart()
        {
            SetDefaultParameters();
            if(timer == null)
            {
                return; 
            }
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        private void RecountFlightParameters(object state)
        {       
            ascendingSpeed += aerostatAcceleration/10;
            currentHeight += ascendingSpeed;
            pixelsUp = (int)Math.Round(currentHeight * 5);
            deltaHeight += ascendingSpeed;

            if (deltaHeight >= 50)
            {
                deltaHeight = 0;
                outsideTemp -= 0.15;
                atmosphericPressure = (int)Math.Round(760 * Math.Exp(-0.02896 * accelerationOfGravity * currentHeight / (8.314 * (outsideTemp + 273.15))));
                CountUpForce();
            }

            if (deltaHeight <= -50)
            {
                deltaHeight = 0;
                outsideTemp += 0.15;
                atmosphericPressure = (int)Math.Round(760 * Math.Exp(-0.02896 * accelerationOfGravity * currentHeight / (8.314 * (outsideTemp + 273.15))));
                CountUpForce();
            }
            
            ActivateHeater();

            if (currentHeight + ascendingSpeed >= 10000 && ascendingSpeed > 0)
            {
                currentHeight = 10000;
                pixelsUp = (int)Math.Round(currentHeight * 5);
                CountUpForce();
                return;
            }
            if (currentHeight <= 30 && ascendingSpeed < 0)
            {
                currentHeight = 0;
                ascendingSpeed = 0;
                ActivateHeater();
                CountUpForce();
                return;
            }
        }
        private void ActivateHeater()
        {
            
            deltaTemp += Math.Round(energyGiven / energyNeeded, 1) - airCoolingCoefficient; // Изменять плотность воздуха каждые +-10 градусов температуры воздуха в оболочке

            if (deltaTemp >= 10.0)
            {
                deltaTemp = 0.0;
                airDensity -= 0.03;
            }

            if (deltaTemp <= -10.0)
            {
                deltaTemp = 0.0;
                airDensity += 0.03;
            }

            airMass = airDensity * aerostat.CasingVolume;

            energyNeeded = 0.717 * airMass; // В Джоулях
            energyGiven = HeaterCurrentPower * 1000000 / 3600; // Вт*секунду = Джоуль

            if (aerostat.InsideTemp - outsideTemp >= 50)
            {
                airCoolingCoefficient = 0.3;                           // 0.3 -- теплопотери. Так, при выключенной горелке воздух в оболочке будет остывать на 0.1 град/сек
            }                                                         // При этом, чем больше разница температур внутри шара и снаружи, тем быстрее остывает 
            else if (aerostat.InsideTemp - outsideTemp >= 75)        // воздух внутри шара, и, соответственно, оболочка
            {
                airCoolingCoefficient = 0.6;
            }
            else
            {
                airCoolingCoefficient = 0.1;
            }

            aerostat.InsideTemp += Math.Round(energyGiven / energyNeeded, 1) - airCoolingCoefficient;
            aerostat.CasingCurrentTemp = aerostat.InsideTemp - 5;

            if (CasingCurrentTemp >= CasingMaxTemp + 1)
            {
                overheat = true;
            }

        }
    }
}
