using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.DataStructures;
using UMLEditor.Components.UML.Enums;
using UMLEditor.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace UMLEditor.Components.UML
{
    public class UmlClass : UmlObject
    {
        public string ClassName { get; set; } = string.Empty;

        public string _fieldsText { get; set; }  = string.Empty;   // definice fieldů a metod uchovávána jakožto víceřádkový text, který se přímo zobrazuje či edituje v GUI
        public string _methodsText { get; set; }  = string.Empty;

        public List<Field> Fields { get => ParseFields(_fieldsText); }

        public List<Method> Methods { get => ParseMethods(_methodsText); }

        public bool Collapsed { get; set; } = false;


        public UmlClass(UmlObject parent, string className) : base(parent)
        {
            Size = new SizeF(100, 100);
            ClassName = className;

            Children.Add(new UmlCollapseButton(this));
            Children.Add(new UmlResizeHandle(this));
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.White, 0, 0, Size.Width, Size.Height);

            g.DrawRectangle(Selected ? Pens.Red : Pens.Black, 0, 0, Size.Width, Size.Height);

            base.Draw(g);
        }

        private List<Field> ParseFields(string text)
        {
            List<Field> list = new List<Field>();

            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                // [viditelnost] [název]: [typ]
                // ^[\+\-\#\~] \w[\w\d_]*: \w[\w\d_]*$

                var parts = line.Split(new[] { ' ', ':', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 3) continue; // chybná definice fieldu => přeskočení

                list.Add(new Field
                {
                    Name = parts[1],
                    DataType = parts[2],
                    Visibility = GetVisibility(parts[0])
                });
            }

            return list;
        }

        private List<Method> ParseMethods(string text)
        {
            List<Method> list = new List<Method>();

            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                // <viditelnost> <název>(<parametry>): <návratový typ>
                // ^[\+\-\#\~] \w[\w\d_]*\(\s*(\w[\w\d_]*: \w[\w\d_]*(,\s*\w[\w\d_]*: \w[\w\d_]*)*)?\s*\): \w[\w\d_]*$

                string symbol = line[0].ToString();
                string[] parts = line.Substring(2).Split(new[] { '(', ')'});

                string name = parts[0];
                string parameters = parts[1].Trim();
                string returnType = parts[2].Replace(':', ' ').Trim();

                if (returnType == string.Empty)
                    returnType = "void";

                list.Add(new Method
                {
                    Name = name,
                    ReturnType = returnType,
                    Visibility = GetVisibility(symbol),
                    Parameters = ParseParameters(parameters)
                });
            }

            return list;
        }

        private List<Parameter> ParseParameters(string text)
        {
            List<Parameter> list = new List<Parameter>();

            // <name>: <typ>, <name>: <typ>, <name>: <typ> ...

            foreach (var param in text.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                string[] parts = param.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries);

                list.Add(new Parameter
                {
                    Name = parts[0].Trim(),
                    DataType = parts[1].Trim()
                });
            }

            return list;
        }

        private string GetVisibility(string symbol)
        {
            switch (symbol)
            {
                case "+": return "public";
                case "-": return "private";
                case "#": return "protected";
                case "~": return "internal";
                default: return "";
            }
        }

        private string GetSymbol(string visibility)
        {
            switch (visibility)
            {
                case "public": return "+";
                case "private": return "-";
                case "protected": return "#";
                case "internal": return "~";
                default: return "-";
            }
        }

        public override string GetSourceCode()
        {
            throw new NotImplementedException();
        }
    }
}
