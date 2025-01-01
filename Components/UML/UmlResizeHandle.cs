using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML
{
    internal class UmlResizeHandle : UmlObject
    {
        public UmlResizeHandle(UmlClass parent) : base(parent)
        {
            Size = new SizeF(12, 12); // velikost 10 x 10 bodů
        }

        public override PointF Position
        {
            get => new PointF(Parent!.Size.Width - Size.Width, Parent!.Size.Height - Size.Height); // pozice automaticky odvozena od parenta - v pravém dolním rohu
            set 
            {
                if(Parent != null) 
                    Parent.Size = new SizeF(value.X + Size.Width, value.Y + Size.Height); // změna pozice v rámci rodiče znamená změnu velikosti rodiče
            }          
        }

        public override void Draw(Graphics g)
        {
            // trojúhleník v pravém dolním rohu
            PointF[] trianglePoints = { BottomLeftCorner, TopRightCorner, BottomRightCorner };
            g.FillPolygon(Selected ? Brushes.Red : Brushes.White, trianglePoints);
            g.DrawPolygon(Pens.Black, trianglePoints);
            base.Draw(g);
        }
    }
}
