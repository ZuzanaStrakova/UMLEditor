using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLEditor.Interfaces;
using UMLEditor.Components.UML.Enums;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UMLEditor.Components.UML
{

    // Asociace:     A ──- B  ... A se odkazuje na B, B se odkazuje na A, např. Author (1) ──- (1..*) Book
    // Asociace:     A ──> B  ... A se odkazuje na B                    , např. Employee   ──> (1) Manager
    // Agregace:     A ◇── B  ... B je částí A, B může existovat i bez A, např. Form       ◇── (0..*) Button
    // Kompozice:    A ◆── B  ... B je částí A, B nemůže existovat bez A, např. Car        ◆── (1) Motor   (A řídí životní cyklus B a má ho private) 
    // Generalizace: A -─▷ B  ... A dědí z B
    

    public class UmlConnector : UmlObject
    {
        public ConnectionType Type { get; set; } = ConnectionType.Association;
        public UmlClass? StartObject { get; set; } // třída A z komentáře výše
        public UmlClass? EndObject { get; set; }   // třída B z komentáře výše
        public string StartMultiplicity { get => textBoxStartMultiplicity.Text; set => textBoxStartMultiplicity.Text = value; } // povolené hodnoty jsou jen "1" a "*" nebo ""
        public string EndMultiplicity { get => textBoxEndMultiplicity.Text; set => textBoxEndMultiplicity.Text = value; } // povolené hodnoty jsou jen "1" a "*"

        public override PointF Position { get => new PointF(0, 0); }
        public override SizeF  Size     { get => new SizeF(10000, 10000); }

        // child komponenty
        private UmlLineEnding startEnding;
        private UmlLineEnding endEnding;
        private UmlMultiplicity textBoxStartMultiplicity;
        private UmlMultiplicity textBoxEndMultiplicity;

        public UmlConnector(UmlObject parent, UmlClass startObject, UmlClass endObject, ConnectionType type) : this(parent)
        {
            StartObject = startObject;
            EndObject = endObject;
            Type = type;
        }

        public UmlConnector(UmlObject parent, ConnectionType type) : this(parent)
        {
            Type = type;
        }

        public UmlConnector(UmlObject parent) : base(parent)
        {
            startEnding = new UmlLineEnding(this);
            endEnding = new UmlLineEnding(this);
            textBoxStartMultiplicity = new UmlMultiplicity(this);
            textBoxEndMultiplicity = new UmlMultiplicity(this);

            Children.Add(startEnding);
            Children.Add(endEnding);
            Children.Add(textBoxStartMultiplicity);
            Children.Add(textBoxEndMultiplicity);
        }


        public override bool IsInCollision(float x, float y)
        {
            return false; // čára nekoliduje, kolidují jen její děti
        }


        public override void Draw(Graphics g)
        {
            // výpočet polohy důležitých bodů v souřadnicích plátna
 
            PointF startPoint  = GetIntersection(new RectangleF(StartObject.WorldTopLeftCorner, StartObject.Size), EndObject.WorldCenterPoint, out Edge edge1);
            PointF endPoint    = GetIntersection(new RectangleF(EndObject.WorldTopLeftCorner, EndObject.Size), StartObject.WorldCenterPoint, out Edge edge2);
            PointF middlePoint = new PointF((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            // převod souřadnic na lokální

            startPoint  = GlobalToLocal(startPoint);             // bod ve kterém čára začíná
            endPoint    = GlobalToLocal(endPoint);               // bod ve kterém končí
            middlePoint = GlobalToLocal(middlePoint);            // bod uprostřed, se kterým půjde později hýbat

            Direction startDirection = GetArrowDirection(edge1); // orientace šipky, aby mířila kolmo na stěnu počátečního objektu UmlClass
            Direction endDirection   = GetArrowDirection(edge2); // orientace šipky, aby mířila kolmo na stěnu koncového objektu UmlClass

            UpdateLineEnding(startEnding, startPoint, startDirection, GetStartSymbol());
            UpdateLineEnding(endEnding  , endPoint  , endDirection  , GetEndSymbol());
            UdateMultiplicity(textBoxStartMultiplicity, startPoint, startDirection, StartMultiplicity);
            UdateMultiplicity(textBoxEndMultiplicity  , endPoint  , endDirection  , EndMultiplicity);

            // body, kterými bude lomená čára procházet

            bool horizontal_start = (startDirection == Direction.Left || startDirection == Direction.Right);
            bool horizontal_end   = (endDirection   == Direction.Left || endDirection   == Direction.Right);

            PointF[] points = new PointF[5];
            points[0] = startPoint;
            points[1] = horizontal_start ? new PointF(middlePoint.X, startPoint.Y) : new PointF(startPoint.X, middlePoint.Y);
            points[2] = middlePoint;
            points[3] = horizontal_end   ? new PointF(middlePoint.X, endPoint.Y) : new PointF(endPoint.X, middlePoint.Y);
            points[4] = endPoint;

            Pen pen = Selected ? new Pen(Color.CornflowerBlue) : new Pen(Color.Black);

            g.DrawLines(pen, points);

            base.Draw(g);    // vykreslení vnořených komponent    
        }


        public Direction GetArrowDirection(Edge edge)
        {
            switch (edge)
            {
                case Edge.Left:   return Direction.Right;
                case Edge.Right:  return Direction.Left;
                case Edge.Top:    return Direction.Down;
                case Edge.Bottom: return Direction.Up;
                default: throw new Exception("unknown Edge");
            }
        }

        public LineEnd GetStartSymbol()
        {
            switch (Type)
            {
                case ConnectionType.Association:       return LineEnd.None;
                case ConnectionType.OneWayAssociation: return LineEnd.None;
                case ConnectionType.Aggregation:       return LineEnd.Diamond;
                case ConnectionType.Composition:       return LineEnd.FilledDiamond;
                case ConnectionType.Generalization:    return LineEnd.None;
                default: throw new Exception("unknown ConnectionType");
            }
        }

        public LineEnd GetEndSymbol()
        {
            switch (Type)
            {
                case ConnectionType.Association:       return LineEnd.None;
                case ConnectionType.OneWayAssociation: return LineEnd.Arrow;
                case ConnectionType.Aggregation:       return LineEnd.None;
                case ConnectionType.Composition:       return LineEnd.None;
                case ConnectionType.Generalization:    return LineEnd.Triangle;
                default: throw new Exception("unknown ConnectionType");
            }
        }

        // výpočet průsečíku úsečky vedoucí od bodu point do středu obdélníku rect se stěnami obdélníku
        // vrací polohu průsečíku a stěnu edge, na které průsečík leží
        public static PointF GetIntersection(RectangleF rect, PointF point, out Edge edge)
        {
            PointF center = new PointF(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            float dx = point.X - center.X;
            float dy = point.Y - center.Y;

            float slope = dy / dx;

            PointF intersection = new PointF();
            edge = Edge.Top;

            if (Math.Abs(slope) > rect.Height / rect.Width)
            {
                if (dy > 0)
                {
                    intersection = new PointF(center.X + (rect.Height / 2) / slope, rect.Bottom);
                    edge = Edge.Bottom;
                }
                else
                {
                    intersection = new PointF(center.X - (rect.Height / 2) / slope, rect.Top);
                    edge = Edge.Top;
                }
            }
            else
            {
                if (dx > 0)
                {
                    intersection = new PointF(rect.Right, center.Y + (rect.Width / 2) * slope);
                    edge = Edge.Right;
                }
                else
                {
                    intersection = new PointF(rect.Left, center.Y - (rect.Width / 2) * slope);
                    edge = Edge.Left;
                }
            }

            return intersection;
        }

        private void UpdateLineEnding(UmlLineEnding ending, PointF connectionPoint, Direction direction, LineEnd symbol)  // Vypočítá a nastaví polohu symbolu na konci čáry (šipka, diamant, trojuhelnik) z bodu připojení a směru připojení
        {
            // pokud má šipka mířit vlevo , musí být symbol šipky vpravo od ConnectionPointu
            // pokud má šipka mířit vpravo, musí být symbol šipky zleva od ConnectionPointu
            // pokud má šipka mířit dolů  , musí být symbol šipky nad ConnectionPointem
            // pokud má šipka mířit nahoru, musí být symbol šipky pod ConnectionPointem

            ending.Direction = direction;
            ending.Symbol = symbol;
        
            float width  = ending.Size.Width;
            float height = ending.Size.Height;
        
            switch (direction)
            {
                case Direction.Left:  ending.Position = new PointF(connectionPoint.X            , connectionPoint.Y - height / 2); break;
                case Direction.Right: ending.Position = new PointF(connectionPoint.X - width    , connectionPoint.Y - height / 2); break;
                case Direction.Up:    ending.Position = new PointF(connectionPoint.X - width / 2, connectionPoint.Y             ); break;
                case Direction.Down:  ending.Position = new PointF(connectionPoint.X - width / 2, connectionPoint.Y - height    ); break;
                default: throw new Exception("unknown Direction");
            }
        }

        private void UdateMultiplicity(UmlMultiplicity textBox, PointF connectionPoint, Direction direction, string multiplicity)
        {
            textBox.Text = multiplicity;
            textBox.ReadOnly = true;

            float width  = textBox.Size.Width/2;
            float height = textBox.Size.Height/2;

            switch (direction)
            {
                case Direction.Left:  textBox.Position = new PointF(connectionPoint.X +   width , connectionPoint.Y - 2*width); break;
                case Direction.Right: textBox.Position = new PointF(connectionPoint.X - 3*width , connectionPoint.Y - 2*width); break;
                case Direction.Up:    textBox.Position = new PointF(connectionPoint.X + width/2 , connectionPoint.Y +   width); break;
                case Direction.Down:  textBox.Position = new PointF(connectionPoint.X + width/2 , connectionPoint.Y - 2*width); break;
                default: throw new Exception("unknown Direction");
            }
        }

    }
}
