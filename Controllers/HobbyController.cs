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
            var hobbies = _mapper.Map<List<HobbyDto>>(await _hobbyRepository.GetHobbiesAsync());
            return (!ModelState.IsValid) ? BadRequest(ModelState) : Ok(hobbies);
        }


        [HttpGet("{hobbyId}")]
        [ProducesResponseType(200, Type = typeof(HobbyDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetHobby(Guid hobbyId)
        {
            if (!await _hobbyRepository.HobbyExistsAsync(hobbyId)) return NotFound();
            var hobbyDto = _mapper.Map<HobbyDto>(await _hobbyRepository.GetHobbyAsync(hobbyId));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(hobbyDto);
        }

    }
}