using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLEditor.Interfaces
{
    public interface IUmlObject
    {
        public bool Selected { get; set; }
        public PointF MiddlePoint { get; set; }
        public SizeF Size { get; set; }


        string GetSourceCode();

        string IsInCollision();

        void Select();

        void Unselect();

        void Draw(Graphics g);

        public abstract void Move(int x, int y);

        //public abstract void Resize(int x, int y);
    }
}
