namespace DemoVarAnonyme.Models;

public enum TypePersonnage
{
    Humain,
    Ane,
    Dragon,
    Grenouille,
    Ogre
}

public class Personnage
{
    public string Nom { get; set; }
    public TypePersonnage Type { get; set; }
    public string CouleurPeau { get; set; }
}

