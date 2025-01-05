using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML;
using UMLEditor.Components.UML.DataStructures;
using UMLEditor.Components.UML.Enums;

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


                w.WriteLine($"public class {child.ClassName} {GetListOfAncestors(diagram, child)}");
                w.WriteLine("{");

                foreach (Field field in child.Fields)
                {
                    w.WriteLine($"\t{field.Visibility} {field.DataType} {field.Name} {{ get; set; }}\n"); 
                }


                foreach (ConnectionInfo item in GetConnections(diagram, child))
                {
                    if (item.Multiplicity == "*")
                    {
                        if (item.Type == ConnectionType.Composition)
                        {
                            w.WriteLine($"\tpublic List<{item.ClassName}> {item.ClassName}s {{ get; private set; }} = new List<{item.ClassName}>();\n");
                        }
                        else
                        {
                            w.WriteLine($"\tpublic List<{item.ClassName}> {item.ClassName}s {{ get; set; }}\n");
                        }
                    }
                    else if (item.Multiplicity == "1")
                    {
                        if (item.Type == ConnectionType.Composition)
                        {
                            w.WriteLine($"\tpublic {item.ClassName} {item.ClassName} {{ get; private set; }} = new {item.ClassName}();\n");
                        }
                        else
                        {
                            w.WriteLine($"\tpublic {item.ClassName} {item.ClassName} {{ get; set; }}\n");
                        }
                    }
                    else if (item.Multiplicity == "0")
                    {
                        if (item.Type == ConnectionType.Composition)
                        {
                            w.WriteLine($"\tpublic {item.ClassName}? {item.ClassName} {{ get; private set; }}\n");
                        }
                        else
                        {
                            w.WriteLine($"\tpublic {item.ClassName}? {item.ClassName} {{ get; set; }}\n");
                        }
                    }
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


        public static string GetListOfAncestors(UmlDiagram diagram, UmlClass c)
        {
            var ancestors = GetConnections(diagram, c).Where(x => x.Type == ConnectionType.Generalization).Select(x => x.ClassName);

            if (ancestors.Count() == 0)
                return string.Empty;

            return ": " + String.Join(", ", ancestors);
        }


        public static IEnumerable<ConnectionInfo> GetConnections(UmlDiagram diagram, UmlClass c)
        {
            return diagram.Children.OfType<UmlConnector>().Where(x => x.StartObject == c).Select(x => new ConnectionInfo() { ClassName = x.EndObject?.ClassName??"", Type = x.Type, Multiplicity = x.EndMultiplicity});
        }
    }

    public struct ConnectionInfo
    {
        public string ClassName { get; set; }
        public ConnectionType Type { get; set; }
        public string Multiplicity { get; set; }
    }
}
