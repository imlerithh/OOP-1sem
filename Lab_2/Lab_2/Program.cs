using System;
using System.Linq;
using System.Text;

namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Types
            Console.WriteLine("TYPES");
            //a)
            bool bltype = true; Console.Write(bltype.GetType() + " "); Console.WriteLine(bltype);
            byte bttype = 244; Console.Write(bttype.GetType() + " "); Console.WriteLine(bttype);
            char ctype = 'f'; Console.Write(ctype.GetType() + " "); Console.WriteLine(ctype);
            decimal dtype = 6.9m; Console.Write(dtype.GetType() + " "); Console.WriteLine(dtype);
            double dbltype = 66.99; Console.Write(dbltype.GetType() + " "); Console.WriteLine(dbltype);
            float ftype = 69.69f; Console.Write(ftype.GetType() + " "); Console.WriteLine(ftype);
            int itype = -1111; Console.Write(itype.GetType() + " "); Console.WriteLine(itype);
            uint uitype = 111111111; Console.Write(uitype.GetType() + " "); Console.WriteLine(uitype);
            long ltype = -2222222; Console.Write(ltype.GetType() + " "); Console.WriteLine(ltype);
            ulong ultype = 2222222; Console.Write(ultype.GetType() + " "); Console.WriteLine(ultype);
            short stype = -2222; Console.Write(stype.GetType() + " "); Console.WriteLine(stype);  
            ushort ustype = 33333; Console.Write(ustype.GetType() + " "); Console.WriteLine(ustype);
            string name = "falling"; Console.Write(name.GetType() + " "); Console.WriteLine(name);
            object o = "hello"; Console.Write(o.GetType() + " "); Console.WriteLine(o); 
            dynamic p = null;
            //b)
            Console.WriteLine("Converting");
            dbltype = ftype; //float к double
            itype = stype; // short к int
            dtype = bttype; // bttype к decimal
            ltype = itype; // int к long
            ultype = uitype; // uint к ulong

            ltype = (long)uitype; 
            ctype = (char)bttype; 
            itype = (int)dbltype; 
            stype = (short)bttype;
            ftype = (float)dbltype;

            o = "33";
            itype = Convert.ToInt32(o);
            //c)
            Console.WriteLine("Boxing&Unboxing");
            itype = 123;
            o = itype;
            o = (int)o + 7;
            Console.WriteLine("Integer before unboxing: " + itype); // h = 123
            itype = (int)o; // unboxing
            Console.WriteLine("Integer after unboxing: " + itype); // h = 130
            

            o = itype;
            itype = (int)o;

            o = ftype;
            ftype = (float)o;

            o = dbltype;
            dbltype = (double)o;

            o = stype;
            stype = (short)o;

            o = name;
            name = (string)o;

            o = bltype;
            bltype = (bool)o;
            //d)
            //неявная типизация var
            var v1 = 4.0;
            var v2 = 5.0;
            Console.Write("Hypotenuse with sides " +
                            v1 + " by " + v2 + " equals ");

            Console.WriteLine("{0:#.###}.", Math.Sqrt((v1 * v1) + (v2 * v2)));
            //e)
            int? x = null; // Nullable<int> x = null;
            if (x.HasValue)
                Console.WriteLine(x.Value);
            else
                Console.WriteLine("x is equal to null");
            //f)
            var v = 33;
            //v = "sss";

            //2.Strings
            Console.WriteLine("\nSTRINGS");
            //a)
            string first = "first";
            string second = new string("second");
            string third = "third";
            if (first == second)
                Console.WriteLine("first and second strings are equal");
            else Console.WriteLine("first and second strings are not equal");
            //b)
            string[] strlist = { first, second, third };
            string joinstr = String.Join(" ", strlist);
            Console.WriteLine(joinstr); // объединение строк

            string strCopy = String.Copy(first); //копирование
            Console.WriteLine(strCopy);
            Console.WriteLine(third.Substring(1, 3)); //выделение подстроки

            string[] splitstr = joinstr.Split(' ');
            foreach (string s in splitstr)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine(joinstr.Insert(3, second.Substring(0, 3)));
            Console.WriteLine(third.Remove(2, 3));

            //c)
            string sEmpty = ""; 
            string sNull = null;
            Console.WriteLine(string.IsNullOrEmpty(sEmpty));
            Console.WriteLine(string.IsNullOrEmpty(sNull));
            Console.WriteLine(sEmpty == sNull);
            //d)
            StringBuilder sb = new StringBuilder("The Weeknd", 30); //строка и выделяемая ей память
            sb.Remove(7, 2);
            sb.Append("woo");
            sb.Insert(0, "not");
            Console.WriteLine(sb);

            //Массивы
            Console.WriteLine("\nARRAYS");
            int[,] arr = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                    Console.Write(arr[i, y] + " ");
                Console.WriteLine();
            }

            string[] arrstring = { "QQQQ", "WWWW", "EEEE", "RRRR", "TTTT", "YYYY" };
            for (int i = 0; i < arrstring.Length; i++)
            {
                Console.Write(" " + arrstring[i]);
            }
            Console.WriteLine();
            Console.WriteLine($"Длина массива: {arrstring.Length}");
            Console.WriteLine("Введите позицию нового элемента массива, а затем его значение");
            arrstring[Convert.ToInt32(Console.ReadLine()) - 1] = Console.ReadLine();
            for (int i = 0; i < arrstring.Length; i++)
            {
                Console.Write(" " + arrstring[i]);
            }


            double[][] jaggedArr = new double[][]
            {
                new double[2],
                new double[3],
                new double[4]
            };
            Console.WriteLine("\nВведите элементы ступенчатого массива");
            for (int i = 0; i < jaggedArr.Length; i++)
                for (int y = 0; y < jaggedArr[i].Length; y++)
                    jaggedArr[i][y] = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine();
            for (int i = 0; i < jaggedArr.Length; i++)
            {
                for (int y = 0; y < jaggedArr[i].Length; y++)
                {
                    Console.Write("{0} ", jaggedArr[i][y]);
                }
                Console.WriteLine();
            }
            var array = new object[0];
            var str = "";

            //КОРТЕЖИ
            Console.WriteLine("\nTUPLES");
            (string, int, char, string, ulong) VarTuple = ("Kevin", 69, 'F', "De Bruyne", 88);
            Console.WriteLine("Кортеж целиком: " + VarTuple);
            Console.WriteLine($"1, 3 и 4 элементы кортежа: {VarTuple.Item1}, {VarTuple.Item3}, {VarTuple.Item4}");
            string firstT = VarTuple.Item1;
            int secondT = VarTuple.Item2;
            char thirdT = VarTuple.Item3;
            string fourthT = VarTuple.Item4;
            ulong fifthT = VarTuple.Item5;

            var NewTuple = ("Kevin", 69, 'F', "De Bruyne", (ulong)88);

            if (VarTuple.CompareTo(NewTuple) == 0)
                Console.WriteLine("Кортежи равны");
            else
                Console.WriteLine("Кортежи не равны");


            //ЛОКАЛЬНАЯ ФУНКЦИЯ
            Console.WriteLine("\nLOCAL FUNCTION");
            (int, int, int, char) LocalFunction(int[] mas, string str1)
            {
                int max = mas.Max();
                int min = mas.Min();
                int sum = mas.Sum();
                char FL = str1[0];
                return (max, min, sum, FL);

            }
            var Array1 = new[] { 11, 28, 6, 27, 10, 27, 20, 26, 22, 7, 29, 36, 17, 28, 40 };
            string Str1 = "Alexander Lacazete";
            Console.WriteLine(LocalFunction(Array1, Str1));


            //checked unchecked
            Console.WriteLine("\nCHECKED/UNCHECKED");
            int Checksum(int x, int y)
            {
                try
                {
                    checked
                    {
                        int result = x + y;
                        return result;
                    }
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                    return 1;
                }
            }

            int Unchecksum(int x, int y)
            {
                try
                {
                    unchecked
                    {
                        int result = x + y;
                        return result;
                    }
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                    return 1;
                }
            }

            int a = Int32.MaxValue;
            int b = 1;
            Console.WriteLine(Checksum(a, b));
            Console.WriteLine(Unchecksum(a, b));

        }

    }
}
