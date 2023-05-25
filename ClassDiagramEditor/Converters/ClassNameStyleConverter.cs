using Avalonia.Data.Converters;
using Avalonia.Media;
using ClassDiagramEditor.Models.RectangleElements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Converters
{
    public class ClassNameStyleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string par = (string)parameter;
            if (targetType.IsAssignableTo(typeof(TextDecorationCollection)) == true && par == "static")
            {
                TextDecorationCollection style = TextDecorations.Underline;
                if(value is string stereotype1 && stereotype1 == "static")
                    return style;
                if(value is AttributeElement attr && attr.IsStatic == true)
                    return style;
                if (value is OperationElement oper && oper.IsStatic == true)
                    return style;
            }
            if (targetType.IsAssignableTo(typeof(FontStyle)) == true && par == "abstract")
            {
                FontStyle style = FontStyle.Italic;
                if (value is string stereotype2 && stereotype2 == "abstract") 
                    return style;
                if (value is OperationElement oper && oper.IsAbstract == true)
                    return style;
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
