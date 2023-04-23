using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class JSONLoader : IShapeEntityLoader
    {
        public IEnumerable<RectangleWithConnectors> Load(string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling= Newtonsoft.Json.TypeNameHandling.Auto;
            using (StreamReader file = File.OpenText(path))
            {
                ObservableCollection<RectangleWithConnectors>? shapes = (ObservableCollection<RectangleWithConnectors>)serializer.Deserialize(file, typeof(ObservableCollection<RectangleWithConnectors>));
                return shapes;
            }
        }
    }
}
