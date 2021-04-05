/*
  Microsoft Software and Systems Academy
  C# Excersice 11
  Author: Dakin T. Werneburg
  Date: 4/2/2021
  
  Purpose:  To Calculate the Distance between
            A collection of 100 2D Points and
            A collection of 1000 3D Points 
            without a built-in library to calculate.
*/

namespace cssbs_ex11_werneburg
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y) { X = x; Y = y; }

        public override int GetHashCode()
        {
            return X << 16 ^ Y;
        }
        public override bool Equals(object obj)
        {
            Point p = obj as Point;
            if (p is null) return false;
            return X == p.X && Y == p.Y;
        }
    }
}
