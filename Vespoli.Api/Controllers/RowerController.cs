using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vespoli.Api.Dtos;
using Vespoli.Data;
using Vespoli.Domain;

namespace Vespoli.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RowerController : ControllerBase
    {
        private readonly IRowerRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<RowerController> _logger;

        public RowerController(IRowerRepository repo, IMapper mapper, ILogger<RowerController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rower>>> GetAllRowers()
        {
            try
            {
                var rowers = await _repo.GeAllRowers();

                var rowersToReturn = _mapper.Map<IEnumerable<RowerForListDto>>(rowers);

                return Ok(rowersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get rowers: {ex}");
                return BadRequest("Failed to get rowers");                
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRowerById(int id)
        {
            try
            {
                var rower = await _repo.GetSingleRower(id);
                
                var rowerToReturn = _mapper.Map<RowerForDetailDto>(rower);
                    
                return Ok(rowerToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get rower with id {id}: {ex}");
                return BadRequest("Failed to get rower with id {id}");                
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRower(int id, RowerForUpdateDto rowerForUpdateDto)
        {
            try
            {
                if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized();

                var rowerFromRepo = await _repo.GetSingleRower(id);

                _mapper.Map(rowerForUpdateDto, rowerFromRepo);

                if (await _repo.SaveAll())
                    return NoContent();

                throw new Exception($"Updating user {id} failed on save");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update rower with id {id}: {ex}");
                return BadRequest($"Failed to update rower with id {id}");                
            }


        }
    }
}
