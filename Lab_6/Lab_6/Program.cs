using System;
using System.Collections.Generic;

namespace ComputerClass
{
    class Program
    {
        static void Main(string[] args)
        {
            ComputerClass computerClass = new ComputerClass();
            computerClass.AddElementToList(new PC(800, 5, "JET Gamer"));
            computerClass.AddElementToList(new Monitor(90, 10, "Philips", 18.5));
            computerClass.AddElementToList(new Headphones(700, 3, "Airpods"));
            computerClass.AddElementToList(new Projector(200, 12, "Benq MS527", 2.2));
            computerClass.AddElementToList(new PC(900, 4, "AMD Ryzen 3"));

            Console.WriteLine("Total Price Of All Products: " + ComputerClassController.GetTotalPrice(computerClass) + "\n\n");
            ComputerClassController.DisplayTechichsByPrice(computerClass);
            ComputerClassController.FindDecommissionedEquipment(computerClass);
        }
    }
    static class ComputerClassController
    {
        public static int GetTotalPrice(ComputerClass compClass)
        {
            int sum = 0;
            List<Technics> temp = compClass.GetList().GetRange(0, compClass.GetList().Count); 
            foreach(Technics technics in temp)
            {
                sum += technics.price;
            }
            return sum;
        }
        public static void DisplayTechichsByPrice(ComputerClass compClass)
        {
            List<Technics> temp = compClass.GetList().GetRange(0, compClass.GetList().Count);
            temp.Sort(new TechnicsComparer());
            Console.WriteLine("Display Techichs By Price".ToUpper() +"\n(descending)\n");
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
            foreach(Technics technics in temp)
            {
                if(technics.age>8)
                Console.WriteLine(technics.ToString());
            }
            Console.WriteLine("\n");
        }

        class TechnicsComparer : IComparer<Technics>
        {
            public int Compare(Technics t1, Technics t2)
            {
                if (t1.price < t2.price)
                    return 1;
                else if (t1.price > t2.price)
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
    abstract class Product
    {
        public int price;
        public int age;
        public bool isUpgraded;

        public abstract void Upgrade();
    }
    abstract class Technics : Product, IUpgradable
    {
        public string model;
        public override void Upgrade()
        {
            isUpgraded = true;
        }
        public override string ToString()
        {
            return $"Product: {this.GetType()}\nModel: {model}\nPrice: {price}\nAge: {age}\nIs Upgraded: {isUpgraded}\n";
        }
    }
    class PC : Technics
    {
        public PC(int price, int age, string model)
        {
            this.price = price;
            this.model = model;
            this.age = age;
        }
    }
    class Monitor : Technics
    {
        Screen screen;
        public Monitor(int price, int age, string model, double diagonal)
        {
            this.price = price;
            this.model = model;
            this.age = age;
            screen = new Screen(diagonal);
        }
        public override string ToString()
        {
            return $"Product: {this.GetType()}\nModel: {model}\nScreen: {screen.diagonal}'\nPrice: {price}\nAge: {age}\nIs Upgraded: {isUpgraded}\n";
        }
    }
    class Screen
    {
        public double diagonal;
        public Screen(double diagonal)
        {
            this.diagonal = diagonal;
        }
    }
    class Headphones : Technics
    {
        public Headphones(int price, int age, string model)
        {
            this.price = price;
            this.model = model;
            this.age = age;
        }
    }
    class Projector : Technics
    {
        Screen screen;
        public Projector(int price, int age, string model, double diagonal)
        {
            this.price = price;
            this.model = model;
            this.age = age;
            screen = new Screen(diagonal);
        }
        public override string ToString()
        {
            return $"Product: {this.GetType()}\nModel: {model}\nScreen: {screen.diagonal}'\nPrice: {price}\nAge: {age}\nIs Upgraded: {isUpgraded}\n";
        }
    }
    sealed class Printer
    {
        public void IAmPrinting(Product product)
        {
            if (product is PC) product = (PC)product;
            if (product is Headphones) product = (Headphones)product;
            if (product is Monitor) product = (Monitor)product;
            if (product is Projector) product = (Projector)product;
            Console.WriteLine(product);
        }
    }
    struct User
    {
        public string name;
        public int age;

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {name}  Age: {age}");
        }
    }
    enum Color {
        White, 
        Black,
        Green,
        Blue,
    }
}
