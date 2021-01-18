using System;
using System.Collections;
using System.Linq;

namespace Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack s1 = new Stack();
            s1 += 67;
            if (s1)
            {
                s1 += 7;
                s1 += 1;
                s1 += 55;
            }
            Console.WriteLine("Stack1 before decrementing:\n" + s1);
            s1--;
            Console.WriteLine("Stack1 after decrementng:\n" + s1);
            Console.WriteLine("Second element of Stack1: " + s1[1] + "\n");
            Stack s2 = new Stack();
            s2 += 6;
            s2 += 5;
            s2 += 4;
            Console.WriteLine("Stack2 before copy:\n" + s2);
            s2 = s2 < s1;
            Console.WriteLine("Stack2 after copy:\n" + s2);

            Stack.Owner owner = new Stack.Owner(77, "Marc", "BSTU");
            Console.WriteLine("Time of creation of Stack1: " + s1.date.dateTime);

            Console.WriteLine("\nSum between max and min of Stack2: " + StatisticOperation.Sum(s2));
            Console.WriteLine("Sub between max and min of Stack2: " + StatisticOperation.Sub(s2));
            Console.WriteLine("Count of elements of Stack2: " + StatisticOperation.Count(s2));
            Console.WriteLine("The average element of Stack2: " + s2.AverageElement());
            string paleFire = "The border. Everything I loved was lost. But no aorta could report regret.A sun of rubber was convulsed and set." +
            "And blood-black nothingness began to spin.A system of cells interlinked within.Cells interlinked within cells interlinked.Within one stem.";
            Console.WriteLine("\n" + paleFire + "\nNumber of sentences: " + paleFire.SentencesCount());

        }
    }
    public class Stack
    {
        public ArrayList elements;
        public Date date;

        public Stack()
        {
            elements = new ArrayList();
            date = new Date();
        }
       
        public int this[int index]
        {
            get
            {
                return (int)elements[index];
            }
            private set
            {
                elements[index] = value;
            }
        }

        public static Stack operator +(Stack c1, int c2)
        {
            ArrayList temp = c1.elements;
            temp.Add(c2);
            return new Stack { elements = temp };
        }
        public static Stack operator --(Stack c1)
            {
            ArrayList temp = c1.elements;
            temp.RemoveAt(temp.Count-1);
            return new Stack { elements = temp };
            }
        public static bool operator true(Stack c1)
        {
            return c1.elements.Count != 0;
        }
        public static bool operator false(Stack c1)
        {
            return c1.elements.Count == 0;
        }
        public static Stack operator >(Stack c1, Stack c2)
        {
            ArrayList temp = c1.elements;
            temp.Sort();
            return new Stack { elements = temp };
        }
        public static Stack operator <(Stack c1, Stack c2)
        {
            ArrayList temp = c2.elements;
            temp.Sort();
            return new Stack { elements = temp };
        }
        public override string ToString()
        {
            string output = "----------STACK----------\n";
            if (elements.Count <= 0)
                output += "empty\n";
            else
            foreach(int a in elements)
            {
                output+=a + "\n";
            }
            return output;
        }
        public class Owner
        {
            private int id;
            private string name;
            private string organization;
            public Owner(int id, string name, string organization)
            {
                this.id = id;
                this.name = name;
                this.organization = organization;
            }
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
    public static class StatisticOperation
    {
        public static int Sum(Stack s)
        {
            int[] temp = new int[s.elements.Count];
            s.elements.CopyTo(temp);
            return temp.Max() + temp.Min();
        }
        public static int Sub(Stack s)
        {
            int[] temp = new int[s.elements.Count];
            s.elements.CopyTo(temp);
            return temp.Max() - temp.Min();
        }
        public static int Count(Stack s)
        {
            return s.elements.Count;
        }
        public static int SentencesCount(this string str)
        {
            int counter = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                    counter++;
            }
            return counter;
        }
        public static int AverageElement(this Stack s)
        {
            ArrayList temp = s.elements.GetRange(0,s.elements.Count);
            temp.Sort(); 
            return (int)(temp[temp.Count/2]);
        } 
    }
}
