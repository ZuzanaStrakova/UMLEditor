using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML.Enums
{
    public enum LineEnd
    {
        None,           // Žádný symbol
        Arrow,          // Šipka (pro závislost)
        Diamond,        // Prázdný kosočtverec (pro agregaci)
        FilledDiamond,  // Plný kosočtverec (pro kompozici)
        Triangle        // Prázdný trojúhelník (pro dědičnost a realizaci)
    }
}
