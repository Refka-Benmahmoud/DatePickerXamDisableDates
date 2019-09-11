using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(DatePickerXamDisableDates.DatePickerRender), typeof(DatePickerXamDisableDates.Droid.DatePickerRenderAndroid))]
namespace DatePickerXamDisableDates.Droid
{
  public  class DatePickerRenderAndroid : ViewRenderer<DatePickerXamDisableDates.DatePickerRender, Android.Widget.EditText>
    {
        DatePickerDialog _dialog;
        public DatePickerRenderAndroid(Context context) : base(context)
        {
            //UpdateMaximumDate();
            //UpdateMinimumDate();
        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePickerXamDisableDates.DatePickerRender> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new Android.Widget.EditText(Forms.Context));
            if (Control == null || e.NewElement == null)
                return;

            this.Control.Click += OnPickerClick;
            this.Control.Text = Element.Date.ToString(Element.Format);
            this.Control.KeyListener = null;
            this.Control.FocusChange += OnPickerFocusChange;
             this.Control.Enabled = Element.IsEnabled;
            CreateDatePickerDialog(this.Element.Date.Year, this.Element.Date.Month - 1, this.Element.Date.Day);
            GradientDrawable gd = new GradientDrawable();
            gd.SetStroke(0, Android.Graphics.Color.Transparent);
            Control.SetBackgroundDrawable(gd);
           // SetDatemax(Element.Maximumdate);
            //UpdateMaximumDate();
            //   UpdateMinimumDate();
           // SetDatemin(Element.Minimumdate);

        }
        void UpdateMaximumDate()
        {
            //if (_dialog != null)
            //{
            _dialog.DatePicker.MaxDate = (long)(DateTime.Now.Date - new DateTime(1970, 1, 1)).TotalMilliseconds + 1000 * 60 * 60 * 24 * 3;

                if (_dialog.DatePicker.MaxDate > _dialog.DatePicker.DayOfMonth)
                {
                SetDate(Element.Maximumdate);
            }
            //}
        }

        void UpdateMinimumDate()
        {
            //if (_dialog != null)
            //{
                _dialog.DatePicker.MinDate = (long)(DateTime.Now.Date - new DateTime(1970, 1, 1)).TotalMilliseconds - 1000 * 60 * 60 * 24 * 3;

                if ( _dialog.DatePicker.MinDate< _dialog.DatePicker.DayOfMonth)
                {
                SetDate(Element.Minimumdate);
            }
           // }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Xamarin.Forms.DatePicker.DateProperty.PropertyName || e.PropertyName == Xamarin.Forms.DatePicker.FormatProperty.PropertyName)
                SetDate(Element.Date);
             if (e.PropertyName == DatePicker.MinimumDateProperty.PropertyName)
                SetDate(Element.Minimumdate);
             // UpdateMinimumDate();
            if (e.PropertyName == DatePicker.MaximumDateProperty.PropertyName)
                SetDate(Element.Maximumdate);
            // UpdateMaximumDate();
        }

        void OnPickerFocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ShowDatePicker();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                this.Control.Click -= OnPickerClick;
                this.Control.FocusChange -= OnPickerFocusChange;

                if (_dialog != null)
                {
                    _dialog.Hide();
                    _dialog.Dispose();
                    _dialog = null;
                }
            }

            base.Dispose(disposing);
        }

        void OnPickerClick(object sender, EventArgs e)
        {
            ShowDatePicker();
        }

        void SetDate(DateTime date)
        {
            this.Control.Text = date.ToString(Element.Format);
            Element.Date = date;
        }
        void SetDatemin(DateTime date)
        {
            this.Control.Text = date.ToString(Element.Format);
            Element.Minimumdate = date;
        }
        void SetDatemax(DateTime date)
        {
            this.Control.Text = date.ToString(Element.Format);
            Element.MaximumDate = date;
        }

        private void ShowDatePicker()
        {
            CreateDatePickerDialog(this.Element.Date.Year, this.Element.Date.Month - 1, this.Element.Date.Day);
            _dialog.Show();
        }

        void CreateDatePickerDialog(int year, int month, int day)
        {
            DatePickerXamDisableDates.DatePickerRender view = Element;
            _dialog = new DatePickerDialog(Context, (o, e) =>
            {
                view.Date = e.Date;
                ((IElementController)view).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                Control.ClearFocus();

                _dialog = null;
            }, year, month, day);



            var languageDevice = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            var done = "تم";
            var cancel = "إلغاء";
            var clear = "إعادة";

            if (languageDevice.ToLower().Contains("en"))
            {
                done = "Done";
                cancel = "Cancel";
                clear = "Clear";
            }
            _dialog.SetButton(done, (sender, e) =>
            {
                SetDate(_dialog.DatePicker.DateTime);
                this.Element.Format = this.Element._originalFormat;
                this.Element.AssignValue();
                // App.DateSelected = _dialog.DatePicker.DateTime;
            });

            _dialog.SetButton2(cancel, (sender, e) =>
            {
            });
            _dialog.SetButton3(clear, (sender, e) =>
            {
                this.Element.CleanDate();
                Control.Text = this.Element.Format;
                //App.DateSelected = default(DateTime);
            });
        }

    }
}