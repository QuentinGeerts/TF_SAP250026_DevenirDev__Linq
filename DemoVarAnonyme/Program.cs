/*
 *  Démonstration 01 - Var et types anonymes
 */


using DemoVarAnonyme.Models;

List<Personnage> personnagesShrek =
[
    new Personnage { Nom = "Shrek", Type = TypePersonnage.Ogre, CouleurPeau = "Vert" },
    new Personnage { Nom = "L'Âne", Type = TypePersonnage.Ane, CouleurPeau = "Gris" },
    new Personnage { Nom = "Fiona", Type = TypePersonnage.Ogre, CouleurPeau = "Vert" },
    new Personnage { Nom = "Lord Farquaad", Type = TypePersonnage.Humain, CouleurPeau = "Beige" },
];

// Parcourt de la liste et récupération des données modifiées
foreach (Personnage p in personnagesShrek)
{
    // Création d'un type anonyme
    var personnage = new { Nom = p.Nom, Race = p.Type };

    Console.WriteLine($"Personnage: {personnage.Nom} {personnage.Race}");
}