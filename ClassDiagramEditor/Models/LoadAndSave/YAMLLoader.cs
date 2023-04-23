using ClassDiagramEditor.Models.RectangleElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    public class YAMLLoader : IShapeEntityLoader
    {
        public IEnumerable<RectangleWithConnectors> Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}
