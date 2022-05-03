using System;
using System.Collections.Generic;

#nullable disable

namespace Linq.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Equipes = new HashSet<Equipe>();
        }

        public int IdVille { get; set; }
        public string Nom { get; set; }
        public int IdEtat { get; set; }
        public int Population { get; set; }
        public int AnneeFondation { get; set; }

        public virtual Etat IdEtatNavigation { get; set; }
        public virtual ICollection<Equipe> Equipes { get; set; }
    }
}
