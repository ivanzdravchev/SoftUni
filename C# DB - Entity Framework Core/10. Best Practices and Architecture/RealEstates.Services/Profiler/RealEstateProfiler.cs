using AutoMapper;
using RealEstates.Models;
using RealEstates.Services.DTOs;
using System.Linq;

namespace RealEstates.Services.Profiler
{
    public class RealEstateProfiler : Profile
    {
        public RealEstateProfiler()
        {
            this.CreateMap<Property, PropertyInfoDto>()
                .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.BuildingType.Name));

            this.CreateMap<District, DistrictInfoDto>()
                .ForMember(x => x.AveragePricePerSquareMeter, y => y.MapFrom(z => z.Properties
                .Where(p => p.Price.HasValue)
                .Average(p => p.Price / (decimal)p.Size) ?? 0));

            this.CreateMap<Property, PropertyInfoFullDataDto>()
                .ForMember(x => x.BuildingType, y => y.MapFrom(z => z.BuildingType.Name))
                .ForMember(x => x.PropertyType, y => y.MapFrom(z => z.PropertyType.Name));

            this.CreateMap<Tag, TagInfoDto>();
        }
    }
}