using System;
using System.Collections.Generic;
using System.Text;

namespace hexamap.Modell
{
    public class GameEventArgs : EventArgs
    {
        private int _currPlayer;
        private int _round;

        private int _settlements;
        private int _roads;
        private int _cities;
        private int _gabona;
        private int _erc;
        private int _fa;
        private int _tegla;
        private int _legelo;

        public GameEventArgs(int currPlayer, int round, int settlements, int roads, int cities, int gabona, int erc, int fa, int tegla, int legelo)
        {
            _currPlayer = currPlayer;
            _round = round;
            _settlements = settlements;
            _roads = roads;
            _cities = cities;
            _gabona = gabona;
            _erc = erc;
            _fa = fa;
            _tegla = tegla;
            _legelo = legelo;
        }

        public int CurrentPlayer { get { return _currPlayer; } }
        public int Round { get { return _round; } }

        public int Settlements{ get => _settlements;  }
        public int Roads { get => _roads; }
        public int Cities { get => _cities; }
        public int Gabona { get => _gabona; }
        public int Erc { get => _erc;}
        public int Fa { get => _fa;}
        public int Tegla { get => _tegla; }
        public int Legelo { get => _legelo; }

       
    }
}
