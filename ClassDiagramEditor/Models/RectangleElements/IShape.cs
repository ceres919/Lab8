﻿using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public interface IShape
    {
        string Name { get; set; }
        Point StartPoint { get; set; }
    }
}
