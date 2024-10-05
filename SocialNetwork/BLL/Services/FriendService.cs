using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories.Parts;

namespace SocialNetwork.BLL.Services;
public class FriendService
{
  private readonly IFriendRepository _friendRepository;

  public FriendService()
  {
    _friendRepository = new FriendRepository();
  }

  public IEnumerable<Friend> GetFriends(int userId)
  {
    List<Friend> friends = [];

    _friendRepository.FindAllByUserId(userId).ToList().ForEach(m =>
    {
      friends.Add(new Friend(m.id, m.user_id, m.friend_id));
    });

    return friends;
  }

  public bool AddFriend(FriendEntity friend)
  {
    if(_friendRepository.Create(friend) == 0)
    {
      return false;
    }

    return true;
  }

  public bool DeleteFriend(int id)
  {
    if (_friendRepository.Delete(id) == 0)
    {
      return false;
    }

    return true;
  }
}