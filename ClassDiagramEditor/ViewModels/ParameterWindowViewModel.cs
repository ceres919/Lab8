using ClassDiagramEditor.Models.RectangleElements;
using ClassDiagramEditor.Views;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus;

namespace ClassDiagramEditor.ViewModels
{
    public class ParameterWindowViewModel : ViewModelBase
    {
        private RectangleWithConnectors sendClassRectangle;
        public RectangleWithConnectors tempClassRectangle;
        private ObservableCollection<AttributeElement>? classAttributes;
        private ObservableCollection<OperationElement>? classOperations;
        private AttributeElement selectedAttribute;
        private OperationElement selectedOperation;
        private int classVisabilityIndex, classStereotypeIndex;
        private int propertyVisabilityIndex, propertyStereotypeIndex;
        private PropertyEntity currentAttribute, currentOperation;

        public ParameterWindowViewModel(RectangleWithConnectors senderClass)
        {
            ClassAttributes = new ObservableCollection<AttributeElement>();
            ClassOperations = new ObservableCollection<OperationElement>();
            SendClassRectangle = senderClass;
            tempClassRectangle = senderClass;
            ClassAttributes = SendClassRectangle.Attributes;
            ClassOperations = SendClassRectangle.Operations;
            SetClassIndex();

            AddAttributeButton = ReactiveCommand.Create(() =>
            {
                classAttributes.Add(new AttributeElement { Name = "new_item" });
            });
            AddOperationButton = ReactiveCommand.Create(() =>
            {
                classOperations.Add(new OperationElement { Name = "new_item" });
            });
            DeleteAttributeButton = ReactiveCommand.Create<AttributeElement>(param =>
            {
                classAttributes.Remove(param);
            });
            DeleteOperationButton = ReactiveCommand.Create<OperationElement>(param =>
            {
                classOperations.Remove(param);
            });
        }
        public PropertyEntity CurrentAttribute
        {
            get => currentAttribute;
            set
            {
                this.RaiseAndSetIfChanged(ref currentAttribute, value);
            }
        }
        public PropertyEntity CurrentOperation
        {
            get => currentOperation;
            set
            {
                this.RaiseAndSetIfChanged(ref currentOperation, value);
            }
        }
        void SetContent(PropertyEntity selected)
        {
            CurrentAttribute = null;
            CurrentOperation = null;
            SetIndex(selected);
            if (selected is AttributeElement attr)
                CurrentAttribute = attr;
            else if (selected is OperationElement oper)
                CurrentOperation = oper;
        }
        void SetIndex(PropertyEntity selected)
        {
            switch (selected.Visibility)
            
            {
                case "+":
                    PropertyVisabilityIndex = 0;
                    break;
                case "#":
                    PropertyVisabilityIndex = 1;
                    break;
                case "-":
                    PropertyVisabilityIndex = 2;
                    break;
                default:
                    PropertyVisabilityIndex = 0;
                    break;
            }
            if(selected is AttributeElement attr)
            {
                switch (attr.Stereotype)
                {
                    case "event":
                        PropertyStereotypeIndex = 0;
                        break;
                    case "property":
                        PropertyStereotypeIndex = 1;
                        break;
                    case "required":
                        PropertyStereotypeIndex = 2;
                        break;
                    case "не выбран":
                        PropertyStereotypeIndex = 3;
                        break;
                    default:
                        PropertyStereotypeIndex = 3;
                        break;
                }
            }
            else if(selected is OperationElement oper)
            {
                switch (oper.Stereotype)
                {
                    case "create":
                        PropertyStereotypeIndex = 0;
                        break;
                    case "не выбран":
                        PropertyStereotypeIndex = 1;
                        break;
                    default:
                        PropertyStereotypeIndex = 1;
                        break;
                }
            }
        }
        void SetClassIndex()
        {
            switch (SendClassRectangle.Visibility)
            {
                case "public":
                    classVisabilityIndex = 0;
                    break;
                case "internal":
                    classVisabilityIndex = 1;
                    break;
                default:
                    classVisabilityIndex = 0;
                    break;
            }
            switch (SendClassRectangle.Stereotype)
            {
                case "static":
                    classStereotypeIndex = 0;
                    break;
                case "abstract":
                    classStereotypeIndex = 1;
                    break;
                case "не выбран":
                    classStereotypeIndex = 2;
                    break;
                default:
                    classStereotypeIndex = 2;
                    break;
            }
        }
        public int ClassVisabilityIndex
        {
            get => classVisabilityIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref classVisabilityIndex, value);
            }
        }
        public int ClassStereotypeIndex
        {
            get => classStereotypeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref classStereotypeIndex, value);
            }
        }
        public int PropertyVisabilityIndex
        {
            get => propertyVisabilityIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref propertyVisabilityIndex, value);
            }
        }
        public int PropertyStereotypeIndex
        {
            get => propertyStereotypeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref propertyStereotypeIndex, value);
            }
        }
        public RectangleWithConnectors SendClassRectangle
        {
            get => sendClassRectangle;
            set
            {
                this.RaiseAndSetIfChanged(ref sendClassRectangle, value);
            }
        }
        
        public ObservableCollection<AttributeElement>? ClassAttributes
        {
            get => classAttributes;
            set
            {
                this.RaiseAndSetIfChanged(ref classAttributes, value);
            }
        }
        public ObservableCollection<OperationElement>? ClassOperations
        {
            get => classOperations;
            set
            {
                this.RaiseAndSetIfChanged(ref classOperations, value);
            }
        }
        public AttributeElement SelectedAttribute 
        {
            get => selectedAttribute;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedAttribute, value);
                SetContent(SelectedAttribute);
            }
        }
        public OperationElement SelectedOperation 
        {
            get => selectedOperation;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedOperation, value);
                SetContent(SelectedOperation);
            }
        }
        public ReactiveCommand<Unit, Unit> AddAttributeButton { get; }
        public ReactiveCommand<Unit, Unit> AddOperationButton { get; }
        public ReactiveCommand<AttributeElement, Unit> DeleteAttributeButton { get; }
        public ReactiveCommand<OperationElement, Unit> DeleteOperationButton { get; }
    }
}
