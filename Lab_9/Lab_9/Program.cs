using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Lab_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Programmer programmer = new Programmer();
            PL f = new PL("c++", new List<string>() { "Objected oriented" });
            PL s = new PL("c", new List<string>() { "Procedural" });
            PL t = new PL("java", new List<string>() { "Objected oriented" });
            programmer.Rename += s.Rename;
            programmer.NewProperty += f.NewProperty;
            programmer.NewProperty += t.NewProperty;
            Console.WriteLine(f); Console.WriteLine(s); Console.WriteLine(t);
            programmer.EventsCall();
            Console.WriteLine(f); Console.WriteLine(s); Console.WriteLine(t);

            Console.WriteLine("\n");

            

            string transformate = "Just give    me 1 second and I'll be fine. Couldn't see the truth, I was blind";
            Action<string> op = FirstOption;
            ActWithString(transformate, (s)=>Console.WriteLine(s));
            op = SecondOption;
            ActWithString(transformate, op);
        }
        static void ActWithString(string trans, Action<string> op)
        {
            op(trans);
        }
        static void FirstOption(string s)
        {
            Console.WriteLine("*First Option*");
            Console.WriteLine("Before transformation:\n" + s);
            Func<string, string> retFunc = StringExtension.DeleteNumbers;
            s = retFunc(s);
            retFunc = StringExtension.PunctuationMarksDelete;
            s = retFunc(s);
            retFunc = StringExtension.MarkWordsWithDollarSymbol;
            s = retFunc(s);
            Console.WriteLine("After transformation:\n" + s + "\n");
        }
        static void SecondOption(string s)
        {
            Console.WriteLine("*Second Option*");
            Console.WriteLine("Before transformation:\n" + s);
            Func<string, string> retFunc = StringExtension.DeleteOddSpaces;
            s = retFunc(s);
            retFunc = StringExtension.FirstLetterToUpperCase;
            s = retFunc(s);
            Console.WriteLine("After transformation:\n" + s + "\n");
        }
    }

    class Programmer
    {
        public delegate void Handler(string s);
        public event Handler Rename;
        public event Handler NewProperty;

        public void EventsCall()
        {

            Console.WriteLine("Enter the new name: ");
            Rename?.Invoke(Console.ReadLine());


            Console.WriteLine("Enter the new property: ");
            NewProperty?.Invoke(Console.ReadLine());

        }
    }
        class PL
        {
            string name;
            List<string> properties;

            public PL(string name, List<string> properties)
            {
                this.name = name;
                this.properties = properties;
            }
            public void Rename(string name)
            {
                this.name = name;
            }
            public void NewProperty(string property)
            {
                properties.Add(property);
            }
            public override string ToString()
            {
                string output = $"Name: {name}\nProperties: ";
                foreach (string s in properties)
                    output += s + "  ";
                output += "\n\n";
                return output;
            }
        }
    public static class StringExtension
    {
        public static string PunctuationMarksDelete(string str)
        {
            string Nstr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '.' && str[i] != ',') Nstr += str[i]; 
            }
            return Nstr;
        }
        public static string FirstLetterToUpperCase(string str)
        {
            string Nstr = ""; int i = 0;
            if (str[0] != ' ') { Nstr += Nstr += str[0].ToString().ToUpper(); i = 1; }
            for (; i < str.Length; i++)
            {
                Nstr += str[i];
                if (str[i] == ' ')
                {
                    i++; Nstr += str[i].ToString().ToUpper(); 
                }
            }
            return Nstr;
        }
        public static string DeleteOddSpaces(string str)
        {
            string Nstr = "";
            int counter = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ') counter++;
                else counter = 0;
                if (counter < 2) Nstr += str[i]; 
            }
            return Nstr;
        }
        public static string MarkWordsWithDollarSymbol(string str)
        {
            string Nstr = "";
            Nstr += str[0];
            for(int i = 1; i < str.Length; i++)
            {
                if (str[i] == ' ' && str[i-1] != ' ') Nstr += '$';
                Nstr += str[i];
            }
            return Nstr;
        }
        public static string DeleteNumbers(string str)
        {
            string Nstr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '0' || str[i] == '1' || str[i] == '2' || str[i] == '3' || str[i] == '4' || str[i] == '5' || str[i] == '6' || str[i] == '7' || str[i] == '8' || str[i] == 9)
                    continue;
                 Nstr += str[i];
            }
            return Nstr;
        }
    }
}
