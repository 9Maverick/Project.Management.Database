using AutoMapper;
using Project.Management.Database.API.Data.DTO;
using Project.Management.Database.Domain;
using Project.Management.Database.Domain.Enums;

namespace Project.Management.Database.API.Data.Helpers;

public class AutoMappingProfiles : Profile
{
	public AutoMappingProfiles()
	{
		CreateCommentMap();

		CreateProjectMap();

		CreateTeamMap();

		CreateTicketMap();

		CreateUserMap();
	}

	private void CreateCommentMap()
	{
		CreateMap<Comment, CommentDTO>();
		CreateMap<CommentDTO, Comment>();
	}

	private void CreateProjectMap()
	{
		CreateMap<Domain.Project, ProjectDTO>();
		CreateMap<ProjectDTO, Domain.Project>();
	}

	private void CreateTeamMap()
	{
		CreateMap<Team, TeamDTO>();
		CreateMap<TeamDTO, Team>();
	}

	private void CreateTicketMap()
	{
		CreateMap<Ticket, TicketDTO>()
			.ForMember(
				dest => dest.Type,
				opt => opt.MapFrom(src => src.Type.ToString())
			)
			.ForMember(
				dest => dest.Priority,
				opt => opt.MapFrom(src => src.Priority.ToString())
			)
			.ForMember(
				dest => dest.Status,
				opt => opt.MapFrom(src => src.Status.ToString())
			);

		CreateMap<TicketDTO, Ticket>()
			.ForMember(
				dest => dest.Type,
				opt => opt.MapFrom(src => ParseEnum<TicketType>(src.Type))
			)
			.ForMember(
				dest => dest.Priority,
				opt => opt.MapFrom(src => ParseEnum<Priority>(src.Priority))
			)
			.ForMember(
				dest => dest.Status,
				opt => opt.MapFrom(src => ParseEnum<Status>(src.Status))
			);
	}

	private void CreateUserMap()
	{
		CreateMap<User, UserDTO>();
		CreateMap<UserDTO, User>();
	}

	private T? ParseEnum<T>(string value) where T : struct
	{
		Enum.TryParse<T>(value, out T result);
		return result;
	}
}
