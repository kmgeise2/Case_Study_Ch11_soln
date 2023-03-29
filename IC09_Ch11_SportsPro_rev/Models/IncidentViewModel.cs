﻿namespace IC09_Ch11_SportsPro_rev.Models
{
    public class IncidentViewModel
    {
        public Incident Incident { get; set; } = null!;
        public string Action { get; set; } = string.Empty;

        public IEnumerable<Customer> Customers { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = null!;
        public IEnumerable<Technician> Technicians { get; set; } = null!;
    }
}
