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

            System.Console.WriteLine("===================== en standard ci-dessous les équipes de la conférence 1");

            var resultat1 = ObtenirEquipesParConference(1);
            foreach (var equipe in resultat1)
                System.Console.WriteLine(equipe.Nom + " " + equipe.IdVille);
            System.Console.WriteLine("===================== en lambda ci-dessous les équipes de la conférence 1");

            //Exercice d'exécuter en lambda ObtenirEquipesParConference(1);
           var resultat2 = ObtenirEquipesParConferenceLambda(1);
            foreach (var equipe in resultat2)
                System.Console.WriteLine(equipe.Nom + " " + equipe.IdVille);

            System.Console.WriteLine("===================== en standard ci-dessous nombre d'équipe par état =2");
            var resultat3 = ObtenirNombreEquipesParEtat(2);
             System.Console.WriteLine(resultat3);

            System.Console.WriteLine("===================== en lambda ci-dessous nombre d'équipe par état =2");
            var resultat4 = ObtenirNombreEquipesParEtatLambda(2);
            System.Console.WriteLine(resultat4);

            System.Console.WriteLine("===================== en standard ci-dessous la liste des equipes crees avant 1950");
            var resultat5 = ObtenirListeEquipesCreeesAvant1950(1950);
            foreach (var equipe in resultat5) {
                System.Console.WriteLine(equipe.Nom + " a été créée en " + equipe.AnneeFondation);
            }
               

            System.Console.WriteLine("===================== en lambda ci-dessous la liste des equipes crees avant 1950");
            var resultat6 = ObtenirListeEquipesCreeesAvant1950Lambda(1950);
            foreach (var equipe in resultat6)
                System.Console.WriteLine(equipe.Nom + " a été créée en " + equipe.AnneeFondation);

        }

   

        // obtenir la liste des équipes dont l'identifiant de conférence correspond à celui reçu en paramètre
        static IQueryable<Equipe> ObtenirEquipesParConference(int idConference)
        {
             return from equipe in Context.Equipes
                    where equipe.IdConference == idConference
                    select equipe;
        }
        //même chose en lambda :
        private static IQueryable<Equipe> ObtenirEquipesParConferenceLambda(int IdConference)
        {
            return Context.Equipes.Where(e => e.IdConference == IdConference);
        }

        //obtenir la liste des équipes dont la ville  est dans l'état passé en paramètre
        static int ObtenirNombreEquipesParEtat(int idEtat)
        {
            return (from equipe in Context.Equipes
                    where equipe.IdVilleNavigation.IdEtat == idEtat
                    select equipe).Count();
            // les résultats sont triés en ordre croissant de nom d'équipe
        }
        static int ObtenirNombreEquipesParEtatLambda(int idEtat)
        {
            return Context.Equipes.Count(e => e.IdVilleNavigation.IdEtat == idEtat);
        }
        static IQueryable<Equipe> ObtenirListeEquipesCreeesAvant1950(int anneeFondation)
        {
            return from equipe in Context.Equipes
                   where equipe.AnneeFondation < anneeFondation orderby equipe.Nom
                   select equipe;
        }

       static IQueryable<Equipe> ObtenirListeEquipesCreeesAvant1950Lambda(int anneeFondation)
        {
            return Context.Equipes.Where(e => e.AnneeFondation < anneeFondation).OrderBy(e =>e.Nom);

        }

    }
}
