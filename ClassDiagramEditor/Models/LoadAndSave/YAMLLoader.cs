using ClassDiagramEditor.Models.RectangleElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class YAMLLoader : IShapeEntityLoader
    {
        public IEnumerable<IShape> Load(string path)
        {
            var serializer = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithTagMapping("!rectangle", typeof(RectangleWithConnectors))
                .WithTagMapping("!connection", typeof(Connector))
                .Build();
            using (StreamReader file = File.OpenText(path))
            {
                ObservableCollection<IShape>? shapes = (ObservableCollection<IShape>)serializer.Deserialize(file, typeof(ObservableCollection<IShape>));
                return shapes;
            }
        }
    }
}
