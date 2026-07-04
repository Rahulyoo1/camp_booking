using AutoMapper;
using CampBooking.Business.Interfaces;
using CampBooking.Data.Repositories.Interfaces;
using CampBooking.Domain.Common;
using CampBooking.Domain.DTOs.CampDto;
using CampBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CampBooking.Business.Services
{
    public class CampService : ICampService
    {
        private readonly ICampRepository campRepository;
        private readonly IMapper mapper;

        public CampService(ICampRepository campRepository, IMapper mapper)
        {
            this.campRepository = campRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CampResponseDto>> GetAllAsync()
        {
            var camps= await campRepository.GetAllAsync();
            return mapper.Map<IEnumerable<CampResponseDto>>(camps);    //mapping of a collection.
        }
        public async Task<CampResponseDto?> GetByIdAsync(int id)
        {
            var camp = await campRepository.GetByIdAsync(id);
            if (camp == null) return null;
            return mapper.Map<CampResponseDto>(camp);           //mapping a single object instead of a collection.

        }

        public async Task CreateAsync(CreateCampDto dto)
        {
            var camp = mapper.Map<Camp>(dto);
            await campRepository.AddAsync(camp);
            await campRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCampDto dto)
        {
            var camp= await campRepository.GetByIdAsync(dto.Id);
            if ( camp== null)
            {
                throw new Exception("Cmap not found");
            }
            mapper.Map(dto, camp);  //mapping the properties of the dto to the existing camp entity.
            await campRepository.UpdateAsync(camp);
            await campRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var camp= await campRepository.GetByIdAsync(id);
            if ( camp== null)
            {
                throw new Exception("Camp not found");
            }
        
        await campRepository.DeleteAsync(camp);
            await campRepository.SaveChangesAsync();
        }

        public async Task<PagedResponse<CampResponseDto>> SearchAvailableCampsAsync(CampSearchDto dto)
        {
            var result = await campRepository.SearchAvailableCampsAsync(dto);

            return new PagedResponse<CampResponseDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords,
                TotalPages = result.TotalPages,
                Data = mapper.Map<IEnumerable<CampResponseDto>>(result.Data)
            };
        }
    }
}

