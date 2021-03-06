﻿using System;
using System.Globalization;

namespace AdvancedCalculator
{
    class Program
    {
        static double Round(double x)
        {
            return Math.Round(x, 10, mode: MidpointRounding.AwayFromZero);
        }

        static double GetFunctionValue(double x)
        {
            return Round(Math.Pow(x, 2));
        }

        static void Input(out double step, out double min, out double max)
        {
            string[] parametres;
            do
            {
                Console.Write("Введите шаг построения функции и диапазон значений X соответственно через пробел:");
                parametres = Console.ReadLine().Split(' ');
            }
            while (parametres.Length != 3
            || Double.TryParse(parametres[0].Replace('.', ','), out step) == false
            || Double.TryParse(parametres[1].Replace('.', ','), out min) == false
            || Double.TryParse(parametres[2].Replace('.', ','), out max) == false
            || step == 0
            || min == max);

            if ((min > max && step > 0) || (min < max && step < 0))
            {
                double a = max;
                max = min;
                min = a;
            }

        }

        static void PrintTable(int max_length_x, int max_length_y, double min, double step, double max)
        {
            Console.WriteLine($"|{new string('_', max_length_x)}|{new string('_', max_length_y)}|");
            PrintTableRow("X", "Y", max_length_x, max_length_y);
            Console.WriteLine($"{new string('_', max_length_x + max_length_y + 3)}");

            if (step > 0)
            {
                for (; min <= max; min += step)
                {
                    double x = Round(min);
                    string y = Convert.ToString(GetFunctionValue(min));
                    PrintTableRow(Convert.ToString(x), y, max_length_x, max_length_y);
                }
            }
            else
            {
                for (; min >= max; min += step)
                {
                    double x = Round(min);
                    string y = Convert.ToString(GetFunctionValue(min));
                    PrintTableRow(Convert.ToString(x), y, max_length_x, max_length_y);
                }
            }
        }

        static void PrintTableRow(string x, string y, int max_length_x, int max_length_y)
        {
            int spaces_l_x, spaces_r_x, spaces_l_y, spaces_r_y;

            spaces_l_x = (max_length_x - x.Length) / 2;
            spaces_r_x = (max_length_x - x.Length) / 2;
            if ((max_length_x - x.Length) % 2 == 1)
                spaces_l_x++;

            spaces_l_y = (max_length_y - y.Length) / 2;
            spaces_r_y = (max_length_y - y.Length) / 2;
            if ((max_length_y - y.Length) % 2 == 1)
                spaces_l_y++;
            Console.WriteLine($"|{new string(' ', spaces_l_x)}{x}{new string(' ', spaces_r_x)}|{new string(' ', spaces_l_y)}{y}{new string(' ', spaces_r_y)}|");
        }

        static void Main(string[] args)
        {
            double step, min, max;
            Input(out step, out min, out max);
            int size = Convert.ToInt32((max - min) / step) + 1;
            int max_length_y = 0, max_length_x = 0;

            for (int num = 0; num < size; num++)
            {
                double x = min + step * num;

                if (Convert.ToString(GetFunctionValue(x)).Length > max_length_y)
                    max_length_y = Convert.ToString(GetFunctionValue(x)).Length;

                if (Convert.ToString(Round(x)).Length > max_length_x)
                    max_length_x = Convert.ToString(Round(x)).Length;
            }

            PrintTable(max_length_x, max_length_y, min, step, max);

        }
    }
}