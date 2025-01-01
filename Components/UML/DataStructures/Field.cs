using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;

namespace UMLEditor.Components.UML.DataStructures
{
    public class Field
    {
        public string Visibility { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
