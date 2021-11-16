using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> customers = new Queue<int>();
            int storeAccount = 0;

            customers.Enqueue(32);
            customers.Enqueue(12);
            customers.Enqueue(46);
            customers.Enqueue(243);
            customers.Enqueue(77);
            customers.Enqueue(188);
            customers.Enqueue(99);

            while (customers.Count > 0)
            {
                Console.WriteLine($"Клиент сделал покупку на {customers.Peek()}");

                storeAccount += customers.Dequeue();

                Console.WriteLine($"Наш счет :{storeAccount}");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
