using CampBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampBooking.Business.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateToken(ApplicationUser user);

    }
}
