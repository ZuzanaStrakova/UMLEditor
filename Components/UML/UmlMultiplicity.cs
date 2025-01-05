using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace UMLEditor.Components.UML
{
    internal class UmlMultiplicity : UmlTextBox
    {

        ContextMenuStrip contextMenu = new ContextMenuStrip();

        public UmlMultiplicity(UmlConnector parent) : base(parent)
        {
            InitializeContextMenu();
            Size = new SizeF(20, 20);
            Text = "1";
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }

        private void InitializeContextMenu()
        {
            // přidání položek do menu (podle zadání se má zobrazovat jen 1 nebo *
            contextMenu.Items.Add("1"   , null, (s,e) => { Text = "1"; });
            contextMenu.Items.Add("*"   , null, (s,e) => { Text = "*"; });
            contextMenu.AutoSize = true;
        }

        public void OnRightMouseButtonClick()
        {
            Point cursorPosition = Cursor.Position;
            contextMenu.Show(cursorPosition);

        }

    }
}
