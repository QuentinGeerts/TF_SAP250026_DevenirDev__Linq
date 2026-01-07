/*
 *  Démonstration 05 - Linq
 */

using DemoLinq.Models;

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

List<Animal> Animaux = [
    new Chien(),
    new Chien(),
    new Chat(),
    new Chien(),
    new Chat(),
    new Chat(),
    new Chat(),
];


// 1.  Opérateur Cast<T>()
// Permet de convertir une collection en une séquence de T.
// <!> Si un élément n'est pas convertissable, lève une exception

//object[] contactsArrayList = [.. Contacts.ToArray(), 12]; // <!> 12 n'est pas convertissable
object[] contactsArrayList = [.. Contacts.ToArray()];

IEnumerable<Contact> c1 = contactsArrayList.Cast<Contact>().ToList(); // Opérateur
IEnumerable<Contact> c2 = (from Contact c in contactsArrayList select c).ToList(); // Expression de requête

// .ToList() permet de rendre immédiat l'exécution (pour le test)


// 2.  Opérateur OfType<T>()
// Permet de filtrer une collection afin de n'avoir plus qu'une séquence de type T

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
//static IEnumerable<Contact> Where(this IEnumerable<Contact> items, Func<Contact, bool> predicate)
//{
//    foreach (var item in items)
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
// Permet de retourner un élément uniquement d'une séquence ou une valeur par défaut si aucun élément ne correspond à la condition
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

var contactsAvecDeuxTris = Contacts.OrderBy(c => c.AnneeDeNaissance).ThenBy(c => c.Nom);
var contactsAvecDeuxTris2 = from Contact c in Contacts 
                            orderby c.AnneeDeNaissance, c.Nom descending 
                            select c; // Expression de requête

foreach (var c in contactsAvecDeuxTris)
{
    Console.WriteLine($"2 tri: {c.AnneeDeNaissance} {c.Nom} {c.Prenom}");
}
