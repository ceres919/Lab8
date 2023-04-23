using ClassDiagramEditor.Models.RectangleElements;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class JSONSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<RectangleWithConnectors> shapes, string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, shapes);
            }
        }
    }
}
