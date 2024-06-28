using Microsoft.AspNetCore.Mvc;
using Egzamin.Model;
using Egzamin.DTO;
using Egzamin.Context;

namespace Egzamin.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientWithVisits(int id)
        {
            var patient = _context.Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientWithVisitsDTO
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthdate = p.Birthdate,
                    TotalAmountMoneySpent = p.Visits.Sum(v => v.Price) + " zł",
                    NumberOfVisit = p.Visits.Count,
                    Visits = p.Visits.Select(v => new VisitDTO
                    {
                        IdVisit = v.Id,
                        Doctor = v.Doctor.Name,
                        Date = v.Date,
                        Price = v.Price + " zł"
                    }).ToList()
                })
                .FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost("add-visit")]
        public IActionResult AddVisit([FromBody] VisitDTO visitDto)
        {
            var patient = _context.Patients.Find(visitDto.IdPatient);
            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var doctor = _context.Doctors.Find(visitDto.IdDoctor);
            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            if (_context.Visits.Any(v => v.PatientId == visitDto.IdPatient && v.Date > DateTime.Now))
            {
                return BadRequest("Patient already has a scheduled visit.");
            }

            var schedule = _context.Schedules
                .FirstOrDefault(s => s.DoctorId == visitDto.IdDoctor && s.DayOfWeek == visitDto.Date.DayOfWeek);
            if (schedule == null || visitDto.Date.TimeOfDay < schedule.StartTime || visitDto.Date.TimeOfDay > schedule.EndTime)
            {
                return BadRequest("Doctor is not available at this time.");
            }

            if (_context.Visits.Count(v => v.DoctorId == visitDto.IdDoctor && v.Date.Date == visitDto.Date.Date) >= 5)
            {
                return BadRequest("Doctor has reached the maximum number of visits for this day.");
            }

            var price = doctor.VisitPrice;
            if (_context.Visits.Count(v => v.PatientId == visitDto.IdPatient) > 10)
            {
                price *= 0.9M;
            }

            var visit = new Visit
            {
                PatientId = visitDto.IdPatient,
                DoctorId = visitDto.IdDoctor,
                Date = visitDto.Date,
                Price = price
            };

            _context.Visits.Add(visit);
            _context.SaveChanges();

            return Ok(new { Id = visit.Id });
        }
    }
}