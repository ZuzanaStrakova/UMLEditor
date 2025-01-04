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
        public string Multiplicity { get; set; } = "1";

        private List<PointF> points = new List<PointF>();

        public UmlConnector(UmlObject parent, UmlClass startObject, UmlClass endObject, ConnectionType type) : this(parent)
        {
            StartObject = startObject;
            EndObject   = endObject;
            Type        = type;
        }

        public UmlConnector(UmlObject parent, ConnectionType type) : base(parent)
        {
            Type = type;

            UmlLineEnding startEnding = new UmlLineEnding(this);
            UmlLineEnding endEnding = new UmlLineEnding(this);

            Children.Add(startEnding);
            Children.Add(endEnding);
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
            PointF startPoint = GetIntersection(new RectangleF(StartObject.WorldTopLeftCorner, StartObject.Size), EndObject.WorldCenterPoint, out Edge edge1);
            PointF endPoint = GetIntersection(new RectangleF(EndObject.WorldTopLeftCorner, EndObject.Size), StartObject.WorldCenterPoint, out Edge edge2);
            PointF middlePoint = new PointF((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            PointF[] points = new PointF[5];
            points[0] = startPoint;
            points[1] = new PointF(middlePoint.X, startPoint.Y);
            points[2] = middlePoint;
            points[3] = new PointF(middlePoint.X, endPoint.Y);
            points[4] = endPoint;

            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);

            g.DrawLines(pen, points);

            base.Draw(g);    // vykreslení vnořených komponent    
        }


        // výpočet průsečíku úsečky vedoucí od bodu point do středu obdélníku rect se stěnami obdélníku
        // vrací polohu průsečíku a stěnu edge, na které průsečík leží
        public static PointF GetIntersection(RectangleF rect, PointF point, out Edge edge)
        {
            PointF center = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            float dx = point.X - center.X;
            float dy = point.Y - center.Y;

            float slope = dy / dx;

            PointF intersection = new PointF();
            edge = Edge.Top;

            if (Math.Abs(slope) > rect.Height / rect.Width)
            {
                if (dy > 0)
                {
                    intersection = new PointF(center.X + (rect.Height / 2) / slope, rect.Bottom);
                    edge = Edge.Bottom;
                }
                else
                {
                    intersection = new PointF(center.X - (rect.Height / 2) / slope, rect.Top);
                    edge = Edge.Top;
                }
            }
            else
            {
                if (dx > 0)
                {
                    intersection = new PointF(rect.Right, center.Y + (rect.Width / 2) * slope);
                    edge = Edge.Right;
                }
                else
                {
                    intersection = new PointF(rect.Left, center.Y - (rect.Width / 2) * slope);
                    edge = Edge.Left;
                }
            }

            return intersection;
        }
    }
}
