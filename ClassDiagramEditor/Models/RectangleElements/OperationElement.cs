using Newtonsoft.Json.Linq;
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
        private bool isStatic;
        private bool isVirtual;
        private bool isAbstract;
        private string stereotype;
        private string methodParameters;
        public OperationElement() { }
        
        public bool IsStatic
        {
            get => isStatic;
            set => SetAndRaise(ref isStatic, value);
        }
        public bool IsVirtual
        {
            get => isVirtual;
            set => SetAndRaise(ref isVirtual, value);
        }
        public bool IsAbstract
        {
            get => isAbstract;
            set => SetAndRaise(ref isAbstract, value);
        }

        public string Stereotype
        {
            get => stereotype;
            set => SetAndRaise(ref stereotype, value);
        }
        public string MethodParameters
        {
            get => methodParameters;
            set => SetAndRaise(ref methodParameters, value);
        }
    }
}
