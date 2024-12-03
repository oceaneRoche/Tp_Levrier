using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Levrier
{
    public string Nom { get; private set; }
    public ConsoleColor Couleur { get; private set; }
    private static Random random = new Random();
    private static int classementCounter = 1;
    public static List<(string Nom, int Classement)> Classements = new List<(string, int)>();

    private ManualResetEvent startEvent;
    private Mutex classementMutex;

    public Levrier(string nom, ConsoleColor couleur, ManualResetEvent startEvent, Mutex classementMutex)
    {
        Nom = nom;
        Couleur = couleur;
        this.startEvent = startEvent;
        this.classementMutex = classementMutex;
    }

    public void Run()
    {
        startEvent.WaitOne();

        for (int i = 50; i <= 10000; i += 50)
        {
            Thread.Sleep(random.Next(50, 100));

            lock (Console.Out)
            {
                Console.ForegroundColor = Couleur;
                Console.WriteLine($"Lévrier {Nom} a parcouru {i} mètre(s).");
                Console.ResetColor();
            }
        }

        classementMutex.WaitOne();
        try
        {
            Classements.Add((Nom, classementCounter++));
        }
        finally
        {
            classementMutex.ReleaseMutex();
        }

        lock (Console.Out)
        {
            Console.ForegroundColor = Couleur;
            Console.WriteLine($"Le lévrier nommé {Nom} est arrivé !");
            Console.ResetColor();
        }
    }
}