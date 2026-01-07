using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();


Console.WriteLine($"1.  Opérateur « Select »");

// Exercice 1.1 Ecrire une requête pour présenter, pour chaque étudiant, le nom de l’étudiant, la
// date de naissance, le login et le résultat pour l’année de l’ensemble des étudiants.

var resultat1_1a = context.Students.Select(s => new
{
    LastName = s.Last_Name,
    s.BirthDate,
    s.Login,
    YearResult = s.Year_Result
}); // Opérateur

var resultat1_1b = from Student s in context.Students
                   select new 
                   {
                       LastName = s.Last_Name,
                       s.BirthDate,
                       s.Login,
                       YearResult = s.Year_Result
                   }; // Expression de requête


foreach (var s in resultat1_1a)
{
    Console.WriteLine($"{s.LastName} {s.BirthDate} {s.Login} {s.YearResult}");
}

Console.WriteLine();

foreach (var s in resultat1_1b)
{
    Console.WriteLine($"{s.LastName} {s.BirthDate} {s.Login} {s.YearResult}");
}

// Exercice 1.2 Ecrire une requête pour présenter, pour chaque étudiant, son nom complet (nom
// et prénom séparés par un espace), son id et sa date de naissance.



// Exercice 1.3 Ecrire une requête pour présenter, pour chaque étudiant, dans une seule chaine de
// caractère l’ensemble des données relatives à un étudiant séparées par le symbole |.


