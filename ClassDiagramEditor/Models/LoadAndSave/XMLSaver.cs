using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class XMLSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<RectangleWithConnectors> data, string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<RectangleWithConnectors>));

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                formatter.Serialize(stream, data);
            }
        }
    }
}
