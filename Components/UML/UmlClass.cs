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

        public List<PropertyDef> Properties { get; set; } = new List<PropertyDef>();

        public List<MethodDef> Methods { get; set; } = new List<MethodDef>();



        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
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
