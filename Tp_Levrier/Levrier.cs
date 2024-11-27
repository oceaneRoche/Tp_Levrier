using System;
using System.Threading;

namespace Tp_Levrier
{
    class Levrier
    {
        private static Random random = new Random();
        private int numero;
        private AutoResetEvent eventArrivee;

        public Levrier(int numero, AutoResetEvent eventArrivee)
        {
            this.numero = numero;
            this.eventArrivee = eventArrivee;
        }

        public void Run()
        {
            Console.WriteLine($"Lévrier {numero} commence la course.");

            for (int i = 0; i <= 10; i++)
            {
                Thread.Sleep(random.Next(1, 10));
                Console.WriteLine($"Levrier {numero} a parcouru{i} metres");

                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Lévrier {numero} est arrivé !");
                }
            }          
            eventArrivee.Set();
        }
    }

}
