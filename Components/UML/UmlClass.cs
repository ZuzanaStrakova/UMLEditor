using System.Windows.Forms.VisualStyles;
using UMLEditor.Components.UML.DataStructures;

namespace UMLEditor.Components.UML
{
    public class UmlClass : UmlObject
    {
        public string ClassName   { get => textBoxClassName.Text; set => textBoxClassName.Text = value; }
        public string FieldsText  { get => textBoxFields.Text; set => textBoxFields.Text = value; }
        public string MethodsText { get => textBoxMethods.Text; set => textBoxMethods.Text = value; }

        public List<Field> Fields   { get => ParseFields(FieldsText); }
        public List<Method> Methods { get => ParseMethods(MethodsText); }

        public bool Collapsed { get; set; } = false;

        // child komponenty
        private UmlTextBox textBoxClassName;
        private UmlTextBox textBoxFields;
        private UmlTextBox textBoxMethods;
        private UmlCollapseButton collapseButton;
        private UmlResizeHandle resizeHandle;

        public UmlClass(UmlObject parent, string className) : base(parent)
        {
            Size = new SizeF(100, 100);

            textBoxClassName = new UmlTextBox(this) { Border = true };
            textBoxFields    = new UmlTextBox(this) { Multiline = true, Border = true };
            textBoxMethods   = new UmlTextBox(this) { Multiline = true };
            collapseButton   = new UmlCollapseButton(this);
            resizeHandle     = new UmlResizeHandle(this);

            Children.Add(textBoxClassName);
            Children.Add(textBoxFields);
            Children.Add(textBoxMethods);
            Children.Add(collapseButton);
            Children.Add(resizeHandle);

            ClassName = className;
        }

        private void RefreshLayout()
        {
            textBoxClassName.Position  = new PointF(0, 0);
            textBoxClassName.Size      = new SizeF(Size.Width, textBoxClassName.TextSize().Height+10);

            textBoxFields.Position = new PointF(0, textBoxClassName.Position.Y + textBoxClassName.Size.Height);
            textBoxFields.Size     = new SizeF(Size.Width, textBoxFields.TextSize().Height+10);

            textBoxMethods.Position = new PointF(0, textBoxFields.Position.Y + textBoxFields.Size.Height);
            textBoxMethods.Size     = new SizeF(Size.Width, textBoxMethods.TextSize().Height+10);
        }

        public override void Draw(Graphics g)
        {
            RefreshLayout();

            g.FillRectangle(Brushes.White, 0, 0, Size.Width, Size.Height);

            base.Draw(g);

            g.DrawRectangle(Selected ? Pens.Red : Pens.Black, 0, 0, Size.Width, Size.Height);
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

    }
}
