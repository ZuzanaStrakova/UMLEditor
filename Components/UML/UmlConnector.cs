using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;
using UMLEditor.Components.UML.Enums;
using Newtonsoft.Json.Linq;

namespace UMLEditor.Components.UML
{
    public class UmlConnector : UmlObject
    {
        public static UmlConnector EmptyObject { get; set; } = new UmlConnector(null);

        public UmlObject StartObject { get; set; } = EmptyObject;
        public UmlObject EndObject { get; set; } = EmptyObject;

        public ConnectionType StartType 
        { 
            get => ((UmlLineEnding)Children[0]).Type; 
            set => ((UmlLineEnding)Children[0]).Type = value; 
        }
        public ConnectionType EndType 
        { 
            get => ((UmlLineEnding)Children[1]).Type; 
            set => ((UmlLineEnding)Children[1]).Type = value; 
        }

        public Multiplicity StartMultiplicity
        {
            get => ((UmlLineEnding)Children[0]).Multiplicity;
            set => ((UmlLineEnding)Children[0]).Multiplicity = value;
        }
        public Multiplicity EndMultiplicity
        {
            get => ((UmlLineEnding)Children[1]).Multiplicity;
            set => ((UmlLineEnding)Children[1]).Multiplicity = value;
        }


        public UmlConnector(UmlObject parent, UmlObject startObject, UmlObject endObject, ConnectionType startType, ConnectionType endType, Multiplicity startMultiplicity, Multiplicity endMultiplicity) : this(parent)
        {
            StartObject = startObject;
            EndObject = endObject;
            StartType = startType;
            EndType = endType;
            StartMultiplicity = startMultiplicity;
            EndMultiplicity = endMultiplicity;
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

        public override string IsInCollision()
        {
            throw new NotImplementedException();
        }

        public override void Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override void Select()
        {
            throw new NotImplementedException();
        }

        public override void Unselect()
        {
            throw new NotImplementedException();
        }
    }
}
