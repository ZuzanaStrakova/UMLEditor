using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.DataStructures;
using UMLEditor.Components.UML.Enums;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public class UmlClass : UmlObject
    {
        public string ClassName { get; set; } = string.Empty;

        public List<string> Properties { get; set; } = new List<string>();

        public List<string> Methods { get; set; } = new List<string>();



        public UmlClass(UmlObject parent, string className) : base(parent)
        {
            Size = new SizeF(100, 100);
            ClassName = className;

            //UmlText text = new UmlText();
            //text.Text = className;
            //text.Position = new PointF(0, 0);
            //text.Size = new SizeF(this.Size.Width, text.Font.Size * 1.2f);

            //Children.Add(text);
        }


        public void AddMember(string item)
        {
            UmlObject? lastChild = Children.LastOrDefault();

            float y = 0;

            if (lastChild != null)
                y = lastChild.Position.Y + lastChild.Size.Height;

            UmlClassMember text = new UmlClassMember(this);
            text.Text = item;
            text.Position = new PointF(0, y);
            text.Size = new SizeF(this.Size.Width, text.Font.Size * 1.2f);
        }

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, 0, 0, Size.Width, Size.Height);

            base.Draw(g);
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
