using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using ClassDiagramEditor.Models.LoadAndSave;
using ClassDiagramEditor.Models.RectangleElements;
using ClassDiagramEditor.Views;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace ClassDiagramEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IEnumerable<ISaverLoaderFactory> SaverLoaderFactoryCollection { get; set; }
        private ObservableCollection<IShape> shapes;
        public MainWindow mainWindow;

        public MainWindowViewModel(Window main)
        {
            mainWindow = (MainWindow)main;
            Shapes = new ObservableCollection<IShape>();

            ImportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.OpenFileDialogMenu(param);
            });
            ExportButton = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.SaveFileDialogMenu(param);
            });
            CurrentConnection = ReactiveCommand.Create<string>(param =>
            {
                mainWindow.ConnectionParameter = param;
            });
        }
        public void AddNewClass(string parameter)
        {
            Shapes.Add(new RectangleWithConnectors()
                {
                    Height = 170,
                    Width = 200,
                    Name = "NewCLass",
                    Type = parameter,
                    StartPoint = new Point(10, 10),
                    Attributes = new ObservableCollection<AttributeElement>(),
                    Operations = new ObservableCollection<OperationElement>(),
                });
        }
        public void LoadDiagram(string path)
        {
            Shapes.Clear();
            var shapeLoader = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateLoader();

            if (shapeLoader != null)
            {
                var newList = new ObservableCollection<IShape>(shapeLoader.Load(path));
                Shapes = newList;
            }
        }
        public void SaveDiagram(string path, string parametr, Canvas canvas)
        {
            if (parametr != "png")
            {
                var shapeSaver = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateSaver();

                if (shapeSaver != null)
                {
                     shapeSaver.Save(Shapes, path);
                }
            }
            else
            {
                var shapeSaver = new PNGSaver();
                shapeSaver.Save(canvas, path);
            }
        }
        public ObservableCollection<IShape> Shapes
        {
            get => shapes;
            set => this.RaiseAndSetIfChanged(ref shapes, value);
        }
        public ReactiveCommand<string, Unit> ImportButton { get; }
        public ReactiveCommand<string, Unit> ExportButton { get; }
        public ReactiveCommand<string, Unit> CurrentConnection { get; }
    }
}