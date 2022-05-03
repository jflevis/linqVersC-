using System;
using System.Collections.Generic;

#nullable disable

namespace Linq.Models
{
    public partial class Joueur
    {
        public Joueur()
        {
            JoueurEquipes = new HashSet<JoueurEquipe>();
        }

        public int IdJoueur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }

        public virtual ICollection<JoueurEquipe> JoueurEquipes { get; set; }
    }
}
