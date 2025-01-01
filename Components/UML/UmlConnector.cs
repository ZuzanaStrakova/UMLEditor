using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;
using UMLEditor.Components.UML.Enums;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace UMLEditor.Components.UML
{
    public class UmlConnector : UmlObject
    {
        public UmlClass? StartObject { get; set; }
        public UmlClass? EndObject { get; set; }
        public ConnectionType Type { get; set; } = ConnectionType.Association;
        public int MinMultiplicity { get; set; } = 0;
        public int MaxMultiplicity { get; set; } = int.MaxValue;

        private List<PointF> points = new List<PointF>();

        public UmlConnector(UmlObject parent, UmlClass startObject, UmlClass endObject, ConnectionType type, ConnectionType endType, int minMultiplicity, int maxMultiplicity) : this(parent)
        {
            StartObject = startObject;
            EndObject   = endObject;
            Type        = type;
            MinMultiplicity = minMultiplicity;
            MaxMultiplicity = maxMultiplicity;
        }

        public UmlConnector(UmlObject parent) : base(parent)
        {
            UmlLineEnding startEnding = new UmlLineEnding(this);
            UmlLineEnding endEnding = new UmlLineEnding(this);

            Children.Add(startEnding);
            Children.Add(endEnding);
        }

        public override void Draw(Graphics g)
        {
            PointF startPoint = ((UmlLineEnding)Children[0]).ConnectionPoint;
            PointF endPoint = ((UmlLineEnding)Children[1]).ConnectionPoint;

            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);

            g.DrawLine(pen, startPoint, endPoint);

            base.Draw(g);    // vykreslení vnořených komponent    
        }

        public override string GetSourceCode()
        {
            throw new NotImplementedException();
        }

    }
}
