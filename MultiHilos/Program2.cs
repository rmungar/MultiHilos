// ReSharper disable All
namespace MultiHilos;

class Program2
{
    static string[] opciones = { "Piedra", "Papel", "Tijera" };
    static string[] elecciones;
    static object _bloqueo = new object();
    static Random random = new Random();
    static int numJugadores = 16;

    static void Jugador(object index)
    {
        int i = (int)index;
        lock (_bloqueo)
        {
            elecciones[i] = opciones[random.Next(3)];
            Console.WriteLine($"Jugador {i + 1} elige: {elecciones[i]}");
        }
    }

    static int GetGanador(int j1, int j2)
    {
        if (elecciones[j1] == elecciones[j2])
            return -1;
        if ((elecciones[j1] == "Piedra" && elecciones[j2] == "Tijera") ||
            (elecciones[j1] == "Papel" && elecciones[j2] == "Piedra") ||
            (elecciones[j1] == "Tijera" && elecciones[j2] == "Papel"))
            return j1;
        return j2;
    }

    static void Competicion()
    {
        int[] jugadores = new int[numJugadores];
        for (int i = 0; i < numJugadores; i++)
            jugadores[i] = i;

        while (jugadores.Length > 1)
        {
            int nuevaRonda = jugadores.Length / 2;
            int[] ganadores = new int[nuevaRonda];

            for (int i = 0; i < nuevaRonda; i++)
            {
                int j1 = jugadores[i * 2];
                int j2 = jugadores[i * 2 + 1];
                Console.WriteLine($"\n---- Jugador {j1 + 1} vs Jugador {j2 + 1} ----\n");
                Thread.Sleep(1000); // Pausa antes de la partida
                int ganador = GetGanador(j1, j2);
                
                if (ganador == -1)
                {
                    Console.WriteLine($"> Empate entre Jugador {j1 + 1} y Jugador {j2 + 1}, avanzando aleatoriamente.\n");
                    ganador = random.Next(2) == 0 ? j1 : j2;
                }

                Console.WriteLine($"> Jugador {ganador + 1} avanza.");
                Thread.Sleep(1000); // Pausa entre partidas
                ganadores[i] = ganador;
            }
            jugadores = ganadores;
        }
        Console.WriteLine($"\nEl ganador del torneo es el Jugador {jugadores[0] + 1}!\n");
    }

    public static void Torneo()
    {
        elecciones = new string[numJugadores];
        Thread[] hilos = new Thread[numJugadores];

        for (int i = 0; i < numJugadores; i++)
        {
            hilos[i] = new Thread(Jugador);
            hilos[i].Start(i);
        }

        foreach (var hilo in hilos)
        {
            hilo.Join();
        }

        Competicion();
    }
}
