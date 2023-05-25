using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.ObjectModel;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public interface IShapeEntitySaver
    {
        void Save(ObservableCollection<IShape> people, string path);
    }
}
