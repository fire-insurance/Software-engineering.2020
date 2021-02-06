using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;


namespace Aerostat.ViewModels
{
    public class CasingOverheatDialogViewModel : Screen
    {

       public CasingOverheatDialogViewModel()
        {

        }

        public void Close()
        {
            this.TryClose();
        }
    }
}
