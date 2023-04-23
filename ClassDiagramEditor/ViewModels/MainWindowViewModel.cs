using Avalonia.Controls;
using ClassDiagramEditor.Models.LoadAndSave;
using ClassDiagramEditor.Models.RectangleElements;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClassDiagramEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IEnumerable<ISaverLoaderFactory> SaverLoaderFactoryCollection { get; set; }
        private ObservableCollection<IShape> shapes;

        public MainWindowViewModel()
        {
            Shapes = new ObservableCollection<IShape>();
            Shapes.Add(new RectangleWithConnectors
            {
                Height = 170,
                Width = 200,
                Name = "First",
                Type = "interface",
                Stereotype = "static",
                VisibilitySpecifier = "public",
                StartPoint = new Avalonia.Point(100, 100),
                Attributes = new() 
                { 
                    new AttributeElement(){Name="atr1"}
                },
                Operations = new() 
                { 
                    new OperationElement(){Name="opr1"}
                },
            });

            Shapes.Add(new RectangleWithConnectors
            {
                Height = 170,
                Width = 200,
                Name = "Second",
                Type = "class",
                Stereotype = "abstract",
                StartPoint = new Avalonia.Point(500, 100),
            });

            Shapes.Add(new RectangleWithConnectors
            {
                Height = 170,
                Width = 200,
                Name = "First",
                Type = "interface",
                StartPoint = new Avalonia.Point(100, 500),
            });

            Shapes.Add(new RectangleWithConnectors
            {
                Height = 170,
                Width = 200,
                Name = "Second",
                Type = "class",
                StartPoint = new Avalonia.Point(500, 500),
            });
        }
        public void LoadDiagram(string path)
        {
            //list.shapeList.Clear();

            var shapeLoader = SaverLoaderFactoryCollection
                .FirstOrDefault(factory => factory.IsMatch(path) == true)?
                .CreateLoader();

            if (shapeLoader != null)
            {
                var newList = new ObservableCollection<RectangleWithConnectors>(shapeLoader.Load(path));
                //foreach (var shape in newList)
                //{
                //    ShapeCreator.Load(shape, list);
                //}
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
                   // shapeSaver.Save(ShapeList, path);
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
    }
}