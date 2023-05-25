using Avalonia;
using DynamicData.Binding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using System.Runtime.Serialization;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class RectangleWithConnectors : IShape
    {
        private Point startPoint;
        private string stringStartPoint;
        private string name;
        private string type;
        private string visibility;
        private string stereotype;
        private double height;
        private double width;
        private ObservableCollection<AttributeElement>? attributes;
        private ObservableCollection<OperationElement>? operations;
        public RectangleWithConnectors() { }
        [XmlIgnore]
        [IgnoreDataMember]
        [JsonIgnore]
        [YamlIgnore]
        public Point StartPoint
        {
            get => startPoint;
            set
            {
                Point oldPoint = StartPoint;
                
                SetAndRaise(ref startPoint, value);

                if (ChangeStartPoint != null)
                {
                    ChangeStartPointEventArgs args = new ChangeStartPointEventArgs
                    {
                        OldStartPoint = oldPoint,
                        NewStartPoint = StartPoint,
                    };
                    
                    ChangeStartPoint(this, args);
                }
                StringStartPoint = value.ToString();
            }

        }
        public event EventHandler<ChangeStartPointEventArgs> ChangeStartPoint;
        public string StringStartPoint 
        { 
            get => stringStartPoint;
            set
            {
                stringStartPoint = value;
                if (StartPoint.IsDefault)
                {
                    var point = value.Split(',');
                    var x = double.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat);
                    var y = double.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat);
                    StartPoint =  new Point(x, y);
                }
            }
        }
        public string Name
        {
            get => name;
            set => SetAndRaise(ref name, value);
        }
        public string Type
        {
            get => type;
            set => SetAndRaise(ref type, value);
        }
        public string Visibility
        {
            get => visibility;
            set => SetAndRaise(ref visibility, value);
        }
        public string Stereotype
        {
            get => stereotype;
            set => SetAndRaise(ref stereotype, value);
        }
        public double Height
        {
            get => height;
            set => SetAndRaise(ref height, value);
        }
        public double Width
        {
            get => width;
            set => SetAndRaise(ref width, value);
        }
        public ObservableCollection<AttributeElement>? Attributes
        {
            get => attributes;
            set => SetAndRaise(ref attributes, value);
        }
        public ObservableCollection<OperationElement>? Operations
        {
            get => operations;
            set => SetAndRaise(ref operations, value);
        }
    }
}
