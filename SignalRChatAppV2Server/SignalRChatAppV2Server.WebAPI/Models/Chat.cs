namespace SignalRChatAppV2Server.WebAPI.Models;

public sealed class Chat
{
    public Chat() 
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ToUserId { get; set; }
    public string? Message { get; set; }
    public string? Image {  get; set; }
    public DateTime Date { get; set; }
}
