using System.Xml.Serialization;

namespace ProductShop.Dtos.Export.SoldProducts
{
    [XmlType("Product")]
    public class ProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
