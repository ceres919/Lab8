using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ClassDiagramEditor.Models.LoadAndSave;
using ClassDiagramEditor.Models.RectangleElements;
using ClassDiagramEditor.ViewModels;
using DynamicData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ClassDiagramEditor.Views
{
    public partial class MainWindow : Window
    {
        private Point pointPointerPressed;
        private Point pointerPositionIntoShape;
        private RectangleWithConnectors? firstRectangle;
        protected bool isDragging;
        private string connectionParameter;
        private Canvas canv;
        private double currentRectangleWidth, currentRectangleHeight;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this)
            {
                SaverLoaderFactoryCollection = new ISaverLoaderFactory[]
                    {
                        new XMLSaverLoaderFactory(),
                        new JSONSaverLoaderFactory(),
                        new YAMLSaverLoaderFactory(),
                    },
            };
            AddHandler(DragDrop.DragEnterEvent, CanvasDragEnter);
            AddHandler(DragDrop.DropEvent, CanvasDrop);
            ConnectionParameter = "inheritance";
        }
        public string ConnectionParameter 
        {
            get => connectionParameter; 
            set => connectionParameter = value;
        }
        private void DeleteConnection(object sender, RoutedEventArgs routedEventArgs)
        {
            if (routedEventArgs.Source is Control control)
            {
                if (control.DataContext is Connector connector) 
                {
                    if (DataContext is MainWindowViewModel dataContext)
                    {
                        dataContext.Shapes.Remove(connector);
                    }
                }
            }     
        }
        private void CanvasDragEnter(object sender, DragEventArgs dragEventArgs)
        {
            dragEventArgs.DragEffects = DragDropEffects.Copy;
        }
        public void CanvasDrop(object sender, DragEventArgs dragEventArgs)
        {
            List<string> path = (List<string>)dragEventArgs.Data.Get(DataFormats.FileNames);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (path != null)
                {
                    dataContext.LoadDiagram(path.ElementAt(0));
                }
            }
        }
        private async void OpenParameterWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            if (routedEventArgs.Source is Control control)
            {
                if (control.DataContext is RectangleWithConnectors rectangle)
                {
                    ParameterWindow parameterWindow = new ParameterWindow(rectangle);

                    var result = await parameterWindow.ShowDialog<string>(this);
                    if (this.DataContext is MainWindowViewModel viewModel)
                    {
                        viewModel.Shapes.Remove(rectangle);
                        if (parameterWindow.DataContext is ParameterWindowViewModel parameterVM)
                        {
                            switch (result)
                            {
                                case "undo-changes":
                                    viewModel.Shapes.Add(parameterVM.tempClassRectangle);
                                    break;
                                case "save-changes":
                                    viewModel.Shapes.Add(parameterVM.SendClassRectangle);
                                    break;
                                case "delete-class":
                                    break;
                                default:
                                    viewModel.Shapes.Add(parameterVM.tempClassRectangle);
                                    break;

                            }
                        } 
                    }
                }
            }
        }
        private void OnPointerPressed(object? sender, PointerPressedEventArgs pointerPressedEventArgs)
        {
            if (pointerPressedEventArgs.Source is Control control)
            {
                if (control.DataContext is RectangleWithConnectors rectangle)
                {
                    pointPointerPressed = pointerPressedEventArgs
                        .GetPosition(
                        this.GetVisualDescendants()
                        .OfType<Canvas>()
                        .FirstOrDefault(canvas => string.IsNullOrEmpty(canvas.Name) == false &&
                        canvas.Name.Equals("highLevelCanvas")));

                    if (pointerPressedEventArgs.Source is not Rectangle && pointerPressedEventArgs.Source is not Ellipse)
                    {
                        pointerPositionIntoShape = pointerPressedEventArgs.GetPosition(control);
                        isDragging = true;
                        this.PointerMoved += PointerMoveDragShape;
                        this.PointerReleased += PointerPressedReleasedDragShape;
                    }
                    else
                    {
                        if(pointerPressedEventArgs.Source is Ellipse)
                        {
                            currentRectangleWidth = rectangle.Width;
                            currentRectangleHeight = rectangle.Height;
                            this.PointerMoved += PointerResizeShape;
                            this.PointerReleased += PointerPressedReleasedResizeShape;
                        }
                        else
                        {
                            if (this.DataContext is MainWindowViewModel viewModel)
                            {
                                string fill;
                                if (connectionParameter == "association" || connectionParameter == "dependency")
                                {
                                    fill = "Transperent";
                                }
                                else
                                {
                                    if (connectionParameter == "composition")
                                    {
                                        fill = "MediumPurple";
                                    }
                                    else
                                    {
                                        fill = "White";
                                    }
                                }
                                viewModel.Shapes.Add(new Connector
                                {
                                    StartPoint = pointPointerPressed,
                                    EndPoint = pointPointerPressed,
                                    Name = connectionParameter,
                                    FillColor = fill,
                                    FirstRectangle = rectangle,
                                });
                                firstRectangle = rectangle;

                                this.PointerMoved += PointerMoveDrawLine;
                                this.PointerReleased += PointerPressedReleasedDrawLine;
                            }
                        }
                        
                    }
                }
            }
        }
        private void PointerResizeShape(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (pointerEventArgs.Source is Control control)
            {
                if (control.DataContext is RectangleWithConnectors rectangle)
                {
                    Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                    rectangle.Width = currentRectangleWidth + (currentPointerPosition.X - pointPointerPressed.X);
                    rectangle.Height = currentRectangleHeight + (currentPointerPosition.Y - pointPointerPressed.Y);
                }
            }
        }
        private void PointerPressedReleasedResizeShape(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            this.PointerMoved -= PointerResizeShape;
            this.PointerReleased -= PointerPressedReleasedResizeShape;
        }

        private void PointerMoveDragShape(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (pointerEventArgs.Source is Control control)
            {
                if (control.DataContext is RectangleWithConnectors rectangle && isDragging)
                {
                    Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                    rectangle.StartPoint = new Point(
                        currentPointerPosition.X - pointerPositionIntoShape.X,
                        currentPointerPosition.Y - pointerPositionIntoShape.Y);
                }
            }
        }

        private void PointerPressedReleasedDragShape(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            isDragging = false;
            this.PointerMoved -= PointerMoveDragShape;
            this.PointerReleased -= PointerPressedReleasedDragShape;
        }

        private void PointerMoveDrawLine(object? sender, PointerEventArgs pointerEventArgs)
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                Debug.WriteLine(sender);
                Connector connector = viewModel.Shapes[viewModel.Shapes.Count - 1] as Connector;
                Point currentPointerPosition = pointerEventArgs
                    .GetPosition(
                    this.GetVisualDescendants()
                    .OfType<Canvas>()
                    .FirstOrDefault());

                connector.EndPoint = new Point(
                        currentPointerPosition.X - 1,
                        currentPointerPosition.Y - 1);
            }
        }

        private void PointerPressedReleasedDrawLine(object? sender,
            PointerReleasedEventArgs pointerReleasedEventArgs)
        {
            this.PointerMoved -= PointerMoveDrawLine;
            this.PointerReleased -= PointerPressedReleasedDrawLine;

            var canvas = this.GetVisualDescendants()
                        .OfType<Canvas>()
                        .FirstOrDefault(canvas => string.IsNullOrEmpty(canvas.Name) == false &&
                        canvas.Name.Equals("highLevelCanvas"));

            var coords = pointerReleasedEventArgs.GetPosition(canvas);

            var element = canvas.InputHitTest(coords);
            MainWindowViewModel viewModel = this.DataContext as MainWindowViewModel;

            if (element is Rectangle Rectangle)
            {
                if (Rectangle.DataContext is RectangleWithConnectors rectangle && rectangle != firstRectangle)
                {
                    Connector connector = viewModel.Shapes[viewModel.Shapes.Count - 1] as Connector;
                    connector.SecondRectangle = rectangle;
                    return;
                }
            }

            viewModel.Shapes.RemoveAt(viewModel.Shapes.Count - 1);
        }
        public async void OpenFileDialogMenu(string parametr)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            switch (parametr)
            {
                case "xml":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                         });
                    break;
                case "yaml":
                    openFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "YAML files",
                            Extensions = new string[] { "yaml" }.ToList()
                        });
                    break;
            }
            string[]? result = await openFileDialog.ShowAsync(this);
            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    dataContext.LoadDiagram(result[0]);
                }
            }
        }

        public async void SaveFileDialogMenu(string parametr)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            switch (parametr)
            {
                case "png":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "PNG files",
                            Extensions = new string[] { "png" }.ToList()
                        });
                    break;
                case "xml":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "XML files",
                            Extensions = new string[] { "xml" }.ToList()
                        });
                    break;
                case "json":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "JSON files",
                            Extensions = new string[] { "json" }.ToList()
                        });
                    break;
                case "yaml":
                    saveFileDialog.Filters.Add(
                        new FileDialogFilter
                        {
                            Name = "YAML files",
                            Extensions = new string[] { "yaml" }.ToList()
                        });
                    break;
            }
            string? result = await saveFileDialog.ShowAsync(this);

            if (DataContext is MainWindowViewModel dataContext)
            {
                if (result != null)
                {
                    canv = this.GetVisualDescendants().OfType<Canvas>().FirstOrDefault();
                    dataContext.SaveDiagram(result, parametr, canv);
                }
            }
        }
    }
}