using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;

namespace UMLEditor.Components.UML.DataStructures
{
    public class Method
    {
        public string Name { get; set; }       = String.Empty;
        public string ReturnType { get; set; } = String.Empty;
        public string Visibility { get; set; } = String.Empty;
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
