using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class PropertiesService : BaseService, IPropertiesService
    {
        private readonly ApplicationDbContext context;

        public PropertiesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string district, int floor, int maxFloor, int size, int yardSize, int year, string propertyType, string buildingType, int price)
        {
            var property = new Property
            {
                Size = size,
                Floor = floor <= 0 || floor > 255 ? null : (byte)floor,
                TotalFloors = maxFloor <= 0 || maxFloor > 255 ? null : (byte)maxFloor,
                YardSize = yardSize <= 0 ? null : yardSize,
                Year = year <= 1800 ? null : year,
                Price = price <= 0 ? null : price
            };

            var dbDistrict = context.Districts.FirstOrDefault(d => d.Name == district);
            if (dbDistrict is null)
            {
                dbDistrict = new District { Name = district };
            }
            property.District = dbDistrict;

            var dbPropertyType = context.PropertyTypes.FirstOrDefault(pt => pt.Name == propertyType);
            if (dbPropertyType is null)
            {
                dbPropertyType = new PropertyType { Name = propertyType };
            }
            property.PropertyType = dbPropertyType;

            var dbBuildingType = context.BuildingTypes.FirstOrDefault(bt => bt.Name == buildingType);
            if (dbBuildingType is null)
            {
                dbBuildingType = new BuildingType { Name = buildingType };
            }
            property.BuildingType = dbBuildingType;

            context.Properties.Add(property);
            context.SaveChanges();
        }

        public decimal AveragePricePerSquareMeter()
        {
            return context.Properties
                .Where(p => p.Price.HasValue)
                .Average(p => p.Price / (decimal)p.Size) ?? 0;
        }

        public decimal AveragePricePerSquareMeter(int districtId)
        {
            return context.Properties
                .Where(p => p.Price.HasValue && p.DistrictId == districtId)
                .Average(p => p.Price / (decimal)p.Size) ?? 0;
        }

        public double AverageSize(int districtId)
        {
            return context.Properties
                .Where(p => p.DistrictId == districtId)
                .Average(p => p.Size);
        }

        public IEnumerable<PropertyInfoFullDataDto> GetFullData(int count)
        {
            var properties = context.Properties
                .Where(p => p.Floor.HasValue && p.Floor.Value > 1)
                .ProjectTo<PropertyInfoFullDataDto>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Price)
                .ThenBy(p => p.Size)
                .ThenBy(p => p.Year)
                .Take(count)
                .ToList();

            return properties;
        }

        public IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize)
        {
            var properties = context.Properties
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.Size >= minSize && p.Size <= maxSize)
                .ProjectTo<PropertyInfoDto>(this.Mapper.ConfigurationProvider)
                .ToList();

            return properties;
        }
    }
}
