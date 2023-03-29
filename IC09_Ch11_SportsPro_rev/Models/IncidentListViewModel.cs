namespace IC09_Ch11_SportsPro_rev.Models
{
    public class IncidentListViewModel
    {
        public string Filter { get; set; } = string.Empty;
        public IEnumerable<Incident> Incidents { get; set; } = null!;
    }
}
