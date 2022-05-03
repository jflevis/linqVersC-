using System;
using System.Collections.Generic;

//#nullable disable

namespace Linq.Models
{
    public partial class Equipe
    {
        public Equipe()
        {
            JoueurEquipes = new HashSet<JoueurEquipe>();
        }

        public int IdEquipe { get; set; }
        public string Nom { get; set; }
        public int AnneeFondation { get; set; }
        public int IdVille { get; set; }
        public int IdConference { get; set; }
        public string Surnom { get; set; }

        public virtual Conference IdConferenceNavigation { get; set; }
        public virtual Ville IdVilleNavigation { get; set; }
        public virtual ICollection<JoueurEquipe> JoueurEquipes { get; set; }
    }
}
