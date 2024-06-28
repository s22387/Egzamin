namespace Egzamin.DTO
{
    public class VisitDTO
    {
        public int IdVisit { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public string Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Price { get; set; }
    }
}

