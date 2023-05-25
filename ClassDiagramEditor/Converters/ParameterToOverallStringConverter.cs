using Avalonia.Data.Converters;
using ClassDiagramEditor.Models.RectangleElements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Converters
{
    public class ParameterToOverallStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;
            if (value is AttributeElement attr && targetType.IsAssignableTo(typeof(string)) == true && par == "attribute")
            {
                var overall = attr.Visibility + " ";
                if(attr.IsReadonly == true)
                    overall += "readonly ";
                if(attr.Stereotype != "не выбран" && attr.Stereotype != null)
                    overall += "«" + attr.Stereotype + "» ";
                overall += attr.Name + ": " + attr.Type;
                return overall;
            }
            if (value is OperationElement oper && targetType.IsAssignableTo(typeof(string)) == true && par == "operation")
            {
                var overall = oper.Visibility + " ";
                if (oper.IsVirtual == true)
                    overall += "virtual ";
                if (oper.Stereotype != "не выбран" && oper.Stereotype != null)
                    overall += "«" + oper.Stereotype + "» ";
                overall += oper.Name + " (";
                if (oper.MethodParameters != null)
                    overall += oper.MethodParameters;
                overall += "): " + oper.Type;
                return overall;
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
