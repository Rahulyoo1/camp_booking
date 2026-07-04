using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CampBooking.Domain.Entities;
using CampBooking.Domain.DTOs.CampDto;
using CampBooking.Domain.DTOs.BookingDto;

namespace CampBooking.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
           // CAMP MAppings
            CreateMap<Camp, CampResponseDto>()
                .ForMember(
                dest => dest.AverageRating,      // dest specifies the destination property in CampResponseDto  (here it is AverageRating)
                opt => opt.MapFrom(src => src.Ratings.Any()?src.Ratings.Average(r=>r.Stars):0)              // src is source object ,  which is taking rating collection from source (camp entity), check if rating present then give average of stars else 0.
                );
        
                
            CreateMap<CreateCampDto, Camp>();
            CreateMap<UpdateCampDto, Camp>();
            CreateMap<Camp, UpdateCampDto>();

            CreateMap<CreateBookingDto, Booking>();
        }

    }
}
