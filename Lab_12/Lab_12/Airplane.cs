using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab_12
{
    enum DayOfWeek
    {
        Sunday = 0,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }


    public partial class Airplane
    {
        private static int amountOfAirplanes;
        public const double maxHigh = 12_500;
        private string destination;
        private int flightNumber;
        private string airplaneType;
        private DateTime departureTime;
        private ArrayList daysOfWeek;
        private readonly int id;

        public Airplane()
        {

        }
        public string Destination
        {
            get => destination;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Destination must not be blank");
                else destination = value;
            }
        }
        public int FlightNumber
        {
            get => flightNumber;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Flight number must be more than zero");
            }
        }
        public string AirplaneType
        {
            get => airplaneType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Airplane Type must not be blank");
                else airplaneType = value;
            }
        }
        public DateTime DepartureTime
        {
            get => departureTime;
            private set { }
        }
        public ArrayList DaysOfWeek
        {
            get => daysOfWeek;
            set
            {
                foreach (object d in daysOfWeek)
                {
                    if (d.GetType() != typeof(DayOfWeek))
                        throw new ArgumentException("Wrong type of day of the week");
                }
            }
        }
        public long ID
        {
            get => id;
        }
        public Airplane(string destination, int flightNumber, string airplaneType, DateTime departureTime, ArrayList daysOfWeek)
        {
            if (string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Destination must not be blank");
            else this.destination = destination;

            if (flightNumber < 0)
                throw new ArgumentException("Flight number must be more than zero");
            else this.flightNumber = flightNumber;

            if (string.IsNullOrWhiteSpace(airplaneType))
                throw new ArgumentException("Airplane Type must not be blank");
            else this.airplaneType = airplaneType;

            this.departureTime = departureTime;
            foreach (object d in daysOfWeek)
            {
                if (d.GetType() != typeof(DayOfWeek))
                    throw new ArgumentException("Wrong type of day of the week");
            }
            this.daysOfWeek = daysOfWeek;


            amountOfAirplanes++;
            id = GetHashCode();
        }
        public Airplane(string destination, DateTime departureTime, ArrayList daysOfWeek, int flightNumber = 0, string airplaneType = "N/A")
        {
            if (string.IsNullOrWhiteSpace(destination))
                throw new ArgumentException("Destination must not be blank");
            else this.destination = destination;

            this.airplaneType = airplaneType;
            this.flightNumber = flightNumber;
            this.departureTime = departureTime;
            foreach (object d in daysOfWeek)
            {
                if (d.GetType() != typeof(DayOfWeek))
                    throw new ArgumentException("Wrong type of day of the week");
            }
            this.daysOfWeek = daysOfWeek;

            amountOfAirplanes++;
            id = GetHashCode();
        }
        static Airplane()
        {
            amountOfAirplanes = 0;
        }
        void method(ref int x, out int y)
        {
            y = 1;

        }
        public static int getAmountOfAirplanes()
        {
            return amountOfAirplanes;
        }
        public override string ToString()
        {
            string output = $"----------Flight Information----------\nDestination: {destination}\n";
            if (flightNumber > 0) output += $"Flight Number: {flightNumber}\n";
            if (!airplaneType.Equals("N/A")) output += $"Airplane Type: {airplaneType}\n";
            output += $"Departure Time: {departureTime.Hour}:{departureTime.Minute}\n";
            output += "Days Of Week: ";
            foreach (DayOfWeek d in daysOfWeek)
            {
                output += $"{d}  ";
            }
            output += "\n\n";
            return output;
        }
        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            int code = departureTime.Hour * departureTime.Minute;
            foreach (char c in destination)
            {
                code += (int)c;
            }
            foreach (char c in airplaneType)
            {
                code *= (int)c;
            }
            if (flightNumber != 0)
                code *= flightNumber;
            return code;
        }
    }
}
