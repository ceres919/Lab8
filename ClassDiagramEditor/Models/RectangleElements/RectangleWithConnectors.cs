using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class RectangleWithConnectors : AbstractNotifyPropertyChanged, IShape
    {
        private Point startPoint;
        private double height;
        private double width;
        private ObservableCollection<AttributeElement>? attributes;
        private ObservableCollection<OperationElement>? operations;
        public RectangleWithConnectors() { }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Stereotype { get; set; }
        public string? VisibilitySpecifier { get; set; }
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
            }
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
        public event EventHandler<ChangeStartPointEventArgs> ChangeStartPoint;
    }
}
