using System;
using Wintellect.PowerCollections;

namespace NetCoreCheckTestNetCompatibility
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var data = new Bag<int>() { 1, 2, 3 };
            foreach (var element in data)
                Console.WriteLine(element);
        }
    }
}
