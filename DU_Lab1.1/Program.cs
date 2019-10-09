using System;
using System.Collections.Generic;
using System.Linq;

namespace DU_Lab1._1
{
    class Vector
    {
        static public int Dimension;
        public double[] v;
        static Vector() { Dimension = 3; }
        public Vector() { v = new double[Dimension]; }
        public Vector(List<double> z) {
            v = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                v[i] = z[i];
            }
        }
        public Vector(Vector z) { v = new double[Dimension]; for (int i = 0; i < Dimension; i++) v[i] = z.v[i]; }
        public double Norm
        {
            get
            {
                double s = 0; for (int i = 0; i < Dimension; i++) s += v[i] * v[i];
                return Math.Sqrt(s);
            }
        }
        static public Vector operator +(Vector lop, Vector rop) { Vector t = new Vector(); for (int i = 0; i < Dimension; i++) t.v[i] = lop.v[i] + rop.v[i]; return t; }
        static public Vector operator -(Vector lop, Vector rop) { Vector t = new Vector(); for (int i = 0; i < Dimension; i++) t.v[i] = lop.v[i] - rop.v[i]; return t; }

        static public Vector operator *(Vector v, double c)
        {
            for (int i = 0; i < Dimension; i++)
            {
                v.v[i] *= c;
            }
            return v;
        }
    }

    class PureSolution
    {
        public static double Y1(double t)
        {
            return 1;
        }

        public static double Y2(double t)
        {
            return t;
        }

        public static double Y3(double t)
        {
            return t * t;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            SLDE system = new SLDE();
            system.Runge();
            Console.ReadKey();
        }
    }
}
