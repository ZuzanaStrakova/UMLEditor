using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML.Enums
{
    public enum Multiplicity
    {
        One,                    // 1
        ZeroOrOne,              // 0..1
        Many,                   // *
        ZeroOrMany,             // 0..*
        OneOrMany               // 1..*
    }
}
