﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;

namespace UMLEditor.Components.UML.DataStructures
{
    public class MethodDef
    {
        public AccessModifier Modifier { get; set; }
        public DataType ReturnType { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}