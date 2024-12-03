﻿using System;
using System.Threading;

namespace Tp_Levrier
{
    class Levrier
    {
        private static Random random = new Random();
        private static object lockObject = new object();
        private static List<int> classement = new List<int>();
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

            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(random.Next(1, 10));
                Console.WriteLine($"Levrier {numero} a parcouru{i} metres");

                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Levrier {numero} est arrive !");
                }
            }          
            eventArrivee.Set();
            lock (lockObject)
            {
                classement.Add(numero);
            }

            Console.WriteLine($"Lévrier {numero} a terminé !");
            eventArrivee.Set();
        }

        public static List<int> GetClassement()
        {
            return classement;
        }

    }

}
