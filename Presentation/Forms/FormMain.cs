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
            UmlClass class1 = new UmlClass();
            UmlClass class2 = new UmlClass();
            UmlConnector connector = new UmlConnector();

            diagram.Position = new PointF(0, 0);
            diagram.Size = new SizeF(1000, 1000);
            class1.Position = new PointF(200, 100);
            class1.Size = new SizeF(100, 160);
            class2.Position = new PointF(400, 120);
            class2.Size = new SizeF(100, 180);

            diagram.Children.Add(class1);
            diagram.Children.Add(class2);
            diagram.Children.Add(connector);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            var g = canvasControl1.GetGraphics();
            diagram.Draw(g);
        }
    }
}
