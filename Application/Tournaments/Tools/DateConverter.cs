using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tournaments.Tools
{
    /// <summary> 
    /// Converts a DateTime to a full-sized string and vice versa
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter as string)
            {
                case "dateTime":
                    return ((DateTime)value).ToString("dd/MM/yyyy (HH:mm)", CultureInfo.CreateSpecificCulture("fr-FR"));
                default:
                    return ((DateTime)value).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime resultDateTime;
            return DateTime.TryParse(value as string, out resultDateTime)
                        ? resultDateTime
                        : DependencyProperty.UnsetValue;
        }
    }
}
