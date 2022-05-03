using System;
using System.Linq;
using Linq.Models;


namespace Linq
{
    class Program
    {
        private static FootballContext Context;
        static void Main(string[] args)
        {
            Context = new FootballContext();
            //voici quelques exemples d'interrogation de données :
 
            var resultat1 = ObtenirEquipesParConference(1);

            foreach (var equipe in resultat1)
                System.Console.WriteLine(equipe.Nom + " " + equipe.IdVille);

            //Exercice d'exécuter en lambda ObtenirEquipesParConference(1);

            var resultat2 = ObtenirNombreEquipesParEtat(2);
            //Exercie d'afficher le contenu (remarquer le join)
            //Exercice d'exécuter en lambda ObtenirNombreEquipesParEtat(2)

            //Exercice: obtenir en LINQ standard et afficher
            //var resultat3 = ObtenirListeEquipesCreeesAvant1950();

            //Exercice: obtenir en LINQ lambda et afficher
            //var resultat4 = ObtenirListeEquipesCreeesAvant1950();
        }

        // obtenir la liste des équipes dont l'identifiant de conférence correspond à celui reçu en paramètre
        static IQueryable<Equipe> ObtenirEquipesParConference(int idConference)
        {
             return from equipe in Context.Equipes
                    where equipe.IdConference == idConference
                    select equipe;

            //même chose en lambda :
            //return Context.Equipes.Where(e => e.IdConference == idConference);
 

        
        }

        //obtenir la liste des équipes dont la ville  est dans l'état passé en paramètre
        static int ObtenirNombreEquipesParEtat(int idEtat)
        {
            return (from equipe in Context.Equipes
                    where equipe.IdVilleNavigation.IdEtat == idEtat
                    select equipe).Count();

            //même chose en lambda :
            //return Context.Equipes
            //    .Count(e => e.IdVilleNavigation.IdEtat == idEtat);

        }
        // obtenir la liste des équipes dont l'année de fondation est inférieure à 1950
        // les résultats sont triés en ordre croissant de nom d'équipe

        
    }
}
