    using Avalonia;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class Connector : AbstractNotifyPropertyChanged, IShape, IDisposable
    {
        private Point startPoint;
        private Point endPoint;
        private RectangleWithConnectors firstRectangle;
        private RectangleWithConnectors secondRectangle;
        public string Name { get; set; }
        public Point StartPoint
        {
            get => startPoint;
            set => SetAndRaise(ref startPoint, value);
        }

        public Point EndPoint
        {
            get => endPoint;
            set => SetAndRaise(ref endPoint, value);
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
