using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Components.UML.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace UMLEditor.Components.UML
{
    internal class UmlMultiplicity : UmlText
    {
        int Min { get; set; } = 0;
        int Max { get; set; } = int.MaxValue;

        ContextMenuStrip contextMenu = new ContextMenuStrip();

        public UmlMultiplicity(UmlConnector parent) : base(parent)
        {
            InitializeContextMenu();
            Size = new SizeF(30, 20);
        }

        public override string Text
        {
            get => this.ToString();
            //set => base.Text = value;
        }

        public override string ToString()
        {
            if (Max == int.MaxValue)
            {
                return $"{Min}..*";
            }
            else if (Min == Max)
            {
                return $"{Min}";
            }
            else
            {
                return $"{Min}..{Max}";
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }

        private void InitializeContextMenu()
        {
            // přidání položek do menu
            contextMenu.Items.Add("1"   , null, (s,e) => { Min = 1; Max = 1; });
            contextMenu.Items.Add("0..1", null, (s,e) => { Min = 0; Max = 1; });
            contextMenu.Items.Add("0..*", null, (s,e) => { Min = 0; Max = int.MaxValue; });
            contextMenu.Items.Add("1..*", null, (s,e) => { Min = 1; Max = int.MaxValue; });

            // Asociace:     A ──> B   ... obecný vztah mezi A a B (šipka - jednosměrný, bez šipky - obousměrný)
            // Agregace:     A ◇── B  ... B je částí A, B může existovat i bez A
            // Kompozice:    A ◆── B  ... B je částí A, B nemůže existovat bez A
            // Generalizace: A -─▷ B  ... A dědí z B
        }

        public void OnRightMouseButtonClick()
        {
            contextMenu.Show();
        }

        private void SetMultiplicity(int min, int max)
        {

        }
    }
}
