using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
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


            class1.FieldsText = """ 
                                 + Id: Guid
                                 # Name: string
                                 - BirthYear: int
                                 ~ RegistrationDate: DateTime
                                 """;

            class1.MethodsText = """ 
                                 + ToString(): string
                                 # GetName(id: Guid): string
                                 - Register(name: string, birthYear: int)
                                 """;


            diagram.Children.Add(class1);
            diagram.Children.Add(class2);
            //diagram.Children.Add(connector);
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
        public bool multiSelect = false;
        public List<UmlClass> multiSelectedObjects = new List<UmlClass>();

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                multiSelect = true;                    // funguje pouze pro select pøi vytváøení vztahu, shift se nepouští

                selectedObject = diagram.ObjectFromPoint(e.X, e.Y);

                if (selectedObject != null && selectedObject is UmlClass)
                {
                    selectedObject.Select();
                    multiSelectedObjects.Add((UmlClass)selectedObject);
                    Refresh();
                }

            }
            else if(e.Button == MouseButtons.Left)
            {
                multiSelectedObjects.Clear();
                foreach(var item in multiSelectedObjects)
                {
                    item.Unselect();
                }

                startPoint = e.Location;
                isDragging = false;

                // Kód pro výbìr objektu 
                selectedObject = diagram.ObjectFromPoint(e.X, e.Y);
                if (selectedObject != null)
                {
                    if (selectedObject is UmlTextBox && selectedObject.Parent != null) // místo textových objektù zvolí jejich parenta, textové objekty reagují jen na dvojklik
                        selectedObject = selectedObject.Parent;
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
                    // Kód pro ukonèení drag & drop
                }
                else
                {
                    // Kód pro kliknutí
                }
            }

            // Kód pro zrušení výbìru
            if (selectedObject != null && Control.ModifierKeys != Keys.Shift)
            {
                selectedObject.Unselect();
                selectedObject = null;
                Refresh();
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // myš se hýbe a je stisknuté levé tlaèítko (mùže to být tøes ruky nebo zámìrý drag & drop)
            {
                if (Math.Abs(e.X - startPoint.X) > SystemInformation.DoubleClickSize.Width ||
                    Math.Abs(e.Y - startPoint.Y) > SystemInformation.DoubleClickSize.Height) // pokud je posunutí vìtší než tolerance, je to zámìr
                {
                    isDragging = true;
                    // Kód pro pøetažení objektu
                    if (selectedObject != null)
                    {
                        selectedObject.Position += new SizeF(e.X - startPoint.X, e.Y - startPoint.Y);
                        startPoint = e.Location;
                        Refresh();
                    }
                }
            }
        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedObject != null)
            {
                selectedObject.Unselect();
            }

            selectedObject = diagram.ObjectFromPoint(e.X, e.Y);

            if (selectedObject is UmlTextBox)
            {
                selectedObject.Select();
                ((UmlTextBox)selectedObject).StartEditing(pictureBox);
                Refresh();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedObject is UmlTextBox)
            {
                //((UmlTextBox)selectedObject).HandleKeyPress(e.KeyCode, e.Control); ... zatím není potøeba
            }
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fd = new FolderBrowserDialog())
            {
                fd.Description = "Vyberte složku";
                fd.ShowNewFolderButton = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = fd.SelectedPath;

                    GenerateCode.GetSourceCode(diagram, selectedPath);
                }
            }
        }

        private void bitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
                sfd.DefaultExt = "*.bmp";
                sfd.Title = "Save As...";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = new Bitmap((int)diagram.Size.Width, (int)diagram.Size.Height);

                    Graphics g = Graphics.FromImage(bitmap);

                    diagram.Draw(g);

                    bitmap.Save(sfd.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Project files (*.json)|*.json|All files (*.*)|*.*";
                sfd.DefaultExt = "*.json";
                sfd.Title = "Save project";

                if (sfd.ShowDialog() == DialogResult.OK)
                    diagram.SaveTo(sfd.FileName);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Project files (*.json)|*.json|All files (*.*)|*.*";
                ofd.DefaultExt = "*.json";
                ofd.Title = "Open project";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    diagram = UmlDiagram.Load(ofd.FileName) ?? new UmlDiagram();
                    Refresh();
                }
            }
        }

        private void toolStripButtonNewClass_Click(object sender, EventArgs e)
        {
            UmlClass c = new UmlClass(diagram, "ClassName");
            c.Position = new PointF(150, 250);
            c.Size = new SizeF(150, 250);

            diagram.Children.Add(c);
            Refresh();
        }

        private void toolStripButtonAssociation_Click(object sender, EventArgs e)
        {
            if (multiSelect == true && multiSelectedObjects.Count == 2)
            {
                UmlConnector line = new UmlConnector(diagram, ConnectionType.Association);

                line.StartObject = multiSelectedObjects[0];
                line.EndObject = multiSelectedObjects[1];

                diagram.Children.Add(line);
                Refresh();
            }
        }

        private void toolStripButtonOneWayAssociation_Click(object sender, EventArgs e)
        {
            if (multiSelect == true && multiSelectedObjects.Count == 2)
            {
                UmlConnector line = new UmlConnector(diagram, ConnectionType.OneWayAssociation);

                line.StartObject = multiSelectedObjects[0];
                line.EndObject = multiSelectedObjects[1];

                diagram.Children.Add(line);
                Refresh();
            }
        }

        private void toolStripButtonAggregation_Click(object sender, EventArgs e)
        {
            if (multiSelect == true && multiSelectedObjects.Count == 2)
            {
                UmlConnector line = new UmlConnector(diagram, ConnectionType.Aggregation);

                line.StartObject = multiSelectedObjects[0];
                line.EndObject = multiSelectedObjects[1];

                diagram.Children.Add(line);
                Refresh();
            }
        }

        private void toolStripButtonComposition_Click(object sender, EventArgs e)
        {
            if (multiSelect == true && multiSelectedObjects.Count == 2)
            {
                UmlConnector line = new UmlConnector(diagram, ConnectionType.Composition);

                line.StartObject = multiSelectedObjects[0];
                line.EndObject = multiSelectedObjects[1];

                diagram.Children.Add(line);
                Refresh();
            }
        }

        private void toolStripButtonGeneralization_Click(object sender, EventArgs e)
        {
            if (multiSelect == true && multiSelectedObjects.Count == 2)
            {
                UmlConnector line = new UmlConnector(diagram, ConnectionType.Generalization);

                line.StartObject = multiSelectedObjects[0];
                line.EndObject = multiSelectedObjects[1];

                diagram.Children.Add(line);
                Refresh();
            }
        }

        private void toolStripButtonBin_Click(object sender, EventArgs e)
        {
            if (multiSelect)
            {
                foreach (UmlObject item in multiSelectedObjects)
                {
                    diagram.Children.Remove(item);
                    Refresh();
                }
            }
            //else if (selectedObject != null)
            //{
            //    diagram.Children.Remove(selectedObject);
            //    Refresh();
            //}
            else
            {
                Refresh();
            }
        }
    }
}
