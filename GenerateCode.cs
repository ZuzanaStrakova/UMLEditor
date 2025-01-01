using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML;
using UMLEditor.Components.UML.DataStructures;

namespace UMLEditor
{
    public static class GenerateCode
    {
        public static void GetSourceCode(UmlDiagram diagram, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (UmlClass child in diagram.Children.OfType<UmlClass>())
            {
                string filePath = Path.Combine(folderPath, child.ClassName + ".cs");

                StreamWriter w = new StreamWriter(filePath);


                w.WriteLine($"public class {child.ClassName}");
                w.WriteLine("{");

                foreach (Field field in child.Fields)
                {
                    w.WriteLine($"\t{field.Visibility} {field.DataType} {field.Name} {{ get; set; }}\n"); 
                }

                w.WriteLine("\n\n");

                foreach (Method method in child.Methods)
                {
                    w.WriteLine($"\t{method.Visibility} {method.ReturnType} {method.Name}({ParamsToString(method.Parameters)})");
                    w.WriteLine("\t{");
                    w.WriteLine("\t\tthrow new NotImplementedException();");
                    w.WriteLine("\t}\n");
                }

                w.WriteLine("}");

                w.Close();
            }
        }

        public static string ParamsToString(List<Parameter> parameters)
        {
            string text = string.Empty;

            foreach (Parameter parameter in parameters)
            {
                text += $"{parameter.DataType} {parameter.Name}, ";
            }

             return text.TrimEnd(',',' ');
        }
    }
}
