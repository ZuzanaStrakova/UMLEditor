using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public abstract class UmlObject : IUmlObject
    {
        public UmlObject? Parent {  get; set; }
        public List<UmlObject> Children { get; set; } = new List<UmlObject>();
        public bool Selected { get; set; }
        public PointF MiddlePoint { get => Position + Size / 2; }
        public SizeF Size { get; set; }
        public PointF Position { get; set; }

        public virtual void Draw(Graphics g)
        {
            var originalTransform = g.Transform;
            
            foreach (var child in Children)    // rekurzivní vykreslení všech podobjektů
            {
                g.TranslateTransform(child.Position.X, child.Position.Y);    // posunutí grafického kontextu - 0;0 v levém horním rohu potomka

                child.Draw(g);

                g.Transform = originalTransform;    // obnovení transformace
            }
        }

        public virtual string GetSourceCode()
        {
            throw new NotImplementedException();
        }

        public virtual string IsInCollision()
        {
            throw new NotImplementedException();
        }

        public virtual void Move(int x, int y)
        {
            throw new NotImplementedException();
        }

        public virtual void Select()
        {
            throw new NotImplementedException();
        }

        public virtual void Unselect()
        {
            throw new NotImplementedException();
        }
    }
}
