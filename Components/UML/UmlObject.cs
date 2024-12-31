using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using UMLEditor.Components.UML.Enums;
using UMLEditor.Interfaces;

namespace UMLEditor.Components.UML
{
    public abstract class UmlObject : IUmlObject
    {
        public UmlObject? Parent {  get; set; }
        public List<UmlObject> Children { get; set; } = new List<UmlObject>();
        public bool Selected { get; set; }
        public virtual SizeF Size { get; set; }
        public virtual PointF Position { get; set; }

        public PointF CenterPoint       { get => new PointF(Size.Width / 2, Size.Height / 2); }
        public PointF TopLeftCorner     { get => new PointF(0, 0); }
        public PointF TopRightCorner    { get => new PointF(Size.Width, 0); }
        public PointF BottomLeftCorner  { get => new PointF(0, Size.Height); }
        public PointF BottomRightCorner { get => new PointF(Size.Width, Size.Height); }
        public PointF LeftMiddlePoint   { get => new PointF(0, Size.Height / 2); }
        public PointF TopMiddlePoint    { get => new PointF(Size.Width / 2, 0); }
        public PointF RightMiddlePoint  { get => new PointF(Size.Width, Size.Height / 2); }
        public PointF BottomMiddlePoint { get => new PointF(Size.Width / 2, Size.Height); }

        // zřejmě nebude potřeba (budoucí využití pouze pro connector)
        public PointF WorldCenterPoint       { get => LocalToGlobal(CenterPoint      );} 
        public PointF WorldTopLeftCorner     { get => LocalToGlobal(TopLeftCorner    );} 
        public PointF WorldTopRightCorner    { get => LocalToGlobal(TopRightCorner   );} 
        public PointF WorldBottomLeftCorner  { get => LocalToGlobal(BottomLeftCorner );} 
        public PointF WorldBottomRightCorner { get => LocalToGlobal(BottomRightCorner);} 
        public PointF WorldLeftMiddlePoint   { get => LocalToGlobal(LeftMiddlePoint  );} 
        public PointF WorldTopMiddlePoint    { get => LocalToGlobal(TopMiddlePoint   );} 
        public PointF WorldRightMiddlePoint  { get => LocalToGlobal(RightMiddlePoint );} 
        public PointF WorldBottomMiddlePoint { get => LocalToGlobal(BottomMiddlePoint);} 


        protected UmlObject(UmlObject? parent)
        {
            Parent = parent;
        }


        public PointF LocalToGlobal(PointF point) // absolutní souřadnice z lokálních
        {
            point = new PointF(point.X + Position.X, point.Y + Position.Y);

            if (Parent != null)
            {
                return Parent.LocalToGlobal(point);
            }
            else
            {
                return point;
            }
        }


        public PointF GlobalToLocal(PointF point) // lokální souřadnice z absolutních
        {
            point = new PointF(point.X - Position.X, point.Y - Position.Y); 

            if (Parent != null)
            {
                return Parent.GlobalToLocal(point);
            }
            else
            {
                return point;
            }
        }


        public virtual UmlObject? ObjectFromPoint(float x, float y)   // x,y jsou souřadnice v souřadnicovém systému rodiče
        {
            float local_x = x - Position.X;                           // převedení na své lokální souřadnice
            float local_y = y - Position.Y;

            if (!IsInCollision(local_x, local_y)) return null;

            // nastala kolize, ale objekt může být schován pod svými potomky, kteří se vykreslují později a překrývají ho

            for (int i = Children.Count - 1; i >= 0; i--)             // => procházení potomků v opačném pořadí, aby bylo zaručeno nalezení těch překrývajících
            {
                UmlObject child = Children[i];
                UmlObject? result = child.ObjectFromPoint(local_x, local_y);
                if (result != null) return result;                    // tento potomek objekt překrývá a stejně jako on koliduje s bodem => má předonost
            }

            return this;                                              // žádné překrývání
        }


        public virtual void Draw(Graphics g)
        {
            var originalTransform = g.Transform;
            
            foreach (var child in Children)    // rekurzivní vykreslení všech podobjektů
            {
                g.TranslateTransform(child.Position.X, child.Position.Y);    // posunutí grafického kontextu na 0;0 - v levém horním rohu potomka

                child.Draw(g);

                g.Transform = originalTransform;    // obnovení transformace
            }
        }

        public virtual string GetSourceCode()
        {
            throw new NotImplementedException();
        }

        /* test kolize s bodem vyjádřeným v souřadném systému parenta
        public virtual bool IsInCollision(float x, float y)
        {
            return x >= Position.X
                && x <= Position.X + Size.Width
                && y >= Position.Y
                && y <= Position.Y + Size.Height;
        }
        */

        public virtual bool IsInCollision(float x, float y) // test na kolizi s bodem vyjádřeným v lokálních souřadnicích, počátek v levém horním rohu tohoto objektu
        {
            return x >= 0
                && x <= Size.Width
                && y >= 0
                && y <= Size.Height;
        }

        public virtual void Move(float x, float y)
        {
            throw new NotImplementedException();
        }

        public virtual void Select()
        {
            Selected = true;
        }

        public virtual void Unselect()
        {
            Selected = false;
        }
    }
}
