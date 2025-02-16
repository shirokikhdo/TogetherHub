using AutoMapper;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateTopicDto, Topic>()
            .ForMember(dest => dest.Title,
                options => options.MapFrom(source => source.Title))
            .ForMember(dest => dest.Summary,
            options => options.MapFrom(source => source.Summary))
            .ForMember(dest => dest.TopicType,
                options => options.MapFrom(source => source.TopicType))
            .ForMember(dest => dest.EventStart,
                options => options.MapFrom(source => source.EventStart))
            .ForMember(dest => dest.Location,
                options => options.MapFrom(source => Location.Of(
                    source.Location.City,
                    source.Location.Street)))
            .ForMember(dest => dest.Id,
                options => options.MapFrom((source, dest) => dest.Id));
    }
}