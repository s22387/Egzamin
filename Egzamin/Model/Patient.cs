using System;
using System.Collections.Generic;

namespace Egzamin.Model
{
    public partial class Patient
    {
        public Patient()
        {
            Visits = new HashSet<Visit>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}