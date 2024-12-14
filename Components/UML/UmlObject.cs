using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public abstract class UmlObject : IUmlObject
    {
        public bool Selected { get; set; }
        public PointF MiddlePoint { get; set; }
        public SizeF Size { get; set; }

        public virtual void Draw(Graphics g)
        {
            throw new NotImplementedException();
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
