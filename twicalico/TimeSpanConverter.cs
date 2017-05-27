using System;
using Windows.UI.Xaml.Data;

namespace twicalico
{
    class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var t = (DateTimeOffset)value;
            var now = DateTimeOffset.Now;
            var diff = now - t;
            if (value == null) return "null";
            if (diff < TimeSpan.FromMinutes(1))
            {
                return Math.Floor(diff.TotalSeconds) + "s";
            }
            else if (diff < TimeSpan.FromHours(1))
            {
                return Math.Floor(diff.TotalMinutes) + "m";
            }
            else if (diff < TimeSpan.FromDays(1))
            {
                return Math.Floor(diff.TotalHours) + "h";
            }
            else if (diff < TimeSpan.FromDays(7))
            {
                return Math.Floor(diff.TotalHours / 24) + "d";
            }
            else
            {
                return Math.Floor(diff.TotalHours / 24 / 7) + " month";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
