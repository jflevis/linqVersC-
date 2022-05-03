using System;
using System.Collections.Generic;

#nullable disable

namespace Linq.Models
{
    public partial class JoueurEquipe
    {
        public int IdJoueur { get; set; }
        public int IdEquipe { get; set; }
        public int DateDebut { get; set; }
        public int? DateFin { get; set; }

        public virtual Equipe IdEquipeNavigation { get; set; }
        public virtual Joueur IdJoueurNavigation { get; set; }
    }
}
