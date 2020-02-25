using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer_Opgave4
{
    class Program
    {
        static object _lock = new object();
        static Queue<int> numbers = new Queue<int>();
        static int[] vec = { 1, 2, 3 };

        public static void Main()
        {
            new Thread(Producer).Start();
            new Thread(Consumer).Start();
            new Thread(Consumer).Start();
        }
        
        public static void Consumer()
        {
            while (true)
            {
                lock (_lock)
                {
                    while (numbers.Count == 0)
                    {
                        Console.WriteLine("Consumer waits");
                        Monitor.Wait(_lock);
                    }

                    Console.WriteLine("Consumer is taking " + numbers.Dequeue());
                    Monitor.Pulse(_lock);
                }
                Thread.Sleep(1000);
            }
        }

        public static void Producer()
        {
            while (true)
            {
                for (int i = 0; i < vec.Length; i++)
                {
                    lock (_lock)
                    {
                        while (numbers.Count > vec.Length)
                        {
                            Console.WriteLine("             Producer waits");
                            Monitor.Wait(_lock);
                        }

                        numbers.Enqueue(vec[i]);
                        Monitor.Pulse(_lock);
                        Console.WriteLine("                 Producer is adding " + vec[i]);
                    }

                   
                }
            }
        }
            

    }
}
