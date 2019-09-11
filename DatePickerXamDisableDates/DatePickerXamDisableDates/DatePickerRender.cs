using System;
using Xamarin.Forms;

namespace DatePickerXamDisableDates
{
   public class DatePickerRender : DatePicker
    {
        public DatePickerRender()
        {
            Format = "d";
        }
        public string _originalFormat = null;

        public static readonly BindableProperty PlaceHolderProperty =
            BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(Picker), "all");

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }
        }
        public DateTime Maximumdate
        {
            get { return (DateTime)GetValue(MaximumdateProperty); }
            set { SetValue(MaximumdateProperty, value); }
        }

        public DateTime Minimumdate
        {
            get { return (DateTime)GetValue(MinimumdateProperty); }
            set { SetValue(MinimumdateProperty, value); }
        }
        public static readonly BindableProperty MinimumdateProperty = BindableProperty.Create(nameof(Minimumdate),
            typeof(DateTime), typeof(DatePicker));

        public static readonly BindableProperty MaximumdateProperty = BindableProperty.Create(nameof(Maximumdate),
            typeof(DateTime), typeof(DatePicker)
         );
        public static readonly BindableProperty NullableDateProperty =
        BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(DatePickerRender), null, defaultBindingMode: BindingMode.TwoWay);

        static bool ValidateMaximumDate(BindableObject bindable, object value)
        {
            return (DateTime)value >= ((DatePicker)bindable).MinimumDate;
        }

        static bool ValidateMinimumDate(BindableObject bindable, object value)
        {
            return (DateTime)value <= ((DatePicker)bindable).MaximumDate;
        }
        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        private void UpdateDate()
        {
            if (NullableDate != null)
            {
                if (_originalFormat != null)
                {
                    Format = _originalFormat;
                }
            }
            else
            {
                Format = PlaceHolder;

            }

        }
        void UpdateMaximumDate()
        {
                this.MaximumDate = Maximumdate;// (long)(DateTime.Now.Date.AddDays(3).Millisecond);

           
        }

        void UpdateMinimumDate()
        {
            
                MinimumDate = Minimumdate;// (long)(DateTime.Now.Date - new DateTime(1970, 1, 1)).TotalMilliseconds - 1000 * 60 * 60 * 24 * 3;

           
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                _originalFormat = Format;
                UpdateDate();
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DateProperty.PropertyName || (propertyName == IsFocusedProperty.PropertyName && !IsFocused && (Date.ToString("d") == DateTime.Now.ToString("d"))))
            {
                AssignValue();
            }

            if (propertyName == NullableDateProperty.PropertyName && NullableDate.HasValue)
            {
                Date = NullableDate.Value;
                if (Date.ToString(_originalFormat) == DateTime.Now.ToString(_originalFormat))
                {
                    //this code was done because when date selected is the actual date the"DateProperty" does not raise  
                    UpdateDate();
                }
            }
        }

        public void CleanDate()
        {
            NullableDate = null;
            UpdateDate();
        }
        public void AssignValue()
        {
            NullableDate = Date;
           // UpdateMaximumDate();
           // UpdateMinimumDate();
            UpdateDate();

        }
    }

}
