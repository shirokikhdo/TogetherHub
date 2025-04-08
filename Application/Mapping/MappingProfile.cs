﻿namespace Application.Mapping;

/// <summary>
/// Профиль отображения для преобразования объектов данных в сущности и обратно.
/// Используется для конфигурации маппинга между DTO и сущностями.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Конструктор, который настраивает правила маппинга.
    /// </summary>
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

        CreateMap<CreateTopicDto, Topic>()
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
                options => options.MapFrom(_ => TopicId.Of(Guid.NewGuid())));

    }
}