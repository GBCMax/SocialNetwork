﻿namespace SocialNetwork.BLL.Models;
public class Friend
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int FriendId { get; set; }

  public Friend(int id, int userId, int friendId)
  {
    this.Id = id;
    this.UserId = userId;
    this.FriendId = friendId;
  }
}