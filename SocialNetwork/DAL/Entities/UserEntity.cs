namespace SocialNetwork.DAL.Entities;
public class UserEntity
{
  public int id { get; set; }
  public string firstname { get; set; } = string.Empty;
  public string lastname { get; set; } = string.Empty;
  public string password { get; set; } = string.Empty;
  public string email { get; set; } = string.Empty;
  public string photo { get; set; } = string.Empty;
  public string favorite_movie { get; set; } = string.Empty;
  public string favorite_book { get; set; } = string.Empty;
}