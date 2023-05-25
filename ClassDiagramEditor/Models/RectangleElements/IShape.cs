using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassDiagramEditor.Models.RectangleElements
{
    [XmlInclude(typeof(RectangleWithConnectors))]
    [XmlInclude(typeof(Connector))]
    public abstract class IShape : AbstractNotifyPropertyChanged
    {
        string Name { get; set; }
        Point StartPoint { get; set; }
    }
}
