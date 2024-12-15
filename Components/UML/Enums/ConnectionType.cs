using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML.Enums
{
    public enum ConnectionType
    {
        Association,            // multiplicita (čára bez zakončení)
        OneWayAssociation,      // jednosměrná asociace (šipka)
        Aggregation,            // agregace (prázdný kosočtverec)
        Composition,            // kompozice (plný kosočtverec)
        Generalization          // generalizace (prázdný trojúhelník)
    }
}
