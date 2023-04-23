using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Converters
{
    public class PointPositionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;
            if (value is double mid && targetType.IsAssignableTo(typeof(double)) == true && par == "middle")
            {
                var x = (mid - 10) / 2;
                return x;
            }
            if (value is double bot && targetType.IsAssignableTo(typeof(double)) == true && par == "botton")
            {
                var x = bot - 10;
                return x;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
