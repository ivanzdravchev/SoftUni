using System.Collections.Generic;

namespace RealEstates.Models
{
    public class PropertyType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    }
}
