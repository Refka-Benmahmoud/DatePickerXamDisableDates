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
            Mypicker.MinimumDate = DateTime.Now;
            Mypicker.MaximumDate = DateTime.Now.Date.AddDays(5.0);
            DateEvent.MinimumDate = DateTime.Now;
            DateEvent.MaximumDate = DateTime.Now.Date.AddDays(5.0);
        }
    }
}
