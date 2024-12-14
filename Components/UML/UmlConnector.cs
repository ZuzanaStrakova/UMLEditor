using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public class UmlConnector : UmlObject
    {
        public static UmlConnector EmptyObject { get; set; } = new UmlConnector();

        public UmlObject StartObject { get; set; } = EmptyObject;
        public UmlObject EndObject { get; set; } = EmptyObject;

        public ConnectionType StartType { get; set; }
        public ConnectionType EndType { get; set; }

        public Multiplicity StartMultiplicity { get; set; }
        public Multiplicity EndMultiplicity { get; set; }



        public override void Draw(Graphics g)
        {
            PointF startPoint = new PointF(StartObject.MiddlePoint.X, StartObject.MiddlePoint.Y + StartObject.Size.Width / 2);
            PointF endPoint = new PointF(EndObject.MiddlePoint.X, EndObject.MiddlePoint.Y + EndObject.Size.Width / 2);

            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);

            g.DrawLine(pen, startPoint, endPoint);
            
            switch (StartType)
            {
                case ConnectionType.Association:
                    break;
                case ConnectionType.OneWayAssociation: 
                    g.DrawLine(pen, startPoint, startPoint + new SizeF(5, 5));
                    g.DrawLine(pen, startPoint, startPoint + new SizeF(5, -5));
                    break;
                case ConnectionType.Aggregation:
                    g.DrawLine(pen, startPoint, startPoint + new SizeF(5, 5));
                    g.DrawLine(pen, startPoint, startPoint + new SizeF(5, -5));
                    g.DrawLine(pen, startPoint + new SizeF(10, 0), startPoint + new SizeF(5, 5));
                    g.DrawLine(pen, startPoint + new SizeF(10, 0), startPoint + new SizeF(5, -5));
                    break;
                case ConnectionType.Composition: 
                    break;
                case ConnectionType.Generalization: 
                    break;
            }
        }

        public string GetSourceCode()
        {
            throw new NotImplementedException();
        }

        public string IsInCollision()
        {
            throw new NotImplementedException();
        }

        public void Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public void Unselect()
        {
            throw new NotImplementedException();
        }
    }

    public enum ConnectionType
    {
        Association,            // multiplicita (čára bez zakončení)
        OneWayAssociation,      // jednosměrná asociace (šipka)
        Aggregation,            // agregace (prázdný kosočtverec)
        Composition,            // kompozice (plný kosočtverec)
        Generalization          // generalizace (prázdný trojúhelník)
    }

    public enum Multiplicity
    {
        One,                    // 1
        ZeroOrOne,              // 0..1
        Many,                   // *
        ZeroOrMany,             // 0..*
        OneOrMany               // 1..*
    }
}
