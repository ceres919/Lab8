using Avalonia;
using Avalonia.Media;
using DynamicData.Binding;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using YamlDotNet.Serialization;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class Connector : IShape, IDisposable
    {
        private Point startPoint;
        private Point endPoint;
        private Geometry geometryPoints;
        private string stringGemetry;
        private string stringStartPoint;
        private string stringEndPoint;
        private RectangleWithConnectors firstRectangle;
        private RectangleWithConnectors secondRectangle;
        private double rotationAngle, rotationX, rotationY;
        public string Name { get; set; }
        public string FillColor { get; set; }
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
                    StartPoint = new Point(x, y);
                }
            }
        }
        public string StringEndPoint
        {
            get => stringEndPoint;
            set
            {
                stringEndPoint = value;
                if (EndPoint.IsDefault)
                {
                    var point = value.Split(',');
                    var x = double.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat);
                    var y = double.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat);
                    EndPoint = new Point(x, y);
                }
            }
        }
        [XmlIgnore]
        [IgnoreDataMember]
        [JsonIgnore]
        [YamlIgnore]
        public Point StartPoint
        {
            get => startPoint;
            set
            {
                SetAndRaise(ref startPoint, value);
                SetPoints();
                StringStartPoint = value.ToString();
            }
        }
        [XmlIgnore]
        [IgnoreDataMember]
        [JsonIgnore]
        [YamlIgnore]
        public Point EndPoint
        {
            get => endPoint;
            set
            {
                SetAndRaise(ref endPoint, value);
                SetPoints();
                StringEndPoint = value.ToString();
            }
        }
        [XmlIgnore]
        [IgnoreDataMember]
        [JsonIgnore]
        [YamlIgnore]
        public Geometry GeometryPoints
        {
            get => geometryPoints;
            set
            {
                SetAndRaise(ref geometryPoints, value);
            }
        }
        public string StringGeometry
        {
            get => stringGemetry;
            set
            {
                stringGemetry = value;
                if (geometryPoints == null)
                {
                    GeometryPoints = Geometry.Parse(value);
                }
            }
        }
        public double RotationAngle
        {
            get => rotationAngle;
            set => SetAndRaise(ref rotationAngle, value);
        }
        public double RotationX
        {
            get => rotationX;
            set => SetAndRaise(ref rotationX, value);
        }
        public double RotationY
        {
            get => rotationY;
            set => SetAndRaise(ref rotationY, value);
        }
        private void SetPoints()
        {
            StringBuilder newPoints = new();
            newPoints.Append($"M {startPoint.X},{startPoint.Y} ");
            var delta = startPoint - endPoint;
            int count = (int)Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));
            int dashes = (count / 20) - 1;
            switch (Name)
            {
                case "inheritance":
                    newPoints.Append($"l {count - 22},0 l 0,10 l 20,-10 l -20,-10 l 0,10");
                    break;
                case "realization":
                    while (dashes > 0)
                    {
                        newPoints.Append("l 10,0 m 10,0 ");
                        dashes--;
                    }
                    newPoints.Append($"l 0,10 l 20,-10 l -20,-10 l 0,10");
                    break;
                case "association":
                    newPoints.Append($"l {count-2},0 m -20,10 l 20,-10 l -20,-10");
                    break;
                case "dependency":
                    while (dashes > 0)
                    {
                        newPoints.Append ("l 10,0 m 10,0 ");
                        dashes--;
                    }
                    newPoints.Append($"m 0,10 l 20,-10 l -20,-10");
                    break;
                case "aggregation":
                    newPoints.Append($"l {count - 32},0 l 15,10 l 15,-10  l -15,-10 l -15,10"); 
                    break;
                case "composition":
                    newPoints.Append($"l {count - 32},0 l 15,10 l 15,-10  l -15,-10 l -15,10");
                    break;
            }
            GeometryPoints = Geometry.Parse(newPoints.ToString());
            StringGeometry = newPoints.ToString();

            double new_diag = count;
            double orig_diag = new_diag > 0 ? new_diag : 0.001;
            if (new_diag < 20 * 1.5) new_diag = 20 * 1.5;
            var diag = new_diag - 20;

            double angle = Math.Acos(delta.X / orig_diag);
            angle = angle * 180 / Math.PI;
            if (delta.Y < 0) angle = 360 - angle;
            angle = (angle + 180) % 360;
            RotationAngle = angle;
            RotationX = (startPoint.X - diag) / 2 - 10;
            RotationY= (startPoint.Y - 10) / 2 ;

        }
        public RectangleWithConnectors FirstRectangle
        {
            get => firstRectangle;
            set
            {
                if (firstRectangle != null)
                {
                    firstRectangle.ChangeStartPoint -= OnFirstRectanglePositionChanged;
                }

                firstRectangle = value;

                if (firstRectangle != null)
                {
                    firstRectangle.ChangeStartPoint += OnFirstRectanglePositionChanged;
                }
            }
        }

        public RectangleWithConnectors SecondRectangle
        {
            get => secondRectangle;
            set
            {
                if (secondRectangle != null)
                {
                    secondRectangle.ChangeStartPoint -= OnSecondRectanglePositionChanged;
                }

                secondRectangle = value;

                if (secondRectangle != null)
                {
                    secondRectangle.ChangeStartPoint += OnSecondRectanglePositionChanged;
                }
            }
        }

        private void OnFirstRectanglePositionChanged(object? sender, ChangeStartPointEventArgs e)
        {
            StartPoint += e.NewStartPoint - e.OldStartPoint;
        }

        private void OnSecondRectanglePositionChanged(object? sender, ChangeStartPointEventArgs e)
        {
            EndPoint += e.NewStartPoint - e.OldStartPoint;
        }

        public void Dispose()
        {
            if (FirstRectangle != null)
            {
                FirstRectangle.ChangeStartPoint -= OnFirstRectanglePositionChanged;
            }

            if (SecondRectangle != null)
            {
                SecondRectangle.ChangeStartPoint -= OnSecondRectanglePositionChanged;
            }
        }
    }
}
