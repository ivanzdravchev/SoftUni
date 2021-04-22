using AutoMapper.QueryableExtensions;
using RealEstates.Data;
using RealEstates.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RealEstates.Services
{
    public class DistrictsService : BaseService, IDistrictsService
    {
        private readonly ApplicationDbContext context;

        public DistrictsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DistrictInfoDto> GetMostExpensiveDistricts(int count)
        {
            var districts = context.Districts
                .ProjectTo<DistrictInfoDto>(this.Mapper.ConfigurationProvider)
                .OrderByDescending(d => d.AveragePricePerSquareMeter)
                .Take(count)
                .ToList();

            return districts;
        }
    }
}
