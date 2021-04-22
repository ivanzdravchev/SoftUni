using SoftJail.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
	[XmlType("Officer")]
    public class OfficerDto
    {
		[XmlElement("Name")]
		[StringLength(30, MinimumLength = 3)]
		public string FullName { get; set; }

		[XmlElement("Money")]
		[Range(0.0, double.MaxValue)]
		public decimal Salary { get; set; }

		[Required]
		[XmlElement("Position")]
		public string Position { get; set; }

		[Required]
		[XmlElement("Weapon")]
		public string Weapon { get; set; }

		[Required]
		[XmlElement("DepartmentId")]
		public int DepartmentId { get; set; }

		[XmlArray("Prisoners")]
		public OfficerPrisonersDto[] Prisoners { get; set; }
    }
}