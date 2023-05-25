using ClassDiagramEditor.Models.RectangleElements;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class JSONLoader : IShapeEntityLoader
    {
        public IEnumerable<IShape> Load(string path)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.TypeNameHandling= Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            using (StreamReader file = File.OpenText(path))
            {
                ObservableCollection<IShape>? shapes = (ObservableCollection<IShape>)serializer.Deserialize(file, typeof(ObservableCollection<IShape>));
                return shapes;
            }
        }
    }
}
