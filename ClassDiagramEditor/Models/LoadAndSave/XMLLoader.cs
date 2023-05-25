using ClassDiagramEditor.Models.RectangleElements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class XMLLoader : IShapeEntityLoader
    {
        public IEnumerable<IShape> Load(string path)
        {
            var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true, KnownTypes = new List<System.Type> { typeof(RectangleWithConnectors), typeof(Connector) } };
            var dcs = new DataContractSerializer(typeof(ObservableCollection<IShape>), dcss);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                ObservableCollection<IShape>? newList = dcs.ReadObject(reader) as ObservableCollection<IShape>;
                if (newList == null)
                {
                    newList = new ObservableCollection<IShape>();
                }
                return newList;
            }
        }
    }
}
