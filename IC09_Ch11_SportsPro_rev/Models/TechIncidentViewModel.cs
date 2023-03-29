namespace IC09_Ch11_SportsPro_rev.Models
{
    public class TechIncidentViewModel
    {
        //Notice the null-forgiving operator: null!
        //  Inform the compiler that passing null is expected
        //  and shouldn't be warned about.
        public Technician Technician { get; set; } = null!;
        public Incident Incident { get; set; } = null!;
        public IEnumerable<Incident> Incidents { get; set; } = null!;
    }
}
