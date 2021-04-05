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

using System;
using System.Collections.Generic;

namespace cssbs_ex11_werneburg
{
    /// <summary>
    /// A
    /// </summary>
    class PointUtility
    {
        public double MinimumDistance { get; set; } = double.MaxValue;  
        public Point[] points { get; set; } = new Point[2];               
        public Point3D[] points3D { get; set; } = new Point3D[2];
        public void CleanUp()
        {
            MinimumDistance = double.MaxValue;
            points = new Point[2];
            points3D = new Point3D[2];
        }



        /// <summary>
        /// Generates Random X and Y coordinates and assigns 
        /// them to a collection of Points
        /// </summary>
        /// <param name="max">number of Points</param>
        /// <returns>collection of points</returns>
        public Point[] Get2DPoints(int count, int min, int max)
        {
            var rand = new Random();
            HashSet<Point> points = new HashSet<Point>();
            while (points.Count < count)
            {
                int x = rand.Next(min, max);
                int y = rand.Next(min, max);
                points.Add(new Point(x, y));
            }
            Point[] p = new Point[points.Count];
            points.CopyTo(p);
            return p;
        }
        public Point3D[] Get3DPoints(int count, int min, int max)
        {
            var rand = new Random();
            HashSet<Point3D> points = new HashSet<Point3D>();
            while (points.Count < max)
            {
                int x = rand.Next(1, max + 1);
                int y = rand.Next(1, max + 1);
                int z = rand.Next(1, max + 1);
                points.Add(new Point3D(x, y, z));
            }
            Point3D[] p = new Point3D[points.Count];
            points.CopyTo(p);
            return p;
        }




        /// <summary>
        /// Utilty to sort points according to axes
        /// uses Array.Sort method O(n log n)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="axis">cartis axes</param>
        /// <returns>sorted list of points</returns>
        public Point[] Sort2DPoints(Point[] points, string axis)
        {

            switch (axis)
            {
                case "X":
                    Point[] xSorted = new Point[points.Length];
                    points.CopyTo(xSorted, 0);
                    Array.Sort<Point>(xSorted, new XPointComparer());
                    return xSorted;
                case "Y":
                    Point[] ySorted = new Point[points.Length];
                    points.CopyTo(ySorted, 0);
                    Array.Sort<Point>(ySorted, new YPointComparer());
                    return ySorted;
            }
            return null;
        }
        public Point3D[] Sort3DPoints(Point3D[] points, string axis)
        {
            switch (axis)
            {
                case "X":
                    Point3D[] xSorted = new Point3D[points.Length];
                    points.CopyTo(xSorted, 0);
                    Array.Sort<Point3D>(xSorted, new XPointComparer());
                    return xSorted;
                case "Y":
                    Point3D[] ySorted = new Point3D[points.Length];
                    points.CopyTo(ySorted, 0);
                    Array.Sort<Point3D>(ySorted, new YPointComparer());
                    return ySorted;
                case "Z":
                    Point3D[] zSorted = new Point3D[points.Length];
                    points.CopyTo(zSorted, 0);
                    Array.Sort<Point3D>(zSorted, new ZPointComparer());
                    return zSorted;
            }
            return null;
        }




        /// <summary>
        /// Uses divide and conquer to find closest pair of points.
        /// first we sort the x axes, then divide into a left and right side.
        /// This happens recursively unit it reaches just two points,
        /// then will calculate the distance.  As it propogates back up 
        /// from the stack it will determine which one is the minimum distance.
        /// To account for the other dimension, I sort by those axes and only look
        /// at the points that are less then the middle point of the sorted x axes.
        /// and then add them to some list called the strip.  I then calculate the 
        /// minimum distance of the points that are in these lists.  
        /// Lastly, I will compare all three minimum 
        /// values for each axes and return the one that is the least
        /// </summary>
        /// <param name="xSorted"></param>
        /// <param name="ySorted"></param>
        /// <param name="n"></param>
        /// <returns></returns>

        public double GetClosest2DPoints(Point[] xSorted, int n)
        {

            //Base Case
            if (n <= 3)
            {
                return BruteForce(xSorted);
            }

            //Divide
            int mid = n / 2;
            Point midpoint = xSorted[mid];
            Point[] leftArray = new Point[mid];
            Array.Copy(xSorted, 0, leftArray, 0, mid);
            Point[] rightArray = new Point[n - mid];
            Array.Copy(xSorted, mid, rightArray, 0, (n - mid));


            //Conquer

            var left = GetClosest2DPoints(leftArray, mid);
            var right = GetClosest2DPoints(rightArray, n - mid);



            //Combine
            double min = Math.Min(left, right);
            List<Point> stripY = new List<Point>();  //stores all Points are within the strip
            Point[] ySorted = Sort2DPoints(xSorted, "Y");

            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(ySorted[i].X - midpoint.X) < min)
                {
                    stripY.Add(ySorted[i]);
                }
            }
            var minYPointFromMid = ClosestY2D(stripY, min);
            min = Math.Min(min, minYPointFromMid);

            return (min);
        }
        public double GetClosest3DPoints(Point3D[] xSorted, int n)
        {
            //Base
            if (n <= 3)
            {
                return BruteForce(xSorted);
            }

            //Divide
            int mid = n / 2;
            Point3D midpoint = xSorted[mid];
            Point3D[] leftArray = new Point3D[mid];
            Array.Copy(xSorted, 0, leftArray, 0, mid);
            Point3D[] rightArray = new Point3D[n - mid];
            Array.Copy(xSorted, mid, rightArray, 0, (n - mid));

            //Conquer by getting minumum distance from left and right sides
            var left = GetClosest3DPoints(leftArray, mid);
            var right = GetClosest3DPoints(rightArray, n - mid);
            double min = Math.Min(left, right);

            /**
             * Merge by sorting other dimensions and then only finds points that are with 
             * minimum distance from midpoint and then compares current 
             * minimum with each minimum from other dimensions
             */
            List<Point3D> stripY = new List<Point3D>();   
            List<Point3D> stripZ = new List<Point3D>();
            Point3D[] ySorted = Sort3DPoints(xSorted, "Y");
            Point3D[] zSorted = Sort3DPoints(xSorted, "Z");
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(ySorted[i].X - midpoint.X) < min)   //Is it within the strip and less than
                {
                    stripY.Add(ySorted[i]);
                }
                if (Math.Abs(zSorted[i].X - midpoint.X) < min)
                {
                    stripZ.Add(zSorted[i]);
                }
            }
            var minYPointFromMid = ClosestY3D(stripY, min);   // gets minimum distance from each dimension's strip
            var minZPointFromMid = ClosestZ(stripZ, min);

            //Determines which minimum distance is less.
            min = Math.Min(min, Math.Min(minYPointFromMid, minZPointFromMid));

            return (min);
        }




        /// <summary>
        /// This will use brute force but at only looking at
        /// points that fall within the strip in O(n) time
        /// </summary>
        /// <param name="strip">Points that fall within the stip</param>
        /// <param name="minValue">minimum distance</param>
        /// <returns></returns>
        public double ClosestY2D(List<Point> strip, double minValue)
        {
            var min = minValue;
            for (int i = 0; i < strip.Count; i++)
            {
                for (int j = i + 1; j < strip.Count && (strip[j].Y - strip[i].Y < min); j++)
                {
                    double distance = Distance(strip[i], strip[j]);
                    if (distance < min)
                    {
                        min = distance;
                    }
                }
            }
            return min;
        }
        public double ClosestY3D(List<Point3D> strip, double minValue)
        {
            var min = minValue;
            for (int i = 0; i < strip.Count; i++)
            {
                for (int j = i + 1; j < strip.Count && (strip[j].Y - strip[i].Y < min); j++)
                {
                    double distance = Distance(strip[i], strip[j]);
                    if (distance < min)
                    {
                        min = distance;
                    }
                }
            }
            return min;
        }
        public double ClosestZ(List<Point3D> strip, double minValue)
        {
            var min = minValue;
            for (int i = 0; i < strip.Count; i++)
            {
                for (int j = i + 1; j < strip.Count && (strip[j].Z - strip[i].Z < min); j++)
                {
                    if (Distance(strip[i], strip[j]) < min)
                    {
                        min = Distance(strip[i], strip[j]);
                    }
                }
            }
            return min;
        }




        /// <summary>
        /// Utility to calculate the distance of two points
        /// and return the distance and points O(n^2)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double BruteForce(Point[] p)
        {
            double minValue = double.MaxValue;
            for (int i = 0; i < p.Length; i++)
            {
                for (int j = i + 1; j < p.Length; j++)
                {
                    double distance = Distance(p[i], p[j]);
                    if (distance < minValue)
                    {
                        minValue = distance;
                    }
                }
            }
            return minValue;
        }



        /// <summary>
        /// Gets Distance using Euclidean Distance formaula
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public double Distance(Point point1, Point point2)
        {
            double distance;
            if (point1 is Point3D p1 && point2 is Point3D p2)
            {
                distance =  Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
                if (distance < MinimumDistance)
                {
                    MinimumDistance = distance;
                    points3D[0] = p1;
                    points3D[1] = p2;
                    Console.WriteLine($"Closest Points So Far: {$"({p1.X},{p1.Y},{p1.Z})",20} to {$"({p2.X},{p2.Y},{p2.Z})",-20} {"Distance: ",-10}{distance:0.000}");
                }
            }
            else
            {
                distance = Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
                if(distance < MinimumDistance)
                {
                    MinimumDistance = distance;
                    points[0] = point1;
                    points[1] = point2;
                    Console.WriteLine($"Closest Points So Far: {$"({point1.X},{point1.Y})",20} to {$"({point2.X},{point2.Y})"} {"Distance: ",10}{distance:0.000}");
                }
            }
            return distance;
        }
    }
}
