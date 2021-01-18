using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
// SOAP
//Console.WriteLine("\nSOAP");
//SoapFormatter formatter = new SoapFormatter();

//using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
//{
//    formatter.Serialize(fs, people);

//    Console.WriteLine("Объект сериализован");
//}

//// десериализация
//using (FileStream fs = new FileStream("people.soap", FileMode.OpenOrCreate))
//{
//    Person[] newPeople = (Person[])formatter.Deserialize(fs);

//    Console.WriteLine("Объект десериализован");
//}
namespace Lab_14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Monitor monitor = new Monitor(100, "LG", 1.8);

            // Binary
            Console.WriteLine("Binary");
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("monitor.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, monitor);
                Console.WriteLine("Object serialized");
            }
            using (FileStream fs = new FileStream("monitor.dat", FileMode.OpenOrCreate))
            {
                Monitor newMonitor = (Monitor)formatter.Deserialize(fs);

                Console.WriteLine("Object deserialized\n");
                Console.WriteLine(newMonitor);
            }
            //JSON
            Console.WriteLine("\nJSON");
            File.WriteAllText("monitor.json", JsonSerializer.Serialize(monitor));
            Console.WriteLine("Object serialized");
            Monitor monitor1 = JsonSerializer.Deserialize<Monitor>(File.ReadAllText("monitor.json"));
            Console.WriteLine("Object deserialized\n");
            Console.WriteLine(monitor1);
            //XML
            Console.WriteLine("\nXML");
            XmlSerializer XMLformatter = new XmlSerializer(typeof(Monitor));
            using (FileStream fs = new FileStream("monitor.xml", FileMode.OpenOrCreate))
            {
                XMLformatter.Serialize(fs, monitor);
                Console.WriteLine("Object serialized");
            }
            using (FileStream fs = new FileStream("monitor.xml", FileMode.OpenOrCreate))
            {
                Monitor newMonitor = (Monitor)XMLformatter.Deserialize(fs);
                Console.WriteLine("Object deserialized\n");
                Console.WriteLine(newMonitor);
            }

            //2
            Console.WriteLine("array serialization/deserialization".ToUpper());
            Monitor[] monitors = new Monitor[] { new Monitor(150,"X", 1.3), new Monitor(80, "S", 1.1) };

            formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("monitor.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, monitors);
                Console.WriteLine("\nObjects serialized");
            }
            Console.WriteLine("\nObjects deserialized");
            using (FileStream fs = new FileStream("monitor.dat", FileMode.OpenOrCreate))
            {
                Monitor[] deserilizeMonitors = (Monitor[])formatter.Deserialize(fs);

                foreach (Monitor m in deserilizeMonitors)
                {
                    Console.WriteLine(m);
                }
            }
            //3
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("monitor.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.OuterXml);

            XmlNode childnode = xRoot.SelectSingleNode("Monitor[Model='LG']");
            //4
            XDocument xdoc = XDocument.Load("monitors.xml");
            Console.WriteLine("\nMonitors before 200");
            var items = from xe in xdoc.Element("Monitors").Elements("Monitor")
                        where Int32.Parse(xe.Element("Price").Value) < 200
                        select new Monitor
                        {
                            Model = xe.Element("Model").Value,
                            Price = Int32.Parse(xe.Element("Price").Value)
                        };

            foreach (var item in items)
                Console.WriteLine($"Model: {item.Model} - Price: {item.Price}");
        }
        [Serializable]
        public class Monitor
        {
            public string Model { get; set; }
            public int Price { get; set; }
            public bool IsUpgraded { get; set; }
            public Screen screen { get; set; }
            public Monitor(int price, string model, double diagonal)
            {
                Price = price;
                Model = model;
                screen = new Screen(diagonal);
            }
            public Monitor()
            {

            }
            public void Upgrade()
            {
                IsUpgraded = true;
            }
            public override string ToString()
            {
                return $"Product: {this.GetType()}\nModel: {Model}\nScreen: {screen.Diagonal}'\nPrice: {Price}\nIs Upgraded: {IsUpgraded}\n";
            }
            [Serializable]
            public class Screen
            {
                public double Diagonal { get; set; }
                public Screen(double diagonal)
                {
                    Diagonal = diagonal;
                }
                public Screen()
                {

                }
            }
        }
    }
}