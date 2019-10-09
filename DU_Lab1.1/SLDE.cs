using System;
using System.Collections.Generic;
using System.Linq;

namespace DU_Lab1._1
{
    class SLDE
    {
        double t0;
        double tmax;
        double h = 0.1;
        Vector U0;
        Vector Yn;
        Vector Y0;
        Vector Upr;
        Vector Ucor;
        Vector Uprev;
        Vector Un;

        Vector F(double t, Vector Y)
        {
            List<double> sol = new List<double> { Y.v[1] - t, Y.v[0], 2 * Y.v[1] };
            return new Vector(sol);
        }

        public void Print(int n, double t)
        {
            Console.Write($"{n,3} |{t:0.00} |");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Yn.v[i] = Yt[i](t);
                Console.Write($"{Yn.v[i]:0.00}| ");
            }
            Console.Write($")| ");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Console.Write($"{Un.v[i]:0.00}| ");
            }
            Console.Write($")| ");
            double dy = new Vector(Yn - Un).Norm;
            Console.Write($"{dy:0.00}\n");
            Uprev = Un;
            n++;
        }

        public void Eiler()
        {
            Console.Write($"n tn yn* un ||Yn-Un*||\n");

            Console.Write($"{1,3} |{t0:0.00} |");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Y0.v[i] = Yt[i](0);
                Console.Write($"{Y0.v[i]:0.00}| ");
            }
            Console.Write($")| ");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Console.Write($"{U0.v[i]:0.00}| ");
            }
            Console.Write($")| ");
            double dy = new Vector(Y0 - U0).Norm;
            Console.Write($"{dy:0.00}\n");


            //вывод нулевой шапки

            Console.Write($"{2,3} |{h:0.00} |");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Yn.v[i] = Yt[i](h);
                Console.Write($"{Yn.v[i]:0.00}| ");
            }
            Console.Write($")| ");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Console.Write($"{Un.v[i]:0.00}| ");
            }
            Console.Write($")| ");
            dy = new Vector(Yn - Un).Norm;
            Console.Write($"{dy:0.00}\n");

            //вывод 1й шапки

            int n = 2;
            for (double t = t0 + 2 * h; t <= tmax || Math.Abs(t - tmax) / t <= 1e-7; t = t + h)
            {
                Un = Uprev + F(t - h, Uprev) * h;
                //Console.Write($"{n,3} |{t:0.00} |");

                //Console.Write($"(");
                //for (int i = 0; i < Vector.Dimension; i++)
                //{
                //    Yn.v[i] = Yt[i](t);
                //    Console.Write($"{Yn.v[i]:0.00}| ");
                //}
                //Console.Write($")| ");

                //Console.Write($"(");
                //for (int i = 0; i < Vector.Dimension; i++)
                //{
                //    Console.Write($"{Un.v[i]:0.00}| ");
                //}
                //Console.Write($")| ");
                //dy = new Vector(Yn - Un).Norm;
                //Console.Write($"{dy:0.00}\n");
                Print(n, t);
                Uprev = Un;
                n++;
            }
        }

        public void Runge()
        {
            Console.Write($"n tn yn* un ||Yn-Un*||\n");

            Console.Write($"{1,3} |{t0:0.00} |");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Y0.v[i] = Yt[i](0);
                Console.Write($"{Y0.v[i]:0.00}| ");
            }
            Console.Write($")| ");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Console.Write($"{U0.v[i]:0.00}| ");
            }
            Console.Write($")| ");
            double dy = new Vector(Y0 - U0).Norm;
            Console.Write($"{dy:0.00}\n");


            //вывод нулевой шапки

            Console.Write($"{2,3} |{h:0.00} |");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Yn.v[i] = Yt[i](h);
                Console.Write($"{Yn.v[i]:0.00}| ");
            }
            Console.Write($")| ");

            Console.Write($"(");
            for (int i = 0; i < Vector.Dimension; i++)
            {
                Console.Write($"{Un.v[i]:0.00}| ");
            }
            Console.Write($")| ");
            dy = new Vector(Yn - Un).Norm;
            Console.Write($"{dy:0.00}\n");

            //вывод 1й шапки

            int n = 2;
            for (double t = t0 + 2 * h; t <= tmax || Math.Abs(t - tmax) / t <= 1e-7; t = t + h)
            {
                Upr = Un+(F(t-h,Un)*3-F(t-2*h,Uprev))*(h/2);
                Ucor = Un + (F(t - h, Un) + F(t, Upr))*(h/2);
                Uprev = Un;
                Un = Ucor;
                //Console.Write($"{n,3} |{t:0.00} |");

                //Console.Write($"(");
                //for (int i = 0; i < Vector.Dimension; i++)
                //{
                //    Yn.v[i] = Yt[i](t);
                //    Console.Write($"{Yn.v[i]:0.00}| ");
                //}
                //Console.Write($")| ");

                //Console.Write($"(");
                //for (int i = 0; i < Vector.Dimension; i++)
                //{
                //    Console.Write($"{Un.v[i]:0.00}| ");
                //}
                //Console.Write($")| ");
                //dy = new Vector(Yn - Un).Norm;
                //Console.Write($"{dy:0.00}\n");
                Print(n, t);
                n++;
            }
        }

        List<Func<double, double>> Yt = new List<Func<double, double>>();

        public SLDE()
        {
            Yn = new Vector();

            Yt.Add(PureSolution.Y1);
            Yt.Add(PureSolution.Y2);
            Yt.Add(PureSolution.Y3);
            U0 = new Vector(new List<double> { 1, 0, 0 });
            Y0 = new Vector(U0);
            t0 = 0;
            tmax = 1;
            
            Un = U0 + F(t0, U0) * h;
            Uprev = Un;

        }
    }

}
