using RealEstates.Data;
using RealEstates.Models;
using System;
using System.Linq;

namespace RealEstates.Services
{
    public class TagService : BaseService, ITagService
    {
        private readonly ApplicationDbContext context;
        private readonly IPropertiesService propertiesService;

        public TagService(ApplicationDbContext context, IPropertiesService propertiesService)
        {
            this.context = context;
            this.propertiesService = propertiesService;
        }

        public void Add(string name, int? importance)
        {
            var tag = new Tag
            {
                Name = name,
                Importance = importance
            };

            context.Tags.Add(tag);
            context.SaveChanges();
        }

        public void BulkTagToProperties()
        {
            var allProperties = context.Properties.ToList();

            foreach (var property in allProperties)
            {
                var averagePriceForDistrict = this.propertiesService
                    .AveragePricePerSquareMeter(property.Id);

                if (property.Price >= averagePriceForDistrict)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == "скъп-имот");
                    property.Tags.Add(tag);
                }
                else if (property.Price < averagePriceForDistrict)
                {
                    var tag = context.Tags.FirstOrDefault(t => t.Name == "евтин-имот");
                    property.Tags.Add(tag);
                }

                var currentDate = DateTime.Now.AddYears(-15);
                if (property.Year.HasValue && property.Year <= currentDate.Year)
                {
                    var tag = GetTag("старо-строителство");
                    property.Tags.Add(tag);
                }
                else if (property.Year.HasValue && property.Year > currentDate.Year)
                {
                    var tag = GetTag("ново-строителство");
                    property.Tags.Add(tag);
                }

                var averagePropertySize = this.propertiesService.AverageSize(property.DistrictId);
                if (property.Size >= averagePropertySize)
                {
                    var tag = GetTag("голям-имот");
                    property.Tags.Add(tag);
                }
                else if (property.Size < averagePropertySize)
                {
                    var tag = GetTag("малък-имот");
                    property.Tags.Add(tag);
                }

                if (property.Floor.HasValue && property.Floor.Value == 1)
                {
                    var tag = GetTag("първи-етаж");
                    property.Tags.Add(tag);
                }
                else if (property.Floor.HasValue && property.TotalFloors.HasValue && property.Floor.Value == property.TotalFloors)
                {
                    var tag = GetTag("последен-етаж");
                    property.Tags.Add(tag);
                }
            }

            context.SaveChanges();
        }

        public Tag GetTag(string name) => context.Tags.FirstOrDefault(t => t.Name == name);
    }
}
