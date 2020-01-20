
using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class PatientsDbContext : DbContext
    {
        public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) { }
        public DbSet<PatientRecord> Patients { get; set; }

    }
}
