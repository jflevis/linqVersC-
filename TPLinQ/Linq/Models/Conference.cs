using System;
using System.Collections.Generic;

//#nullable disable

namespace Linq.Models
{
    public partial class Conference
    {
        public Conference()
        {
            Equipes = new HashSet<Equipe>();
        }

        public int IdConference { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Equipe> Equipes { get; set; }
    }
}
