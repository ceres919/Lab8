using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Converters
{
    public class VisabilityIndexConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;

            if (value is ComboBoxItem item && par == "value")
            {
                if (item != null)
                {
                    return item.Content.ToString();
                }
            }
            return null;
        }
    }
}
