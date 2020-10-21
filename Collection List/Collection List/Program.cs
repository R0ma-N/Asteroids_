using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int> { 6, 3, 8, 4, 7, 5, 6, 2, 8, 8};
            foreach (int i in list.Distinct())
            {
                Console.WriteLine(i + " повторяется " + list.Where(x => x == i).Count() + " раз(а)");
            }

            Console.ReadKey();
        }
    }
}
