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
    class Point3D : Point
    {
        public int Z { get; set; }
        public Point3D(int x, int y, int z) : base(x, y) { Z = z; }

        public override int GetHashCode()
        {
            return X << 16 ^ Y ^ Z;
        }
        public override bool Equals(object obj)
        {
            Point3D p = obj as Point3D;
            if (p is null) return false;
            return X == p.X && Y == p.Y && Z == p.Z;
        }
    }
}
