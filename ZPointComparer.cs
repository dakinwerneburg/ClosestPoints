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

using System.Collections.Generic;

namespace cssbs_ex11_werneburg
{
    class ZPointComparer : IComparer<Point3D>
    {
        public int Compare(Point3D p1, Point3D p2)
        {
            if (p1.Z == p2.Z) return 0;
            if (p1.Z < p2.Z) return -1;
            return 1;
        }
    }
}
