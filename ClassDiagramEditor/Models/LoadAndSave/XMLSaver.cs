using ClassDiagramEditor.Models.RectangleElements;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class XMLSaver : IShapeEntitySaver
    {
        public void Save(ObservableCollection<IShape> data, string path)
        {
            var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true,  KnownTypes = new List<System.Type> { typeof(RectangleWithConnectors),typeof(Connector)} };
            var dcs = new DataContractSerializer(typeof(ObservableCollection<IShape>), dcss);

            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                dcs.WriteObject(stream, data);
            }
        }
    }
}
