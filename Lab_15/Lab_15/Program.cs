using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;

namespace Lab_15
{
    class Program
    {
        static Mutex mutexObj = new Mutex();
        static bool evenFirst = false;
        static void Main(string[] args)
        {
            //1
            //Console.WriteLine("PROCESS");
            //foreach (Process process in Process.GetProcesses())
            //{
            //    Console.Write($"ID: {process.Id}  Name: {process.ProcessName} Priority: {process.BasePriority}");
            //    try
            //    {
            //        DateTime startTime = process.StartTime;
            //        Console.Write($" StartTime: {process.StartTime}");
            //    }
            //    catch
            //    {
            //        Console.Write($" StartTime: No access");
            //    }
            //    try
            //    {
            //        TimeSpan prosessorTime = process.TotalProcessorTime;
            //        Console.WriteLine($" TotalProcessorTime: {process.TotalProcessorTime}");
            //    }
            //    catch
            //    {
            //        Console.WriteLine($" TotalProcessorTime: No access");
            //    }
            //}
            ////2
            //Console.WriteLine("\nDOMAIN");
            //AppDomain domain = AppDomain.CurrentDomain;
            //Console.WriteLine($"Name: {domain.FriendlyName}");
            //Console.WriteLine($"Base Directory: {domain.BaseDirectory}");
            //Console.WriteLine($"Setup Information: {domain.SetupInformation.TargetFrameworkName}");
            //Console.WriteLine();

            //LoadAssembly(3);
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Console.WriteLine("Current Assemblies:");
            //foreach (Assembly asm in domain.GetAssemblies())
            //    Console.WriteLine(asm.GetName().Name);
            ////3
            //Thread counterThread = new Thread(new ParameterizedThreadStart(Count)); counterThread.Name = "counterThread";
            //counterThread.Start(12);
            //Thread.Sleep(1800);
            //Console.WriteLine($"Thread Name: {counterThread.Name}");
            //Console.WriteLine($"IsAlive: {counterThread.IsAlive}");
            //Console.WriteLine($"Priority: {counterThread.Priority}");
            //Console.WriteLine($"ThreadState: {counterThread.ThreadState}");
            ////4
            //Console.WriteLine("\nEVEN\\ODD THREADS");
            //Thread evenThread = new Thread(new ParameterizedThreadStart(Even)); evenThread.Name = "evenThread";
            //Thread oddThread = new Thread(new ParameterizedThreadStart(Odd)); oddThread.Name = "oddThread";
            //int n = 16;
            ////evenThread.Priority = ThreadPriority.AboveNormal;
            //evenThread.Start(n); Thread.Sleep(10); oddThread.Start(n);
            //Thread.Sleep(3000);
            //5
            //Console.WriteLine("\nTIMER");
            //Timer timer = new Timer(new TimerCallback(Count), 3, 0, 2000);
            //Thread.Sleep(6000);
        }
        public static void Count(object n)
        {
                try
                {
                    using (StreamWriter sw = new StreamWriter("count.txt", false, System.Text.Encoding.Default))
                    {
                    for (int i = 1; i <= (int)n; i++)
                    {
                        sw.WriteLine(i);
                        Console.WriteLine(i);
                        Thread.Sleep(300);
                    }
                    }
                    Console.WriteLine("Writing done");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }
        public static void Even(object n)
        {
            if(evenFirst) mutexObj.WaitOne();
            for (int i = 2; i <= (int)n; i+=2)
            {
                if (!evenFirst) mutexObj.WaitOne();
                Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
                Thread.Sleep(10);
                if (!evenFirst) mutexObj.ReleaseMutex();
            }
            if(evenFirst) mutexObj.ReleaseMutex();
        }
        public static void Odd(object n)
        {
            if (evenFirst) mutexObj.WaitOne();
            for (int i = 1; i <= (int)n; i += 2)
            {
                if (!evenFirst) mutexObj.WaitOne();
                Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
                Thread.Sleep(5);
                if (!evenFirst) mutexObj.ReleaseMutex();
            }
            if (evenFirst) mutexObj.ReleaseMutex();
        }

        private static void LoadAssembly(int number)
        {
            var context = new CustomAssemblyLoadContext();
            
            context.Unloading += Context_Unloading;
            
            var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp.dll");
            
            Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
            
            var type = assembly.GetType("Temp.Class1");
            
            var greetMethod = type.GetMethod("Factorial");

            //вызываем метод
            var instance = Activator.CreateInstance(type);
            int result = (int)greetMethod.Invoke(instance, new object[] { number });
            
            Console.WriteLine($"Factorial of {number} equals {result}\n");

            //какие сборки у нас загружены
            Console.WriteLine("Current Assemblies:");
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                Console.WriteLine(asm.GetName().Name);

            // выгружаем контекст
            context.Unload();
        }
        // обработчик выгрузки контекста
        private static void Context_Unloading(AssemblyLoadContext obj)
        {
            Console.WriteLine("\nLibriary Temp unloaded \n");
        }
    }
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public CustomAssemblyLoadContext() : base(isCollectible: true) { }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }
    }
}

