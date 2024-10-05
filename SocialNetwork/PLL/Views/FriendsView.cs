using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;
public class FriendsView
{
  FriendService _friendService;
  UserService _userService;
  static AdditionFriendView _friendView;
  static DeletionFriendView _deletionView;
  public FriendsView()
  {
    _friendService = new FriendService();
    _userService = new UserService();
    _friendView = new AdditionFriendView();
    _deletionView = new DeletionFriendView();
  }

  public void Show(User user)
  {
    while (true)
    {
      Console.WriteLine("Показать список моих друзей (нажмите 1)");
      Console.WriteLine("Добавить друга (нажмите 2)");
      Console.WriteLine("Удалить из друзей (нажмите 3)");
      Console.WriteLine("На главную (нажмите 4)");

      var choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
          {
            var friends = _friendService.GetFriends(user.Id);

            if (!friends.Any())
            {
              AlertMessage.Show("У вас еще нет друзей");
              break;
            }

            foreach (var friend in friends)
            {
              var myFriend = _userService.FindById(friend.FriendId);
              Console.WriteLine($"{myFriend.FirstName} {myFriend.LastName}");
            }
            break;
          }
        case "2":
          {
            _friendView.Show(user);
            break;
          }
        case "3":
          {
            _deletionView.Show(user);
            break;
          }
        case "4":
          {
            Program.userMenuView.Show(user);
            break;
          }
      }
    }
  }
}