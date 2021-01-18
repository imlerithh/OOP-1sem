using System;

namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p = new Monitor(222, "LG", 22.3);
            p.Upgrade();
            Furniture f = p as Furniture;
            if (f == null) Console.WriteLine("\"p\" is not a furniture");
            else Console.WriteLine("\"p\" is a furniture");
            Monitor m;
            if (p is Monitor) m = (Monitor)p;  

            Console.WriteLine("\n");


            Product[] products = new Product[] {new PC(800, "JET Gamer"),new PC(900, "AMD Ryzen 3"),new Monitor(90,"Philips",18.5),new Headphones(700,"Airpods"), new Projector(200,"Benq MS527", 3), new Table(80, "Canfor") };
            Printer printer = new Printer();
            foreach(Product product in products)
            {
                printer.IAmPrinting(product);
            }
        }
    }

    interface IUpgradable
    {
        void Upgrade();
    }
    abstract class Product
    {
        public int price;
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
            return $"Product: {this.GetType()}\nModel: {model}\nPrice: {price}\nIs Upgraded: {isUpgraded}\n";
        }
    }
    abstract class Furniture : Product, IUpgradable
    {
        public string firm;
        public override void Upgrade()
        {
            isUpgraded = true;
        }
        public override string ToString()
        {
            return $"Product: {this.GetType()}\nFirm: {firm}\nPrice: {price}\nIs Upgraded: {isUpgraded}\n";
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    class PC : Technics
    {
        public PC(int price, string model)
        {
            this.price = price;
            this.model = model;
        }
    }
    class Monitor : Technics
    {
        Screen screen;
        public Monitor(int price, string model, double diagonal)
        {
            this.price = price;
            this.model = model;
            screen = new Screen(diagonal);
        }
        public override string ToString()
        {
                return $"Product: {this.GetType()}\nModel: {model}\nScreen: {screen.diagonal}'\nPrice: {price}\nIs Upgraded: {isUpgraded}\n";
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
        public Headphones(int price, string model)
        {
            this.price = price;
            this.model = model;
        }
    }
    class Projector : Technics
    {
        Screen screen;
        public Projector(int price, string model, double diagonal)
        {
            this.price = price;
            this.model = model;
            screen = new Screen(diagonal);
        }
        public override string ToString()
        {
            return $"Product: {this.GetType()}\nModel: {model}\nScreen: {screen.diagonal}'\nPrice: {price}\nIs Upgraded: {isUpgraded}\n";
        }
    }
    class Table : Furniture
    {
        public Table(int price, string firm)
        {
            this.price = price;
            this.firm = firm;
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
            if (product is Table) product = (Table)product;
            Console.WriteLine(product);
        }
    }
}
