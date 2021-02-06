using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Threading;
using System.Windows.Threading;
using static System.Windows.Application;

namespace Aerostat.ViewModels
{
    public class FlightViewModel : Conductor<object>
    {

        private readonly Models.MainModel model;
        private readonly ModifyViewModel modifyViewModel;
        private readonly string casingImage;
        private readonly string heaterImage;
        private readonly string basketImage;
        private string visibility = "Hidden";
        private string casingTempTextColor = "White";
        private System.Threading.Timer timer;
     
        public string CurrentPower
        {
            get
            {
                return ( (100 * Math.Round(model.HeaterCurrentPower / (double)model.HeaterPower,2) ) ).ToString() + " %";
            }
        }
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                NotifyOfPropertyChange(() => Visibility);
            }
        }
        public int PixelsUp
        {
            get
            {
                return model.PixelsUp;
            }
        }

        public string CasingImage
        {
            get
            {
                return casingImage;
            }
        }
        public string HeaterImage
        {
            get
            {
                return heaterImage;
            }

        }
        public string BasketImage
        {
            get
            {
                return basketImage;
            }
        }

        public string InsideTemp
        {
            get
            {
                return "t воздуха в оболочке: " + (Math.Round(model.InsideTemp,1)).ToString() + " C°";
            }
        }
        public string CasingTemp
        {
            get
            {
                return "t оболочки: " + (Math.Round(model.CasingCurrentTemp,1)).ToString();
            }
        }
        public string CasingTempTextColor
        {
            get
            {
                return casingTempTextColor;
            }
        }
        public string OutsideTemp
        {
            get
            {
                return "t воздуха: " + (Math.Round(model.OutsideTemp,1)).ToString() + " C°";
            }
        }
        public string AtmosphericPressure
        {
            get
            {
                return "Атм. давление: " + model.AtmosphericPressure.ToString() + " мм. рт. ст.";
            }
        }
        public string UpForce
        {
            get
            {
                return "Подъемная сила: " + (model.UpForce - model.CountGravityForce()).ToString() + " Н";
            }
        }
        public string CurrentHeight
        {
            get
            {
                return "Высота: " + ((int)model.CurrentHeight).ToString() + " м";
            }
        }

        public FlightViewModel(ModifyViewModel modifyVM)
        {
            modifyViewModel = modifyVM;
            model = modifyViewModel.Model;
            Views.FlightView fl = new Views.FlightView();

            casingImage = model.CasingType;
            heaterImage = model.HeaterType;
            basketImage = model.BasketType;
            model.HeaterCurrentPower = model.HeaterPower / 2;

            if (model.Overheat)
            {
                ActivateItem(new CasingOverheatDialogViewModel());
            }
            
        }

        public void Start()
        {
            Visibility = "Visible";
            model.StartTimer();
            StartTimer();
        }
        public void StartTimer()
        {
            timer = new System.Threading.Timer(PropertiesChanged, null, 0, 1000);
        }
        private void PropertiesChanged(object state)
        {    
            NotifyOfPropertyChange(() => CurrentHeight);
            NotifyOfPropertyChange(() => OutsideTemp);
            NotifyOfPropertyChange(() => AtmosphericPressure);
            NotifyOfPropertyChange(() => UpForce);
            NotifyOfPropertyChange(() => PixelsUp);
            NotifyOfPropertyChange(() => InsideTemp);
            NotifyOfPropertyChange(() => CasingTemp); 

            if (model.Overheat)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    Restart();
                });
            }

            if (model.CasingCurrentTemp >= model.CasingMaxTemp - 10)
            {
                casingTempTextColor = "Red";
                NotifyOfPropertyChange(() => CasingTempTextColor);
            }
            else
            {
                casingTempTextColor = "White";
                NotifyOfPropertyChange(() => CasingTempTextColor);
            }
        }

        public void CloseWindow()
        {
            this.TryClose();

            Visibility = "Hidden";
            model.Restart();
           
            if (timer == null)
            {
                return;
            }
            
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer.Dispose();
            model.Overheat = false;
        }
        public void Restart()
        {
            this.TryClose();
            model.Restart();
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer.Dispose();
            Visibility = "Hidden";
            modifyViewModel.Start();
            model.Overheat = false;
        }

        public void IncreaseHeaterPower()
        {
            if ((int)model.HeaterCurrentPower != (int)model.HeaterPower)
            {
                model.HeaterCurrentPower += Math.Round((double)model.HeaterPower / 10, 1);
                NotifyOfPropertyChange(() => CurrentPower);
            }
            else return;
            
        }
        public void DecreaseHeaterPower()
        {
            if (Math.Round(model.HeaterCurrentPower) != 0)
            {
                model.HeaterCurrentPower -= Math.Round((double)model.HeaterPower / 10, 1);
                NotifyOfPropertyChange(() => CurrentPower);
            }
            else return;
        }
    }
}
