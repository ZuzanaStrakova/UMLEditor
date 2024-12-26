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
            UmlClass class1 = new UmlClass(diagram, "Client");
            UmlClass class2 = new UmlClass(diagram, "IPerson");
            UmlConnector connector = new UmlConnector(diagram);

            diagram.Position = new PointF(0, 0);
            diagram.Size = new SizeF(1000, 1000);
            class1.Position = new PointF(200, 100);
            class1.Size = new SizeF(100, 160);
            class2.Position = new PointF(400, 120);
            class2.Size = new SizeF(100, 180);


            class1.AddMember("int Id");
            class1.AddMember("string Prijmeni");
            class1.AddMember("string Jmeno");
            class1.AddMember("DateTime DatumNarozeni");


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
