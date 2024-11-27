using System;
using System.Threading;

namespace Tp_Levrier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entrez le nombre de Lévriers dans la course : ");
            int nombreLeveriers = int.Parse(Console.ReadLine());
            List<AutoResetEvent> events = new List<AutoResetEvent>();
            List<Thread> threads = new List<Thread>();
            for (int i = 1; i <= nombreLeveriers; i++)
            {
                AutoResetEvent eventArrivee = new AutoResetEvent(false);
                events.Add(eventArrivee);
                Levrier leverier = new Levrier(i, eventArrivee);
                Thread thread = new Thread(leverier.Run);
                threads.Add(thread);
                thread.Start();
            }
            foreach (var eventArrivee in events)
            {
                eventArrivee.WaitOne();
            }

            Console.WriteLine("La course est terminée !");
        }
    }

}
