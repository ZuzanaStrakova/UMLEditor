using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;

namespace UMLEditor.Components.UML.DataStructures
{
    public class PropertyDef
    {
        public AccessModifier Modifier { get; set; }
        public DataType DataType { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
