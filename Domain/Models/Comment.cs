namespace Domain.Models;

public class Comment : Entity<CommentId>
{
    public CustomIdentityUser Author { get; set; } = default!;
    public Topic CurrentTopic { get; set; } = default!;
    public DateTime CreateAt { get; set; } = default!;
    public string Text { get; set; } = default!;

    public static Comment Create(
        CommentId id,
        CustomIdentityUser author,
        Topic currentTopic,
        DateTime createdAt,
        string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        return new Comment
        {
            Id = id,
            Author = author,
            CurrentTopic = currentTopic,
            CreateAt = createdAt,
            Text = text
        };
    }
}