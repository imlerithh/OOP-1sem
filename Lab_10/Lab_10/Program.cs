using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Lab_10
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            FirmServises peopleCollection = new FirmServises(3);
            peopleCollection.Add("Adidas", new Servises() {servises = new List<string>() { "Creating an impressive sports field", "Inventing sport things" } });
            peopleCollection.Add("UnitedHealth Group", new Servises() { servises = new List<string>() { "Health care", "Scientific services", "Social assistance" } });
            peopleCollection.Add("Oracle", new Servises() { servises = new List<string>() { "N/A" } });

            Console.WriteLine("Displaying the entries in Collection:");
            foreach (DictionaryEntry de in peopleCollection)
            {
                Console.WriteLine("{0}:  {1}", de.Key, de.Value);
            }

            Console.WriteLine();
            Console.WriteLine("Displaying the entries in the modified Collection:");
            peopleCollection["Oracle"] = new Servises() { servises = new List<string>() { "Professional and technical services", "Information services" } };
            peopleCollection.Remove("Adidas");
            peopleCollection.Insert(0, "Nike", new Servises() { servises = new List<string>() { "Creating sport things" } });

            foreach (DictionaryEntry de in peopleCollection)
            {
                Console.WriteLine("{0}:  {1}", de.Key, de.Value);
            }
            Console.WriteLine("\n");
            //2
            Queue<object> queue = new Queue<object>();
            queue.Enqueue("Me"); queue.Enqueue(1); queue.Enqueue('F'); queue.Enqueue(false); queue.Enqueue("Falling"); queue.Enqueue(488); queue.Enqueue("Apart");
            int i = 1;
            foreach (object o in queue) { Console.WriteLine($"{i}. " + o); i++; } Console.WriteLine('\n');
            for (i = 0; i < 2; i++) queue.Dequeue();
            List<object> list = new List<object>();
            foreach (object o in queue) { list.Add(o); }
            foreach (object o in list) { Console.WriteLine(o); } Console.WriteLine('\n');
            object suspect = "Falling";
            if (list.IndexOf(suspect) >= 0) Console.WriteLine("Desired value found, its index in list - " + list.IndexOf(suspect));
            else Console.WriteLine("There is no such element in list"); Console.WriteLine('\n');
            //3
            ObservableCollection<Servises> servises = new ObservableCollection<Servises>
            {
                new Servises() { servises = new List<string>() { "servise11", "servise12" } },
                new Servises() { servises = new List<string>() { "servise21", "servise22" } }
            };
            servises.CollectionChanged += Servises_CollectionChanged;

            servises.Add(new Servises() { servises = new List<string>() { "servise31", "servise32" } });
            servises.RemoveAt(1);
            servises[0] = new Servises() { servises = new List<string>() { "servise41", "servise42" } };

            foreach (Servises s in servises)
            {
                Console.WriteLine(s);
            }
        }
        private static void Servises_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Servises newServises = e.NewItems[0] as Servises;
                    Console.WriteLine($"Добавлены новые услуги: {newServises}");
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    Servises oldServises = e.OldItems[0] as Servises;
                    Console.WriteLine($"Удалены услуги: {oldServises}");
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    Servises replacedServises = e.OldItems[0] as Servises;
                    Servises replacingServises = e.NewItems[0] as Servises;
                    Console.WriteLine($"Услуги {replacedServises} заменены eуслугами {replacingServises}");
                    break;
            }
        }
    }
    class Servises
    {
        public List<string> servises;
        public Servises()
        {

        }
        public override string ToString()
        {
            string temp = "";
            foreach(string s in servises)
            {
                temp += s + ". ";
            }
            return temp;
        }
    }
    class FirmServises : IOrderedDictionary
    {
        private ArrayList _list;
        public FirmServises(int numItems)
        {
            _list = new ArrayList(numItems);
        }
        public int IndexOfKey(object key)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (((DictionaryEntry)_list[i]).Key == key)
                    return i;
            }

            // key not found, return -1.
            return -1;
        }
        public object this[int index] {
            get
            {
                return ((DictionaryEntry)_list[index]).Value;
            }
            set
            {
                object key = ((DictionaryEntry)_list[index]).Key;
                _list[index] = new DictionaryEntry(key, value);
            }
        }
        public object this[object key] {
            get
            {
                return ((DictionaryEntry)_list[IndexOfKey(key)]).Value;
            }
            set
            {
                _list[IndexOfKey(key)] = new DictionaryEntry(key, value);
            }
        }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public ICollection Keys
        {
            get
            {
                ArrayList KeyCollection = new ArrayList(_list.Count);
                for (int i = 0; i < _list.Count; i++)
                {
                    KeyCollection.Add(((DictionaryEntry)_list[i]).Key);
                }
                return KeyCollection;
            }
        }

        public ICollection Values
        {
            get
            {
                ArrayList ValueCollection = new ArrayList(_list.Count);
                for (int i = 0; i < _list.Count; i++)
                {
                    ValueCollection.Add(((DictionaryEntry)_list[i]).Value);
                }
                return ValueCollection;
            }
        }

        public int Count => _list.Count;

        public bool IsSynchronized => _list.IsSynchronized;

        public object SyncRoot => _list.SyncRoot;

        public void Add(object key, object value)
        {
            if (IndexOfKey(key) != -1)
            {
                throw new ArgumentException("An element with the same key already exists in the collection.");
            }
            _list.Add(new DictionaryEntry(key, value));
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(object key)
        {
            if (IndexOfKey(key) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return new FirmServisesEnum(_list);
        }

        public void Insert(int index, object key, object value)
        {
            if (IndexOfKey(key) != -1)
            {
                throw new ArgumentException("An element with the same key already exists in the collection.");
            }
            _list.Insert(index, new DictionaryEntry(key, value));
        }

        public void Remove(object key)
        {
            _list.RemoveAt(IndexOfKey(key));
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FirmServisesEnum(_list);
        }

        public class FirmServisesEnum : IDictionaryEnumerator
        {
            public ArrayList _list;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

            public FirmServisesEnum(ArrayList list)
            {
                _list = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _list.Count);
            }

            public void Reset()
            {
                position = -1;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return _list[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public DictionaryEntry Entry
            {
                get
                {
                    return (DictionaryEntry)Current;
                }
            }

            public object Key
            {
                get
                {
                    try
                    {
                        return ((DictionaryEntry)_list[position]).Key;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public object Value
            {
                get
                {
                    try
                    {
                        return ((DictionaryEntry)_list[position]).Value;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}