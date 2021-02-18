using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hexamap.Modell
{
    public class Hexagon
    {

        private PointF[] _points;       
        private (int, int) _center; //középpont koordinátái
        private int _size;          // középpont és csúcs távolsága
        private int _width;         // (középpont és oldal távolsága)*2


        public Hexagon(int x, int y, int size)
        {
            _center = (x, y);
            _size = size;
            _width = (int)(Math.Sqrt(3) * _size);

            _points = new PointF[6];

            _points[0] = new PointF(_center.Item1, _center.Item2 - _size);
            _points[1] = new PointF(_center.Item1 + (_width / 2), y - (_size / 2));
            _points[2] = new PointF(_center.Item1 + (_width / 2), y + (_size / 2));
            _points[3] = new PointF(_center.Item1, _center.Item2 + size);
            _points[4] = new PointF(_center.Item1 - (_width / 2), _center.Item2 + (_size / 2));
            _points[5] = new PointF(_center.Item1 - (_width / 2), y - (_size / 2));
           
        }

        public int Size { get { return _size; } }
        public int Width { get { return _width; } }
        public PointF[] Points { get { return _points; } }
        public (int, int) Center { get => _center; set => _center = value; }
    }
}
