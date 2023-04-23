using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.Generic;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public interface IShapeEntityLoader
    {
        IEnumerable<RectangleWithConnectors> Load(string path);
    }
}
