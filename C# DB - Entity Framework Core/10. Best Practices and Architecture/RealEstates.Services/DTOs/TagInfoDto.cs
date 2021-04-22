using System.Xml.Serialization;

namespace RealEstates.Services.DTOs
{
    [XmlType("Tag")]
    public class TagInfoDto
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}