﻿namespace Application.Topics.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для обновления темы.
/// </summary>
/// <param name="Title">Название темы.</param>
/// <param name="Summary">Краткое описание темы.</param>
/// <param name="TopicType">Тип темы.</param>
/// <param name="Location">Местоположение, связанное с темой.</param>
/// <param name="EventStart">Дата и время начала события.</param>
public record UpdateTopicDto(
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime EventStart);