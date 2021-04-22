using System.Xml.Serialization;

namespace ProductShop.Dtos.Export.UsersWithProducts
{
    [XmlType("User")]
    public class FullUserDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public SoldProductsDto SoldProducts { get; set; }
    }
}
