using System.Collections.Generic;

namespace RealEstates.Models
{
    public class Property
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public int? YardSize { get; set; }

        public byte? Floor { get; set; }

        public byte? TotalFloors { get; set; }

        public int? Year { get; set; }

        /// <summary>
        /// In Euro
        /// </summary>
        public int? Price { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public BuildingType BuildingType { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
