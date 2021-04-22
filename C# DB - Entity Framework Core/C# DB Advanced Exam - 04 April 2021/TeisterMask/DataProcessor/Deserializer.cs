namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(ImportProjectDto[]), new XmlRootAttribute("Projects"));

            var projectsDto = (ImportProjectDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Project> projects = new List<Project>();

            foreach (var projectDto in projectsDto)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var isOpenDateParsed = DateTime.TryParseExact(projectDto.OpenDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var OpenDate);

                if (!isOpenDateParsed)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                var project = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = OpenDate
                };

                var isDueDateParsed = DateTime.TryParseExact(projectDto.DueDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime DueDate);

                if (!isDueDateParsed)
                {
                    project.DueDate = null;
                }
                else
                {
                    project.DueDate = DueDate;
                }

                foreach (var taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var isTaskOpenDateParsed = DateTime.TryParseExact(taskDto.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime TaskOpenDate);

                    if (!isTaskOpenDateParsed)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var isTaskDueDateParsed = DateTime.TryParseExact(taskDto.DueDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime TaskDueDate);

                    if (!isTaskDueDateParsed)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (TaskOpenDate < OpenDate || (TaskDueDate > DueDate && isDueDateParsed))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var isExecutionTypeParsed = Enum.IsDefined(typeof(ExecutionType), taskDto.ExecutionType);
                    if (!isExecutionTypeParsed)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    var parsedExecutionType = (ExecutionType)taskDto.ExecutionType;

                    var isLabelTypeParsed = Enum.IsDefined(typeof(LabelType), taskDto.LabelType);
                    if (!isLabelTypeParsed)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    var parsedLabelType = (LabelType)taskDto.LabelType;

                    var task = new Task
                    {
                        Name = taskDto.Name,
                        OpenDate = TaskOpenDate,
                        DueDate = TaskDueDate,
                        ExecutionType = parsedExecutionType,
                        LabelType = parsedLabelType
                    };

                    project.Tasks.Add(task);
                }

                projects.Add(project);
                sb.AppendLine($"Successfully imported project - {project.Name} with {project.Tasks.Count} tasks.");
            }

            context.Projects.AddRange(projects);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            var employeesDto = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

            List<Employee> employees = new List<Employee>();

            var validTaskIds = context.Tasks.Select(t => t.Id).ToList();

            foreach (var employeeDto in employeesDto)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone
                };

                List<int> usedIds = new List<int>();
                foreach (var employeeTaskDto in employeeDto.Tasks)
                {
                    if (!validTaskIds.Contains(employeeTaskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!usedIds.Contains(employeeTaskDto))
                    {
                        usedIds.Add(employeeTaskDto);
                        employee.EmployeesTasks.Add(new EmployeeTask { TaskId = employeeTaskDto });
                    }
                }

                employees.Add(employee);
                sb.AppendLine($"Successfully imported employee - {employee.Username} with {employee.EmployeesTasks.Count} tasks.");
            }

            context.Employees.AddRange(employees);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}