using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSJuniorClon
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] array = { 2, 5, 1, 7, 9, 1, 4, 8 };

            array = ShuffleArray(array, random);

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }

            Console.ReadKey();
        }

        static int[] ShuffleArray(int[] array, Random random)
        {
            int tempNumber;
            int tempIndex;

            for (int i = 0; i < array.Length; i++)
            {
                tempIndex = random.Next(i + 1);
                tempNumber = array[tempIndex];
                array[tempIndex] = array[i];
                array[i] = tempNumber;
            }

            return array;
        }
    }
}
