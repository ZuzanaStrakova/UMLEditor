using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UMLEditor.Components.UML
{
    public class UmlDiagram : UmlObject
    {
        public string SerializeToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static UmlDiagram? DeserializeJSON(string json)
        {
            return JsonConvert.DeserializeObject<UmlDiagram>(json);
        }

        public void SaveTo(string fileName)
        {
            File.WriteAllText(fileName, SerializeToJSON());
        }

        public static UmlDiagram? Load(string fileName)
        {
            return DeserializeJSON(File.ReadAllText(fileName));
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }
    }
}
