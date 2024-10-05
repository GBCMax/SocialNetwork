using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;
public class DeletionFriendView
{
  private readonly FriendService _friendService;
  private readonly UserService _userService;

  public DeletionFriendView()
  {
    _friendService = new FriendService();
    _userService = new UserService();
  }
  public void Show(User user)
  {
    Console.Write("Введите почтовый адрес друга:");

    var friendEmail = Console.ReadLine();

    if (string.IsNullOrEmpty(friendEmail))
    {
      AlertMessage.Show("Вы не ввели адрес");
      Program.additionFriendView.Show(user);
    }

    try
    {
      var foundedUser = _userService.FindByEmail(friendEmail);

      var friend = _friendService.GetFriends(user.Id);

      var neededFriend = friend.Where(x => x.UserId == foundedUser.Id).FirstOrDefault();

      if(neededFriend is null)
      {
        AlertMessage.Show("Не удалось найти друга");

        Program.additionFriendView.Show(user);
      }

      if (!_friendService.DeleteFriend(neededFriend.Id))
      {
        AlertMessage.Show("Не удалось удалить друга");

        Program.additionFriendView.Show(user);
      }

      SuccessMessage.Show("Друг успешно удален");

      Program.additionFriendView.Show(user);
    }
    catch (UserNotFoundException)
    {
      AlertMessage.Show("Пользователь не найден");

      Program.additionFriendView.Show(user);
    }
    catch (Exception)
    {
      AlertMessage.Show("Неизвестная ошибка");

      Program.additionFriendView.Show(user);
    }
  }
}