using Exo_Linq_Context;

Console.WriteLine("Exercice Linq");
Console.WriteLine("*************");

DataContext context = new DataContext();


Console.WriteLine($"1.  Opérateur « Select »");

// Exercice 1.1 Ecrire une requête pour présenter, pour chaque étudiant, le nom de l’étudiant, la
// date de naissance, le login et le résultat pour l’année de l’ensemble des étudiants.

Console.WriteLine($"\nExercice 1.1.\n");

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


// Exercice 1.2 Ecrire une requête pour présenter, pour chaque étudiant, son nom complet (nom
// et prénom séparés par un espace), son id et sa date de naissance.

Console.WriteLine($"\nExercice 1.2.\n");


var resultat1_2a = context.Students
    .Select(s => new { 
        NomComplet = $"{s.Last_Name} {s.First_Name}", 
        Id = s.Student_ID,
        s.BirthDate
    });

var resultat1_2b = from s in context.Students
                   select new
                   {
                       NomComplet = $"{s.Last_Name} {s.First_Name}",
                       Id = s.Student_ID,
                       s.BirthDate
                   };

foreach (var s in resultat1_2a)
{
    Console.WriteLine($"{s.Id} {s.NomComplet} {s.BirthDate}");
}

// Exercice 1.3 Ecrire une requête pour présenter, pour chaque étudiant, dans une seule chaine de
// caractère l’ensemble des données relatives à un étudiant séparées par le symbole |.

Console.WriteLine($"\nExercice 1.3.\n");

var resultat1_3a = context.Students
    .Select(s => $"{s.Student_ID}|{s.Last_Name}|{s.First_Name}|{s.BirthDate}|{s.Login}|{s.Year_Result}|{s.Course_ID}|{s.Section_ID}");

var resultat1_3b = from s in context.Students 
                   select string.Join("|", s.Student_ID, s.Last_Name, s.First_Name, s.BirthDate, s.Login, s.Year_Result, s.Course_ID, s.Section_ID);

foreach (var s in resultat1_3a)
{
    Console.WriteLine($"{s}");
}


Console.WriteLine($"\n2 Opérateurs « Where » et « OrderBy »\n");

// Exercice 2.1 Pour chaque étudiant né avant 1955, donner le nom, le résultat annuel et le statut.
// Le statut prend la valeur « OK » si l’étudiant à obtenu au moins 12 comme résultat annuel
// et « KO » dans le cas contraire. 

Console.WriteLine($"\nExercice 2.1.\n");

var resultat2_1a = context.Students
    .Where(s => s.BirthDate.Year < 1955)
    .Select(s => new {
        LastName = s.Last_Name,
        YearResult = s.Year_Result,
        Statut = s.Year_Result >= 12 ? "OK" : "KO"
    }); // Opérateur

var resultat2_1b = from s in context.Students
                   where s.BirthDate.Year < 1955
                   select new
                   {
                       LastName = s.Last_Name,
                       YearResult = s.Year_Result,
                       Statut = s.Year_Result >= 12 ? "OK" : "KO"
                   }; // Expression de requête

foreach (var s in resultat2_1a)
{
    Console.WriteLine($"{s.LastName} {s.YearResult} {s.Statut}");
}


