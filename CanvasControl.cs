using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UMLEditor
{
    public partial class CanvasControl : UserControl
    {
        private Bitmap canvas;

        private Graphics graphics;

        public CanvasControl()
        {
            InitializeComponent();
        }


        public Graphics GetGraphics()
        {
            return graphics;
        }


        protected override void OnLoad(EventArgs e)
        {
            canvas = new Bitmap(this.Width, this.Height);

            graphics = Graphics.FromImage(canvas);

            base.OnLoad(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            g.DrawImage(canvas, 0, 0);
        }
    }
}
