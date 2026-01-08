/*
 *  Démonstration 05 - Linq
 */

using DemoLinq.Models;
using System.Runtime.Intrinsics.Arm;

List<Contact> Contacts = new List<Contact>();
Contacts.AddRange(new Contact[] {
    new Contact(){ Nom = "Person", Prenom="Michael", Email="michael.person@cognitic.be", AnneeDeNaissance = 1982 },
    new Contact(){ Nom = "Person", Prenom="Michael", Email="michael.person2@cognitic.be", AnneeDeNaissance = 1982 },
    new Contact(){ Nom = "Morre", Prenom="Thierry", Email="thierry.morre@cognitic.be", AnneeDeNaissance = 1974 },
    new Contact(){ Nom = "Morre", Prenom="Thierry", Email="thierry.morre2@cognitic.be", AnneeDeNaissance = 1974 },
    new Contact(){ Nom = "Dupuis", Prenom="Thierry", Email="thierry.dupuis@cognitic.be", AnneeDeNaissance = 1988 },
    new Contact(){ Nom = "Faulkner", Prenom="Stéphane", Email="stephane.faulkner@cognitic.be", AnneeDeNaissance = 1969 },
    new Contact(){ Nom = "Selleck", Prenom = "Tom", Email = "tom.selleck@imdb.com", AnneeDeNaissance = 1945 },
    new Contact(){ Nom = "Anderson", Prenom = "Richard Dean", Email = "richard.dean.anderson@imdb.com", AnneeDeNaissance = 1950 },
    new Contact(){ Nom = "Bullock", Prenom = "Sandra", Email = "sandra.bullock@imdb.com", AnneeDeNaissance = 1964 },
    new Contact(){ Nom = "Peppard", Prenom = "George", Email = "peppard.george@ateam.com", AnneeDeNaissance = 1928 },
    new Contact(){ Nom = "Estevez", Prenom = "Emilio", Email = "emilio.estevez@breakfirstclub.com", AnneeDeNaissance = 1962 },
    new Contact(){ Nom = "Moore", Prenom = "Demi", Email = "demi.moore@imdb.com", AnneeDeNaissance = 1962 },
    new Contact(){ Nom = "Willis", Prenom = "Bruce", Email = "bruce.willis@diehard.com", AnneeDeNaissance = 1955 },
});

List<object> Animaux = [
    new Chien(),
    new Chien(),
    new Chat(),
    new Chien(),
    new Chat(),
    new Chat(),
    new Chat(),
    12
];


// 1.  Opérateur Cast<T>()
// Permet de convertir une collection en une séquence de T.
// <!> Si un élément n'est pas convertissable, lève une exception

//object[] contactsArrayList = [.. Contacts, 12]; // <!> 12 n'est pas convertissable
object[] contactsArrayList = [.. Contacts];

IEnumerable<Contact> c1 = contactsArrayList.Cast<Contact>().ToList(); // Opérateur
IEnumerable<Contact> c2 = (from Contact c in contactsArrayList select c).ToList(); // Expression de requête

// .ToList() permet de rendre immédiat l'exécution (pour le test)


// 2.  Opérateur OfType<T>()
// Permet de filtrer une collection afin de n'avoir plus qu'une séquence de type T

var animaux = Animaux.OfType<Animal>().ToList();
var chiens = Animaux.OfType<Chien>().ToList();
var chats = Animaux.OfType<Chat>();
var furets = Animaux.OfType<Furet>().ToList();
var contacts = Animaux.OfType<Contact>().ToList(); // Type incompatible → séquence vide (pas d'erreur)

// Il n'existe pas d'équivalent en expression de requête MAIS vous pouvez le simuler:
//var chiens2 = from Chien chien in Animaux.OfType<Chien>() select chien;

foreach (var chat in chats)
{
    Console.WriteLine($"chat: {chat.Nom}");
}


// 3.  Opérateur Where<TSource>(Func<TSource, bool>())
// Permet de filtrer une séquence sur base d'une condition

var contactNeApres1970 = Contacts.Where(c => c.AnneeDeNaissance > 1970); // Opérateur
var contactNeAvant1970 = from Contact c in Contacts 
                         where c.AnneeDeNaissance < 1970 
                         select c; // Expression de requête


// Exemple du code de Where (by Quentin)
//static IEnumerable<Contact> Where(this IEnumerable<Contact> source, Func<Contact, bool> predicate)
//{
//    foreach (var item in source)
//    {
//        if (predicate(item)) yield return item;
//    }
//}

foreach (var c in contactNeApres1970)
{
    Console.WriteLine($"contact: {c.Nom} {c.Prenom} {c.AnneeDeNaissance}");
}

foreach (var c in contactNeAvant1970)
{
    Console.WriteLine($"contact: {c.Nom} {c.Prenom} {c.AnneeDeNaissance}");
}

Console.Clear();

// 4.  Opérateur Select
// Permet de projeter chaque élément d'une séquence dans un nouveau type ou une nouvelle forme

var contactsModifies = Contacts.Select(c => new { NomComplet = $"{c.Nom} {c.Prenom}", Courriel = c.Email }); // Opérateur
var contactsModifies2 = from Contact c in Contacts
                        select new { NomComplet = $"{c.Nom} {c.Prenom}", Courriel = c.Email }; // Expression de requête

foreach (var c in contactsModifies)
{
    Console.WriteLine($"Contact: {c.NomComplet} {c.Courriel}");
}


// 5.  Opérateur Distinct
// Permet de supprimer les doublons d'une séquence

var contactsSansDoublon = Contacts.Select(c => new { c.Nom, Prenom = c.Prenom }).Distinct(); // Opérateur
var contactsSansDoublon2 = (from Contact c in Contacts 
                           select new { c.Nom, Prenom = c.Prenom }).Distinct(); // Expression de requête

foreach (var c in contactsSansDoublon)
{
    Console.WriteLine($"Contact sans doublon: {c.Nom} {c.Prenom}");
}

foreach (var c in contactsSansDoublon2)
{
    Console.WriteLine($"Contact sans doublon: {c.Nom} {c.Prenom}");
}


// 6.  Opérateur SingleOrDefault
// Permet de retourner un élément unique d'une séquence ou une valeur par défaut si aucun élément ne correspond à la condition
// <!> SingleOrDefault() peut déclencher une exception quand plus d'une valeur !!

Contact? thierryMorre = Contacts
    .Where(c => c.Nom == "Morre" && c.Prenom == "Thierry")
    .DistinctBy(c => new { c.Nom, c.Prenom }) // Solution pour SingleOrDefault dans le cas de doublons
    .SingleOrDefault(); // Opérateur

Contact? stephaneFaulkner = Contacts.SingleOrDefault(c => c.Nom == "Faulkner" && c.Prenom == "Stéphane"); // Opérateur


Contact? quentinGeerts = (from Contact c in Contacts
                         where c.Nom == "Geerts" && c.Prenom == "Quentin"
                         select c).SingleOrDefault() ?? new Contact { Nom = "Geerts", Prenom = "Quentin" }; // Expression de requête


// 7.  Opérateur FirstOrDefault
// Permet de retourner le premier élément d'une séquence ou une valeur par défaut si aucun élément ne correspond à la condition
// Ne déclenchera pas d'erreur dans le cas où plus d'une valeur

Contact? michaelPerson = Contacts
    .Where(c => c.Nom == "Person" && c.Prenom == "Michael")
    .FirstOrDefault();

Contact? michaelPerson2 = Contacts.FirstOrDefault(c => c.Nom == "Person" && c.Prenom == "Michael");


// 8.  Opérateur OrderBy | OrderByDescending
// Permet de trier les éléments d'une séquence en ordre alphabétique sur base d'un champ

var contactsTriesParAnnee = Contacts.OrderBy(c => c.AnneeDeNaissance); // Opérateur
var contactsTriesParAnnee2 = from Contact c in Contacts orderby c.AnneeDeNaissance select c; // Expression de requête

var contactsTriesParAnneeInv = Contacts.OrderByDescending(c => c.AnneeDeNaissance); // Opérateur
var contactsTriesParAnneeInv2 = from Contact c in Contacts orderby c.AnneeDeNaissance descending select c; // Expression de requête

foreach (var c in contactsTriesParAnnee)
{
    Console.WriteLine($"Contact trié: {c.Nom} {c.Prenom} {c.AnneeDeNaissance}");
}

foreach (var c in contactsTriesParAnneeInv)
{
    Console.WriteLine($"Contact trié: {c.Nom} {c.Prenom} {c.AnneeDeNaissance}");
}


// 9.  Opérateurs ThenBy | ThenByDescending
// Permet d'ajouter un tri secondaire sur les éléments d'une séquence

var contactsAvecDeuxTris = Contacts
    .OrderBy(c => c.AnneeDeNaissance)
    .ThenByDescending(c => c.Nom)
    .ThenBy(c => c.Prenom);

var contactsAvecDeuxTris2 = from Contact c in Contacts 
                            orderby c.AnneeDeNaissance, c.Nom descending 
                            select c; // Expression de requête

foreach (var c in contactsAvecDeuxTris)
{
    Console.WriteLine($"2 tri: {c.AnneeDeNaissance} {c.Nom} {c.Prenom}");
}


// 10.  Opérateurs Count | LongCount
// Permet de compter le nombre d'éléments dans une séquence (sur base d'une condition)

// Opérateurs
long countContacts = Contacts.LongCount();
int countContactsNeApres1970 = Contacts.Where(c => c.AnneeDeNaissance >= 1970).Count();
int countContactsNeApres1980 = Contacts.Count(c => c.AnneeDeNaissance >= 1980);

// Expression de requête
int countContactsNeApres1960 = (from c in Contacts 
                                where c.AnneeDeNaissance >= 1960 
                                select c).Count();

Console.WriteLine($"Nombre de contacts nés après 1970: {countContactsNeApres1970}");


// 11.  Opérateurs Min | Max
// Permet de récupérer la valeur (min / max) dans une séquence basé sur une clef

// Opérateurs
int anneeNaissancePlusAgee = Contacts.Min(c => c.AnneeDeNaissance);
int anneeNaissancePlusJeune = Contacts.Max(c => c.AnneeDeNaissance);

// Expression de requête
int anneeNaissancePlusAgee2 = (from c in Contacts select c.AnneeDeNaissance).Min();

Console.WriteLine($"Année de naissance de la personne la plus âgée: {anneeNaissancePlusAgee}");
Console.WriteLine($"Année de naissance de la personne la plus jeune: {anneeNaissancePlusJeune}");


// 12.  Opérateurs Sum | Average
// Permet de calculer la somme et la moyenne sur base d'une clef

int[] ints = [1, 2, 3, 4, 5];

// Opérateurs
int sommeInts = ints.Sum();
int sommeAnnee = Contacts.Sum(c => c.AnneeDeNaissance);

// Expression de requête
(from i in ints select i).Sum();
(from c in Contacts select c.AnneeDeNaissance).Sum();
(from c in Contacts select c).Sum(c => c.AnneeDeNaissance);

double moyenneInts = ints.Average();
double moyenneInts2 = (from i in ints select i).Average();


// 13.  Opérateur GroupBy
// Permet de grouper les éléments d'une séquence en fonction d'une clef

// Opérateur
IEnumerable<IGrouping<string, Contact>> contactsByDomaine = Contacts.GroupBy(c => c.Email.Split('@')[1]); // Opérateurs

// Pour changer les clefs retournées par l'objet, vous devez utiliser le Select
var contactsByDomaineModifie = Contacts
    .Select(c => new { NomComplet = c.Nom + " " + c.Prenom, Courriel = c.Email })
    .GroupBy(c => c.Courriel.Split('@')[1]);

// Expression de requête
var contactsByDomaine2 = from c in Contacts
                        group c by c.Email.Split('@')[1];

var contactsByDomaine2Modifie = from c in Contacts
                                select new { NomComplet = c.Nom + " " + c.Prenom, Courriel = c.Email } into contact
                                group contact by contact.Courriel.Split('@')[1];

foreach (IGrouping<string, Contact> group in contactsByDomaine)
{
    Console.WriteLine($"Domaine: {group.Key}");

    foreach (Contact c in group)
    {
        Console.WriteLine($" - {c.Nom} {c.Prenom} {c.Email} {c.AnneeDeNaissance}");
    }
}

foreach (var g in contactsByDomaineModifie)
{
    Console.WriteLine($"Domaine: {g.Key}");

    foreach (var c in g)
    {
        Console.WriteLine($" - {c.NomComplet} {c.Courriel}");
    }
}

foreach (var g in contactsByDomaine2Modifie)
{
    Console.WriteLine($"Domaine: {g.Key}");

    foreach (var c in g)
    {
        Console.WriteLine($" - {c.NomComplet} {c.Courriel}");
    }
}


// 14.  Opérateur Join
// Permet de joindre deux séquences sur base d'une clef commune

List<Rdv> RendezVous = new List<Rdv>();
RendezVous.AddRange(new Rdv[] {
    new Rdv(){ Email = "stephane.faulkner@cognitic.be", Date = new DateTime(2012,5,12)},
    new Rdv(){ Email = "peppard.george@ateam.com", Date = new DateTime(2011,8,14)},
    new Rdv(){ Email = "bruce.willis@diehard.com", Date = new DateTime(2012,6,19)},
    new Rdv(){ Email = "bruce.willis@diehard.com", Date = new DateTime(2012,6,20)},
    new Rdv(){ Email = "michael.person@cognitic.be", Date = new DateTime(2012,4,19)},
});

// Opérateur
var tousLesRendezVous = Contacts
    .Join(RendezVous, c => c.Email, r => r.Email, (c, r) => new { c.Email, c.Nom, c.Prenom, r.Date });

// Paramètres:
// [1] Séquence à joindre
// [2] Clef de la première séquence pour faire la liaison
// [3] Clef de la deuxième séquence pour faire la liaison
// [4] Le résultat qui sera retourné

foreach (var c in tousLesRendezVous)
{
    Console.WriteLine($"{c.Email} {c.Nom} {c.Prenom} {c.Date}");
}

// Expression de requête
var rdvs = from c in Contacts
           join r in RendezVous on c.Email equals r.Email
           select new { c.Email, c.Nom, c.Prenom, r.Date };

foreach (var r in rdvs)
{
    Console.WriteLine($"{r.Email} {r.Nom} {r.Prenom} {r.Date.ToShortDateString()}");
}


// 15.  Opérateur GroupJoin
// Permet de joindre deux séquences en créant un groupe pour chaque élément de la séquence externe

// Opérateur
var rdvsByContact = Contacts
    .GroupJoin(RendezVous, c => c.Email, r => r.Email, (c, r) => new { c.Email, c.Nom, c.Prenom, Rdv = r })
    .Where(c => c.Rdv.Count() > 0);


foreach (var c in rdvsByContact)
{
    Console.WriteLine($"Contact: {c.Email} {c.Nom} {c.Prenom}");

    foreach (Rdv r in c.Rdv)
    {
        Console.WriteLine($" - {r.Date}");
    }
}

// Expression de requête
var rdvsByContact2 = from c in Contacts
                     join r in RendezVous on c.Email equals r.Email into Rdv
                     select new { c.Email, c.Nom, c.Prenom, Rdv };

foreach (var c in rdvsByContact2)
{
    Console.WriteLine($"Contact: {c.Email} {c.Nom} {c.Prenom}");

    foreach (Rdv r in c.Rdv)
    {
        Console.WriteLine($" - {r.Date}");
    }
}

// 16.  Multiple clause from
// Permet de croiser deux séquences ensemble (équivalent CROSS JOIN)
// <!> uniquement possible avec les expressions de requête

var cross = from c in Contacts
            from r in RendezVous
            select new { c.Nom, c.Prenom, r.Date };

foreach (var c in cross)
{
    Console.WriteLine($"{c.Nom} {c.Prenom} {c.Date}");
}

