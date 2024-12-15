using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;

namespace UMLEditor.Components.UML
{
    public class UmlLineEnding : UmlObject
    {
        public ConnectionType Type { get; set; }
        public Multiplicity Multiplicity { get; set; }
        private PointF LeftMiddlePoint   { get => new PointF(0, Size.Height / 2); }
        private PointF TopMiddlePoint    { get => new PointF(Size.Width / 2, 0); }
        private PointF RightMiddlePoint  { get => new PointF(Size.Width, Size.Height / 2); }
        private PointF BottomMiddlePoint { get => new PointF(Size.Width / 2, Size.Height); }
        public PointF ConnectionPoint { get => new PointF(Size.Width * 3 / 2, Size.Height / 2); }


        public override void Draw(Graphics g)
        {
            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);
            Direction direction = Direction.Left;


            switch (Type)
            {
                case ConnectionType.Association:
                    DrawAssociation(g, direction, pen);
                    break;
                case ConnectionType.OneWayAssociation:
                    DrawOneWayAssociation(g, direction, pen);
                    break;
                case ConnectionType.Aggregation:
                    DrawAggregation(g, direction, pen);
                    break;
                case ConnectionType.Composition:
                    DrawComposition(g, direction, pen);
                    break;
                case ConnectionType.Generalization:
                    DrawGeneralization(g, direction, pen);
                    break;
            }

            g.DrawLine(pen, RightMiddlePoint, ConnectionPoint); 
        }

        

        private void DrawAssociation(Graphics g, Direction direction, Pen pen)
        {
            g.DrawLine(pen, TopMiddlePoint, RightMiddlePoint);
        }

        private void DrawOneWayAssociation(Graphics g, Direction direction, Pen pen)
        {
            g.DrawLine(pen, LeftMiddlePoint, RightMiddlePoint);

            g.DrawLine(pen, LeftMiddlePoint, TopMiddlePoint);
            g.DrawLine(pen, LeftMiddlePoint, BottomMiddlePoint);
        }

        private void DrawAggregation(Graphics g, Direction direction, Pen pen)
        {
            PointF[] points = new PointF[4];

            PointF point0 = LeftMiddlePoint;
            PointF point1 = TopMiddlePoint;
            PointF point2 = RightMiddlePoint;
            PointF point3 = BottomMiddlePoint;

            g.DrawPolygon(pen, points);
        }

        private void DrawComposition(Graphics g, Direction direction, Pen pen)
        {
            PointF[] points = new PointF[4];

            PointF point0 = LeftMiddlePoint;
            PointF point1 = TopMiddlePoint;
            PointF point2 = RightMiddlePoint;
            PointF point3 = BottomMiddlePoint;

            g.FillPolygon(pen.Brush, points);
        }

        private void DrawGeneralization(Graphics g, Direction direction, Pen pen)
        {
            g.DrawLine(pen, MiddlePoint, RightMiddlePoint);

            g.DrawLine(pen, LeftMiddlePoint, TopMiddlePoint);
            g.DrawLine(pen, LeftMiddlePoint, BottomMiddlePoint);
            g.DrawLine(pen, TopMiddlePoint, BottomMiddlePoint);
        }
    }
}
