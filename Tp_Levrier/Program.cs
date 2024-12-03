using System;
using System.Threading;

namespace Tp_Levrier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entrez le nombre de lévriers dans la course : ");
            if (!int.TryParse(Console.ReadLine(), out int nombreLevriers) || nombreLevriers <= 0)
            {
                Console.WriteLine("Veuillez entrer un nombre valide.");
                return;
            }

            List<string> nomsLevriers = new List<string> { "Lucas", "Oceane", "Joel", "Souleyman", "Sorin", "Pavlo", "Baptiste", "Romain", "Nicolas", "Mael" };
            ConsoleColor[] couleurs = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

            ManualResetEvent startEvent = new ManualResetEvent(false);
            Mutex classementMutex = new Mutex();

            Thread[] threads = new Thread[nombreLevriers];
            Levrier[] levriers = new Levrier[nombreLevriers];

            for (int i = 0; i < nombreLevriers; i++)
            {
                string nom = nomsLevriers[i % nomsLevriers.Count] + (i / nomsLevriers.Count + 1);
                ConsoleColor couleur = couleurs[(i + 1) % couleurs.Length];
                levriers[i] = new Levrier(nom, couleur, startEvent, classementMutex);
                threads[i] = new Thread(new ThreadStart(levriers[i].Run));
                threads[i].Start();
            }

            Console.WriteLine("Appuyez sur une touche pour lancer la course...");
            Console.ReadKey();
            startEvent.Set();

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("La course est terminée ! Voici le classement final :");
            foreach (var classement in Levrier.Classements.OrderBy(c => c.Classement))
            {
                Console.WriteLine($"Position {classement.Classement}: Lévrier {classement.Nom}");
            }
        }
    }

}
