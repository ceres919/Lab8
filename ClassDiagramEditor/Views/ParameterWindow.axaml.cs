using Avalonia.Controls;
using ClassDiagramEditor.Models.RectangleElements;
using ClassDiagramEditor.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Reactive;

namespace ClassDiagramEditor.Views
{
    public partial class ParameterWindow : Window
    {
        public ParameterWindow()
        {
            InitializeComponent();
        }
        public ParameterWindow(RectangleWithConnectors sender)
        {
            InitializeComponent();
            DataContext = new ParameterWindowViewModel(sender);
        }
    }
}
