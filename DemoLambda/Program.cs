/*
 *  Démonstration 02 - Lambda
 */


// Partie 1 - Délégué custom
using DemoLambda.Delegates;


// Fonction nommée
Operation op1 = Addition;

// Fonction anonyme
Operation op2 = delegate (double a, double b) { return a - b; };

// Fonction anonyme (expression lambda)
Operation op3 = (double a, double b) => { return a * b; };
Operation op4 = (a, b) => a / b;

Console.WriteLine($"{5} + {2} = {op1.Invoke(5, 2)}");
Console.WriteLine($"{5} - {2} = {op2.Invoke(5, 2)}");
Console.WriteLine($"{5} * {2} = {op3.Invoke(5, 2)}");
Console.WriteLine($"{5} / {2} = {op4(5, 2)}");

Console.WriteLine($"Addition: {Addition}");

double Addition (double a, double b)
{
    return a + b;
}

// Partie 2 - Délégués génériques

// Action
// → ne retourne pas de valeur (void)
// Func
// → retourne une valeur (préciser le type de retour)
// Predicate
// → retourne une valeur (bool)

// 2.1.  Action
Action act1 = () => Console.WriteLine();
Action<string> act2 = (string message) => Console.WriteLine(message);
Action<string, string> act3 = (exp, dest) => Console.WriteLine($"{exp} dit bonjour à {dest}");

act1.Invoke();
act2?.Invoke("Il est bientôt l'heure de partir en pause.");
act3("Steve", "Ibrahima");


// 2.2.  Func
Func<string> func1 = () => "Hello";
Func<string, string> func2 = (message) => message.ToUpper();
var func3 = () => $"Hello"; // func<string>

Console.WriteLine($"Func1: {func1()}"); // Hello
Console.WriteLine($"Func2: {func2("Coucou")}"); // COUCOU
Console.WriteLine($"Func3: {func3()}"); // Hello


// 2.3.  Predicate
Predicate<int> predicate1 = (a) => a > 5;
//Predicate<int, int> predicate2 = (a, b) => a > b; // Pas possible
Func<int, int, bool> predicate2 = (a, b) => a > b; // Possible


Console.WriteLine($"Predicate1: {predicate1(10)}");

// 3.  Paramètre délégué
string[] Filtrer(string[] array, Predicate<string> compareFn)
{
    List<string> filteredValues = new();

    foreach (string value in array)
    {
        if (compareFn(value)) filteredValues.Add(value);
    }

    return filteredValues.ToArray();
}

var salutationsAvecI = Filtrer(["Hello", "Bonjour", "Hi", "Konichiwa"], str => str.Contains("i"));


Console.WriteLine($"salutationsAvecI: {string.Join(", ", salutationsAvecI)}");