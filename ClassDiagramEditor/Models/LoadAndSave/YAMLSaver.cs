using ClassDiagramEditor.Models.RectangleElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class YAMLSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<IShape> item, string path)
        {
            var serializer = new YamlDotNet.Serialization.SerializerBuilder()
                .WithTagMapping("!rectangle",typeof(RectangleWithConnectors))
                .WithTagMapping("!connection",typeof(Connector))
                .Build();
            using (StreamWriter sw = new StreamWriter(path))
                serializer.Serialize(sw,item);
        }
    }
}
