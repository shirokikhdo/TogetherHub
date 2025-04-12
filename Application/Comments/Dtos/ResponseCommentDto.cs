namespace Application.Comments.Dtos;

public class ResponseCommentDto
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public DateTime CreateAt { get; set; }
}