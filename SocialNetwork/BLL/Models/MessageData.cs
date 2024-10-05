namespace SocialNetwork.BLL.Models;
public class MessageData
{
  public int SenderId { get; set; }
  public string Content { get; set; } = string.Empty;
  public string RecipientEmail { get; set; } = string.Empty;
}