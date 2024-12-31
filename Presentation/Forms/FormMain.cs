using UMLEditor.Components.UML;
using UMLEditor.Components.UML.Enums;

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

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            diagram.Draw(g);
        }

        /*
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            var g = pictureBox.GetGraphics();

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            diagram.Draw(g);
        }
        */

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private UmlObject? selectedObject = null;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                isDragging = false;

                // k�d pro v�b�r objektu 
                selectedObject = diagram.ObjectFromPoint(e.X, e.Y);
                if (selectedObject != null)
                {
                    selectedObject.Select();
                    Refresh();
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDragging)
                {
                    // K�d pro ukon�en� drag & drop
                }
                else
                {
                    // K�d pro kliknut�
                }
            }

            // K�d pro zru�en� v�b�ru
            if (selectedObject != null)
            {
                selectedObject.Unselect();
                selectedObject = null;
                Refresh();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // my� se h�be a je stisknut� lev� tla��tko (m��e b�t t�es ruky nebo z�m�r� drag & drop) -------------------
            {
                isDragging = true;
                // K�d pro p�eta�en� objektu
                if (selectedObject != null)
                {
                    selectedObject.Position += new SizeF(e.X - startPoint.X, e.Y - startPoint.Y);
                    startPoint = e.Location;
                    Refresh();
                }
            }
        }

    }
}
