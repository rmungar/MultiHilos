// ReSharper disable All
namespace MultiHilos;
using static MultiHilos.Program2;
class Program
{
    static string[] opciones = { "Piedra", "Papel", "Tijera" };
    static string? p1 = null;
    static string? p2 = null;
    static object _bloqueo = new object();
    static Random random = new Random();

    static void Jugador1()
    {
        lock (_bloqueo)
        {
            p1 = opciones[random.Next(3)];
            Console.WriteLine("Jugador 1 elige: " + p1);
        }
    }

    static void Jugador2()
    {
        lock (_bloqueo)
        {
            p2 = opciones[random.Next(3)];
            Console.WriteLine("Jugador 2 elige: " + p2);
        }
    }

    static void DeterminarGanador()
    {
        if (p1 == p2)
        {
            Console.WriteLine("Empate!");
        }
        else if ((p1 == "Piedra" && p2 == "Tijera") || (p1 == "Papel" && p2 == "Piedra") || (p1 == "Tijera" && p2 == "Papel"))
        {
            Console.WriteLine("Jugador 1 gana!");
        }
        else
        {
            Console.WriteLine("Jugador 2 gana!");
        }
    }


    static void Dobles()
    {
        Thread jugador1 = new Thread(Jugador1);
        Thread jugador2 = new Thread(Jugador2);
        
        jugador1.Start();
        jugador2.Start();
        
        jugador1.Join();
        jugador2.Join();
        
        DeterminarGanador();
    }

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Seleccione el modo de juego:");
            Console.WriteLine("1 - 1v1");
            Console.WriteLine("2 - Torneo con 16 jugadores");
            Console.WriteLine("3 - Salir");
            string? opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Dobles();
            }
            else if (opcion == "2")
            {
                Torneo();
            }
            else if (opcion == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opcion no válida");
            }
        }
        
        
    }
}