using System.Xml.Serialization;

namespace ProductShop.Dtos.Export.UsersWithProducts
{
    [XmlType("")]
    public class AllUsersDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public FullUserDto[] Users { get; set; }
    }
}
