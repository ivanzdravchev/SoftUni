using RealEstates.Services.DTOs;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public interface IPropertiesService
    {
        void Add(string district, int floor, int maxFloor, int size, int yardSize,
            int year, string propertyType, string buildingType, int price);

        decimal AveragePricePerSquareMeter();

        decimal AveragePricePerSquareMeter(int districtId);

        double AverageSize(int districtId);

        IEnumerable<PropertyInfoFullDataDto> GetFullData(int count);

        IEnumerable<PropertyInfoDto> Search(int minPrice, int maxPrice, int minSize, int maxSize);
    }
}
