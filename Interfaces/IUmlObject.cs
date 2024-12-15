using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML;

namespace UMLEditor.Interfaces
{
    public interface IUmlObject
    {
        public UmlObject? Parent { get; set; }
        public List<UmlObject> Children { get; set; }
        public bool Selected { get; set; }
        public PointF MiddlePoint { get; }
        public SizeF Size { get; set; }
        public PointF Position { get; set; }


        string GetSourceCode();

        string IsInCollision();

        void Select();

        void Unselect();

        void Draw(Graphics g);

        public abstract void Move(int x, int y);

        //public abstract void Resize(int x, int y);
    }
}
