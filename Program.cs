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

    class Program
    {
        static PointUtility Util = new PointUtility();

        const int NUM_2D_POINTS = 100;
        const int NUM_3D_POINTS = 100;
        static Point[] points;
        static Point3D[] points3D;


        public static void Main(string[] args)
        {
            //Default Configuration to generator random points
            points = Util.Get2DPoints(NUM_2D_POINTS, 1, 100);
            points3D = Util.Get3DPoints(NUM_3D_POINTS, 1, 1000);

            GetMenu();
            Console.WriteLine("\nNew Random Points created,  2D has 100 points [1-100] and 3D has 1000 points [1-1000]!\n");


            //Application Loop until exit
            while (true)
            {
                Console.Write("Selection: ");
                int selection = GetIntegerUI(1, 8);
                Console.WriteLine();
                switch (selection)
                {
                    case 1:
                        RunBruteForceClosestPoints(points);
                        break;
                    case 2:
                        RunOptimizedClosestPoints(points);
                        break;
                    case 3:
                        RunBruteForceClosestPoints(points3D);
                        break;
                    case 4:
                        RunOptimizedClosestPoints(points3D);
                        break;
                    case 5:
                        DistanceCalculatroUI();
                        ClearOutPut();
                        break;
                    case 6:
                        CustomRangePointsUI();
                        ClearOutPut();
                        break;
                    case 7:
                        points = Util.Get2DPoints(NUM_2D_POINTS, 1, 100);
                        points3D = Util.Get3DPoints(NUM_3D_POINTS, 1, 1000);
                        Console.WriteLine("New Random Points created,  2D has 100 points [1-100] and 3D has 1000 points [1-1000]D");
                        ClearOutPut();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void DistanceCalculatroUI()
        {
            Console.Write("How many dimensions [2 or 3]? ");
            int selection = GetIntegerUI(2, 3);
            Point point1;
            Point point2;
            if (selection == 3)
            {
                point1 = GetPointsUI(3);
                point2 = GetPointsUI(3);
            }
            else
            {
                point1 = GetPointsUI(2);
                point2 = GetPointsUI(2);
            }
            double dist = Util.Distance(point1, point2);
            Console.WriteLine($"Distance: {dist:0.000}");
            ClearOutPut();
        }

        /// <summary>
        /// Gets distance using brute force method O(n^2)
        /// </summary>
        /// <param name="points">Collection of points</param>
        static void RunBruteForceClosestPoints(Point[] points)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            double distance = Util.BruteForce(points);
            watch.Stop();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Shortest Distance is: {distance,-25:0.000} Execution Time: {watch.ElapsedMilliseconds:0.0} ms\n");
            Util.CleanUp();
            ClearOutPut();
        }

        /// <summary>
        /// Runs an Closest point problem using divide and conquer algorithm
        /// runtime is O (n log n)
        /// </summary>
        /// <param name="p">Collection of Points</param>
        static void RunOptimizedClosestPoints(Point[] p)
        {
            
    
            if (p is Point3D[] p_3D)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Point3D[] xSorted = Util.Sort3DPoints(p_3D, "X");
                double distance = Util.GetClosest2DPoints(xSorted, p_3D.Length);
                watch.Stop();
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Shortest Distance is: {distance,-25:0.000} Execution Time: {watch.ElapsedMilliseconds:0.0} ms\n");
            }
            else
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Point[] xSorted = Util.Sort2DPoints(p, "X");
                double distance = Util.GetClosest2DPoints(xSorted, p.Length);
                watch.Stop();
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"Shortest Distance is: {distance,-25:0.000} Execution Time: {watch.ElapsedMilliseconds:0.0} ms\n");
            }
            Util.CleanUp();
            ClearOutPut();
        }

        /// <summary>
        /// User selects lowest  and highestvales for 
        /// each dimension (x,y) or (x,y,z) scale
        /// </summary>
        static void CustomRangePointsUI()
        {
            Console.Write($"How many Points: ");
            int count = GetIntegerUI(1, int.MaxValue);
            Console.WriteLine("How many dimensions [2-3]");
            int dim = GetIntegerUI(2, 3);
            Console.Write($"Enter lowest interger value for dimension: ");
            int min = GetIntegerUI(int.MinValue, int.MaxValue);
            Console.Write($"Enter Highest integer value for dimension: ");
            int max = GetIntegerUI(int.MinValue, int.MaxValue);
            if (dim == 3)
            {
                points3D = Util.Get3DPoints(count, min, max);
            }
            else
            {
                points = Util.Get2DPoints(count, min, max);
            }
        }

        /// <summary>
        /// Get points from user
        /// </summary>
        /// <param name="dimension">2 or 3 for (x,y) or (x,y,z)</param>
        /// <returns>Collecton of points</returns>
        static Point GetPointsUI(int dimension)
        {
            Console.WriteLine("Enter point values:");
            Console.Write("X: ");
            int x1 = GetIntegerUI(int.MinValue, int.MaxValue);
            Console.Write("Y: ");
            int y1 = GetIntegerUI(int.MinValue, int.MaxValue);
            int z1 = 0;
            if (dimension == 3)
            {
                Console.Write("Z: ");
                z1 = GetIntegerUI(int.MinValue, int.MaxValue);
                return new Point3D(x1, y1, z1);
            }
            else
            {
                return new Point(x1, y1);
            }
        }

        /// <summary>
        /// Clears Console and resets to menu
        /// </summary>
        static void ClearOutPut()
        {
            Console.WriteLine("Press any Key to Continue");
            Console.ReadKey();
            Console.Clear();
            GetMenu();
        }

        static void GetMenu()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("                      MSSA Exercise 11 - Closest Point Problem                            ");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("                                   Menu:                                                  ");
            Console.WriteLine("                                                                                          ");
            Console.WriteLine("    2D         [1] Run Brute Force of 2D Points                                           ");
            Console.WriteLine("               [2] Run Div & Conquer of 2D Points                                         ");
            Console.WriteLine("                                                                                          ");
            Console.WriteLine("    3D         [3] Run Brute Force of 3D Points                                           ");
            Console.WriteLine("               [4] Run Div & Conquer of 3D Points                                         ");
            Console.WriteLine("                                                                                          ");
            Console.WriteLine("               [5] Distance Calculator                                                    ");
            Console.WriteLine("               [6] Manually Generate New Random Points                                    ");
            Console.WriteLine("               [7] Auto Generate New Random Points                                        ");
            Console.WriteLine("               [8] Exit                                                                   ");
            Console.WriteLine("                                                                                          ");
            Console.WriteLine("------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Validates user input
        /// </summary>
        /// <param name="min">largest number allowed</param>
        /// <param name="max">smallest number allowed</param>
        /// <returns></returns>
        static int GetIntegerUI(int min, int max)
        {
            while (true)
            {
                int xPos = Console.CursorLeft;
                string input = Console.ReadLine();
                try
                {
                    if (int.TryParse(input, out int number) && number >= min && number <= max)
                    {
                        return number;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(input);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Error: {input} is not a valid selection.  Press any key to continue.");
                    Console.ReadKey();
                    ClearInput(xPos);
                }
            }
        }

        /// <summary>
        /// This clears user entered input
        /// </summary>
        /// <param name="cursorPos">last place where the cursor was</param>
        static void ClearInput(int cursorPos)
        {
            int y = Console.CursorTop - 1;
            Console.SetCursorPosition(0, y);
            Console.Write(new string(' ', Console.BufferWidth));

            Console.SetCursorPosition(cursorPos, y - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(cursorPos, y - 1);
        }

    }
}
