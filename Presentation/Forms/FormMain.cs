using UMLEditor.Components.UML;

namespace UMLEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        UmlDiagram diagram = new UmlDiagram();

        private void FormMain_Load(object sender, EventArgs e)
        {
            UmlClass class1 = new UmlClass("Client");
            UmlClass class2 = new UmlClass("IPerson");
            UmlConnector connector = new UmlConnector();

            diagram.Position = new PointF(0, 0);
            diagram.Size = new SizeF(1000, 1000);
            class1.Position = new PointF(200, 100);
            class1.Size = new SizeF(100, 160);
            class2.Position = new PointF(400, 120);
            class2.Size = new SizeF(100, 180);



            for (int i = 0; i < 10; i++)
            {
                var text = new UmlText() { Text = i.ToString() };
                text.Position = new PointF(i * 2, i * 20);
                text.Border = true;
                text.Size = new SizeF(50, 18);


                class1.Children.Add(text);
            }

            diagram.Children.Add(class1);
            diagram.Children.Add(class2);
            diagram.Children.Add(connector);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            var g = canvasControl1.GetGraphics();

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            diagram.Draw(g);
        }
    }
}
