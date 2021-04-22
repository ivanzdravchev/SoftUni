using System.Xml.Serialization;

namespace ProductShop.Dtos.Export.UsersWithProducts
{
    [XmlType("SoldProducts")]
    public class SoldProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public UserProductDto[] Products { get; set; }
    }
}
