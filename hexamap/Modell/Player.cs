using System;
using System.Collections.Generic;
using System.Text;

namespace hexamap.Modell
{

    /*Kártyák: 
    * Gabona   0
    * Érc      1
    * Fa       2
    * Tégla    3  
    * Gyapjú   4
    */
    public partial class Player
    {
        public struct Road
        {
            public (int, int) Start { get; set; }
            public (int, int) End { get; set; }
            public int Owner { get; set; }

        }

        private List<(int, int)> _settlements;
        private List<(int, int)> _cities;
        private List<Road> _roads;
        private List<int> _cards;

        public Player()
        {
           
            _settlements = new List<(int, int)>();
            _cities = new List<(int, int)>();
            _roads = new List<Road>();
            _cards = new List<int> { 0,0,0,0,0 };

        }

        public List<(int, int)> Settlements { get => _settlements; set => _settlements = value; }
        public List<(int, int)> Cities { get => _cities; set => _cities = value; }
        public List<Road> Roads { get => _roads; set => _roads = value; }
        public List<int> Cards { get => _cards; set => _cards = value; }
    }
}
