using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.RectangleElements
{
    public class OperationElement : PropertyEntity
    {
        //private ObservableCollection<string[]> methodParameters;
        public OperationElement() { }
        public ObservableCollection<string[]> MethodParameters { get; set; }
        public string Specifier { get; set; }
        public string Stereotype { get; set; }

    }
}
