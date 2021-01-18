using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.IO;

namespace Lab_16
{
    class Program
    {
        static BlockingCollection<string> products;
        static int productNumber = 0;
        static void Main(string[] args)
        {
            int n = 100000;
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;
            string elapsedTime;
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            //1
            //stopWatch.Start();
            //Task task1 = Task.Factory.StartNew(() => PrimeNumbers(n));
            //while (!task1.IsCompleted)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine($"CurrentId: {task1.Id}\nIsCompleted: {task1.IsCompleted}\nStatus: {task1.Status}\n");
            //}
            //stopWatch.Stop();
            //ts = stopWatch.Elapsed;
            //elapsedTime = String.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);
            ////2
            //Task task2 = Task.Factory.StartNew(() => PrimeNumbers(n, token));
            //cancelTokenSource.Cancel();
            ////3
            //Task<List<int>> task3 = new Task<List<int>>(() => PrimeNumbers(8));
            //Task<int> task3_1 = task3.ContinueWith(task3 =>
            //{
            //    int sum = 0;
            //    foreach (int a in task3.Result) sum += a;
            //    return sum;
            //});
            //task3.Start();
            //Thread.Sleep(1000);
            //Console.WriteLine(task3_1.Result);
            ////4
            //stopWatch.Start();
            ////Parallel.For(1, 4, ArrayGeneration);
            //ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 100, 200, 300 }, ArrayGeneration);
            //while (!result.IsCompleted) { }
            //stopWatch.Stop();
            //ts = stopWatch.Elapsed;
            //elapsedTime = String.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds / 10);
            //Console.WriteLine("\nRunTime " + elapsedTime);

            //stopWatch.Start();
            //ArrayGeneration(100); ArrayGeneration(200); ArrayGeneration(300);
            //stopWatch.Stop();
            //ts = stopWatch.Elapsed;
            //elapsedTime = String.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime + '\n');
            ////5
            //Parallel.Invoke(
            //() =>
            //{
            //    for (int i = 1; i < 20; i += 2)
            //    {
            //        Console.WriteLine($"Task:{Task.CurrentId}  {i}");
            //        Thread.Sleep(10);
            //    }
            //},
            //    () =>
            //    {
            //        for (int i = 2; i < 20; i += 2)
            //        {
            //            Console.WriteLine($"Task:{Task.CurrentId}  {i}");
            //            Thread.Sleep(10);
            //        }
            //    });
            //6
            WriteToFileAsync();
            products = new BlockingCollection<string>();
            Task[] customersTasks = new Task[5];
            for (int i = 1; i <= 5; i++)
            {
                Task.Factory.StartNew(() => new Provider("provider_" + i, i * 1500).Provide(token));
                Thread.Sleep(300);
                customersTasks[i - 1] = Task.Factory.StartNew(() => new Customer("customer_" + i, i * 250).Buy());
                Thread.Sleep(300);
            }
            Thread.Sleep(1000);
            Task.WaitAll(customersTasks);
            Console.WriteLine("\nAll the customers leave the store");
            cancelTokenSource.Cancel();
            Console.WriteLine("Providers stopped\n");
            Thread.Sleep(1000);
        }
        class Customer
        {
            string name;
            int waitingTime;
            string product;
            public void Buy()
            {
                while (true)
                {
                    if (products.TryTake(out product))
                    {
                        Console.WriteLine(name + " buy " + product);
                    }
                    else
                    {
                        Console.WriteLine(name + " is waiting");
                        if (products.TryTake(out product, TimeSpan.FromMilliseconds(waitingTime)))
                        {
                            Console.WriteLine(name + " buy " + product);
                        }
                        else{
                            Console.WriteLine(name + " is leaving");
                            break;
                        }
                    }
                    Thread.Sleep(100);
                }
            }
            public Customer(string name, int waitingTime)
            {
                this.name = name;
                this.waitingTime = waitingTime;
            }
        }
        class Provider
        {
            string name;
            int providingTime;
            string product = "product_";
            public void Provide(CancellationToken token)
            {
                while (true)
                {
                    Console.WriteLine(name + " preparing to provide the product");
                    Thread.Sleep(providingTime);
                    products.Add($"{product} {++productNumber}");
                    Console.WriteLine(name + " provides " + product + productNumber);
                    Thread.Sleep(100);
                    if (token.IsCancellationRequested) break;
                }
            }
            public Provider(string name, int providingTime)
            {
                this.name = name;
                this.providingTime = providingTime; 
            }
        }

        static void ArrayGeneration(int n)
        {
            n *= 1000000;
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = i;
        }
        static List<int> PrimeNumbers(int n)
        {
            List<int> numbers = new List<int>();
            for (int i = 2; i <= n; i++) numbers.Add(i);
            int p = 2;
            while (p*p<n)
            {
                if (numbers.Contains(p))
                {
                    int j = 2;
                    while (p * j <= n)
                    {
                        if(numbers.Contains(p*j)) numbers.Remove(p * j);
                        j++;
                    }
                }
                p++;
            }
            return numbers;
        }
        static List<int> PrimeNumbers(int n, CancellationToken token)
        {
            List<int> numbers = new List<int>();
            for (int i = 2; i <= n; i++) numbers.Add(i);
            int p = 2;
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("\nOperation is canceled by token\n");
                return numbers;
            }
            while (p * p < n)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("\nOperation is canceled by token\n");
                    return numbers;
                }
                if (numbers.Contains(p))
                {
                    int j = 2;
                    while (p * j <= n)
                    {
                        if (numbers.Contains(p * j)) numbers.Remove(p * j);
                        j++;
                    }
                }
                p++;
            }
            return numbers;
        }
        static async void WriteToFileAsync()
        {
            await Task.Run(() => WriteToFile());
        }
        static void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter("affectio.txt", false))
            {
                for (int i = 1; i <= 10; i++)
                {
                    writer.WriteLine(i);
                    Thread.Sleep(800);
                }
            }
            Console.WriteLine("\nASYNC OPERATION IS DONE\n");
        }
    }
}
