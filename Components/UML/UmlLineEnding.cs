using Newtonsoft.Json;
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
        public Direction Direction { get; set; }    // orientace šipky
        public LineEnd Symbol { get; set; }         // symbol (nic, šipka, diamant, plný diamant, trojúhelník)

        public UmlLineEnding(UmlObject parent) : base(parent)
        {
            Size = new SizeF(10, 10);
        }

        public override void Draw(Graphics g)
        {
            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);

            switch (Symbol)
            {
                case LineEnd.None:
                    DrawLine(g, pen);
                    break;
                case LineEnd.Arrow:
                    DrawArrow(g, pen);
                    break;
                case LineEnd.Diamond:
                    DrawDiamond(g, pen);
                    break;
                case LineEnd.FilledDiamond:
                    DrawFilledDiamond(g, pen);
                    break;
                case LineEnd.Triangle:
                    DrawTriangle(g, pen);
                    break;
            }

            g.ResetClip(); // symbol by se měl vykreslit celý i kdyby přesahoval rámeček parenta
        }
    
        private void DrawLine(Graphics g, Pen pen)
        {
            if (Direction == Direction.Left || Direction == Direction.Right)
            {
                g.DrawLine(pen, LeftMiddlePoint, RightMiddlePoint);
            }
            else
            {
                g.DrawLine(pen, TopMiddlePoint, BottomMiddlePoint);
            }
        }

        private void DrawArrow(Graphics g, Pen pen)
        {
            switch(Direction)
            {
                case Direction.Left:
                    g.DrawLine(pen, LeftMiddlePoint, TopRightCorner);
                    g.DrawLine(pen, LeftMiddlePoint, RightMiddlePoint);
                    g.DrawLine(pen, LeftMiddlePoint, BottomRightCorner);
                    break;
                case Direction.Right:
                    g.DrawLine(pen, RightMiddlePoint, TopLeftCorner);
                    g.DrawLine(pen, RightMiddlePoint, LeftMiddlePoint);
                    g.DrawLine(pen, RightMiddlePoint, BottomLeftCorner);
                    break;
                case Direction.Up:
                    g.DrawLine(pen, TopMiddlePoint, BottomLeftCorner);
                    g.DrawLine(pen, TopMiddlePoint, BottomMiddlePoint);
                    g.DrawLine(pen, TopMiddlePoint, BottomRightCorner);
                    break;
                case Direction.Down:
                    g.DrawLine(pen, BottomMiddlePoint, TopLeftCorner);
                    g.DrawLine(pen, BottomMiddlePoint, TopMiddlePoint);
                    g.DrawLine(pen, BottomMiddlePoint, TopRightCorner);
                    break;
            }
        }

        private void DrawDiamond(Graphics g, Pen pen)
        {
            g.ResetClip();
            PointF[] points = new PointF[4];

            points[0] = LeftMiddlePoint;
            points[1] = TopMiddlePoint;
            points[2] = RightMiddlePoint;
            points[3] = BottomMiddlePoint;

            g.FillPolygon(Brushes.White, points); // aby vnitřek obrazce nebyl průhledný a symbol překryl kresbu pod sebou
            g.DrawPolygon(pen, points);
        }

        private void DrawFilledDiamond(Graphics g, Pen pen)
        {
            PointF[] points = new PointF[4];

            points[0] = LeftMiddlePoint;
            points[1] = TopMiddlePoint;
            points[2] = RightMiddlePoint;
            points[3] = BottomMiddlePoint;

            g.FillPolygon(pen.Brush, points);
        }

        private void DrawTriangle(Graphics g, Pen pen)
        {
            PointF[] points = new PointF[3];

            switch (Direction)
            {
                case Direction.Left:
                    points[0] = LeftMiddlePoint;
                    points[1] = TopRightCorner;
                    points[2] = BottomRightCorner;
                    break;
                case Direction.Right:
                    points[0] = RightMiddlePoint;
                    points[1] = TopLeftCorner;
                    points[2] = BottomLeftCorner;
                    break;
                case Direction.Up:
                    points[0] = TopMiddlePoint;
                    points[1] = BottomLeftCorner;
                    points[2] = BottomRightCorner;
                    break;
                case Direction.Down:
                    points[0] = BottomMiddlePoint;
                    points[1] = TopLeftCorner;
                    points[2] = TopRightCorner;
                    break;
            }

            g.FillPolygon(Brushes.White, points); // aby vnitřek trojúhelníku nebyl průhledný a překryl kresbu pod sebou
            g.DrawPolygon(pen, points);
        }
    }
}
