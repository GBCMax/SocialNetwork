using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories.Interfaces;
using SocialNetwork.DAL.Repositories.Parts;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Services;
public class UserService
{
  private readonly MessageService _messageService;
  private readonly IUserRepository _userRepository;

  public UserService()
  {
    _userRepository = new UserRepository();
    _messageService = new MessageService();
  }

  public void Register(UserRegistrationData userRegistrationData)
  {
    if (string.IsNullOrEmpty(userRegistrationData.Firstname))
    {
      throw new ArgumentNullException();
    }

    if (string.IsNullOrEmpty(userRegistrationData.Lastname))
    {
      throw new ArgumentNullException();
    }

    if (string.IsNullOrEmpty(userRegistrationData.Password))
    {
      throw new ArgumentNullException();
    }

    if (string.IsNullOrEmpty(userRegistrationData.Email))
    {
      throw new ArgumentNullException();
    }

    if(userRegistrationData.Password.Length < 8)
    {
      throw new ArgumentNullException();
    }

    if(!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
    {
      throw new ArgumentNullException();
    }

    if (_userRepository.FindByEmail(userRegistrationData.Email) != null)
    {
      throw new ArgumentNullException();
    }

    var userEntity = new UserEntity()
    {
      firstname = userRegistrationData.Firstname,
      lastname = userRegistrationData.Lastname,
      email = userRegistrationData.Email,
      password = userRegistrationData.Password
    };

    if(_userRepository.Create(userEntity) == 0)
    {
      throw new Exception();
    }
  }

  public User Authenticate(UserAuthenticationData userAuthenticationData)
  {
    var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email) ?? throw new UserNotFoundException();
    if (findUserEntity.password != userAuthenticationData.Password)
      throw new WrongPasswordException();

    return ConstructUserModel(findUserEntity);
  }

  public User FindById(int id)
  {
    var findUserEntity = _userRepository.FindById(id);
    if (findUserEntity is null) throw new UserNotFoundException();

    return ConstructUserModel(findUserEntity);
  }

  public User FindByEmail(string email)
  {
    var findUserEntity = _userRepository.FindByEmail(email);
    return findUserEntity is null ? throw new UserNotFoundException() : ConstructUserModel(findUserEntity);
  }

  public void Update(User user)
  {
    var updatableUserEntity = new UserEntity()
    {
      id = user.Id,
      firstname = user.FirstName,
      lastname = user.LastName,
      password = user.Password,
      email = user.Email,
      photo = user.Photo,
      favorite_movie = user.FavoriteMovie,
      favorite_book = user.FavoriteBook
    };

    if (this._userRepository.Update(updatableUserEntity) == 0)
      throw new Exception();
  }

  private User ConstructUserModel(UserEntity userEntity)
  {
    var incomingMessages = _messageService.GetIncomingMessagesByUserId(userEntity.id);

    var outgoingMessages = _messageService.GetOutcomingMessagesByUserId(userEntity.id);

    return new User(userEntity.id,
                  userEntity.firstname,
                  userEntity.lastname,
                  userEntity.password,
                  userEntity.email,
                  userEntity.photo,
                  userEntity.favorite_movie,
                  userEntity.favorite_book,
                  incomingMessages,
                  outgoingMessages
                  );
  }
}