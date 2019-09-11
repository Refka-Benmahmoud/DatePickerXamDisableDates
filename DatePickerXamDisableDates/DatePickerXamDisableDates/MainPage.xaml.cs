using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DatePickerXamDisableDates
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DateEvent.Format = "yyyy'-'MM'-'dd";
            Mypicker.MinimumDate = DateTime.Now;
            Mypicker.MaximumDate = DateTime.Now.Date.AddDays(5.0);
            DateEvent.Minimumdate = DateTime.Now;
            DateEvent.Maximumdate = DateTime.Now.AddDays(5.0);
        }
    }
}
