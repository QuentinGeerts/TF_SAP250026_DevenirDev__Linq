/*
 *  Démonstration 04 - Yield (Immediat vs Différé)
 */

int[] numbers = [1, 2, 3, 4, 5];

// 1.  Exécution immédiate
// → retourne une valeur

// Opérateur Count retourne une valeur

int count = numbers.Count(); // Exécution directe

Console.WriteLine($"Count: {count}");


// 2.  Exécution différée
// → retourne plusieurs valeurs

// Opérateur Where retourne plusieurs valeurs

var evenNumbers = numbers.Where(n => n % 2 == 0);

numbers[2] = 8;

foreach (var n in evenNumbers)
{
    Console.WriteLine($"n: {n}");
}

// 3.  Mot-clef "yield"


// 3.1.  Fonction retournant une liste directement
List<int> GetNumbers()
{
    List<int> ints = new List<int>();

    for (int i = 0; i < 10; i++)
    {
        ints.Add(i);
    }

    return ints;
}

var entiers =  GetNumbers(); // Immédiat

foreach (var item in entiers)
{
    Console.WriteLine($"item: {item}");
}

// 3.2.  Fonction retourner à la demande les valeurs 

IEnumerable<int> GetNumbersYield ()
{
    for (int i = 0; i < 10; i++)
    {
        yield return i; // Retourne l'élément et suspend l'exécution
    }
}

var entiers2 = GetNumbersYield(); // Différé

foreach (var item in entiers2)
{
    Console.WriteLine($"item: {item}");
}

// 3.3.  Transformer la méthode différée en méthode immédiate

var entiers3 = GetNumbersYield().ToList(); // Immédiat

foreach (var item in entiers2)
{
    Console.WriteLine($"item: {item}");
}