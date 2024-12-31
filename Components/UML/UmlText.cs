﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public class UmlText : UmlObject
    {
        public virtual string Text { get; set; } = string.Empty;
        public bool Border { get; set; } = false;
        public Font Font { get; set; } = new Font("Courier", 12);
        public Brush Brush { get; set; } = Brushes.Black;
        public Pen Pen { get; set; } = Pens.Black;


        public UmlText(UmlObject parent) : base(parent)
        {

        }

        public override void Draw(Graphics g)
        {
            RectangleF rectangle = new RectangleF(0, 0, Size.Width, Size.Height);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            g.DrawString(Text, Font, Brush, rectangle, format);

            if (Border)
            {
                g.DrawRectangle(Pen, rectangle);
            }

            base.Draw(g);
        }

        public override string GetSourceCode()
        {
            throw new NotImplementedException();
        }
    }
}
