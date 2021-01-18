using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace Lab_8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stack<int> s1 = new Stack<int>();
                s1.AddElement(3);
                s1.AddElement(8);
                s1.AddElement(1);
                s1.AddElement(12);
                s1.RemoveElement();
                s1.DisplayElements();

                Stack<string> s2 = new Stack<string>();
                s2.AddElement("a");
                s2.AddElement("bb");
                s2.AddElement("ccc");
                s2.AddElement("dddd");
                s2.DisplayElements();

                Stack<Technics> s3 = new Stack<Technics>();
                s3.AddElement(new Headphones(200, 3, "Redmi"));
                s3.AddElement(new PC(1000, 2, "Asus"));
                s3[1].Upgrade();
                s3.AddElement(new Monitor(300, 4, "LG", 19.5));
                s3.AddElement(new Projector(100, 20, "Fss"));
                s3.RemoveElement();
                s3.WriteElementsToFile(@"file.txt");
                s3.ReadAndDisplayElementsFromFile(@"file.txt");
            //    s3.DisplayElements();


            }
            catch(TechnicsInitializationException ex)
            {
                Console.WriteLine($"TechnicsInitializationException: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IOException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
    interface IStackOperations<T>
    {
        void AddElement(T element);
        void RemoveElement();
        void DisplayElements();
        void WriteElementsToFile(string s);
        void ReadAndDisplayElementsFromFile(string s);
    }
    public class Stack<T> : IStackOperations<T>
    {
        public List<T> elements;
        public Date date;

        public Stack()
        {
            elements = new List<T>();
            date = new Date();
        }
        public void AddElement(T element)
        {
            elements.Add(element);
        }
        public void RemoveElement()
        {
            elements.RemoveAt(elements.Count - 1);
        }
        public void DisplayElements()
        {
            Console.WriteLine(ToString());
        }
        public void WriteElementsToFile(string path)
        {
                Console.WriteLine("Start writing to file");
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(ToString());
                }
                Console.WriteLine("Writing is done\n");
        }
        public void ReadAndDisplayElementsFromFile(string path)
        {
            Console.WriteLine("Start reading from file\n");
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("\nReading is done\n");
        }

        public T this[int index]
        {
            get
            {
                return elements[index];
            }
            private set
            {
                elements[index] = value;
            }
        }
        public override string ToString()
        {
            string output = "----------STACK----------\n";
            if (elements.Count <= 0)
                output += "empty\n";
            else
                foreach (T a in elements)
                {
                    output += a + "\n";
                }
            return output;
        }
        public class Date
        {
            public readonly DateTime dateTime;
            public Date()
            {
                dateTime = DateTime.Now;
            }
        }
    }
    class TechnicsInitializationException : ArgumentException
    {
        public TechnicsInitializationException(string message)
        : base(message)
        { }
    }
    class AgeInitializationException : TechnicsInitializationException
    {
        public AgeInitializationException(string message)
        : base(message)
        { }
    }
    class PriceInitializationException : TechnicsInitializationException
    {
        public PriceInitializationException(string message)
        : base(message)
        { }
    }
    class ModelInitializationException : TechnicsInitializationException
    {
        public ModelInitializationException(string message)
        : base(message)
        { }
    }
    interface IUpgradable
    {
        void Upgrade();
    }
    public abstract class Technics : IUpgradable
    {
        private string model;
        private int price;
        private int age;
        private bool isUpgraded;

        public bool IsUpgraded
        {
            get
            {
                return isUpgraded;
            }
            set
            {
                Debug.Assert(false, "isUpgraded must be initialized with method, not property");
            }
        }
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                if (value.Length < 2)
                    throw new ModelInitializationException("The name is too small");
                else model = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                    throw new PriceInitializationException("Price can't be less than zero");
                else if (value == 0) throw new PriceInitializationException("Price can't be equal to zero");
                else price = value;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 0) throw new AgeInitializationException("Age can't be less than zero");
                else if (value > 60) throw new AgeInitializationException("The age is too big");
                else age = value;
            }
        }
        public void Upgrade()
        {
            isUpgraded = true;
        }
        public override string ToString()
        {
            return $"Product: {GetType()}\nModel: {Model}\nPrice: {price}\nAge: {Age}\nIs Upgraded: {IsUpgraded}\n";
        }
    }
    class PC : Technics
    {
        public PC(int price, int age, string model)
        {
            Price = price;
            Model = model;
            Age = age;
        }
    }
    class Monitor : Technics
    {
        Screen screen;
        public Monitor(int price, int age, string model, double diagonal)
        {
            Price = price;
            Model = model;
            Age = age;
            screen = new Screen(diagonal);
        }
        public override string ToString()
        {
            return $"Product: {GetType()}\nModel: {Model}\nScreen: {screen.Diagonal}'\nPrice: {Price}\nAge: {Age}\nIs Upgraded: {IsUpgraded}\n";
        }
    }
    class Screen
    {
        private double diagonal;

        public double Diagonal
        {
            get
            {
                return diagonal;
            }
            set
            {
                diagonal = value;
            }
        }
        public Screen(double diagonal)
        {
            Diagonal = diagonal;
        }
    }
    class Headphones : Technics
    {
        public Headphones(int price, int age, string model)
        {
            Price = price;
            Model = model;
            Age = age;
        }
    }
    class Projector : Technics
    {
        public Projector(int price, int age, string model)
        {
            Price = price;
            Model = model;
            Age = age;
        }
    }
}