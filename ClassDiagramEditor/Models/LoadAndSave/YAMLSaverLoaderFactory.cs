using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDiagramEditor.Models.LoadAndSave
{
    internal class YAMLSaverLoaderFactory : ISaverLoaderFactory
    {
        public IShapeEntityLoader CreateLoader()
        {
            return new YAMLLoader();
        }

        public IShapeEntitySaver CreateSaver()
        {
            return new YAMLSaver();
        }

        public bool IsMatch(string path)
        {
            return ".yaml".Equals(Path.GetExtension(path));
        }
    }
}
