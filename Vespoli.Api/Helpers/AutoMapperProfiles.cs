using AutoMapper;
using Vespoli.Api.Dtos;
using Vespoli.Domain;

namespace Vespoli.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Rower, RowerForListDto>()
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                })
                .ForMember(
                    dest => dest.NumberWorkouts,
                    opt => opt.ResolveUsing(w => w.Workouts.Count)
                    );
            CreateMap<Rower, RowerForDetailDto>()
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<Workout, WorkoutForDetailDto>();
            CreateMap<Workout, WorkoutDto>();
            CreateMap<Rower, RowerForWorkoutDto>(); 
            CreateMap<RowerForUpdateDto, Rower>();
            CreateMap<WorkoutForAddDto, Workout>().ReverseMap();
        }
    }
}