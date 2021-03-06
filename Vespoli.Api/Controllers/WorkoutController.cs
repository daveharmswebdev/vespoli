using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vespoli.Api.Dtos;
using Vespoli.Data;
using Vespoli.Domain;

namespace Vespoli.Api.Controllers
{
    [Authorize]
    [Route("api/rower")]
    [ApiController]
    [Produces("application/json")]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutRepository repo, IMapper mapper, ILogger<WorkoutController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{rowerId}/workout")]
        public async Task<IActionResult> GetAllWorkouts(int rowerId)
        {
            try
            {
                var workouts = await _repo.GeAllWorkoutsByRower(rowerId);

                var workoutsToReturn = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);

                return Ok(workoutsToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get workouts: {ex}");
                return BadRequest("Failed to get workouts");
            }
        }

        [HttpGet("{rowerId}/workout/{id}")]
        public async Task<IActionResult> GetSingleWorkout(int rowerId, int id)
        {
            try
            {
                var workout = await _repo.GetSingleWorkout(rowerId, id);

                return Ok(workout);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get workout: {ex}");
                return BadRequest("Failed to get workout");
            }
        }

        [HttpPost("{rowerId}/workout")]
        public async Task<IActionResult> AddWorkout(int rowerId, [FromBody] WorkoutForAddDto workoutForAddDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    workoutForAddDto.RowerId = rowerId;
                    var newWorkout = _mapper.Map<WorkoutForAddDto, Workout>(workoutForAddDto);
                    _repo.Add(newWorkout);
                    if (await _repo.SaveAll())
                        return Created($"/api/rower/{rowerId}/workout/{newWorkout.Id}", newWorkout);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add workout: {ex}");
            }

            return BadRequest($"Failed to add workout");
        }

        [HttpDelete("{rowerId}/workout/{id}")]
        public async Task<IActionResult> RemoveWorkout(int rowerId, int id)
        {
            try
            {
                if (rowerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized();

                var workout = await _repo.GetSingleWorkout(rowerId, id);
                _repo.Delete(workout);
                if (await _repo.SaveAll())
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete workout {id}: {ex}");
            }
            return BadRequest($"Failed to delete workout");
        }

        [HttpPut("{rowerId}/workout/{id}")]
        public async Task<IActionResult> UpdateWorkout(int rowerId, int id, WorkoutForUpdateDto workoutForUpdate)
        {
            try
            {
                if (rowerId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                    return Unauthorized();

                var workoutFromRepo = await _repo.GetSingleWorkout(rowerId, id);

                _mapper.Map(workoutForUpdate, workoutFromRepo);

                if (await _repo.SaveAll())
                    return NoContent();

                throw new Exception($"Updating workout {id} for rower {rowerId} failed on save");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to update workout {id}: {ex}");
            }
            return BadRequest($"Failed to update workout");
        }
    }
}