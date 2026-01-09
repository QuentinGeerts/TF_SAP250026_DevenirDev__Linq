# TF_SAP250026 - Devenir Développeur - LINQ

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LINQ](https://img.shields.io/badge/LINQ-Language%20Integrated%20Query-blueviolet)]()
[![License](https://img.shields.io/badge/License-Educational-blue.svg)]()

Ce repository contient l'ensemble des démonstrations et exercices du cours LINQ (Language Integrated Query). Chaque module explore les concepts avancés de C# nécessaires pour maîtriser LINQ et ses applications pratiques.

## 📚 Table des Matières

- [Structure du Projet](#-structure-du-projet)
- [Démonstrations](#-démonstrations)
- [Exercices Pratiques](#-exercices-pratiques)
- [Technologies](#-technologies-utilisées)
- [Installation](#-installation)
- [Guide d'Apprentissage](#-guide-dapprentissage)
- [Ressources](#-ressources-complémentaires)

---

## 📂 Structure du Projet

```
TF_SAP250026_DevenirDev__Linq/
├── 01 Var et Types Anonymes/
│   └── DemoVarAnonyme/
├── 02 Lambda/
│   └── DemoLambda/
├── 03 Méthodes extension/
│   └── DemoMethodesExtension/
├── 04 Immediat vs Différé/
│   └── DemoYield/
├── 05 Linq/
│   └── DemoLinq/
└── Exercices/
    ├── Exo_Linq_App/
    └── Exo_Linq_Context/
```

---

## 🎓 Démonstrations

### 01 - Var et Types Anonymes
**Projet:** `DemoVarAnonyme`

Introduction aux types anonymes et à l'inférence de type.

**Concepts abordés:**
- Mot-clé `var` (inférence de type)
- Types anonymes
- Utilisation avec LINQ
- Projection de données

```csharp
List<Personnage> personnagesShrek =
[
    new Personnage { Nom = "Shrek", Type = TypePersonnage.Ogre, CouleurPeau = "Vert" },
    new Personnage { Nom = "L'Âne", Type = TypePersonnage.Ane, CouleurPeau = "Gris" },
    new Personnage { Nom = "Fiona", Type = TypePersonnage.Ogre, CouleurPeau = "Vert" },
    new Personnage { Nom = "Lord Farquaad", Type = TypePersonnage.Humain, CouleurPeau = "Beige" },
];

// Parcours avec création de type anonyme
foreach (Personnage p in personnagesShrek)
{
    // Type anonyme avec projection
    var personnage = new { p.Nom, Race = p.Type };
    
    Console.WriteLine($"Personnage: {personnage.Nom} {personnage.Race}");
}
```

**Caractéristiques des types anonymes:**
- Créés avec `new { Property1 = value1, Property2 = value2 }`
- Type déterminé par le compilateur
- Propriétés en lecture seule
- Égalité basée sur les valeurs
- Portée limitée à la méthode
- Très utiles avec LINQ pour les projections

```csharp
// Inférence de type avec var
var nombre = 42;              // int
var texte = "Hello";          // string
var liste = new List<int>();  // List<int>

// Type anonyme
var personne = new { Nom = "John", Age = 30 };
// Type: <>f__AnonymousType0<string, int>

Console.WriteLine($"{personne.Nom} a {personne.Age} ans");
```

---

### 02 - Expressions Lambda
**Projet:** `DemoLambda`

Fonctions anonymes et délégués génériques.

**Concepts abordés:**
- Évolution : Fonction nommée → Fonction anonyme → Expression lambda
- Délégués personnalisés
- Délégués génériques : `Action<T>`, `Func<T>`, `Predicate<T>`
- Paramètres délégués dans les méthodes

#### Évolution des Syntaxes

```csharp
// 1. Délégué personnalisé
public delegate double Operation(double a, double b);

// 2. Fonction nommée
Operation op1 = Addition;
double Addition(double a, double b)
{
    return a + b;
}

// 3. Fonction anonyme
Operation op2 = delegate(double a, double b) 
{ 
    return a - b; 
};

// 4. Expression lambda (forme complète)
Operation op3 = (double a, double b) => 
{ 
    return a * b; 
};

// 5. Expression lambda (forme courte) ✅
Operation op4 = (a, b) => a / b;

// Utilisation
Console.WriteLine($"{5} + {2} = {op1(5, 2)}");  // 7
Console.WriteLine($"{5} - {2} = {op2(5, 2)}");  // 3
Console.WriteLine($"{5} * {2} = {op3(5, 2)}");  // 10
Console.WriteLine($"{5} / {2} = {op4(5, 2)}");  // 2.5
```

#### Délégués Génériques

**Action<T> - Aucun retour (void)**

```csharp
// Sans paramètre
Action act1 = () => Console.WriteLine();

// Un paramètre
Action<string> act2 = message => Console.WriteLine(message);

// Plusieurs paramètres
Action<string, string> act3 = (exp, dest) => 
    Console.WriteLine($"{exp} dit bonjour à {dest}");

// Utilisation
act1();
act2("Il est bientôt l'heure de partir en pause.");
act3("Steve", "Ibrahima");
```

**Func<T> - Avec retour**

```csharp
// Retourne une string
Func<string> func1 = () => "Hello";

// Paramètre + retour
Func<string, string> func2 = message => message.ToUpper();

// Inférence de type
var func3 = () => $"Hello";  // Func<string>

// Utilisation
Console.WriteLine(func1());      // Hello
Console.WriteLine(func2("Coucou"));  // COUCOU
Console.WriteLine(func3());      // Hello
```

**Predicate<T> - Retourne bool**

```csharp
// Un paramètre, retourne bool
Predicate<int> predicate1 = a => a > 5;

// Pour plusieurs paramètres, utiliser Func<T1, T2, bool>
Func<int, int, bool> predicate2 = (a, b) => a > b;

// Utilisation
Console.WriteLine(predicate1(10));  // True
Console.WriteLine(predicate2(5, 3));  // True
```

#### Paramètres Délégués

```csharp
// Méthode acceptant un délégué en paramètre
string[] Filtrer(string[] array, Predicate<string> compareFn)
{
    List<string> filteredValues = new();
    
    foreach (string value in array)
    {
        if (compareFn(value)) 
            filteredValues.Add(value);
    }
    
    return filteredValues.ToArray();
}

// Utilisation
var salutations = new[] { "Hello", "Bonjour", "Hi", "Konichiwa" };
var salutationsAvecI = Filtrer(salutations, str => str.Contains("i"));

Console.WriteLine($"Résultat: {string.Join(", ", salutationsAvecI)}");
// Résultat: Bonjour, Hi, Konichiwa
```

---

### 03 - Méthodes d'Extension
**Projet:** `DemoMethodesExtension`

Ajouter des méthodes à des types existants sans modifier leur code source.

**Concepts abordés:**
- Déclaration de méthodes d'extension
- Mot-clé `this` sur le premier paramètre
- Classes statiques
- Extension de types primitifs
- Documentation XML

```csharp
/// <summary>
/// Méthodes d'extension pour les entiers (int)
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// Détermine si un nombre est premier
    /// </summary>
    public static bool IsPrime(this int number)
    {
        if (number < 2) return false;
        
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Détermine si un nombre est pair
    /// </summary>
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }
    
    /// <summary>
    /// Détermine si un nombre est impair
    /// </summary>
    public static bool IsOdd(this int number)
    {
        return number % 2 != 0;
    }
    
    /// <summary>
    /// Calcule la factorielle (fonction récursive)
    /// </summary>
    public static int Factorial(this int number)
    {
        if (number < 0) 
            throw new ArgumentException("La valeur doit être positive.");
        
        return number == 0 ? 1 : number * Factorial(number - 1);
    }
}
```

**Utilisation:**

```csharp
// Utilisation comme méthode de l'objet
for (int i = 0; i < 101; i++)
{
    if (i.IsPrime())
    {
        Console.WriteLine($"{i}");
    }
}

Console.WriteLine($"5 est pair ? {5.IsEven()}");      // False
Console.WriteLine($"5 est impair ? {5.IsOdd()}");     // True
Console.WriteLine($"5! = {5.Factorial()}");           // 120

// Exemples supplémentaires
Console.WriteLine($"42 est pair ? {42.IsEven()}");    // True
Console.WriteLine($"17 est premier ? {17.IsPrime()}"); // True
Console.WriteLine($"0! = {0.Factorial()}");           // 1
```

**Règles importantes:**
- ✅ La classe doit être `static`
- ✅ La méthode doit être `static`
- ✅ Premier paramètre avec `this`
- ✅ Accessible via l'objet comme méthode native
- ⚠️ Ne peut pas accéder aux membres privés
- 💡 LINQ est entièrement basé sur des méthodes d'extension

---

### 04 - Exécution Immédiate vs Différée (Yield)
**Projet:** `DemoYield`

Comprendre la différence entre exécution immédiate et différée.

**Concepts abordés:**
- Exécution immédiate (retourne une valeur)
- Exécution différée (retourne une séquence)
- Mot-clé `yield return`
- Mot-clé `yield break`
- Performance et optimisation
- Conversion explicite avec `ToList()`, `ToArray()`

#### Exécution Immédiate

```csharp
int[] numbers = [1, 2, 3, 4, 5];

// Count() retourne une valeur → Exécution immédiate
int count = numbers.Count();
Console.WriteLine($"Count: {count}");  // 5
```

#### Exécution Différée

```csharp
int[] numbers = [1, 2, 3, 4, 5];

// Where() retourne IEnumerable → Exécution différée
var evenNumbers = numbers.Where(n => n % 2 == 0);

// La requête n'est PAS encore exécutée
numbers[2] = 8;  // Modification des données

// L'exécution se fait lors du parcours
foreach (var n in evenNumbers)
{
    Console.WriteLine($"n: {n}");
}
// Résultat: 2, 8, 4 (avec la modification)
```

#### Mot-clé Yield

**Méthode traditionnelle (immédiate):**

```csharp
List<int> GetNumbers()
{
    List<int> ints = new List<int>();
    
    for (int i = 0; i < 10; i++)
    {
        ints.Add(i);
    }
    
    return ints;  // Retourne toute la liste d'un coup
}

var entiers = GetNumbers();  // Exécution immédiate

foreach (var item in entiers)
{
    Console.WriteLine($"item: {item}");
}
```

**Méthode avec yield (différée):**

```csharp
IEnumerable<int> GetNumbersYield()
{
    for (int i = 0; i < 10; i++)
    {
        yield return i;  // Retourne un élément et suspend l'exécution
    }
}

var entiers2 = GetNumbersYield();  // Pas d'exécution

foreach (var item in entiers2)
{
    Console.WriteLine($"item: {item}");
    // L'exécution reprend à chaque itération
}
```

**Conversion différé → immédiat:**

```csharp
// Forcer l'exécution immédiate
var entiers3 = GetNumbersYield().ToList();  // Exécution immédiate
var entiers4 = GetNumbersYield().ToArray(); // Exécution immédiate

foreach (var item in entiers3)
{
    Console.WriteLine($"item: {item}");
}
```

#### Avantages de Yield

✅ **Performance** - Ne crée pas de collection intermédiaire  
✅ **Mémoire** - Traite un élément à la fois  
✅ **Lazy Loading** - Calcul à la demande  
✅ **Pipeline** - Enchaînement d'opérations efficace  

**Exemple pratique:**

```csharp
IEnumerable<int> GetPrimeNumbers(int max)
{
    for (int i = 2; i <= max; i++)
    {
        if (IsPrime(i))
            yield return i;
    }
}

// Seuls les 5 premiers nombres premiers sont calculés
var first5Primes = GetPrimeNumbers(1000000).Take(5);
// Pas de calcul de tous les nombres premiers jusqu'à 1M !
```

---

### 05 - LINQ (Language Integrated Query)
**Projet:** `DemoLinq`

Requêtes intégrées au langage pour manipuler des collections.

**Concepts abordés:**
- 16 opérateurs LINQ essentiels
- Syntaxe de méthode (fluent)
- Syntaxe de requête (query)
- Opérations de filtrage, projection, tri, agrégation, jointure

#### 1. Cast<T>()

Convertir une collection non générique en séquence typée.

```csharp
// Collection non typée
object[] contactsArrayList = [.. Contacts];

// Opérateur
IEnumerable<Contact> c1 = contactsArrayList.Cast<Contact>();

// Expression de requête
IEnumerable<Contact> c2 = from Contact c in contactsArrayList 
                          select c;

// ⚠️ Lance InvalidCastException si un élément n'est pas convertible
```

#### 2. OfType<T>()

Filtrer une collection pour ne garder qu'un type spécifique.

```csharp
List<object> Animaux = 
[
    new Chien(), new Chien(), new Chat(),
    new Chien(), new Chat(), new Chat(),
    new Chat(), 12  // Type incompatible
];

// Opérateur
var chiens = Animaux.OfType<Chien>().ToList();
var chats = Animaux.OfType<Chat>();
var furets = Animaux.OfType<Furet>().ToList();  // Vide (pas d'erreur)

foreach (var chat in chats)
{
    Console.WriteLine($"Chat: {chat.Nom}");
}

// ✅ Pas d'exception si le type est incompatible (collection vide)
```

#### 3. Where()

Filtrer une séquence selon une condition.

```csharp
// Opérateur
var contactNeApres1970 = Contacts
    .Where(c => c.AnneeDeNaissance > 1970);

// Expression de requête
var contactNeAvant1970 = from Contact c in Contacts 
                         where c.AnneeDeNaissance < 1970 
                         select c;

// Conditions multiples
var adultes = Contacts
    .Where(c => c.AnneeDeNaissance > 1970 && c.AnneeDeNaissance < 2000);
```

#### 4. Select()

Projeter chaque élément dans un nouveau type ou forme.

```csharp
// Opérateur - Type anonyme
var contactsModifies = Contacts
    .Select(c => new 
    { 
        NomComplet = $"{c.Nom} {c.Prenom}", 
        Courriel = c.Email 
    });

// Expression de requête
var contactsModifies2 = from Contact c in Contacts
                        select new 
                        { 
                            NomComplet = $"{c.Nom} {c.Prenom}", 
                            Courriel = c.Email 
                        };

foreach (var c in contactsModifies)
{
    Console.WriteLine($"Contact: {c.NomComplet} - {c.Courriel}");
}
```

#### 5. Distinct() / DistinctBy()

Supprimer les doublons d'une séquence.

```csharp
// Opérateur
var contactsSansDoublon = Contacts
    .Select(c => new { c.Nom, c.Prenom })
    .Distinct();

// Avec DistinctBy (C# 10+)
var contactsSansDoublon2 = Contacts
    .DistinctBy(c => new { c.Nom, c.Prenom });

// Expression de requête
var contactsSansDoublon3 = (from Contact c in Contacts 
                            select new { c.Nom, c.Prenom })
                           .Distinct();
```

#### 6. SingleOrDefault() / FirstOrDefault()

Obtenir un élément unique ou le premier élément.

```csharp
// SingleOrDefault - UN SEUL élément attendu
// ⚠️ Exception si plusieurs éléments
Contact? stephaneFaulkner = Contacts
    .SingleOrDefault(c => c.Nom == "Faulkner" && c.Prenom == "Stéphane");

// Avec doublons - utiliser DistinctBy
Contact? thierryMorre = Contacts
    .Where(c => c.Nom == "Morre" && c.Prenom == "Thierry")
    .DistinctBy(c => new { c.Nom, c.Prenom })
    .SingleOrDefault();

// FirstOrDefault - Premier élément (pas d'exception si plusieurs)
Contact? michaelPerson = Contacts
    .FirstOrDefault(c => c.Nom == "Person" && c.Prenom == "Michael");

// Valeur par défaut si non trouvé
Contact? quentinGeerts = Contacts
    .SingleOrDefault(c => c.Nom == "Geerts") 
    ?? new Contact { Nom = "Geerts", Prenom = "Quentin" };
```

#### 7. OrderBy() / OrderByDescending()

Trier les éléments d'une séquence.

```csharp
// Opérateur - Croissant
var contactsTriesParAnnee = Contacts
    .OrderBy(c => c.AnneeDeNaissance);

// Expression de requête
var contactsTriesParAnnee2 = from Contact c in Contacts 
                             orderby c.AnneeDeNaissance 
                             select c;

// Décroissant
var contactsTriesParAnneeDesc = Contacts
    .OrderByDescending(c => c.AnneeDeNaissance);

var contactsTriesParAnneeDesc2 = from Contact c in Contacts 
                                 orderby c.AnneeDeNaissance descending 
                                 select c;
```

#### 8. ThenBy() / ThenByDescending()

Ajouter un tri secondaire.

```csharp
// Opérateur - Tri multiple
var contactsAvecDeuxTris = Contacts
    .OrderBy(c => c.AnneeDeNaissance)        // Tri primaire
    .ThenByDescending(c => c.Nom)            // Tri secondaire
    .ThenBy(c => c.Prenom);                  // Tri tertiaire

// Expression de requête
var contactsAvecDeuxTris2 = from Contact c in Contacts 
                            orderby c.AnneeDeNaissance, 
                                    c.Nom descending, 
                                    c.Prenom
                            select c;

foreach (var c in contactsAvecDeuxTris)
{
    Console.WriteLine($"{c.AnneeDeNaissance} - {c.Nom} {c.Prenom}");
}
```

#### 9. Count() / LongCount()

Compter les éléments d'une séquence.

```csharp
// Opérateur
long countContacts = Contacts.LongCount();
int countContactsNeApres1970 = Contacts
    .Where(c => c.AnneeDeNaissance >= 1970)
    .Count();

// Avec condition directe
int countContactsNeApres1980 = Contacts
    .Count(c => c.AnneeDeNaissance >= 1980);

// Expression de requête
int countContactsNeApres1960 = (from c in Contacts 
                                where c.AnneeDeNaissance >= 1960 
                                select c).Count();

Console.WriteLine($"Contacts nés après 1970: {countContactsNeApres1970}");
```

#### 10. Min() / Max()

Obtenir la valeur minimale ou maximale.

```csharp
// Opérateurs
int anneeNaissancePlusAgee = Contacts
    .Min(c => c.AnneeDeNaissance);

int anneeNaissancePlusJeune = Contacts
    .Max(c => c.AnneeDeNaissance);

// Expression de requête
int anneeNaissancePlusAgee2 = (from c in Contacts 
                               select c.AnneeDeNaissance).Min();

Console.WriteLine($"Personne la plus âgée: {anneeNaissancePlusAgee}");
Console.WriteLine($"Personne la plus jeune: {anneeNaissancePlusJeune}");
```

#### 11. Sum() / Average()

Calculer la somme et la moyenne.

```csharp
int[] ints = [1, 2, 3, 4, 5];

// Opérateurs
int sommeInts = ints.Sum();                        // 15
int sommeAnnee = Contacts.Sum(c => c.AnneeDeNaissance);

double moyenneInts = ints.Average();               // 3
double moyenneAnnee = Contacts.Average(c => c.AnneeDeNaissance);

// Expression de requête
int somme = (from i in ints select i).Sum();
double moyenne = (from c in Contacts 
                  select c.AnneeDeNaissance).Average();
```

#### 12. GroupBy()

Regrouper les éléments par clé.

```csharp
// Opérateur
IEnumerable<IGrouping<string, Contact>> contactsByDomaine = Contacts
    .GroupBy(c => c.Email.Split('@')[1]);

// Avec modification des propriétés
var contactsByDomaineModifie = Contacts
    .Select(c => new 
    { 
        NomComplet = c.Nom + " " + c.Prenom, 
        Courriel = c.Email 
    })
    .GroupBy(c => c.Courriel.Split('@')[1]);

// Expression de requête
var contactsByDomaine2 = from c in Contacts
                         group c by c.Email.Split('@')[1];

// Avec projection
var contactsByDomaine3 = from c in Contacts
                         select new 
                         { 
                             NomComplet = c.Nom + " " + c.Prenom, 
                             Courriel = c.Email 
                         } into contact
                         group contact by contact.Courriel.Split('@')[1];

// Parcours
foreach (IGrouping<string, Contact> group in contactsByDomaine)
{
    Console.WriteLine($"Domaine: {group.Key}");
    
    foreach (Contact c in group)
    {
        Console.WriteLine($" - {c.Nom} {c.Prenom} ({c.Email})");
    }
}
```

#### 13. Join()

Joindre deux séquences (INNER JOIN).

```csharp
List<Contact> Contacts = [...];
List<Rdv> RendezVous = 
[
    new Rdv { Email = "stephane.faulkner@cognitic.be", Date = new DateTime(2012,5,12) },
    new Rdv { Email = "bruce.willis@diehard.com", Date = new DateTime(2012,6,19) },
    // ...
];

// Opérateur
var tousLesRendezVous = Contacts.Join(
    RendezVous,                    // Séquence à joindre
    c => c.Email,                  // Clé de la première séquence
    r => r.Email,                  // Clé de la deuxième séquence
    (c, r) => new                  // Résultat
    { 
        c.Email, 
        c.Nom, 
        c.Prenom, 
        r.Date 
    }
);

// Expression de requête
var rdvs = from c in Contacts
           join r in RendezVous on c.Email equals r.Email
           select new { c.Email, c.Nom, c.Prenom, r.Date };

foreach (var r in rdvs)
{
    Console.WriteLine($"{r.Email} - {r.Nom} {r.Prenom} - {r.Date:dd/MM/yyyy}");
}
```

#### 14. GroupJoin()

Joindre deux séquences en créant des groupes (LEFT JOIN).

```csharp
// Opérateur
var rdvsByContact = Contacts.GroupJoin(
    RendezVous,
    c => c.Email,
    r => r.Email,
    (c, r) => new 
    { 
        c.Email, 
        c.Nom, 
        c.Prenom, 
        Rdv = r  // Sous-collection
    }
).Where(c => c.Rdv.Count() > 0);  // Filtrer ceux qui ont des RDV

foreach (var c in rdvsByContact)
{
    Console.WriteLine($"Contact: {c.Email} - {c.Nom} {c.Prenom}");
    
    foreach (Rdv r in c.Rdv)
    {
        Console.WriteLine($" - {r.Date:dd/MM/yyyy}");
    }
}

// Expression de requête
var rdvsByContact2 = from c in Contacts
                     join r in RendezVous on c.Email equals r.Email into Rdv
                     select new { c.Email, c.Nom, c.Prenom, Rdv };
```

#### 15. Multiple FROM (CROSS JOIN)

Produit cartésien de deux séquences.

```csharp
// Expression de requête uniquement
var cross = from c in Contacts
            from r in RendezVous
            select new { c.Nom, c.Prenom, r.Date };

foreach (var c in cross)
{
    Console.WriteLine($"{c.Nom} {c.Prenom} - {c.Date}");
}

// Équivalent en SQL: SELECT * FROM Contacts CROSS JOIN RendezVous
```

#### 16. Enchaînement d'Opérateurs

```csharp
// Requête complexe
var resultat = Contacts
    .Where(c => c.AnneeDeNaissance >= 1970)
    .OrderBy(c => c.Nom)
    .ThenBy(c => c.Prenom)
    .Select(c => new 
    { 
        NomComplet = $"{c.Nom} {c.Prenom}",
        Age = DateTime.Now.Year - c.AnneeDeNaissance,
        c.Email
    })
    .Take(10);  // Les 10 premiers

foreach (var r in resultat)
{
    Console.WriteLine($"{r.NomComplet} ({r.Age} ans) - {r.Email}");
}
```

---

## 🏋️ Exercices Pratiques

### Exo_Linq_App - Exercices Complets

Série d'exercices progressifs utilisant un contexte académique (étudiants, sections, professeurs, cours).

#### Structure du Contexte

**Classes disponibles:**
- `Student` - Étudiants
- `Section` - Sections académiques
- `Professor` - Professeurs
- `Course` - Cours
- `Grade` - Notes/Grades

#### Partie 1 - Opérateur Select

**Exercice 1.1** - Présenter pour chaque étudiant : nom, date de naissance, login, résultat annuel

```csharp
// Opérateur
var resultat1_1a = context.Students.Select(s => new
{
    LastName = s.Last_Name,
    s.BirthDate,
    s.Login,
    YearResult = s.Year_Result
});

// Expression de requête
var resultat1_1b = from Student s in context.Students
                   select new
                   {
                       LastName = s.Last_Name,
                       s.BirthDate,
                       s.Login,
                       YearResult = s.Year_Result
                   };
```

**Exercice 1.2** - Nom complet, ID et date de naissance

```csharp
var resultat1_2 = context.Students
    .Select(s => new
    {
        NomComplet = $"{s.Last_Name} {s.First_Name}",
        Id = s.Student_ID,
        s.BirthDate
    });
```

**Exercice 1.3** - Toutes les données dans une chaîne séparée par `|`

```csharp
var resultat1_3 = context.Students
    .Select(s => $"{s.Student_ID}|{s.Last_Name}|{s.First_Name}|{s.BirthDate}|{s.Login}|{s.Year_Result}|{s.Course_ID}|{s.Section_ID}");

// Alternative
var resultat1_3b = from s in context.Students
                   select string.Join("|", s.Student_ID, s.Last_Name, 
                                      s.First_Name, s.BirthDate, s.Login, 
                                      s.Year_Result, s.Course_ID, s.Section_ID);
```

#### Partie 2 - Opérateurs Where et OrderBy

**Exercice 2.1** - Étudiants nés avant 1955 avec statut OK/KO

```csharp
var resultat2_1 = context.Students
    .Where(s => s.BirthDate.Year < 1955)
    .Select(s => new
    {
        LastName = s.Last_Name,
        YearResult = s.Year_Result,
        Statut = s.Year_Result >= 12 ? "OK" : "KO"
    });
```

**Exercice 2.2** - Catégories selon le résultat

```csharp
var resultat2_2 = context.Students
    .Where(s => s.BirthDate.Year >= 1955 && s.BirthDate.Year <= 1965)
    .Select(s => new
    {
        LastName = s.Last_Name,
        YearResult = s.Year_Result,
        Categorie = s.Year_Result < 10 ? "inférieure" 
                  : s.Year_Result == 10 ? "neutre" 
                  : "supérieure"
    });
```

**Exercice 2.4** - Résultats ≤ 3, triés par résultat décroissant

```csharp
var resultat2_4 = context.Students
    .Where(s => s.Year_Result <= 3)
    .Select(s => new { LastName = s.Last_Name, YearResult = s.Year_Result })
    .OrderByDescending(s => s.YearResult);
```

**Exercice 2.6** - Conditions multiples avec tri

```csharp
var resultat2_6 = context.Students
    .Where(s => (s.Section_ID == 1010 || s.Section_ID == 1020) 
             && !(s.Year_Result >= 12 && s.Year_Result <= 18))
    .Select(s => new
    {
        LastName = s.Last_Name,
        SectionId = s.Section_ID,
        YearResult = s.Year_Result
    })
    .OrderBy(s => s.SectionId)
    .ThenByDescending(s => s.YearResult);
```

#### Partie 3 - Opérateurs d'Agrégation

**Exercice 3.1** - Résultat annuel moyen

```csharp
var moyenne = context.Students.Average(s => s.Year_Result);
Console.WriteLine($"Moyenne: {moyenne}");
```

**Exercice 3.2 à 3.4** - Min, Max, Sum

```csharp
var meilleureNote = context.Students.Max(s => s.Year_Result);
var pireNote = context.Students.Min(s => s.Year_Result);
var sommeNotes = context.Students.Sum(s => s.Year_Result);
```

**Exercice 3.5** - Compter avec condition

```csharp
var nbResultatsImpairs = context.Students
    .Where(s => s.Year_Result % 2 != 0)
    .Count();

// Ou directement
var nbResultatsImpairs2 = context.Students
    .Count(s => s.Year_Result % 2 != 0);
```

#### Partie 4 - GroupBy, Join et GroupJoin

**Exercice 4.1** - Résultat maximum par section

```csharp
var resultatMaxParSection = context.Students
    .GroupBy(s => s.Section_ID)
    .Select(g => new 
    { 
        SectionId = g.Key, 
        Max = g.Max(s => s.Year_Result) 
    })
    .OrderBy(s => s.SectionId);

foreach (var section in resultatMaxParSection)
{
    Console.WriteLine($"Section {section.SectionId}: max = {section.Max}");
}
```

**Exercice 4.4** - Sections avec plus de 3 étudiants

```csharp
var resultat4_4 = context.Students
    .GroupBy(st => st.Section_ID)
    .Where(g => g.Count() > 3)
    .Select(g => new 
    { 
        SectionId = g.Key, 
        AvgResult = g.Average(st => st.Year_Result) 
    })
    .OrderBy(g => g.SectionId);
```

**Exercice 4.5** - Join Cours-Professeurs-Sections

```csharp
var resultat4_5 = context.Courses
    .Join(context.Professors, 
          c => c.Professor_ID, 
          p => p.Professor_ID, 
          (c, p) => new { Course = c, Professor = p })
    .Join(context.Sections, 
          cp => cp.Professor.Section_ID, 
          s => s.Section_ID, 
          (cp, s) => new 
          { 
              cp.Course.Course_Name, 
              s.Section_Name, 
              cp.Professor.Professor_Name 
          });
```

**Exercice 4.7** - GroupJoin Sections-Professeurs

```csharp
var resultat4_7 = context.Sections
    .GroupJoin(context.Professors, 
               se => se.Section_ID, 
               p => p.Section_ID, 
               (se, SubProfs) => new 
               { 
                   se.Section_ID, 
                   se.Section_Name, 
                   Professors = SubProfs.Select(pr => pr.Professor_Name) 
               });

foreach (var r in resultat4_7)
{
    Console.WriteLine($"{r.Section_ID} - {r.Section_Name}:");
    foreach (string p_name in r.Professors)
    {
        Console.WriteLine($"  - {p_name}");
    }
}
```

**Exercice 4.9** - Join avec condition complexe (grades)

```csharp
var resultat4_9 = context.Students
    .Join(context.Grades, 
          st => true,  // Cross join
          gr => true, 
          (st, grade) => new { Student = st, Grade = grade })
    .Where(join => join.Student.Year_Result >= 12 
                && join.Student.Year_Result >= join.Grade.Lower_Bound 
                && join.Student.Year_Result <= join.Grade.Upper_Bound)
    .Select(join => new 
    { 
        join.Student.Last_Name, 
        join.Student.Year_Result, 
        Grade = join.Grade.GradeName 
    })
    .OrderBy(elt => elt.Grade);
```

---

## 🛠️ Technologies Utilisées

- **.NET 10.0** - Framework de développement
- **C# 12** - Langage de programmation
- **LINQ** - Language Integrated Query
- **Visual Studio 2025** - IDE recommandé

### Fonctionnalités C# Avancées

| Fonctionnalité | Version C# | Utilisation |
|---------------|-----------|-------------|
| Expression lambda | C# 3.0 | `x => x * 2` |
| Types anonymes | C# 3.0 | `new { Nom = "John", Age = 30 }` |
| Méthodes d'extension | C# 3.0 | `public static ... this Type obj` |
| Var (inférence) | C# 3.0 | `var x = 10;` |
| LINQ | C# 3.0 | `collection.Where(x => x > 5)` |
| Yield | C# 2.0 | `yield return value;` |
| Expression-bodied members | C# 6.0 | `public int Total => x + y;` |
| Délégués génériques | .NET 2.0+ | `Action<T>`, `Func<T>`, `Predicate<T>` |

---

## 📥 Installation

### Prérequis
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Un IDE : [Visual Studio 2025](https://visualstudio.microsoft.com/) ou [Rider](https://www.jetbrains.com/rider/)

### Cloner le Projet
```bash
git clone https://github.com/votre-username/TF_SAP250026_DevenirDev__Linq.git
cd TF_SAP250026_DevenirDev__Linq
```

### Ouvrir la Solution
```bash
# Avec Visual Studio
start TF_SAP250026_DevenirDev__Linq.slnx

# Avec VS Code
code .

# Avec CLI
dotnet build
```

### Exécuter un Projet
```bash
# Exemple: lancer DemoLinq
cd DemoLinq
dotnet run

# Ou avec le chemin complet
dotnet run --project DemoLinq/DemoLinq.csproj
```

---

## 📖 Guide d'Apprentissage

### Progression Recommandée

#### 🟢 Semaine 1 - Fondations
**Objectif:** Comprendre les bases avant LINQ

**Jour 1-2: Types anonymes et Var**
- ✅ Inférence de type avec `var`
- ✅ Création de types anonymes
- ✅ Propriétés en lecture seule
- 📝 **Exercice:** Transformer une liste d'objets en types anonymes

**Jour 3-4: Expressions Lambda**
- ✅ Évolution vers les lambdas
- ✅ Délégués personnalisés
- ✅ `Action<T>`, `Func<T>`, `Predicate<T>`
- 📝 **Exercice:** Créer des filtres personnalisés avec lambda

**Jour 5: Méthodes d'Extension**
- ✅ Syntaxe et règles
- ✅ Extension de types primitifs
- ✅ Comprendre que LINQ = méthodes d'extension
- 📝 **Exercice:** Créer des extensions pour strings

---

#### 🟡 Semaine 2 - LINQ Fondamental
**Objectif:** Maîtriser les opérateurs essentiels

**Jour 1: Exécution Immédiate vs Différée**
- ✅ Comprendre `yield return`
- ✅ Différence performance
- ✅ Conversion avec `ToList()` / `ToArray()`
- 📝 **Exercice:** Comparer les performances

**Jour 2-3: Opérateurs de Base**
- ✅ `Where()` - Filtrage
- ✅ `Select()` - Projection
- ✅ `OrderBy()` / `ThenBy()` - Tri
- ✅ `Distinct()` - Doublons
- 📝 **Exercices:** 1.1 à 2.4

**Jour 4-5: Opérateurs d'Agrégation**
- ✅ `Count()`, `Sum()`, `Average()`
- ✅ `Min()`, `Max()`
- ✅ `FirstOrDefault()`, `SingleOrDefault()`
- 📝 **Exercices:** 3.1 à 3.5

---

#### 🔵 Semaine 3 - LINQ Avancé
**Objectif:** Maîtriser groupement et jointures

**Jour 1-2: GroupBy**
- ✅ Regroupement par clé
- ✅ Opérations sur les groupes
- ✅ Agrégations dans les groupes
- 📝 **Exercices:** 4.1 à 4.4

**Jour 3-4: Join et GroupJoin**
- ✅ `Join()` - INNER JOIN
- ✅ `GroupJoin()` - LEFT JOIN
- ✅ Jointures multiples
- 📝 **Exercices:** 4.5 à 4.8

**Jour 5: Requêtes Complexes**
- ✅ Combinaison d'opérateurs
- ✅ Sous-requêtes
- ✅ Cross join
- 📝 **Exercices:** 4.9 à 4.11

---

### Exercices Progressifs

#### Niveau 1 - Débutant
```csharp
// 1. Filtrer les étudiants majeurs
var majeurs = students.Where(s => CalculateAge(s.BirthDate) >= 18);

// 2. Obtenir les noms complets
var noms = students.Select(s => $"{s.First_Name} {s.Last_Name}");

// 3. Trier par nom
var tries = students.OrderBy(s => s.Last_Name);
```

#### Niveau 2 - Intermédiaire
```csharp
// 1. Étudiants par section avec comptage
var parSection = students
    .GroupBy(s => s.Section_ID)
    .Select(g => new { Section = g.Key, Count = g.Count() });

// 2. Top 5 des meilleurs résultats
var top5 = students
    .OrderByDescending(s => s.Year_Result)
    .Take(5);

// 3. Moyenne par section
var moyennes = students
    .GroupBy(s => s.Section_ID)
    .Select(g => new 
    { 
        Section = g.Key, 
        Moyenne = g.Average(s => s.Year_Result) 
    });
```

#### Niveau 3 - Avancé
```csharp
// 1. Étudiants avec leurs cours et professeurs
var complete = students
    .Join(courses, s => s.Course_ID, c => c.Course_ID, (s, c) => new { s, c })
    .Join(professors, sc => sc.c.Professor_ID, p => p.Professor_ID, 
          (sc, p) => new 
          { 
              Student = sc.s.Last_Name,
              Course = sc.c.Course_Name,
              Professor = p.Professor_Name 
          });

// 2. Sections avec statistiques complètes
var stats = sections
    .GroupJoin(students, se => se.Section_ID, st => st.Section_ID, 
               (se, sts) => new
               {
                   se.Section_Name,
                   Count = sts.Count(),
                   Average = sts.Average(st => st.Year_Result),
                   Max = sts.Max(st => st.Year_Result),
                   Min = sts.Min(st => st.Year_Result)
               });
```

---

## 🎯 Objectifs Pédagogiques

À la fin de ce cours, vous serez capable de:

- ✅ Utiliser l'inférence de type et les types anonymes
- ✅ Écrire des expressions lambda efficaces
- ✅ Créer des méthodes d'extension
- ✅ Comprendre la différence entre exécution immédiate et différée
- ✅ Maîtriser tous les opérateurs LINQ essentiels
- ✅ Effectuer des requêtes complexes avec LINQ
- ✅ Optimiser les performances des requêtes
- ✅ Choisir entre syntaxe de méthode et syntaxe de requête
- ✅ Manipuler efficacement les collections de données

---

## 📚 Ressources Complémentaires

### Documentation Officielle
- [LINQ Overview](https://docs.microsoft.com/en-us/dotnet/csharp/linq/)
- [Standard Query Operators](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/standard-query-operators-overview)
- [Lambda Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions)
- [Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)

---

## 💡 Conseils et Bonnes Pratiques

### Performance

**❌ À éviter:**
```csharp
// Multiples énumérations
var count = collection.Count();
var items = collection.ToList();
var first = collection.First();
// Collection énumérée 3 fois !
```

**✅ Préférer:**
```csharp
// Une seule énumération
var list = collection.ToList();
var count = list.Count;
var first = list.First();
```

### Lisibilité

**❌ Difficile à lire:**
```csharp
var result = collection.Where(x => x.A > 5).Select(x => new { x.B, x.C }).OrderBy(x => x.B).ThenBy(x => x.C).Take(10);
```

**✅ Plus clair:**
```csharp
var result = collection
    .Where(x => x.A > 5)
    .Select(x => new { x.B, x.C })
    .OrderBy(x => x.B)
    .ThenBy(x => x.C)
    .Take(10);
```

### Syntaxe de Méthode vs Requête

**Syntaxe de méthode (recommandée):**
- ✅ Plus concise
- ✅ Plus puissante (tous les opérateurs disponibles)
- ✅ Plus flexible
- ✅ Meilleur IntelliSense

**Syntaxe de requête:**
- ✅ Plus lisible pour les requêtes SQL-like
- ✅ Meilleure pour les jointures complexes
- ⚠️ Certains opérateurs non disponibles

---

## 📝 Licence

Ce projet est sous licence **Educational** - voir le fichier [LICENSE](LICENSE) pour plus de détails.

---

## 👨‍🏫 Formateur

**Quentin Geerts**  
Formation: TF_SAP250026 - Devenir Développeur

---


<div align="center">

**⭐ Maîtrisez LINQ pour des requêtes de données puissantes ! ⭐**

Made with ❤️ for learning LINQ

</div>
