using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComputerClass
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 6; i++)
            {
                try
                {
                    //AgeInitExcept
                    if (i == 1) { Technics t1 = new PC(1000, 400, "S-800"); }
                    //PriceInitExcept
                    if (i == 2) { Technics t2 = new PC(-100, 10, "M300"); }
                    //ModelInitExcept
                    if (i == 3) { Technics t3 = new PC(1000, 10, "Q"); }
                    //DivideByZeroExcept
                    if (i == 4) { int x = 5; Technics t4 = new PC(x / 0, 10, "F-300"); }
                    //NullRefException
                    if (i == 5) { string s = null; s.ToUpper(); }
                    //Debug.Assert
                    if (i == 6) { Technics t5 = new PC(1000, 5, "Asus"); t5.IsUpgraded = true; }
                }
                catch (AgeInitializationException ex)
                {
                    Console.WriteLine($"AgeInitializationException: {ex.Message}");
                    Console.WriteLine($"Source: {ex.Source}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
                catch (PriceInitializationException ex)
                {
                    Console.WriteLine($"PriceInitializationException: {ex.Message}");
                    Console.WriteLine($"Source: {ex.Source}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
                catch (ModelInitializationException ex)
                {
                    Console.WriteLine($"ModelInitializationException: {ex.Message}");
                    Console.WriteLine($"Source: {ex.Source}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"DivideByZeroException: {ex.Message}");
                    Console.WriteLine($"Source: {ex.Source}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine($"NullReferenceException: {ex.Message}");
                    Console.WriteLine($"Source: {ex.Source}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");
                }
                catch { 

                }
                finally
                {
                    Console.WriteLine("\n\n");
                }
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
    static class ComputerClassController
    {
        public static int GetTotalPrice(ComputerClass compClass)
        {
            int sum = 0;
            List<Technics> temp = compClass.GetList().GetRange(0, compClass.GetList().Count);
            foreach (Technics technics in temp)
            {
                sum += technics.Price;
            }
            return sum;
        }
        public static void DisplayTechichsByPrice(ComputerClass compClass)
        {
            List<Technics> temp = compClass.GetList().GetRange(0, compClass.GetList().Count);
            temp.Sort(new TechnicsComparer());
            Console.WriteLine("Display Techichs By Price".ToUpper() + "\n(descending)\n");
            foreach (Technics technics in temp)
            {
                Console.WriteLine(technics.ToString());
            }
            Console.WriteLine("\n");
        }
        public static void FindDecommissionedEquipment(ComputerClass compClass)
        {
            List<Technics> temp = compClass.GetList().GetRange(0, compClass.GetList().Count);
            Console.WriteLine("Decomissioned Equipment".ToUpper() + "\n(age>8)\n");
            foreach (Technics technics in temp)
            {
                if (technics.Age > 8)
                    Console.WriteLine(technics.ToString());
            }
            Console.WriteLine("\n");
        }

        class TechnicsComparer : IComparer<Technics>
        {
            public int Compare(Technics t1, Technics t2)
            {
                if (t1.Price < t2.Price)
                    return 1;
                else if (t1.Price > t2.Price)
                    return -1;
                else
                    return 0;
            }
        }
    }
    class ComputerClass
    {
        private List<Technics> listOfTechnics = new List<Technics>();

        public List<Technics> GetList()
        {
            return listOfTechnics;
        }
        public void SetList(List<Technics> listOfTechnics)
        {
            this.listOfTechnics = listOfTechnics;
        }
        public void AddElementToList(Technics tech)
        {
            listOfTechnics.Add(tech);
        }
        public void RemoveElementFromList(Technics tech)
        {
            listOfTechnics.Remove(tech);
        }
        public void DisplayListOfTechnics()
        {
            Console.WriteLine(listOfTechnics);
        }

    }

    interface IUpgradable
    {
        void Upgrade();
    }

    abstract class Technics : IUpgradable
    {
        private string model;
        private int price;
        private int age;
        private bool isUpgraded;

        public bool IsUpgraded{
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
        Screen screen;
        public Projector(int price, int age, string model, double diagonal)
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
}
