using CampBooking.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CampBooking.Domain.DTOs.CampDto;
namespace CampBooking.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampController : ControllerBase
    {
        private readonly ICampService campService;

        public CampController(ICampService campService)
        {
            this.campService = campService;
        }


        //Get all camps
        [HttpGet]
        public async Task<IActionResult> GetAllCamps()
        {
            var camps = await campService.GetAllAsync();
            return Ok(camps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampById(int id)
        {
            var camp = await campService.GetByIdAsync(id);
            if (camp == null)
            {
                return NotFound("Camp not found");
            }
            return Ok(camp);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCamp(CreateCampDto dto)
        {
            await campService.CreateAsync(dto);
            return Ok(new
            {
                Message = "Camp created successfully",
                camp = dto
            });
            
        }

        [Authorize(Roles = "Admin")]
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCamp(int id, UpdateCampDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Camp ID mismatch");
            }
            var existingCamp = await campService.GetByIdAsync(id);
            if (existingCamp == null)
            {
                return NotFound("Camp not found");
            }
            await campService.UpdateAsync(dto);
            return Ok(new
            {
                Message= "Update success",
                Updated_Camp=dto 
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamp(int id)
        {
            await campService.DeleteAsync(id);
            return Ok("Camp deleted successfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAvailableCamps([FromQuery] CampSearchDto dto)
        {
            var camps = await campService.SearchAvailableCampsAsync(dto);

            return Ok(camps);
        }
    }
}
