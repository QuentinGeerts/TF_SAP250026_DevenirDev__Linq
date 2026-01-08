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

// Exercice 2.2 Donner pour chaque étudiant entre 1955 et 1965 le nom, le résultat annuel et la
// catégorie à laquelle il appartient. La catégorie est fonction du résultat annuel obtenu ; un
// résultat inférieur à 10 appartient à la catégorie « inférieure », un résultat égal à 10 appartient
// à la catégorie « neutre », un résultat autre appartient à la catégorie « supérieure ».

Console.WriteLine($"\nExercice 2.2.\n");

var resultat2_2a = context.Students
    .Where(s => s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965)
    .Select(s => new
    {
        LastName = s.Last_Name,
        YearResult = s.Year_Result,
        Categorie = s.Year_Result < 10 ? "inférieure" : s.Year_Result == 10 ? "neutre" : "supérieure"
    });

var resultat2_2b = from s in context.Students
                   where s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965
                   select new
                   {
                       LastName = s.Last_Name,
                       YearResult = s.Year_Result,
                       Categorie = s.Year_Result < 10 ? "inférieure" : s.Year_Result == 10 ? "neutre" : "supérieure"
                   };

foreach (var s in resultat2_2a)
{
    Console.WriteLine($"{s.LastName} {s.YearResult} {s.Categorie}");
}

// Exercice 2.3 Ecrire une requête pour présenter le nom, l’id de section et de tous les étudiants
// qui ont un nom de famille qui termine par r.

Console.WriteLine($"\nExercice 2.3.\n");

var resultat2_3a = context.Students
    .Where(s => s.Last_Name.EndsWith("r"))
    .Select(s => new
    {
        LastName = s.Last_Name,
        SectionId = s.Section_ID
    });

foreach (var s in resultat2_3a)
{
    Console.WriteLine($"{s.LastName} {s.SectionId}");
}


// Exercice 2.4 Ecrire une requête pour présenter le nom et le résultat annuel classé par résultats
// annuels décroissant de tous les étudiants qui ont obtenu un résultat annuel inférieur ou égal
// à 3.

Console.WriteLine($"\nExercice 2.4.\n");

var resultat2_4 = context.Students
    .Where(s => s.Year_Result <= 3)
    .Select(s => new
    {
        LastName = s.Last_Name,
        YearResult = s.Year_Result
    })
    .OrderByDescending(s => s.YearResult);

foreach (var s in resultat2_4)
{
    Console.WriteLine($"{s.LastName} {s.YearResult}");
}


// Exercice 2.5 Ecrire une requête pour présenter le nom complet (nom et prénom séparés par un
// espace) et le résultat annuel classé par nom croissant sur le nom de tous les étudiants
// appartenant à la section 1110.

Console.WriteLine($"\nExercice 2.5.\n");

var resultat2_5 = context.Students
    .Where(s => s.Section_ID == 1110)
    .Select(s => new
    {
        NomComplet = $"{s.Last_Name} {s.First_Name}",
        YearResult = s.Year_Result
    })
    .OrderBy(s => s.NomComplet);

foreach (var s in resultat2_5)
{
    Console.WriteLine($"{s.NomComplet} {s.YearResult}");
}

// Exercice 2.6 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel
// classé par ordre croissant sur la section de tous les étudiants appartenant aux sections 1010
// et 1020 ayant un résultat annuel qui n’est pas compris entre 12 et 18.

Console.WriteLine($"\nExercice 2.6.\n");

var resultat2_6 = context.Students
    .Where(s => (s.Section_ID == 1010 || s.Section_ID == 1020) && !(s.Year_Result >= 12 && s.Year_Result <= 18))
    .Select(s => new
    {
        LastName = s.Last_Name,
        SectionId = s.Section_ID,
        YearResult = s.Year_Result
    })
    .OrderBy(s => s.SectionId)
    .ThenByDescending(s => s.YearResult);

foreach (var s in resultat2_6)
{
    Console.WriteLine($"{s.SectionId} {s.YearResult} {s.LastName}");
}

// Exercice 2.7 Ecrire une requête pour présenter le nom, l’id de section et le résultat annuel sur
// 100 (nommer la colonne ‘result_100’) classé par ordre décroissant du résultat de tous les
// étudiants appartenant aux sections commençant par 13 et ayant un résultat annuel sur 100
// inférieur ou égal à 60.

Console.WriteLine($"\nExercice 2.7.\n");

var resultat2_7 = context.Students
    .Where(s => (s.Section_ID.ToString().StartsWith("13")) && (s.Year_Result * 5 <= 60))
    .Select(s => new
    {
        LastName = s.Last_Name,
        SectionId = s.Section_ID,
        Resultat100 = s.Year_Result * 5
    })
    .OrderByDescending(s => s.Resultat100);

foreach (var s in resultat2_7)
{
    Console.WriteLine($"{s.LastName} {s.SectionId} {s.Resultat100}");
}


