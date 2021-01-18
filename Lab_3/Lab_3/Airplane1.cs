using System;
using System.Collections;

namespace Lab_3
{
    public partial class Airplane
    {
        static void Main(string[] args)
        {
            DateTime time1 = new DateTime(1, 1, 1, 18, 30, 0);
            time1 = default(DateTime).Add(time1.TimeOfDay);
            ArrayList days1 = new ArrayList();
            days1.AddRange(new DayOfWeek[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Sunday });
            Airplane fAirplane = new Airplane("Paris", 11, "M-73", time1, days1);

            DateTime time2 = new DateTime(1, 1, 1, 20, 20, 0);
            time2 = default(DateTime).Add(time2.TimeOfDay);
            ArrayList days2 = new ArrayList();
            days2.AddRange(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Saturday });
            Airplane sAirplane = new Airplane("Milan", 7, "Boing-1818", time2, days2);

            Console.WriteLine("Type of the First Airplane: " + fAirplane.GetType());
            Console.WriteLine("Type of the Second Airplane: " + sAirplane.GetType());
            Console.WriteLine("ID of the first airplane: " + fAirplane.ID);
            Console.WriteLine("ID of the second airplane: " + sAirplane.ID);
            Console.WriteLine("Is the first airplane equal to the second? " + (fAirplane.Equals(sAirplane)));
            Console.WriteLine();
            Console.WriteLine(fAirplane.ToString());
            Console.WriteLine(sAirplane.ToString());

            Airplane tAirplane = new Airplane("Bratislava", 9, "F-1377", new DateTime(1, 1, 1, 13, 50, 0), new ArrayList() { DayOfWeek.Tuesday, DayOfWeek.Saturday });
            Airplane[] listOfAirplanes = new Airplane[] { fAirplane, sAirplane, tAirplane };
            Console.Write("Enter the desired destination: ");
            string desiredLocation = Console.ReadLine();
            bool isFound = false;
            foreach (Airplane airplane in listOfAirplanes)
            {

                if (airplane.Destination.ToUpper().Equals(desiredLocation.ToUpper()))
                {

                    Console.Write("There is the flight in this destination\n" + airplane.ToString());
                    isFound = true;
                }
            }
            if (!isFound) Console.WriteLine("Sorry, there are no flights in this destination");

            Console.Write("\n\nEnter the desired departure day:\n 0 - Sunday\n 1 - Monday\n 2 - Tuesday\n 3 - Wednesday\n 4 - Thursday\n 5 - Friday\n" +
                " 6 - Saturday\n");
            int desiredDay;
            while (true)
            {
                try
                {
                    desiredDay = Int32.Parse(Console.ReadLine());
                    if (desiredDay < 0 || desiredDay > 6) throw new Exception();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect input. Try again");
                }
            }
            isFound = false;
            foreach (Airplane airplane in listOfAirplanes)
            {

                if (airplane.DaysOfWeek.Contains((DayOfWeek)desiredDay))
                {
                    Console.Write($"There is the flight in this day\n");
                    if (airplane.flightNumber > 0)
                        Console.WriteLine($"Flight number: {airplane.FlightNumber}");
                        Console.WriteLine($"Departure Time: {airplane.DepartureTime.Hour}:{airplane.DepartureTime.Minute}\n");
                    isFound = true;
                }
            }
            if (!isFound) Console.WriteLine("Sorry, there are no flights in this day");

            var anonimeAirplane = new { Destination = "Madrid", FlightNumber = 87, AirplaneType = "MI-100", DepartureTime = new DateTime(1, 1, 1, 13, 50, 0), DaysOfWeek = new ArrayList() { DayOfWeek.Monday, DayOfWeek.Sunday } };
            Console.WriteLine("Anonime Type of Airplane: " + anonimeAirplane);


            Console.WriteLine("\nThe total amount of the airplanes - {0}", Airplane.getAmountOfAirplanes());


        }
    }
}
