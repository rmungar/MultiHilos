
public static partial class Shuffler
{
    private static Random rng = new Random();  
    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }

}


public partial class Ej 
{

    private List<int> numeros = new List<int>(Enumerable.Range(1, 75));


    private Thread hilo1 = new Thread(new ThreadStart());
    private Thread hilo2 = new Thread(new ThreadStart());


 
    private void main()
    {

        numeros.Shuffle();


    }

    private int getPiedraPapelTijeras(){
        return numeros.Shuffle();
    }



}