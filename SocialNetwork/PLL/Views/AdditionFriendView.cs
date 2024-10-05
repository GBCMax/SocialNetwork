using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.PLL.Helpers;

namespace SocialNetwork.PLL.Views;
public class AdditionFriendView
{
  private readonly FriendService _friendService;
  private readonly UserService _userService;

  public AdditionFriendView()
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

      var friend = new FriendEntity()
      {
        user_id = user.Id,
        friend_id = foundedUser.Id
      };

      if (!_friendService.AddFriend(friend))
      {
        AlertMessage.Show("Не удалось добавить друга");

        Program.additionFriendView.Show(user);
      }

      SuccessMessage.Show("Друг успешно добавлен");

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