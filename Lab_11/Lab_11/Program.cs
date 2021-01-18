using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab_11
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            string[] monthes = { "June", "July", "August", "September", "October", "November", "December", "January", "February", "March", "April", "May" };
            int n = 6;
            var selectedMonthes = from t in monthes where t.Length == n select t;
            Console.WriteLine("Monthes with length " + n);
            foreach (string s in selectedMonthes)
                Console.WriteLine(s);
            selectedMonthes = from t in monthes where t.Equals("June")|| t.Equals("July") || t.Equals("August") || t.Equals("December") || t.Equals("January") || t.Equals("February") 
                              select t;
            Console.WriteLine("\nWinter and Summer monthes " );
            foreach (string s in selectedMonthes)
                Console.WriteLine(s);
            selectedMonthes = from t in monthes orderby t select t;
            Console.WriteLine("\nSorted Monthes by alphabet");
            foreach (string s in selectedMonthes)
                Console.WriteLine(s);
            Console.WriteLine("\nNumber of monthes with length > 4 and containing 'u'");
            selectedMonthes = from t in monthes where t.Length>4 && t.Contains('u') select t;
                Console.WriteLine(selectedMonthes.Count());
            //2-3
            List<Airplane> airplanes = new List<Airplane>() { 
                new Airplane("Paris", 11, "M-73", new DateTime(1, 1, 1, 18, 30, 0), new ArrayList() { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Sunday }),
                new Airplane("Milan", 7, "Boing-1818", new DateTime(1, 1, 1, 20, 20, 0), new ArrayList() { DayOfWeek.Monday, DayOfWeek.Saturday }),
                new Airplane("Bratislava", 9, "F-1377", new DateTime(1, 1, 1, 13, 50, 0), new ArrayList() { DayOfWeek.Tuesday, DayOfWeek.Saturday }),
                new Airplane("Berlin", 10, "MILF", new DateTime(1, 1, 1, 16, 30, 0), new ArrayList() { DayOfWeek.Wednesday}),
                new Airplane("Berlin", 3, "AG-1488", new DateTime(1, 1, 1, 03, 20, 0), new ArrayList() { DayOfWeek.Monday, DayOfWeek.Tuesday}),
                new Airplane("Moscow", 1, "TEEN-14", new DateTime(1, 1, 1, 10, 50, 0), new ArrayList() { DayOfWeek.Thursday}),
                new Airplane("Warsawa", 4, "MILF", new DateTime(1, 1, 1, 22, 20, 0), new ArrayList() { DayOfWeek.Sunday}),
                new Airplane("Amsterdam", 8, "SS-1337", new DateTime(1, 1, 1, 11, 40, 0), new ArrayList() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Saturday })
            };

            Console.Write("\n\nEnter the destination: "); string dest = Console.ReadLine();
            var selectedAirplanes = from t in airplanes where t.Destination.Equals(dest) select t;
            Console.WriteLine("\nFlights with destination: " + dest);
            foreach (Airplane a in selectedAirplanes) Console.WriteLine(a.FlightNumber);

            Console.Write("\nEnter the desired day of the week:\n 0 - Sunday\n 1 - Monday\n 2 - Tuesday\n 3 - Wednesday\n 4 - Thursday\n 5 - Friday\n" +
               " 6 - Saturday\n ");
            int day;
            while (true)
            {
                try
                {
                    day = Int32.Parse(Console.ReadLine());
                    if (day < 0 || day > 6) throw new Exception();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect input. Try again");
                }
            }
            selectedAirplanes = from t in airplanes where t.DaysOfWeek.Contains((DayOfWeek)day) select t;
            Console.WriteLine("\nFlights with this day: " + (DayOfWeek)day);
            foreach (Airplane a in selectedAirplanes) Console.WriteLine(a.FlightNumber);

            Console.Write("\nEnter the hour: "); int hour;
            while (true)
            {
                try
                {
                    hour = Int32.Parse(Console.ReadLine());
                    if (hour < 0 || hour > 23) throw new Exception();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect input. Try again");
                }
            }
            selectedAirplanes = from t in airplanes where t.DepartureTime.Hour>=hour select t;
            Console.WriteLine("\nFlights after: " + hour + ":00");
            foreach (Airplane a in selectedAirplanes) Console.WriteLine("Flight: " + a.FlightNumber + " Time: " + a.DepartureTime.Hour+ ":" + a.DepartureTime.Minute);

            //selectedAirplanes = from t in airplanes orderby t.DaysOfWeek[0], t.DepartureTime.Hour select t;
            selectedAirplanes = airplanes.OrderBy(t => t.DaysOfWeek[0]).ThenBy(t => t.DepartureTime.Hour);
            Console.WriteLine("\nFlights sorted by day and time:");
            foreach (Airplane a in selectedAirplanes) Console.WriteLine(a);

            Console.WriteLine("Enter the type of airplane: "); string type = Console.ReadLine();
            selectedAirplanes = from t in airplanes where t.AirplaneType.Equals(type) select t;
            Console.WriteLine("Amount of flight with this type of airplane: " + selectedAirplanes.Count());
            
            //4
            var destt = from t in airplanes where true orderby t.Destination select t.Destination;
            string conc = destt.Aggregate((x, y) => x + y);
            Console.WriteLine("\n" + conc);
            foreach (var t in destt.TakeWhile(x => x.StartsWith("A")||x.StartsWith("B")))
                Console.WriteLine(t);

            //5
            List<Event> events = new List<Event>()
            {
                new Event() {City = "Berlin", Eventt = "Film Festival" },
                new Event() {City = "Porto", Eventt = "Consert" },
                new Event() {City = "Amsterdam", Eventt = "UN meeting" }
            };
            var result = from a in airplanes
                         join e in events on a.Destination equals e.City
                         select new { City = a.Destination, Event = e.Eventt };
            result = result.Concat(result).Distinct();

            Console.WriteLine("\nEvents that you can meet:\n");
            foreach (var item in result)
                Console.WriteLine($"{item.City} - {item.Event}");
        }
    }
    class Event
    {
        public string City { get; set; }
        public string Eventt { get; set; }
    }
}
