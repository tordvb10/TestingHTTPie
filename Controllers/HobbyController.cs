using AutoMapper;
using TestingHTTPie.Dto;
using TestingHTTPie.Interfaces;
using TestingHTTPie.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestingHTTPie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : Controller
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IMapper _mapper;

        public HobbyController(IHobbyRepository hobbyRepository, IMapper mapper)
        {
            _hobbyRepository = hobbyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HobbyDto>))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetHobbies()
        {
            var getHobbiesDto = _mapper.Map<ICollection<Hobby>, List<HobbyDto>>(await _hobbyRepository.GetHobbiesAsync());
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getHobbiesDto);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateHobby([FromBody] HobbyDto createHobbyDto)
        {
            if (createHobbyDto == null | !ModelState.IsValid) return BadRequest(ModelState);
            var createHobbyExists = (await _hobbyRepository.GetHobbiesAsync())
                .Where(h => h.Activity == createHobbyDto.Activity)
                .FirstOrDefault();
            if (createHobbyExists != null)
            {
                ModelState.AddModelError("", "Employee already exists.");
                return StatusCode(422, ModelState);
            }
            var createHobby = _mapper.Map<HobbyDto, Hobby>(createHobbyDto);
            if (!await _hobbyRepository.CreateHobbyAsync(createHobby))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created.");
        }

        //[HttpDelete]

        [HttpGet("{hobbyId}")]
        [ProducesResponseType(200, Type = typeof(HobbyDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetHobby(Guid hobbyId)
        {
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound();
            var getHobbyDto = _mapper.Map<Hobby,HobbyDto>(await _hobbyRepository.GetHobbyAsync(hobbyId));
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(getHobbyDto);
        }

        [HttpPut("{hobbyId}")]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateHobby(Guid hobbyId, [FromBody] HobbyDto updateHobbyDto)
        {
            return NoContent();
        }

    }
}