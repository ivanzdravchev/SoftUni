namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var departmentDtos = JsonConvert.DeserializeObject<List<DepartmentDto>>(jsonString);

            List<Department> departments = new List<Department>();

            foreach (var departmentDto in departmentDtos)
            {
                if (!IsValid(departmentDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department
                {
                    Name = departmentDto.Name
                };

                List<Cell> cells = new List<Cell>();

                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        cells.Clear();
                        break;
                    }

                    var cell = new Cell
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    };

                    cells.Add(cell);
                }

                if (!cells.Any())
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                department.Cells = cells;
                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count()} cells");
            }

            context.Departments.AddRange(departments);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var prisonerMails = JsonConvert.DeserializeObject<List<PrisonerDto>>(jsonString);

            List<Prisoner> prisoners = new List<Prisoner>();

            foreach (var prisonerDto in prisonerMails)
            {
                if (!IsValid(prisonerDto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime incarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? releaseDate = null;

                bool isReleaseDateParsed = DateTime.TryParseExact(prisonerDto.ReleaseDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var releaseDateValue);

                if (isReleaseDateParsed)
                {
                    releaseDate = releaseDateValue;
                }

                var prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId
                };

                bool areMailsValid = true;
                foreach (var mailDto in prisonerDto.Mails)
                {
                    //if (!IsValid(mailDto))
                    //{
                    //    sb.AppendLine("Invalid Data");
                    //    areMailsValid = false;
                    //    break;
                    //}

                    var mail = new Mail
                    {
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address
                    };

                    prisoner.Mails.Add(mail);
                }

                if (!areMailsValid)
                {
                    continue;
                }

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));

            var officersDto = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Officer> officers = new List<Officer>();

            foreach (var officerDto in officersDto)
            {
                bool isPositionParsed = Enum.TryParse(officerDto.Position, out Position Position);
                bool isWeaponParsed = Enum.TryParse(officerDto.Weapon, out Weapon Weapon);
                
                if (!(isPositionParsed && isWeaponParsed))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = new Officer
                {
                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Position = Position,
                    Weapon = Weapon,
                    DepartmentId = officerDto.DepartmentId
                };

                foreach (var officerPrisoner in officerDto.Prisoners)
                {
                    officer.OfficerPrisoners.Add(new OfficerPrisoner { PrisonerId = officerPrisoner.PrisonerId });
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count()} prisoners)");
            }

            context.Officers.AddRange(officers);

            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}