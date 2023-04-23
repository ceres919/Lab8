using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class XMLLoader : IShapeEntityLoader
    {
        public IEnumerable<RectangleWithConnectors> Load(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<RectangleWithConnectors>));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ObservableCollection<RectangleWithConnectors>? newList = formatter.Deserialize(fs) as ObservableCollection<RectangleWithConnectors>;
                if (newList == null)
                {
                    newList = new ObservableCollection<RectangleWithConnectors>();
                }
                return newList;
            }
        }
    }
}
