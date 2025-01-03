using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UMLEditor.Components.UML
{
    public class UmlClassMember : UmlTextBox
    {
        public bool IsClassOrInterface { get => Text.StartsWith("class") || Text.StartsWith("interface"); }

        public bool IsMethod { get => Text.Contains('(') && Text.Contains(')'); }

        public bool IsProperty { get => !IsClassOrInterface && !IsMethod; }


        public UmlClassMember(UmlObject parent) : base(parent)
        {

        }

        public override SizeF Size
        {
            get => new SizeF(Parent?.Size.Width ?? base.Size.Width, this.Font.Height + 5);
            set => new SizeF(value.Width, value.Height);
        }


        //public override PointF Position 
        //{ 
        //    get => new PointF(0, base.Position.Y); 
        //    set => new PointF(0, value.Y); 
        //}

        public override void Draw(Graphics g)
        {
            Brush brush = Brushes.White;

            if (IsClassOrInterface)
            {
                brush = Brushes.AliceBlue;
            }

            g.FillRectangle(brush, new RectangleF(0, 0, Size.Width, Size.Height));

            base.Draw(g);
        }


        public void ValidateMethod()
        {
            string pattern = @"^(int|float|double|string|bool|char|byte|short|long|decimal|DateTime)\s+[a-zA-Z][a-zA-Z0-9]\s(\s((int|float|double|string|bool|char|byte|short|long|decimal|DateTime)\s+[a-zA-Z][a-zA-Z0-9]\s(,\s)?)*)$";

            Regex.IsMatch(Text, pattern);
        }

        public void ValidateProperty()
        {
            string pattern = @"^(int|float|double|string|bool|char|byte|short|long|decimal|DateTime)\s+[a-zA-Z][a-zA-Z0-9]*$";

            Regex.IsMatch(Text, pattern);
        }

        public void ValidateClassOrInterface()
        {
            string pattern = @"^(class|interface)\s+[a-zA-Z][a-zA-Z0-9]*$";

            Regex.IsMatch(Text, pattern);
        }
    }
}
