using Egzamin.Model;

namespace Egzamin.Model
{
    public partial class Doctor
    {
        public Doctor()
        {
            Visits = new HashSet<Visit>();
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal VisitPrice { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}