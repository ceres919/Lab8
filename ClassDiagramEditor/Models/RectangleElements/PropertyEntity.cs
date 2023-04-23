using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public abstract class PropertyEntity
    {
        public PropertyEntity() { }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Visibility { get; set; }
    }
}
