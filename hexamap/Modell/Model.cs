using hexamap.Perzisztencia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using static hexamap.Modell.Player;

namespace hexamap.Modell
{
    public enum Field { Gabona, Erc, Fa, Tegla, Legelo, Sivatag }
   // public enum Product { Gabona, Erc, Fa, Tegla, Gyapju }

    /*Kártyák: 
     * Gabona   0
     * Érc      1
     * Fa       2
     * Tégla    3  
     * Gyapjú   4
     */


    public class Model
    {

        private List<Hexagon> _hexagons;
        private GameTable _table;
        private List<Player> _players;
        private int _numOfPlayers = 4;
        private int _round; //körök számlálója
        private int _currentPlayer;
        private (int,int) _kezdoJatekos; //hanyadik játékos, mennyit dobott
        //az első körben legnagyobbat dobó játékos sorszáma, és hogy mennyit dobott
        private List<int> _vertices; // 0 - üres, 1 - település, 2 - város
        //private List<Road> _roads; //csúcspárokat tartalmaz, a 2 csúcs által meghatározott szakasz az út
        private Boolean _canBuildRoad;
        private Boolean _canBuildSettlement;
        private Boolean _canBuildCity;
        private (int, int) _selectedPoint;
       
        

        public GameTable Table { get { return _table; } }
        public List<Hexagon> Hexagons { get { return _hexagons; } }
       // public List<Road> Roads { get { return _roads; } }
        public int Round { get { return _round; } }
        public int CurrentPlayer { get { return _currentPlayer; } }

        public (int,int) KezdoJatekos { get { return _kezdoJatekos; } }

        public bool CanBuildRoad { get => _canBuildRoad; set => _canBuildRoad = value; }
        public bool CanBuildSettlement { get => _canBuildSettlement; set => _canBuildSettlement = value; }
        public bool CanBuildCity { get => _canBuildCity; set => _canBuildCity = value; }
        public List<Player> Players { get { return _players; } }

        // public Boolean CanRefreshRoad { get; set; }


        public event EventHandler<GameEventArgs> GameChanged;
        public event EventHandler<GameEventArgs> GameOver;
        public event EventHandler<GameEventArgs> ZeroRoundFinished;

        public void NewGame()
        {
            _hexagons = new List<Hexagon>();
            _table = new GameTable();
            _players = new List<Player>();
            _vertices = new List<int>();
            _round = 0;
            for (int i = 0; i < _numOfPlayers; i++  )
            {
                _players.Add(new Player());
            }
            _currentPlayer = 0;
            _kezdoJatekos = (1, 0);
           // _roads = new List<Road>();

            for(int i = 0; i < 54; i++)
            {
                _vertices.Add(i);
            }

            _canBuildRoad = false;
            _canBuildSettlement = false;
            _canBuildCity = false;
            _selectedPoint = (-1,-1);
            //CanRefreshRoad = true;

    }

        private void OnGameChanged()
        {
            if (GameChanged != null)
                GameChanged(this, new GameEventArgs(
                    _currentPlayer,
                    _round,
                    _players[_currentPlayer].Settlements.Count, 
                    _players[_currentPlayer].Roads.Count,
                    _players[_currentPlayer].Cities.Count,
                    _players[_currentPlayer].Cards[0],
                    _players[_currentPlayer].Cards[1],
                    _players[_currentPlayer].Cards[2],
                    _players[_currentPlayer].Cards[3],
                    _players[_currentPlayer].Cards[4]

                    ));
        }


        private void OnZeroRoundFinished()
        {
            if (ZeroRoundFinished != null)
                ZeroRoundFinished(this, new GameEventArgs(_currentPlayer,
                   _round,
                   _players[_currentPlayer].Settlements.Count,
                   _players[_currentPlayer].Roads.Count,
                   _players[_currentPlayer].Cities.Count,
                   _players[_currentPlayer].Cards[0],
                   _players[_currentPlayer].Cards[1],
                   _players[_currentPlayer].Cards[2],
                   _players[_currentPlayer].Cards[3],
                   _players[_currentPlayer].Cards[4]

                   ));
        }

        public void SelectNextPlayer()
        {
            if (_currentPlayer < _numOfPlayers - 1 )
            {
                _currentPlayer++;
            }
            else
            {
                _currentPlayer = 0;
                
                _round++;

                if (_round == 1)
                {
                    OnZeroRoundFinished();
                    _currentPlayer = _kezdoJatekos.Item1;
                   // Debug.WriteLine("finished");
                }

                /*végigmegyünk minden játékoson
                 *minden játékos településeihez megkeressük a hozzátartozó hatszögeket (melyik hatszög csúcsai között található a település koordinátája)
                 *a hatszögek indexeit kimentjük egy listába
                 *lekérdezzük, hogy az adott hatszögnek mi az értéke (gabona, sivatag stb..) int val = (int)_table.GetFieldResource(indexOfHexagon);
                 *megnöveljük eggyel a játékos megfelelő kártyáját, ++player.Cards[val];
                 */

                if (_round == 2)
                {
                    foreach (Player player in _players)
                    {
                        foreach ((int, int) settlement in player.Settlements)
                        {
                            List<int> hexagons = new List<int>(whichHexagons(settlement.Item1, settlement.Item2));
                            Debug.WriteLine("hexagons:");
                            hexagons.ForEach(x => Debug.WriteLine(x));
                            foreach (int indexOfHexagon in hexagons)
                            {
                                int resource = (int)_table.GetFieldResource(indexOfHexagon);
                                // Debug.WriteLine(val);
                                if (resource < 5) //tehát nem sivatag
                                {
                                    ++player.Cards[resource];
                                }

                            }
                        }

                    }

                    /*for(int i = 0; i < _players.Count ; i++)
                    {
                       Debug.WriteLine(i + ".játékos kártyái:");
                        _players[i].Cards.ForEach(x => Debug.WriteLine(x));
                        Debug.WriteLine(i + ".települései:");
                        _players[i].Settlements.ForEach(x => Debug.WriteLine(x.Item1 + "," + x.Item2));
                    }*/

                }
            }
            OnGameChanged();
        }

        
        //az 0. körben a játék léptetése
        public void JatekosDobottZeroRound(int num1, int num2)
        {
           // Debug.WriteLine(_currentPlayer + ": " + (num1+num2));
            if(_currentPlayer <= _numOfPlayers)
            {
                if (num1 + num2 > _kezdoJatekos.Item2)
                {
                    _kezdoJatekos.Item1 = _currentPlayer;
                    _kezdoJatekos.Item2 = num1 + num2;

                }

            }

        }

        public void BuildSettlement(int x, int y)
        {
            
            for (int i = 0; i < 19; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                   if((Hexagons[i].Points[j].X == x && Hexagons[i].Points[j].Y == y) && (!_players[_currentPlayer].Settlements.Contains((x, y))))
                   {
                    
                        _players[_currentPlayer].Settlements.Add((x,y));
                   }

                }
            }
            
            OnGameChanged();
        }

        public void BuildRoad(int x, int y)
        {
            if ((_selectedPoint.Item1 == -1 ) &&  (_selectedPoint.Item2 == -1))
            {
                _selectedPoint.Item1 = x;
                _selectedPoint.Item2 = y;
              //  Debug.WriteLine("sel:" + x + "," + y);
            }
            else
            {
                Road r = new Road();
                r.Start = (_selectedPoint.Item1, _selectedPoint.Item2);
                r.End = (x,y);
                r.Owner = _currentPlayer;
                _players[_currentPlayer].Roads.Add(r);

                _selectedPoint.Item1 = -1;
                _selectedPoint.Item2 = -1;
                _canBuildRoad = false;
               // Debug.WriteLine("end:" + x + "," + y);
                //CanRefreshRoad = true;
               
                OnGameChanged();
            }
        }

        //bemenet: egy pont
        //kimenet: azoknak a hatszögeknek a sorszáma, melyek tartalmazzák az adott pontot
        private List<int> whichHexagons(int x, int y)
        {
            List<int> hexagons = new List<int>();
            for (int i = 0; i < 19; i++)
            {
                foreach (PointF point in Hexagons[i].Points)
                {
                    if (point.X == x && point.Y == y)
                    {
                        hexagons.Add(i);
                    }

                }
            }
            
            return hexagons;
        }

        public void JatekosDobott(int num1, int num2)
        {
            int sum = num1 + num2;
            List<int> resources = _table.GetResourcesWithNumber(sum);
            foreach (int r in resources)
            {
                ++_players[_currentPlayer].Cards[r];
            }


        }
 
    }
}
