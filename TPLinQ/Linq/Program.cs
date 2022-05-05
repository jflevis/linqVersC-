using System;
using System.Collections.Generic;
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

            System.Console.WriteLine("A Lambda===================== en lambda ci-dessous la liste des conférence ");

            var resultat8 = ObtenirListeConferencesLambda();
            foreach (var conference in resultat8)
                System.Console.WriteLine("La conférence no: " + conference.IdConference + " est: " + conference.Nom);

            System.Console.WriteLine("B===================== en standard ci-dessous la liste des villes par conférence ");
             ObtenirListeVilleConferences();

            System.Console.WriteLine("B Lambda===================== en lambda ci-dessous la liste des villes par conférence ");
            ObtenirListeVilleConferencesLambda();

            System.Console.WriteLine("C===================== en standard ci-dessous la liste des équipe par ordre decroissant population  ");
            ObtenirListeVillePopulationDecroissant();

            System.Console.WriteLine("D===================== en standard ci-dessous la liste des joueurs/equipe/ville/etat/alpha joueur ");
            ObtenirListeJoueurNomAsc();

            var equipeListe = ObtenirListeEquipes();
            foreach(var e in equipeListe)
                System.Console.WriteLine(e.Nom);

            System.Console.WriteLine("Choissez une équipe?");
            string villeChoisie = Console.ReadLine();
            System.Console.WriteLine("E===================== en standard ci-dessous équipe choisie par le nom ");
            ObtenirEquipeNom(villeChoisie);

            System.Console.WriteLine("Choissez une année pour rechercher >=");
            int anneeChoisie = Convert.ToInt32(Console.ReadLine());
        
            System.Console.WriteLine("F===================== en standard les joeurs qui ont débuté >= à l'année choisie ");
            ObtenirJoueursAnnee(anneeChoisie);

            System.Console.WriteLine("Choisir une équipe pour lister ses joueurs");
            equipeListe = ObtenirListeEquipes();
            foreach (var e in equipeListe)
            System.Console.WriteLine(e.Nom);

            string nomEquipe = Console.ReadLine();
            System.Console.WriteLine("G===================== en standard joueurs par équipes saisie ");
            ObtenirJoueursUneEquipe(nomEquipe);

            System.Console.WriteLine("Choisir une date de naissance <=");
            DateTime joueurDDN = DateTime.Parse(Console.ReadLine());
            System.Console.WriteLine("Indiquer la premier lettre du prénom d'un joueur");
            char joueurFirstLetterPrenom = Char.Parse(Console.ReadLine());

            System.Console.WriteLine("H===================== prenom de joueur commencant par et ddn <= saisie ");
            ObtenirJoueurFirstLetterDDN(joueurDDN, joueurFirstLetterPrenom);
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
                          orderby conference.Nom, ville.Nom
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
            var listeVilleLambda = Context.Villes.Join(Context.Equipes, v => v.IdVille, e => e.IdVille, (v, e) => new { v, e }).Join(Context.Conferences, c => c.e.IdConference, ce => ce.IdConference, (c, ce) => new
            {
                c,
                ce
            }).OrderBy(ce => ce.ce.Nom).OrderBy(e =>e.c.v.Nom).ToList();
            foreach (var ville in listeVilleLambda)
                System.Console.WriteLine(ville.c.e.Nom + " de " + ville.c.v.Nom +" population de: "+ville.c.v.Population + " habitants dans la conférérence " + ville.ce.Nom);
        }
        static void ObtenirListeVillePopulationDecroissant()
        {
            var listVillePopDesc = from ville in Context.Villes
            join equipe in Context.Equipes
            on ville.IdVille equals equipe.IdVille
            join conference in Context.Conferences
            on equipe.IdConference equals conference.IdConference
            join etat in Context.Etats
            on ville.IdEtat equals etat.IdEtat
            orderby ville.Population descending
            select new
            {
                nomEquipe = equipe.Nom,
                villeNom = ville.Nom,
                etatNom = etat.Nom,
                conferenceNom = conference.Nom,
                pop = ville.Population
            };
            foreach(var ville in listVillePopDesc)
            {
                System.Console.WriteLine(ville.nomEquipe + " de " + ville.villeNom + " etat: " + ville.etatNom + " conférence: " + ville.conferenceNom + " avec population de: " + ville.pop + " habitants");
            }
        }
        static void ObtenirListeJoueurNomAsc()
        {
            var listJoueurNomAsc = from joueur in Context.JoueurEquipes
                                   join j in Context.Joueurs
                                   on joueur.IdJoueur equals j.IdJoueur orderby j.Nom,j.Prenom
                                   join equipe in Context.Equipes
                                   on joueur.IdEquipe equals equipe.IdEquipe
                                   join ville in Context.Villes
                                   on  equipe.IdVille equals ville.IdVille
                                   join etat in Context.Etats
                                   on ville.IdEtat equals etat.IdEtat orderby j.Nom,j.Prenom
                                   select new
                                   {
                                       joueurNom = j.Prenom + " " + j.Nom,
                                       idEquipe = joueur.IdEquipeNavigation,
                                       nomEquipe = equipe.Nom,
                                       nomVille = ville.Nom,
                                       nomEtat = etat.Nom
                                   };
            foreach (var joueurNom in listJoueurNomAsc)
                System.Console.WriteLine(joueurNom.joueurNom +"de l'équipe "+joueurNom.idEquipe.IdEquipe+" de "+ joueurNom.nomVille+" dans l'état de "+ joueurNom.nomEtat);
        }
        static void ObtenirEquipeNom(string equipe)
        {
            var equipeNom = from e in Context.Equipes
                            join v in Context.Villes
                            on e.IdVille equals v.IdVille
                            join c in Context.Conferences
                            on e.IdConference equals c.IdConference
                            where e.Nom == equipe
                            select new
                            {
                                nomEquipe = e.Nom,
                                nomVille = v.Nom,
                                nomConference = c.Nom
                            };
            foreach(var eq in equipeNom)
            {
                System.Console.WriteLine("Les "+eq.nomEquipe+" de "+eq.nomVille+" sont dans la conférence: "+eq.nomConference);

            }
        }
        static IQueryable<Equipe> ObtenirListeEquipes()
        {
            return from eq in Context.Equipes
                   where eq.IdEquipe > 0
                   orderby eq.Nom
                   select eq;
        }
        static void ObtenirJoueursAnnee(int anneechoisie)
        {
            var listJoueurAnneeebut = from joueur in Context.JoueurEquipes
            join j in Context.Joueurs
            on joueur.IdJoueur equals j.IdJoueur
            orderby j.Nom, j.Prenom
            join equipe in Context.Equipes
            on joueur.IdEquipe equals equipe.IdEquipe
            join ville in Context.Villes
            on equipe.IdVille equals ville.IdVille
            join etat in Context.Etats
            on ville.IdEtat equals etat.IdEtat
            where joueur.DateDebut >= anneechoisie
            orderby j.Nom, j.Prenom
            select new
            {
                joueurNom = j.Prenom + " " + j.Nom,
                idEquipe = joueur.IdEquipeNavigation,
                nomEquipe = equipe.Nom,
                nomVille = ville.Nom,
                nomEtat = etat.Nom,
                dateDebut =joueur.DateDebut
            };
            foreach (var joueurNom in listJoueurAnneeebut)
                System.Console.WriteLine(joueurNom.joueurNom + " de l'équipe " + joueurNom.idEquipe.Nom + " de " + joueurNom.nomVille + "a débuté en " +joueurNom.dateDebut);
        }

        static void ObtenirJoueursUneEquipe(string nomEquipe)
        {
            var listeJoueurUneEquipe = from j in Context.Joueurs
                join jE in Context.JoueurEquipes
                on j.IdJoueur equals jE.IdJoueur
                join e in Context.Equipes
                on jE.IdEquipe equals e.IdEquipe
                where e.Nom == nomEquipe
                orderby j.Nom, j.Prenom
                select new
                {
                    joueurPrenom = j.Prenom,
                    joueurNom = j.Nom,
                    equipeNom = e.Nom,
                    joueurDDN =j.DateNaissance
                };
            foreach (var j in listeJoueurUneEquipe)
                System.Console.WriteLine(j.joueurPrenom +" "+ j.joueurNom + " des " + nomEquipe );
        }
        static void ObtenirJoueurFirstLetterDDN(DateTime ddn,char firstLetterPrenom)
        {
            var listeJoueurFirstLettreDDN = from j in Context.Joueurs
                join jE in Context.JoueurEquipes
                on j.IdJoueur equals jE.IdJoueur
                join e in Context.Equipes
                on jE.IdEquipe equals e.IdEquipe
                where j.DateNaissance <= ddn && j.Prenom[0] == firstLetterPrenom
                select new
                {
                    joueurNon = j.Prenom + " " + j.Nom,
                    equipeNom = e.Nom,
                    dateNaiss = j.DateNaissance
                };
            System.Console.WriteLine(ddn.ToString() + " " + firstLetterPrenom +" date de naissance:");


          // foreach(var joueur in listeJoueurFirstLettreDDN)
           //     System.Console.WriteLine(joueur.joueurNon+" des "+joueur.equipeNom +" date de naissance: "+joueur.dateNaiss);
        }
    }
}
/*
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