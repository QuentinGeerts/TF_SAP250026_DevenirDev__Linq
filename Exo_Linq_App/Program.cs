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
    .Select(s => new
    {
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
    .Select(s => new
    {
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

Console.Clear();

// 3 Opérateurs « Count », « Min », « Max », « Sum » et « Average »

Console.WriteLine($"\nPartie 2 - Opérateurs Count, Min, Max, Sum et Average\n");

// Exercice 3.1 Donner le résultat annuel moyen pour l’ensemble des étudiants.

Console.WriteLine($"\nExercice 3.1\n");

var moyenne = context.Students.Average(s => s.Year_Result);

Console.WriteLine($"Moyenne de l'ensemble des étudiants: {moyenne}");


// Exercice 3.2 Donner le plus haut résultat annuel obtenu par un étudiant.

Console.WriteLine($"\nExercice 3.2\n");

var meilleureNote = context.Students.Max(s => s.Year_Result);
Console.WriteLine($"Résultat le plus élevé: {meilleureNote}");


// Exercice 3.3 Donner la somme des résultats annuels.

Console.WriteLine($"\nExercice 3.3\n");

var sommeNotes = context.Students.Sum(s => s.Year_Result);
Console.WriteLine($"Somme des notes annuelles des étudiants: {sommeNotes}");


// Exercice 3.4 Donner le résultat annuel le plus faible.

Console.WriteLine($"\nExercice 3.4\n");

var pireNote = context.Students.Min(s => s.Year_Result);
Console.WriteLine($"Réultat le plus bas: {pireNote}");


// Exercice 3.5 Donner le nombre de lignes qui composent la séquence « Students » ayant obtenu
// un résultat annuel impair.

Console.WriteLine($"\nExercice 3.5\n");

var nbResultatsImpairs = context.Students
    .Where(s => s.Year_Result % 2 != 0)
    .Count();

Console.WriteLine($"Nombre de notes impaires pour l'ensemble des étudiants: {nbResultatsImpairs}");

Console.Clear();

// 4 Opérateurs « GroupBy », « Join » et « GroupJoin »

Console.WriteLine($"\nPartie 3 - Opérateurs GroupBy, Join et GroupJoin\n");

// Exercice 4.1 Donner pour chaque section, le résultat maximum (« Max_Result ») obtenu par les
// étudiants.

Console.WriteLine($"\nExercice 4.1\n");
var resultatMaxParSection = context.Students
    .GroupBy(s => s.Section_ID)
    .Select(g => new { SectionId = g.Key, Max = g.Max(s => s.Year_Result) })
    .OrderBy(s => s.SectionId);

foreach (var section in resultatMaxParSection)
{
    Console.WriteLine($"Section: {section.SectionId}, maximum: {section.Max}");
}


// Exercice 4.2 Donner pour toutes les sections commençant par 10, le résultat annuel moyen
// (« AVGResult ») obtenu par les étudiants.

Console.WriteLine($"\nExercice 4.2\n");

var resultatMoyenParSection = context.Students
    .Where(st => st.Section_ID.ToString().StartsWith("10"))
    .GroupBy(st => st.Section_ID)
    .Select(g => new { SectionId = g.Key, AvgResult = g.Average(s => s.Year_Result) })
    .OrderBy(s => s.SectionId);

foreach (var section in resultatMoyenParSection)
{
    Console.WriteLine($"Section: {section.SectionId}, moyenne: {section.AvgResult}");
}


// Exercice 4.3 Donner le résultat moyen (« AVGResult ») et le mois en chiffre (« BirthMonth »)
// pour les étudiants né le même mois entre 1970 et 1985.

Console.WriteLine($"\nExercice 4.3\n");

var resultat4_3 = context.Students
    .Where(st => st.BirthDate.Year >= 1970 && st.BirthDate.Month <= 1985)
    .GroupBy(st => st.BirthDate.Month)
    .Select(g => new { BirthMonth = g.Key, AvgResult = g.Average(st => st.Year_Result) })
    .OrderBy(r => r.BirthMonth);

foreach (var r in resultat4_3)
{
    Console.WriteLine($"{r.BirthMonth}: {r.AvgResult}");
}

// Exercice 4.4 Donner pour toutes les sections qui compte plus de 3 étudiants, la moyenne des
// résultats annuels (« AVGResult »).

Console.WriteLine($"\nExercice 4.4\n");
var resultat4_4 = context.Students
    .GroupBy(st => st.Section_ID)
    .Where(g => g.Count() > 3)
    .Select(g => new { SectionId = g.Key, AvgResult = g.Average(st => st.Year_Result) })
    .OrderBy(g => g.SectionId);

foreach (var r in resultat4_4)
{
    Console.WriteLine($"{r.SectionId}: {r.AvgResult}");
}

// Exercice 4.5 Donner pour chaque cours, le nom du professeur responsable ainsi que la section
// dont le professeur fait partie.

Console.WriteLine($"\nExercice 4.5\n");

var resultat4_5 = context.Courses
    .Join(context.Professors, c => c.Professor_ID, p => p.Professor_ID, (c, p) => new { Course = c, Professor = p })
    .Join(context.Sections, cp => cp.Professor.Section_ID, s => s.Section_ID, (cp, s) => new { cp.Course.Course_Name, s.Section_Name, cp.Professor.Professor_Name });

foreach (var r in resultat4_5)
{
    Console.WriteLine($"{r.Course_Name} - {r.Section_Name} - {r.Professor_Name}");
}

// Exercice 4.6 Donner pour chaque section, l’id, le nom et le nom de son délégué. Classer les
// sections dans l’ordre inverse des id de section.

Console.WriteLine($"\nExercice 4.6\n");
var resultat4_6 = context.Sections
    .Join(context.Students, se => se.Delegate_ID, st => st.Student_ID, (se, st) => new { se.Section_ID, se.Section_Name, Delegate_Last_Name = st.Last_Name })
    .OrderByDescending(jointure => jointure.Section_ID);

foreach (var r in resultat4_6)
{
    Console.WriteLine($"{r.Section_ID} - {r.Section_Name} - {r.Delegate_Last_Name}");
}


// Exercice 4.7 Donner, pour toutes les sections, le nom des professeurs qui en sont membres
// Section_ID - Section_Name :
// -Professor_Name1
// - Professor_Name2
// - …

Console.WriteLine($"\nExercice 4.7\n");
var resultat4_7 = context.Sections
    .GroupJoin(context.Professors, se => se.Section_ID, p => p.Section_ID, (se, SubProfs) => new { se.Section_ID, se.Section_Name, Professors = SubProfs.Select(pr => pr.Professor_Name) });

foreach (var r in resultat4_7)
{
    Console.WriteLine($"{r.Section_ID} - {r.Section_Name} Professors : ");
    foreach (string p_name in r.Professors)
    {
        Console.WriteLine($"- {p_name}");
    }
}

// Exercice 4.8 Même objectif que la question 5.7, mais seules les sections comportant au moins
// un professeur doivent être reprises.

Console.WriteLine($"\nExercice 4.8\n");
var resultat4_8 = context.Sections
    .GroupJoin(context.Professors, se => se.Section_ID, p => p.Section_ID, (se, SubProfs) => new { se.Section_ID, se.Section_Name, Professors = SubProfs.Select(pr => pr.Professor_Name) })
    .Where(Jointure => Jointure.Professors.Count() > 0);

foreach (var r in resultat4_8)
{
    Console.WriteLine($"{r.Section_ID} - {r.Section_Name} Professors : ");
    foreach (string p_name in r.Professors)
    {
        Console.WriteLine($"- {p_name}");
    }
}


// Exercice 4.9 Donner à chaque étudiant ayant obtenu un résultat annuel supérieur ou égal à 12
// son grade en fonction de son résultat annuel et sur base de la table grade. La liste doit être
// classée dans l’ordre alphabétique des grades attribués.

Console.WriteLine($"\nExercice 4.9\n");
var resultat4_9 = context.Students.Join(context.Grades, st => true, gr => true, (st, grade) => new { Student = st, Grade = grade })
    .Where(join => join.Student.Year_Result >= 12 && join.Student.Year_Result >= join.Grade.Lower_Bound && join.Student.Year_Result <= join.Grade.Upper_Bound)
    .Select(join => new { join.Student.Last_Name, join.Student.Year_Result, Grade = join.Grade.GradeName })
    .OrderBy(elt => elt.Grade);

foreach (var r in resultat4_9)
{
    Console.WriteLine($"{r.Last_Name} - {r.Year_Result} - {r.Grade}");
}


// Exercice 4.10 Donner la liste des professeurs et la section à laquelle ils se rapportent ainsi que
// le(s) cour(s) (nom du cours et crédits) dont le professeur est responsable. La liste est triée
// par ordre décroissant des crédits attribués à un cours.

Console.WriteLine($"\nExercice 4.10\n");

var resultat4_10 = context.Professors.GroupJoin(context.Courses, p => p.Professor_ID, c => c.Professor_ID, (p, cs) => new { Professor = p, Courses = cs })
    .SelectMany(r => r.Courses.DefaultIfEmpty(), (pc, c) => new { Professor = pc.Professor, Course = c })
    .GroupJoin(context.Sections, p => p.Professor.Section_ID, s => s.Section_ID, (pc, s) => new { Professor = pc.Professor, Course = pc.Course, Sections = s })
    .SelectMany(r => r.Sections.DefaultIfEmpty(), (pcs, s) => new { Professor = pcs.Professor, Course = pcs.Course, Section = s })
    .Select(pcs => new
    {
        pcs.Professor.Professor_Name,
        Section_Name = (pcs.Section != null) ? pcs.Section.Section_Name : null,
        Course_Name = (pcs.Course != null) ? pcs.Course.Course_Name : null,
        Course_Ects = (pcs.Course != null) ? (float?)pcs.Course.Course_Ects : null
    })
    .OrderByDescending(r => r.Course_Ects)
    .Select(r => new
    {
        r.Professor_Name,
        Section_Name = (r.Section_Name != null) ? r.Section_Name : "NULL",
        Course_Name = (r.Course_Name != null) ? r.Course_Name : "NULL",
        Course_Ects = (r.Course_Ects != null) ? r.Course_Ects.ToString() : "NULL"
    });

foreach (var r in resultat4_10)
{
    Console.WriteLine($"{r.Professor_Name} - {r.Section_Name} - {r.Course_Name} - {r.Course_Ects}");
}


// Exercice 4.11 Donner pour chaque professeur son id et le total des crédits ECTS
// (« ECTSTOT ») qui lui sont attribués. La liste proposée est triée par ordre décroissant de la
// somme des crédits alloués.

Console.WriteLine($"\nExercice 4.11\n");

var resultat4_11 = context.Professors
    .GroupJoin(
        context.Courses,
        p => p.Professor_ID,
        c => c.Professor_ID,
        (p, SubCourses) => new {
            p.Professor_ID,
            ECTSTOT = (SubCourses.Count() > 0) ? (float?)SubCourses.Sum(co => co.Course_Ects) : null
        }
        )
    .OrderByDescending(r => r.ECTSTOT);

foreach (var item in resultat4_11)
{
    Console.WriteLine($"{item.Professor_ID} {item.ECTSTOT}");
}

