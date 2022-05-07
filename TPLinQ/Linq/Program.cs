using System;
using System.Collections.Generic;
using System.Globalization;
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

            System.Console.WriteLine("\r\n" + "===================== en standard ci-dessous la liste des conférence ");
            var resultat7 = ObtenirListeConferences();
            foreach (var conference in resultat7)
                System.Console.WriteLine("\r\n" + "La conférence no: " +conference.IdConference+" est: "+ conference.Nom);

            System.Console.WriteLine("A Lambda===================== en lambda ci-dessous la liste des conférence ");

            var resultat8 = ObtenirListeConferencesLambda();
            foreach (var conference in resultat8)
                System.Console.WriteLine("La conférence no: " + conference.IdConference + " est: " + conference.Nom);

            System.Console.WriteLine("\r\n" + "B standard===================  ci-dessous la liste des villes par conférence ");
             ObtenirListeVilleConferences();

            System.Console.WriteLine("\r\n" + "B Lambda===================== ci-dessous la liste des villes par conférence ");
            ObtenirListeVilleConferencesLambda();

            System.Console.WriteLine("\r\n" + "C standard ===================  ci-dessous la liste des équipe par ordre decroissant population  ");
            ObtenirListeVillePopulationDecroissant();

            System.Console.WriteLine("\r\n"+"C Lambda ===================  ci-dessous la liste des équipe par ordre decroissant population  ");
            ObtenirListeVillePopulationDecroissantLambda();

            System.Console.WriteLine("\r\n" + "D standard===================  ci-dessous la liste des joueurs/equipe/ville/etat/alpha joueur ");
            ObtenirListeJoueurNomAsc();

            System.Console.WriteLine("\r\n" + "D lambda===================  ci-dessous la liste des joueurs/equipe/ville/etat/alpha joueur ");
            ObtenirListeJoueurNomAscLambda();

            var equipeListe = ObtenirListeEquipes();
            foreach(var e in equipeListe)
                System.Console.WriteLine(e.Nom);

            System.Console.WriteLine("\r\n" + "Choissez une équipe?");
            string villeChoisie = Console.ReadLine();
            System.Console.WriteLine("\r\n" + "E  standard==================  ci-dessous équipe choisie par le nom ");
            ObtenirEquipeNom(villeChoisie);

            System.Console.WriteLine("\r\n" + "Choissez une année pour rechercher >=");
            int anneeChoisie = Convert.ToInt32(Console.ReadLine());
        
            System.Console.WriteLine("\r\n" + "F standard===================  les joueurs qui ont débuté >= à l'année choisie ");
            ObtenirJoueursAnnee(anneeChoisie);

            System.Console.WriteLine("\r\n" + "Choisir une équipe pour lister ses joueurs");
            equipeListe = ObtenirListeEquipes();
            foreach (var e in equipeListe)
            System.Console.WriteLine(e.Nom);

            string nomEquipe = Console.ReadLine();
            System.Console.WriteLine("\r\n" + "G standard  ================== en standard joueurs par équipes saisie ");
            ObtenirJoueursUneEquipe(nomEquipe);

            System.Console.WriteLine("\r\n" + "Choisir une date de naissance <=  FORMAT YY/MM/JJ");
            string joueurDDN = Console.ReadLine();
            System.Console.WriteLine("Indiquer la premier lettre du prénom d'un joueur");
            char joueurFirstLetterPrenom = Char.Parse(Console.ReadLine());

            System.Console.WriteLine("\r\n" + "H standard =================== prenom de joueur commencant par et ddn <= saisie ");
            //ObtenirJoueurFirstLetterDDN(joueurDDN, joueurFirstLetterPrenom);
        }

   

        // obtenir la liste des équipes dont l'identifiant de conférence correspond à celui reçu en paramètre
        static IQueryable<Equipe> ObtenirEquipesParConference(int idConference)  //A
        {
             return from equipe in Context.Equipes
                    where equipe.IdConference == idConference
                    select equipe;
        }
        //même chose en lambda :
        private static IQueryable<Equipe> ObtenirEquipesParConferenceLambda(int IdConference) //A Lambda
        {
            return Context.Equipes.Where(e => e.IdConference == IdConference);
        }

        //obtenir la liste des équipes dont la ville  est dans l'état passé en paramètre //B
        static int ObtenirNombreEquipesParEtat(int idEtat)
        {
            return (from equipe in Context.Equipes
                    where equipe.IdVilleNavigation.IdEtat == idEtat
                    select equipe).Count();
            // les résultats sont triés en ordre croissant de nom d'équipe
        }
        static int ObtenirNombreEquipesParEtatLambda(int idEtat) //B Lmabda
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
       static void  ObtenirListeVilleConferencesLambda() //B lambda ville par conférence
        {
            var listeVilleLambda = Context.Villes.Join(Context.Equipes, v => v.IdVille, e => e.IdVille, (v, e) => new { v, e }).Join(Context.Conferences, c => c.e.IdConference, ce => ce.IdConference, (c, ce) => new
            {
                c,
                ce
            }).OrderBy(ce => ce.ce.Nom).OrderBy(e =>e.c.v.Nom).ToList();
            foreach (var ville in listeVilleLambda)
                System.Console.WriteLine(ville.c.e.Nom + " de " + ville.c.v.Nom +" population de: "+ville.c.v.Population + " habitants dans la conférérence " + ville.ce.Nom);

          
        }
        static void ObtenirListeVillePopulationDecroissant() //C ville ordre decroissant population
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

        static void ObtenirListeVillePopulationDecroissantLambda() //C lambda ville ordre decroissant population
        {
            var listEtatConfPop3 = Context.Villes.Join(Context.Etats, v => v.IdEtat, et => et.IdEtat, (v, et) => new { v, et })
                .Join(Context.Equipes, eq => eq.v.IdEtat, veq => veq.IdVille, (eq, veq) => new { eq, veq })
                .Join(Context.Conferences,cf=>cf.veq.IdConference,eqconf =>eqconf.IdConference,(cf,eqconf)=>new {cf,eqconf})
                .OrderByDescending(c =>c.cf.eq.v.Population).ToList();
            foreach (var villeEtat in listEtatConfPop3)
                System.Console.WriteLine( villeEtat.cf.veq.Nom + " de " + villeEtat.cf.eq.v.Nom + " dans l'etat de " + villeEtat.cf.eq.et.Nom + ", de: "+villeEtat.eqconf.Nom+ ". La ville a une population de: " + villeEtat.cf.eq.v.Population);
        }
        static void ObtenirListeJoueurNomAsc() //A
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
                System.Console.WriteLine(joueurNom.joueurNom +"de l'équipe "+joueurNom.idEquipe.Nom+" de "+ joueurNom.nomVille+" dans l'état de "+ joueurNom.nomEtat);
        }

        static void ObtenirListeJoueurNomAscLambda()
        {
            var joueurEquipeLambda = Context.JoueurEquipes.Join(Context.Joueurs, jE => jE.IdJoueur, j => j.IdJoueur, (jE, j) => new { jE, j })
                .Join(Context.Equipes, jeq => jeq.jE.IdEquipe, eq => eq.IdEquipe, (jeq, eq) => new { jeq, eq })
                .Join(Context.Villes, jVille => jVille.eq.IdVille, v => v.IdVille, (jVIlle, v) => new { jVIlle, v })
                .Join (Context.Etats, jVEtat => jVEtat.v.IdEtat, etat => etat.IdEtat, (jVEtat, etat) => new { jVEtat, etat })
                .OrderBy(joueurTri => joueurTri.jVEtat.jVIlle.jeq.j.Nom)
                .OrderBy(joueurTri => joueurTri.jVEtat.jVIlle.jeq.j.Prenom).ToList();
            foreach (var joueur in joueurEquipeLambda)
                System.Console.WriteLine(joueur.jVEtat.jVIlle.jeq.j.Prenom + " " + joueur.jVEtat.jVIlle.jeq.j.Nom + " des " + joueur.jVEtat.jVIlle.eq.Nom + " de " + joueur.jVEtat.v.Nom + " etat: " + joueur.etat.Nom);
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
   /*     static void ObtenirJoueurFirstLetterDDN(string ddn, char firstLetterPrenom)
        {
            DateTime datechoisie = DateTime.Parse(ddn);
            var listeJoueurFirstLettreDDN = from j in Context.Joueurs
                join jE in Context.JoueurEquipes
                on j.IdJoueur equals jE.IdJoueur
                join e in Context.Equipes
                on jE.IdEquipe equals e.IdEquipe
                where j.DateNaissance <= DateTime.Parse(ddn) && j.Prenom[0] == firstLetterPrenom
                select new
                {
                    joueurNon = j.Prenom + " " + j.Nom,
                    equipeNom = e.Nom,
                    dateNaiss = DateTime.Parse(j.DateNaissance)
        };
            foreach(var j in listeJoueurFirstLettreDDN)
                System.Console.WriteLine(j.joueurNon +" des "+j.equipeNom+" dont la date de naisance est: "+j.dateNaiss);
            // foreach(var joueur in listeJoueurFirstLettreDDN)
            //     System.Console.WriteLine(joueur.joueurNon+" des "+joueur.equipeNom +" date de naissance: "+joueur.dateNaiss);
        }*/
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