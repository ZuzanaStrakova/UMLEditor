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

        private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor, // umožnění deserializování objektů, kterým chybí default konstruktor
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,            // UmlConnector drží odkaz na UmlProperty, při deserializaci vznikne nová instance UmlProperty => toto nastavení zajistí, aby se UmlConnector odkazoval na tuto novou UmlProperty
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        public UmlDiagram() : base(null)
        {

        }

        public string SerializeToJSON()
        {
            return JsonConvert.SerializeObject(this, jsonSettings);
        }

        public static UmlDiagram? DeserializeJSON(string json)
        {
            return JsonConvert.DeserializeObject<UmlDiagram>(json, jsonSettings);
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
            g.Clear(Color.LightGray);

            base.Draw(g);
        }
    }
}
