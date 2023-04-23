using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class AttributeElement : PropertyEntity
    {
        public AttributeElement() { }
        public bool IsStatic { get; set; }
        public string Stereotype { get; set; }
    }
}
