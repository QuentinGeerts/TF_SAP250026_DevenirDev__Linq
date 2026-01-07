/*
 *  Démonstration 03 - Méthodes d'extension
 */

using DemoMethodesExtension.Models;

//Int32Extensions int32Extensions = new Int32Extensions(); // Passé static

for (int i = 0; i < 101; i++)
{
    if (i.IsPrime())
    {
        Console.WriteLine($"{i}");
    }
}

Console.WriteLine($"5 pair ?: {5.IsEven()}");
Console.WriteLine($"5!: {5.Factorial()}");