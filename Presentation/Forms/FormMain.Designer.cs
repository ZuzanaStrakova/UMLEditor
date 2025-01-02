namespace UMLEditor
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            panel1 = new Panel();
            toolStrip1 = new ToolStrip();
            toolStripButtonNewClass = new ToolStripButton();
            toolStripButtonAssociation = new ToolStripButton();
            toolStripButtonOneWayAssociation = new ToolStripButton();
            toolStripButtonAggregation = new ToolStripButton();
            toolStripButtonComposition = new ToolStripButton();
            toolStripButtonGeneralization = new ToolStripButton();
            pictureBox = new PictureBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            bitmapToolStripMenuItem = new ToolStripMenuItem();
            cToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            newClassToolStripMenuItem = new ToolStripMenuItem();
            toolStripButtonBin = new ToolStripButton();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(toolStrip1);
            panel1.Controls.Add(pictureBox);
            panel1.Location = new Point(14, 46);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(887, 538);
            panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.AllowDrop = true;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonNewClass, toolStripButtonAssociation, toolStripButtonOneWayAssociation, toolStripButtonAggregation, toolStripButtonComposition, toolStripButtonGeneralization, toolStripButtonBin });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            toolStrip1.Location = new Point(0, 107);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(41, 253);
            toolStrip1.Stretch = true;
            toolStrip1.TabIndex = 2;
            // 
            // toolStripButtonNewClass
            // 
            toolStripButtonNewClass.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonNewClass.Image = (Image)resources.GetObject("toolStripButtonNewClass.Image");
            toolStripButtonNewClass.ImageTransparentColor = Color.Magenta;
            toolStripButtonNewClass.Name = "toolStripButtonNewClass";
            toolStripButtonNewClass.Size = new Size(38, 28);
            toolStripButtonNewClass.ToolTipText = "Add new class";
            toolStripButtonNewClass.Click += toolStripButtonNewClass_Click;
            // 
            // toolStripButtonAssociation
            // 
            toolStripButtonAssociation.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonAssociation.Image = (Image)resources.GetObject("toolStripButtonAssociation.Image");
            toolStripButtonAssociation.ImageTransparentColor = Color.Magenta;
            toolStripButtonAssociation.Name = "toolStripButtonAssociation";
            toolStripButtonAssociation.Size = new Size(28, 28);
            toolStripButtonAssociation.ToolTipText = "Add new association";
            toolStripButtonAssociation.Click += toolStripButtonAssociation_Click;
            // 
            // toolStripButtonOneWayAssociation
            // 
            toolStripButtonOneWayAssociation.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonOneWayAssociation.Image = (Image)resources.GetObject("toolStripButtonOneWayAssociation.Image");
            toolStripButtonOneWayAssociation.ImageTransparentColor = Color.Magenta;
            toolStripButtonOneWayAssociation.Name = "toolStripButtonOneWayAssociation";
            toolStripButtonOneWayAssociation.Size = new Size(28, 28);
            toolStripButtonOneWayAssociation.ToolTipText = "Add new one way association";
            toolStripButtonOneWayAssociation.Click += toolStripButtonOneWayAssociation_Click;
            // 
            // toolStripButtonAggregation
            // 
            toolStripButtonAggregation.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonAggregation.Image = (Image)resources.GetObject("toolStripButtonAggregation.Image");
            toolStripButtonAggregation.ImageTransparentColor = Color.Magenta;
            toolStripButtonAggregation.Name = "toolStripButtonAggregation";
            toolStripButtonAggregation.Size = new Size(28, 28);
            toolStripButtonAggregation.ToolTipText = "Add new aggregation";
            toolStripButtonAggregation.Click += toolStripButtonAggregation_Click;
            // 
            // toolStripButtonComposition
            // 
            toolStripButtonComposition.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonComposition.Image = (Image)resources.GetObject("toolStripButtonComposition.Image");
            toolStripButtonComposition.ImageTransparentColor = Color.Magenta;
            toolStripButtonComposition.Name = "toolStripButtonComposition";
            toolStripButtonComposition.Size = new Size(28, 28);
            toolStripButtonComposition.ToolTipText = "Add new composition";
            toolStripButtonComposition.Click += toolStripButtonComposition_Click;
            // 
            // toolStripButtonGeneralization
            // 
            toolStripButtonGeneralization.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonGeneralization.Image = (Image)resources.GetObject("toolStripButtonGeneralization.Image");
            toolStripButtonGeneralization.ImageTransparentColor = Color.Magenta;
            toolStripButtonGeneralization.Name = "toolStripButtonGeneralization";
            toolStripButtonGeneralization.Size = new Size(38, 28);
            toolStripButtonGeneralization.Text = "toolStripButton1";
            toolStripButtonGeneralization.ToolTipText = "Add new generalization";
            toolStripButtonGeneralization.Click += toolStripButtonGeneralization_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(0, 0);
            pictureBox.Margin = new Padding(3, 4, 3, 4);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(2286, 2666);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(914, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, exportToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(152, 26);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(152, 26);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bitmapToolStripMenuItem, cToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(152, 26);
            exportToolStripMenuItem.Text = "Save As...";
            // 
            // bitmapToolStripMenuItem
            // 
            bitmapToolStripMenuItem.Name = "bitmapToolStripMenuItem";
            bitmapToolStripMenuItem.Size = new Size(140, 26);
            bitmapToolStripMenuItem.Text = "Bitmap";
            bitmapToolStripMenuItem.Click += bitmapToolStripMenuItem_Click;
            // 
            // cToolStripMenuItem
            // 
            cToolStripMenuItem.Name = "cToolStripMenuItem";
            cToolStripMenuItem.Size = new Size(140, 26);
            cToolStripMenuItem.Text = "C#";
            cToolStripMenuItem.Click += cToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newClassToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // newClassToolStripMenuItem
            // 
            newClassToolStripMenuItem.Name = "newClassToolStripMenuItem";
            newClassToolStripMenuItem.Size = new Size(159, 26);
            newClassToolStripMenuItem.Text = "New Class";
            // 
            // toolStripButtonBin
            // 
            toolStripButtonBin.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonBin.Image = (Image)resources.GetObject("toolStripButtonBin.Image");
            toolStripButtonBin.ImageTransparentColor = Color.Magenta;
            toolStripButtonBin.Name = "toolStripButtonBin";
            toolStripButtonBin.Size = new Size(38, 28);
            toolStripButtonBin.ToolTipText = "Remove object";
            toolStripButtonBin.Click += toolStripButtonBin_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            Text = "UML designer";
            Load += FormMain_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem bitmapToolStripMenuItem;
        private ToolStripMenuItem cToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem newClassToolStripMenuItem;
        private ToolStripButton toolStripButtonNewClass;
        private ToolStripButton toolStripButtonOneWayAssociation;
        private ToolStripButton toolStripButtonAssociation;
        private ToolStripButton toolStripButtonComposition;
        public ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonAggregation;
        private ToolStripButton toolStripButtonGeneralization;
        private ToolStripButton toolStripButtonBin;
    }
}
