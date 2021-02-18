using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using hexamap.Modell;
using System.Diagnostics;
using static hexamap.Modell.Player;

namespace hexamap.Nézet
{
    public partial class View : Form
    {
        private Model _model;
        
        private PictureBox pictureBox1 = new PictureBox();
        private int radOfCircles = 10;
        private bool updatePictureBox;

        private FlowLayoutPanel _layout;

        private Label _settlementLabel;
        private Label _roadLabel;
        private Label _cityLabel;
        private Label _gabonaLabel;
        private Label _ercLabel;
        private Label _faLabel;
        private Label _teglaLabel;
        private Label _legeloLabel;



        public View()
        {
            InitializeComponent();
     
        }

        private void map_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen blackPen = new Pen(Color.Black, 6);
            SolidBrush brush = new SolidBrush(Color.Gray);

            //hatszögek kirajzolása
            for (int i = 0; i < 19; i++)
            {
                e.Graphics.DrawPolygon(blackPen, _model.Hexagons[i].Points); // hexagonok kirajzolása

               
                switch((int)_model.Table.GetFieldResource(i))
                {
                    case 0:
                        brush.Color = Color.Yellow; //gabona 
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                    case 1:
                        brush.Color = Color.Gray; //érc
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                    case 2:
                        brush.Color = Color.DarkGreen; //fa
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                    case 3:
                        brush.Color = Color.Brown; //tégla
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                    case 4:
                        brush.Color = Color.LightGreen; //legelő
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                    case 5:
                        brush.Color = Color.White; //sivatag
                        e.Graphics.FillPolygon(brush, _model.Hexagons[i].Points);
                        break;
                      
                }
                e.Graphics.DrawString(""+_model.Table.GetFieldNumber(i), Font, Brushes.Black, _model.Hexagons[i].Center.Item1, _model.Hexagons[i].Center.Item2);


            }


       

            brush.Color = Color.DarkGray;
            Rectangle rect;
            //városok kirajzolása
            for (int i = 0; i < 19; i++)
            {
                foreach (PointF point in _model.Hexagons[i].Points)
                 {
                     brush.Color = Color.DarkGray;
                     rect = new Rectangle((int)point.X-radOfCircles,(int)point.Y-radOfCircles,radOfCircles*2,radOfCircles*2);
                     e.Graphics.FillEllipse(brush, rect);
                 }

          
            }

            for (int i = 0; i < _model.Players.Count; i++)
            {
                foreach ((int, int) s in _model.Players[i].Settlements)
                {
                    brush.Color = ChooseColorForPlayer(i); 
                    rect = new Rectangle(s.Item1 - radOfCircles, s.Item2 - radOfCircles, radOfCircles * 2, radOfCircles * 2);
                    e.Graphics.FillEllipse(brush, rect);

                }
            }


                Pen pen = new Pen(Color.Red, 8);

                foreach (Player player in _model.Players)
                {
                    foreach (Road road in player.Roads)
                    {
                        e.Graphics.DrawLine(pen, road.Start.Item1, road.Start.Item2, road.End.Item1, road.End.Item2);

                    }
                }
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {

          //Debug.WriteLine(whichHexagons(e.X,e.Y));
           // whichHexagons(e.X, e.Y).ForEach(x => Debug.WriteLine(x));
            
            Bitmap b = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(b, pictureBox1.ClientRectangle);
            Color colour = b.GetPixel(e.X, e.Y);
            b.Dispose();

            
            for (int i = 0; i < 19; i++)
            {
                foreach (PointF point in _model.Hexagons[i].Points)
                {
                    if (Math.Pow((e.X - point.X), 2) + Math.Pow((e.Y - point.Y), 2) <= radOfCircles * radOfCircles)
                    {
                        if (_model.CanBuildSettlement)
                        {
                            _model.BuildSettlement((int)point.X, (int)point.Y);
                            _model.CanBuildSettlement = false;
                        }
                        
                    }

                }
            }

            if (_model.CanBuildRoad)
            {
                for (int i = 0; i < 19; i++)
                {
                    foreach (PointF point in _model.Hexagons[i].Points)
                    {
                        if (Math.Pow((e.X - point.X), 2) + Math.Pow((e.Y - point.Y), 2) <= radOfCircles * radOfCircles)
                        {

                           
                            _model.BuildRoad((int)point.X, (int)point.Y);
                            //Debug.WriteLine("buildroad:" + (int)point.X + " " + (int)point.Y);
                            return;
                            


                        }

                    }
                }

            }
           

        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = e.X + " " + e.Y;

        }

       /* public bool isVertex(int x, int y)
        {
            for (int i = 0; i < 19; i++)
            {
               foreach(PointF point in _model.Hexagons[i].Points)
               {
                  if(Math.Pow((x - point.X),2) + Math.Pow((y - point.Y) , 2) <= radOfCircles*radOfCircles)
                  {
                        return true;
                  }
                
               }
            }

            return false;

        }*/

        //ha a kattintás helye fekete, akkor élre kattintottunk
        /*bool onEdge(Color c)
        {

            if (c.R == 0 && c.G == 0 && c.B == 0)
            {
                return true;
            }

            return false;
  
        }*/

        private void View_Load(object sender, EventArgs e)
        {

            _model = new Model();
            _model.NewGame();
            updatePictureBox = true;

            pictureBox1.Paint += map_Paint; //tábla kirajzolásának eseménye
            pictureBox1.MouseMove += map_MouseMove; //tábla kirajzolásának eseménye
            pictureBox1.MouseClick += map_MouseClick;
            _model.GameChanged += new EventHandler<GameEventArgs>(Game_GameChanged);
            _model.GameOver += new EventHandler<GameEventArgs>(Game_GameOver);
            _model.ZeroRoundFinished += new EventHandler<GameEventArgs>(Game_ZeroRoundFinished);

            int hexagonSize = 50;
            int hexagonWidth = (int)(Math.Sqrt(3) * hexagonSize);

            pictureBox1.Height = 8 * hexagonSize;
            pictureBox1.Width = 5 * hexagonWidth;


            pictureBox1.BackColor = Color.LightBlue;

            int x = (int)((1.5) * hexagonWidth);

            int y = hexagonSize;

            int z = 0;
            Controls.Add(pictureBox1);

            for (int i = 0; i < 19; i++)
            {

                if (i == 3 || i == 7)
                {
                    z = 0;
                    x = x - _model.Hexagons[^1].Width / 2;
                    y = y + _model.Hexagons[^1].Size + _model.Hexagons[^1].Size / 2;

                }

                if (i == 12 || i == 16)
                {
                    z = 0;
                    x = x + _model.Hexagons[^1].Width / 2;
                    y = y + _model.Hexagons[^1].Size + _model.Hexagons[^1].Size / 2;
                }

                _model.Hexagons.Add(new Hexagon(x + z, y, hexagonSize)); //felvesszük a hexagont a térképre

                z += _model.Hexagons[^1].Width;

            }

            _layout = new FlowLayoutPanel();
            _layout.Dock = DockStyle.Bottom;
            

            this.Controls.Add(_layout);

            _settlementLabel = new Label();
           // _settlementLabel.Anchor = AnchorStyles.None;

            _roadLabel = new Label();

           // _roadLabel.Anchor = AnchorStyles.None;
            _cityLabel = new Label();

            _gabonaLabel = new Label();
            _ercLabel = new Label();
            _faLabel = new Label();
            _teglaLabel = new Label();
            _legeloLabel = new Label();     

            _layout.Controls.Add(_settlementLabel);
            _layout.Controls.Add(_roadLabel);
            _layout.Controls.Add(_cityLabel);
            _layout.Controls.Add(_gabonaLabel);
            _layout.Controls.Add(_ercLabel);
            _layout.Controls.Add(_faLabel);
            _layout.Controls.Add(_teglaLabel);
            _layout.Controls.Add(_legeloLabel);


        }

        private void Game_GameOver(object sender, GameEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random cube = new Random();

            int num1 = cube.Next(1, 7);
            int num2 = cube.Next(1, 7);
            updatePictureBox = false;

            DialogResult dr = MessageBox.Show("" + num1 + " " + num2, "Dobás", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            if(dr == DialogResult.OK)
            {
                updatePictureBox = true;
            }

            if (_model.Round == 1)
            {
                _model.JatekosDobottZeroRound(num1, num2);
            }
            else
            {
                _model.JatekosDobott(num1, num2);
            }

            button1.Enabled = false;
          
        }

        //játék léptetése
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            _model.SelectNextPlayer();
           
        }

        private void Game_GameChanged(Object sender, GameEventArgs e)
        {

            _currentPlayerLabel.Text = "játékos: " + (e.CurrentPlayer+1);
            _roundCounter.Text = e.Round+".kör";
            // játékidő frissítése
            if (updatePictureBox)
            {
                pictureBox1.Refresh();
            }

            _settlementLabel.Text = "települések: " + e.Settlements;
            _roadLabel.Text = "utak:" + e.Roads;
            _cityLabel.Text = "cities: " + e.Cities;
            _gabonaLabel.Text = "gabona: " + e.Gabona;
            _ercLabel.Text = "erc: " + e.Erc;
            _faLabel.Text = "fa: " + e.Fa;
            _teglaLabel.Text = "tegla: " + e.Tegla;
            _legeloLabel.Text = "legelo: " + e.Legelo;
           
        }

      
        private void Game_ZeroRoundFinished(Object sender, GameEventArgs e)
        {
            updatePictureBox = false;
            DialogResult dr = MessageBox.Show("0.kör vége. Az 1. kört " + _model.KezdoJatekos.Item1 + " játékos kezdi");
            if (dr == DialogResult.OK)
            {
                updatePictureBox = true;
            }
        }


        //visszaad egy olyan listát, ami azoknak a hexagonoknak az indexeit tartalmazza, melyekhez a paraméterként megadott csúcs tartozik
        /*public List<int> whichHexagons(int x, int y)
        {
            List<int> hexagons = new List<int>();
            for (int i = 0; i < 19; i++)
            {
                foreach (PointF point in _model.Hexagons[i].Points)
                {
                    if (Math.Pow((x - point.X), 2) + Math.Pow((y - point.Y), 2) <= radOfCircles * radOfCircles)
                    {
                        hexagons.Add(i);
                    }

                }
            }

            return hexagons;
        }*/

        private Color ChooseColorForPlayer(int player)
        {
            switch (player)
            {
                case 0:
                    return Color.Red;     
                case 1:
                    return Color.Yellow;        
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Blue;
                default:
                    return Color.Gray;
            }
        }

        private void _buildSettlementBtn_Click(object sender, EventArgs e)
        {
            _model.CanBuildSettlement = true;
        }

        private void _buildRoadBtn_Click(object sender, EventArgs e)
        {
            _model.CanBuildRoad = true;


        }
    }
}
