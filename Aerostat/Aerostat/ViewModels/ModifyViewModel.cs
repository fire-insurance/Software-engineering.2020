using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace Aerostat.ViewModels
{
   public class ModifyViewModel: Conductor<object>
    {
        private readonly Models.MainModel model = new Models.MainModel();
        private FlightViewModel flightViewModel;

        public Models.MainModel Model
        {
            get
            {
                return model;
            }
        }

        public string CasingVolume
        {
            get
            {
                return model.CasingVolume.ToString();
            }

            set
            {
                int parsed;  
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    if (parsed <= 5000 && parsed != 0) // Установка максимального допустимого значения
                    {
                        model.CasingVolume = parsed;
                    }
                    else if (parsed == 0)
                    {
                        model.CasingVolume = 1;
                    }
                    else
                    {
                        model.CasingVolume = 5000;
                    }
                }

                NotifyOfPropertyChange(() => CasingVolume);
                NotifyOfPropertyChange(() => UpForce);
            }
        }
        public string CasingMass
        {
            get
            {
                return model.CasingMass.ToString();
            }
            set
            {
                int parsed;
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    if (parsed <= 1500 && parsed != 0) // Установка максимального допустимого значения
                    {
                        model.CasingMass = parsed;
                    }
                    else if (parsed == 0)
                    {
                        model.CasingMass = 1;
                    }
                    else
                    {
                        model.CasingMass = 1500;
                    }
                }

                NotifyOfPropertyChange(() => CasingMass);
                NotifyOfPropertyChange(() => AerostatTotalMass);
            }

        }
        public string CasingMaxTemp
        {
            get
            {
                return model.CasingMaxTemp.ToString();
            }
            set
            {
                int parsed;
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    
                    if (parsed <= 100) // Установка максимального допустимого значения
                    {
                        model.CasingMaxTemp = parsed;
                    }
                    else
                    {
                        model.CasingMaxTemp = 100;
                    }
                    model.InsideTemp = model.CasingMaxTemp - 15;
                    model.CasingCurrentTemp = model.CasingMaxTemp - 15;
                }

                NotifyOfPropertyChange(() => CasingMaxTemp);
            }
        }
        public string CasingImage
        {
            get
            {
                return model.CasingType;
            }
        }

        public string HeaterPower
        {
            get
            {
                return model.HeaterPower.ToString();
            }
            set
            {
                int parsed;
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    if (parsed <= 10 && parsed != 0 ) // Установка максимального допустимого значения
                    {
                        model.HeaterPower = parsed;
                    }
                    else if (parsed == 0)
                    {
                        model.HeaterPower = 1;
                    }
                    else
                    {
                        model.HeaterPower = 10;
                    }
                }

                NotifyOfPropertyChange(() => HeaterPower);
            }
        }
        public string HeaterMass
        {
            get
            {
                return model.HeaterMass.ToString();
            }
            set
            {
                int parsed;
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    if (parsed <= 200 && parsed != 0 ) // Установка максимального допустимого значения
                    {
                        model.HeaterMass = parsed;
                    }
                    else if (parsed == 0)
                    {
                        model.HeaterMass = 1;
                    }
                    else
                    {
                        model.HeaterMass = 200;
                    }
                }

                NotifyOfPropertyChange(() => HeaterMass);
                NotifyOfPropertyChange(() => AerostatTotalMass);
            }
        }
        public string HeaterImage
        {
            get
            {
                return model.HeaterType;
            }
        }
        
        public string BasketMass
        {
            get
            {
                return model.BasketMass.ToString();
            }
            set
            {
                int parsed;
                if (int.TryParse(value, out parsed)) //Проверка на валидность ввода не пропускает символы, не являющиеся цифрами
                {
                    if (parsed <= 300 && parsed != 0 ) // Установка максимального допустимого значения
                    {
                        model.BasketMass = parsed;
                    }
                    else if (parsed == 0)
                    {
                        model.BasketMass = 1;
                    }
                    else
                    {
                        model.BasketMass = 300;
                    }
                }

                NotifyOfPropertyChange(() => BasketMass);
                NotifyOfPropertyChange(() => AerostatTotalMass);
            }
        }
        public string BasketImage
        {
            get
            {
                return model.BasketType;
            }
        }

        public string AerostatTotalMass
        {
            get
            {
                NotifyOfPropertyChange(() => GravityForce);
                return "Общая масса " + model.AerostatTotalMass.ToString() + " кг";
            }
        }
        public string GravityForce
        {
            get
            {
                NotifyOfPropertyChange(() => Payload);
                return "Сила тяжести " + model.CountGravityForce().ToString() + " Н";
            }
        }
        public string UpForce
        {
            get
            {
                NotifyOfPropertyChange(() => Payload);
                return "Подъемная сила " + model.CountUpForce().ToString() + " Н";
                
            }
        }
        public string Payload
        {
            get
            {
                return "Полезная нагрузка " + model.CountPayload().ToString() + " кг";
            }
        }


        public ModifyViewModel()
        { }

        public void Start()
        {
            flightViewModel = new FlightViewModel(this);
            ActivateItem(flightViewModel);
        }

        public void NextCasing()
        {
            model.ChangeCasingImg(1);   
            NotifyOfPropertyChange(() => CasingImage);
        }
        public void PreviousCasing()
        {
            model.ChangeCasingImg(0);
            NotifyOfPropertyChange(() => CasingImage);
        }

        public void NextHeater()
        {
            model.ChangeHeaterImg(1);
            NotifyOfPropertyChange(() => HeaterImage);
        }
        public void PreviousHeater()
        {
            model.ChangeHeaterImg(0);
            NotifyOfPropertyChange(() => HeaterImage);
        }

        public void NextBasket()
        {
            model.ChangeBasketImg(1);
            NotifyOfPropertyChange(() => BasketImage);
        }
        public void PreviousBasket()
        {
            model.ChangeBasketImg(0);
            NotifyOfPropertyChange(() => BasketImage);
        }
    }
}
