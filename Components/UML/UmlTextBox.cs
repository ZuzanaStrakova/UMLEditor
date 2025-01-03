using Newtonsoft.Json;

namespace UMLEditor.Components.UML
{
    public class UmlTextBox : UmlObject
    {
        public virtual string Text { get; set; } = string.Empty;
        public bool Multiline { get; set; } = false;
        public bool ReadOnly { get; set; } = false;
        public bool Border { get; set; } = false;
        [JsonIgnore]
        public Font Font { get; } = SystemFonts.DefaultFont;
        [JsonIgnore]
        public Brush Brush { get; } = Brushes.Black;
        [JsonIgnore]
        public Pen Pen { get; } = Pens.Black;
        [JsonIgnore]
        public int Padding { get; } = 5;

        public UmlTextBox(UmlObject parent) : base(parent)
        {

        }

        public override void Draw(Graphics g)
        {
            RectangleF rect = new RectangleF(Position, Size);

            if (Border)
            {
                g.DrawRectangle(Pen, 0, 0, rect.Width, rect.Height);
            }

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoWrap
            };

            RectangleF textRect = new RectangleF(Padding, Padding, rect.Width - 2 * Padding, rect.Height - 2 * Padding);
            g.DrawString(Text, Font, Brushes.Black, textRect, format);

            // Draw the cursor
            /*
            if (_isEditing)
            {
                string textBeforeCursor = Text.Substring(0, _cursorPosition).Replace("\n", " ");
                SizeF textSize = g.MeasureString(textBeforeCursor, SystemFonts.DefaultFont);
                float cursorX = textRect.X + textSize.Width % textRect.Width;
                float cursorY = textRect.Y + (float)Math.Floor(textSize.Width / textRect.Width) * g.MeasureString("A", SystemFonts.DefaultFont).Height;
                g.DrawLine(Pens.Black, cursorX, cursorY, cursorX, cursorY + g.MeasureString("A", SystemFonts.DefaultFont).Height);
            }
            */

            if (_editingControl != null)
            {
                _editingControl.Bounds = Rectangle.Round(new RectangleF(LocalToGlobal(new PointF(0, 0)), SizeF.Add(Size, new SizeF(1, 1))));
            }

            base.Draw(g);
        }

        public Size TextSize()
        {
            return TextRenderer.MeasureText(Text, Font);
        }

        /*

        private bool _isEditing = false; 
        private int _cursorPosition = 0; 

        public void HandleKeyPress(Keys key, bool ctrlPressed)
        {

            if (!_isEditing) return;

            if (ctrlPressed && key == Keys.C)
            {
                Clipboard.SetText(Text);
            }
            else if (ctrlPressed && key == Keys.V)
            {
                string clipboardText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    Text = Text.Insert(_cursorPosition, clipboardText);
                    _cursorPosition += clipboardText.Length;
                }
            }
            else if (key == Keys.Back && _cursorPosition > 0)
            {
                Text = Text.Remove(_cursorPosition - 1, 1);
                _cursorPosition--;
            }
            else if (key == Keys.Delete && _cursorPosition < Text.Length)
            {
                Text = Text.Remove(_cursorPosition, 1);
            }
            else if (key == Keys.Left && _cursorPosition > 0)
            {
                _cursorPosition--;
            }
            else if (key == Keys.Right && _cursorPosition < Text.Length)
            {
                _cursorPosition++;
            }
            else if (key == Keys.Home)
            {
                _cursorPosition = 0;
            }
            else if (key == Keys.End)
            {
                _cursorPosition = Text.Length;
            }
            else if (key == Keys.Enter)
            {
                Text = Text.Insert(_cursorPosition, "\n");
                _cursorPosition++;
            }
            else if (key != Keys.Back && key != Keys.Delete && key != Keys.Left && key != Keys.Right && key != Keys.Home && key != Keys.End && key != Keys.Escape && key != Keys.Enter)
            {
                Text = Text.Insert(_cursorPosition, ((char)key).ToString());
                _cursorPosition++;
            }
            else if (key == Keys.Escape)
            {
                _isEditing = false;
            }
        }

        public void StartEditing(Control parent)
        {
            _isEditing = true;
            _cursorPosition = Text.Length;
        }

        public void EndEditing()
        {
            _isEditing = false;
        }

        */

        private Control? _editingControl = null;

        public void StartEditing(Control parent)
        {
            if (ReadOnly) return;

            // Implementace editování víceřádkového textu je složité (kreslení kurzoru, kopírování a vkládání atd.), proto je lepší možností dočasný editační ovládací prvek (v tomto případě TextBox)
            TextBox textBox = new TextBox();
            textBox.Bounds = Rectangle.Round(new RectangleF(LocalToGlobal(new PointF(0, 0)), SizeF.Add(Size, new SizeF(1, 1))));
            textBox.Text = Text;
            textBox.Font = Font;
            textBox.Padding = new Padding(Padding); // bohužel nemá žádný efekt, prostě Microsoft
            textBox.BackColor = Color.AliceBlue;
            textBox.WordWrap = false;
            textBox.Multiline = Multiline;

            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift) // Samotný enter potrvdí změny a ukončí editaci (nový řádek => Enter + Shift)
                {
                    string text = textBox.Text; // editovaný text
                    // Doplnit validace
                    Text = text;                // přenes do UMLObject.Text

                    parent.Focus(); // vyvolá event Leave => zahození textboxu
                }
                else if (e.KeyCode == Keys.Escape)   // Escape => zahození změn a ukončení editace
                {
                    parent.Focus();
                }
            };

            textBox.Leave += (s, args) =>
            {
                parent.Controls.Remove(textBox);
                parent.Invalidate();         // překreslení
                _editingControl = null;
            };

            parent.Controls.Add(textBox);  // od této chvíle viditelný textbox
            textBox.Focus();

            _editingControl = textBox;
        }

    }
}
