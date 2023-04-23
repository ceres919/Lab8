using ClassDiagramEditor.Models.RectangleElements;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.ViewModels
{
    public class ParameterWindowViewModel : ViewModelBase
    {
        public RectangleWithConnectors sendClassRectangle;
        private string currentName, currentStereotype, currentVisibilitySpecifier, attrName;
        private int currentStereotypeIndex, currentVisibilitySpecifierIndex;
        private ObservableCollection<AttributeElement>? classAttributes;
        private ObservableCollection<OperationElement>? classOperations;
        private AttributeElement selectedAttribute;
        private OperationElement selectedOperation;
        public ParameterWindowViewModel(RectangleWithConnectors senderClass)
        {
            ClassAttributes = new ObservableCollection<AttributeElement>();
            ClassOperations = new ObservableCollection<OperationElement>();
            this.sendClassRectangle = senderClass;
            SetParameters();
            //ClearButton = ReactiveCommand.Create(() => { Clear(); });
            //DeleteButton = ReactiveCommand.Create<ShapeEntity>(DeleteShape);
            //CurrentName = currentName;
            //CurrentStereotype = currentStereotype;
            //CurrentVisibilitySpecifier = currentVisibilitySpecifier;
        }

        private void SetParameters()
        {
            CurrentName = sendClassRectangle.Name;
            CurrentStereotype = sendClassRectangle.Stereotype;
            CurrentVisibilitySpecifier = sendClassRectangle.VisibilitySpecifier;
            CurrentStereotypeIndex = 1;
            ClassAttributes = sendClassRectangle.Attributes;
            ClassOperations = sendClassRectangle.Operations;

        }
        private void SetCurrentAttribute()
        {
            AttrName = SelectedAttribute.Name;
            //
            //
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
                SetCurrentAttribute();
            }
        }
        public OperationElement SelectedOperation 
        {
            get => selectedOperation;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedOperation, value);
            }
        }

        public string CurrentName 
        {
            get => currentName;
            set
            {
                this.RaiseAndSetIfChanged(ref currentName, value);
            }
        }
        public string AttrName
        {
            get => attrName;
            set
            {
                this.RaiseAndSetIfChanged(ref attrName, value);
            }
        }
        public string? CurrentStereotype 
        {
            //get => currentStereotype;
            set
            {
                this.RaiseAndSetIfChanged(ref currentStereotype, value);
                switch (currentStereotype)
                {
                    case "Не выбран":
                        CurrentStereotypeIndex = 0;
                        break;
                    case "static":
                        CurrentStereotypeIndex = 1;
                        break;
                    case "abstract":
                        CurrentStereotypeIndex = 2;
                        break;
                }
            }
        }
        public int CurrentStereotypeIndex
        {
            get => currentStereotypeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref currentStereotypeIndex, value);
            }
        }
        public string? CurrentVisibilitySpecifier
        {
           // get => currentVisibilitySpecifier;
            set
            {
                this.RaiseAndSetIfChanged(ref currentVisibilitySpecifier, value);
                switch (currentVisibilitySpecifier)
                {
                    case "public":
                        CurrentVisibilitySpecifierIndex = 0;
                        break;
                    case "internal":
                        CurrentVisibilitySpecifierIndex = 1;
                        break;
                }
            }
        }
        public int CurrentVisibilitySpecifierIndex
        {
            get => currentVisibilitySpecifierIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref currentVisibilitySpecifierIndex, value);
            }
        }
    }
}
