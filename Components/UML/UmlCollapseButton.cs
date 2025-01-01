using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML
{
    internal class UmlCollapseButton : UmlObject
    {
        public UmlCollapseButton(UmlClass parent) : base(parent)
        {
            Size = new SizeF(12, 12); // velikost 10 x 10 bodů
        }

        public override PointF Position
        {
            get => new PointF(Parent.Size.Width - Size.Width, 0); // pozice automaticky odvozena od parenta - v pravém horním rohu
        }

        public override void Draw(Graphics g)
        {
            // parent (UmlClass) zkolabován => +, expandován => -
            g.FillRectangle(Selected ? Brushes.Red : Brushes.White, new RectangleF(0, 0, Size.Width, Size.Height));
            g.DrawRectangle(Pens.Black, new RectangleF(0, 0, Size.Width, Size.Height));
            g.DrawLine(Pens.Black, LeftMiddlePoint, RightMiddlePoint); // vodorovná čárka
            if (((UmlClass?)Parent)!.Collapsed)
            {
                g.DrawLine(Pens.Black, TopMiddlePoint, BottomMiddlePoint); // svislá čárka
            }
            base.Draw(g);
        }
    }
}
