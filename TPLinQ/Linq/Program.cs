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

            System.Console.WriteLine("===================== en lambda ci-dessous les équipes de la conférence 2");
           var resultat2 = ObtenirEquipesParConferenceLambda(2);
            foreach (var equipe in resultat2)
                System.Console.WriteLine(equipe.Nom + " " + equipe.IdVille);

            System.Console.WriteLine("===================== en standard ci-dessous nombre d'équipe par état =2");
            var resultat3 = ObtenirNombreEquipesParEtat(2);
             System.Console.WriteLine(resultat3);

            System.Console.WriteLine("===================== en lambda ci-dessous nombre d'équipe par état =2");
            var resultat4 = ObtenirNombreEquipesParEtatLambda(2);
            System.Console.WriteLine(resultat4);

            System.Console.WriteLine("===================== en standard ci-dessous la liste des equipes crees avant 1960");
            var resultat5 = ObtenirListeEquipesCreeesAvant1950(1960);
            foreach (var equipe in resultat5) {
                System.Console.WriteLine(equipe.Nom + " a été créée en " + equipe.AnneeFondation);
            }
               

            System.Console.WriteLine("===================== en lambda ci-dessous la liste des equipes crees avant 1972");
            var resultat6 = ObtenirListeEquipesCreeesAvant1950Lambda(1972);
            foreach (var equipe in resultat6)
                System.Console.WriteLine(equipe.Nom + " a été créée en " + equipe.AnneeFondation);

            System.Console.WriteLine("===================== en standard ci-dessous la liste des conférence ");
            var resultat7 = ObtenirListeConferences();
            foreach (var conference in resultat7)
                System.Console.WriteLine("La conférence no: "+conference.IdConference+" est: "+ conference.Nom);

            System.Console.WriteLine("===================== en lambda ci-dessous la liste des conférence ");

            var resultat8 = ObtenirListeConferencesLambda();
            foreach (var conference in resultat8)
                System.Console.WriteLine("La conférence no: " + conference.IdConference + " est: " + conference.Nom);

            System.Console.WriteLine("===================== en standard ci-dessous la liste des villes par conférence ");
             ObtenirListeVilleConferences();

            System.Console.WriteLine("===================== en lambda ci-dessous la liste des villes par conférence ");
            ObtenirListeVilleConferencesLambda();
 
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
        static IQueryable<Conference> ObtenirListeConferences()
        {
            return from conference in Context.Conferences
                   where conference.IdConference > 0
                   orderby conference.Nom 
                   select conference;
        }
        static IQueryable<Conference> ObtenirListeConferencesLambda()
        {
            return Context.Conferences.Where(e => e.IdConference > 0).OrderBy(e => e.Nom);
        }

        static void ObtenirListeVilleConferences()
        {
          var listeVille =from ville in Context.Villes
                   join equipe in Context.Equipes
                   on ville.IdVille equals equipe.IdVille
                   join conference in Context.Conferences on equipe.IdConference equals conference.IdConference
                   select new
                   {
                       nomVille = ville.Nom,
                       nomConference = conference.Nom
                   };
            foreach (var ville in listeVille)
                System.Console.WriteLine(ville.nomVille +" est dans la conférence: "+ville.nomConference);
        }
       static void  ObtenirListeVilleConferencesLambda()
        {
            var listeVille = Context.Villes.Join(Context.Equipes, villeId => villeId.IdVille, equipeVilleId => equipeVilleId.IdVille, (villeId, equipeVilleId) => new
            {
                nomVille = villeId.Nom,
                nomEquipe = equipeVilleId.Nom,
            
            });
            foreach (var ville in listeVille)
                System.Console.WriteLine(ville.nomEquipe+" de "+ville.nomVille) ;
            System.Console.WriteLine("===================== en lambda ci-dessous la liste des villes par conférence ");

            var listeVilleConf = Context.Equipes.Join(Context.Conferences, equipeConf => equipeConf.IdConference, conferEquipe => conferEquipe.IdConference, (equipeConf, conferEquipe) => new
            {
               nomEquipe = equipeConf.Nom,
               nomConference =conferEquipe.Nom
            }).ToList();
            foreach (var ville in listeVille)
            System.Console.WriteLine(ville.nomEquipe + " de " + ville.nomVille);

            System.Console.WriteLine("===================== en lambda ci-dessous la liste des villes par conférence stackOverflow");

            var listeVilleLambda = Context.Villes.Join(Context.Equipes, v => v.IdVille, e => e.IdVille, (v, e) => new { v, e }).Join(Context.Conferences, c => c.e.IdConference, ce => ce.IdConference, (c, ce) => new
            {
                c,
                ce
            }).ToList();
            foreach (var ville in listeVilleLambda)
                System.Console.WriteLine(ville.c.e.Nom + " de " + ville.c.v.Nom + "dans la conférérence" + ville.ce.Nom);
        }
    }
}
/*
 * 
 * https://www.c-sharpcorner.com/article/writing-complex-queries-using-linq-and-lambda/
 * //Using linq,  
var result1 = from order in context.OrderMasters  
               where order.OrderDate < DateTime.Now.AddDays(-100)  
               select order;  

//Using lambda,  
var lresult1 = context.OrderMasters
               .Where(a => a.OrderDate < DateTime.Now.AddDays(-100)).Select(s => s); 

//Using linq,  

var result2 = from order in context.OrderMasters  
               join orderdetail in context.OrderDetails on order.OrderId equals orderdetail.OrderId  
               //where order.OrderDate < DateTime.Now.AddDays(-10)  
               select new {  
                   order.OrderNo, orderdetail.ProductName, order.OrderDate  
               };  

//Using lambda,  
var lresult2 = context.OrderMasters
               .Join(context.OrderDetails
               , od => od.OrderId
               , o => o.OrderId
               , (o, od) => new {  
                       o.OrderNo, od.ProductName, o.OrderDate  
                })  
                //.Where(a => a.OrderDate < DateTime.Now.AddDays(-100))  
                .Select(s => s);  
===========

//Using linq,  
var result3 = from order in context.OrderMasters  
            join orderdetail in context.OrderDetails  
            on new {  
                order.OrderId, order.UserId  
            }  
            equals new {  
                orderdetail.OrderId, orderdetail.UserId  
            }  
            //where order.OrderDate < DateTime.Now.AddDays(-10)  
            select new {  
               order.OrderNo, orderdetail.ProductName, order.OrderDate  
            };  

//Using lambda,  
var lresult3 = context.OrderMasters
               .Join(context.OrderDetails
               , od => new {  
                    od.OrderId, od.UserId  
                }
               , o => new {  
                    o.OrderId, o.UserId  
                }
               , (o, od) => new {  
                     o.OrderNo,  
                     od.ProductName,  
                     o.OrderDate  
             })  
             //.Where(a => a.OrderDate < DateTime.Now.AddDays(-100))  
             .Select(s => s);  

Console.WriteLine(string.Format("OrderNo \t OrderDate \t Product"));  
foreach(var item in lresult3) {  
    Console.WriteLine(string.Format("{0}\t{1}\t{2}", item.OrderNo, item.OrderDate, item.ProductName));  
}
 */