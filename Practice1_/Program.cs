using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Practice1_
{
    class Program
    {

        //Значення тау (золоте сечіння)
        public static double tau = (Math.Sqrt(5) - 1) / 2; 
        
        //Делегат для алгебраїчної функції
        public delegate double Function(double x);
        
        //Перша функція
        static double f1(double x)
        {
            return Math.Pow(10 - x, 2);
        }

        //Друга функція
        static double f2(double x)
        {
            return 3 * Math.Pow(x, 4) + Math.Pow(x - 1, 2);
        }

        //Пошук мінімума
        static double GoldenRatioSearch(double a, double b, double epsilon, Function f, StreamWriter writer)
        {
            double x1 = a + (1 - tau) * (b - a);
            double x2 = a + tau * (b - a);
            double f1 = f(x1);
            double f2 = f(x2);

            int iteration = 1;

            while (Math.Abs(b - a) > epsilon)
            {
                if (f1 < f2)
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = a + (1 - tau) * (b - a);
                    f1 = f(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = a + tau * (b - a);
                    f2 = f(x2);
                }

                Console.WriteLine($"Ітерація {iteration}: x1 = {x1} x2 = {x2} f(x1) = {f1} f(x2) = {f2}");

                if (iteration < 11)
                {
                    writer.WriteLine($"Ітерація {iteration}: x1 = {x1} x2 = {x2} f(x1) = {f1} f(x2) = {f2}");
                }
                
                
                iteration++;
            }

            writer.WriteLine($"Остання ітерація : x1 = {x1} x2 = {x2} f(x1) = {f1} f(x2) = {f2}");
            writer.WriteLine($"Кількість ітерацій: {iteration}");
            return (a + b) / 2;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            double a = 6; // Початкове значенння лівої межі
            double b = 15;  // Початкове значенння правої межі
            double epsilon = 0.01; // Точність

            string filePath = "function1.txt";

            //Запис у файл "function1.txt"
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                double minimum = GoldenRatioSearch(a, b, epsilon, f1, writer);
                double minValue = f1(minimum);
                
                Console.WriteLine($"Мінімум функції: {minimum}");
                Console.WriteLine($"Значення функції в мінімумі: {minValue}");

                writer.WriteLine($"Мінімум функції: {minimum}");
                writer.WriteLine($"Значення функції в мінімумі: {minValue}");

                writer.Close();
            }




            filePath = "function2.txt";

            a = 0;
            b = 4;

            //Запис у файл "function2.txt"
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                double minimum = GoldenRatioSearch(a, b, epsilon, f2, writer);
                double minValue = f2(minimum);

                Console.WriteLine($"Мінімум функції: {minimum}");
                Console.WriteLine($"Значення функції в мінімумі: {minValue}");

                writer.WriteLine($"Мінімум функції: {minimum}");
                writer.WriteLine($"Значення функції в мінімумі: {minValue}");

                writer.Close();
            }
        }
    }
}
