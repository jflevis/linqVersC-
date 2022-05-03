using System;
using System.Collections.Generic;

#nullable disable

namespace Linq.Models
{
    public partial class Etat
    {
        public Etat()
        {
            Villes = new HashSet<Ville>();
        }

        public int IdEtat { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Ville> Villes { get; set; }
    }
}
