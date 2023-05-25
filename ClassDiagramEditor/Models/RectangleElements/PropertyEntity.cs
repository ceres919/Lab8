using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public abstract class PropertyEntity: AbstractNotifyPropertyChanged
    {
        private string name;
        private string type;
        private string visibility;

        public PropertyEntity() { }
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
    }
}
