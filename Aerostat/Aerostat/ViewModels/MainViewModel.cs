using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Aerostat.ViewModels
{
    public class MainViewModel: Conductor<object>
    {
        public void Proceed()
        {
            ActivateItem(new ModifyViewModel());
        }
    }
}
