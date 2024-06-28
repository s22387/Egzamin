namespace Egzamin.DTO;

public class PatientWithVisitsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public string TotalAmountMoneySpent { get; set; }
    public int NumberOfVisit { get; set; }
    public List<VisitDTO> Visits { get; set; }
}