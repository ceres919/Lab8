using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class AttributeElement : PropertyEntity
    {
        private bool isStatic;
        private bool isReadonly;
        private string stereotype;

        public AttributeElement() { }
        public bool IsStatic
        {
            get => isStatic;
            set => SetAndRaise(ref isStatic, value);
        }
        public bool IsReadonly
        {
            get => isReadonly;
            set => SetAndRaise(ref isReadonly, value);
        }
        public string Stereotype
        {
            get => stereotype;
            set => SetAndRaise(ref stereotype, value);
        }
    }
}
