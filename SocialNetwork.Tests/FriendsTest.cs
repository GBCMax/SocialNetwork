using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.Tests
{
  public class FriendsTest
  {
    [Test]
    public void AdditionFriend()
    {
      var userService = new UserService();

      var userRegistrationData1 = new UserRegistrationData()
      {
        Firstname = "первое имя",
        Lastname = "второе имя",
        Email = "email@email.ru",
        Password = "12345678"
      };

      var userRegistrationData2 = new UserRegistrationData()
      {
        Firstname = "первое имя1",
        Lastname = "второе имя1",
        Email = "email1@email.ru",
        Password = "12345678"
      };

      try
      {
        userService.Register(userRegistrationData1);

        userService.Register(userRegistrationData2);
      }
      catch (Exception)
      {

      }

      var user1 = userService.FindByEmail(userRegistrationData1.Email);

      var user2 = userService.FindByEmail(userRegistrationData2.Email);

      var friendService = new FriendService();

      var friendEntity = new FriendEntity()
      {
        id = 0,
        friend_id = user2.Id,
        user_id = user1.Id,
      };

      friendService.AddFriend(friendEntity);

      var friends = friendService.GetFriends(user1.Id);

      Assert.True(friends.Any(f => f.FriendId == user2.Id));
    }
  }
}