using BL;
using System;

namespace D.I.A.N.A_
{
    class Program
    {
        static void Main(string[] args)
        {

            Start();
            Console.ReadLine();
        }

        static void Start()
        {
            BusinessLogic bl = new BusinessLogic();

            Console.WriteLine("Fai la tua domanda");
            string question = Console.ReadLine();
            Console.WriteLine(bl.Return(question));
            Start();
        }
    }
}
