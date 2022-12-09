public class UserRepository : IUserRepository
{
    private List<UserDto> _user => new()
    {
        new UserDto("Jhon", "123"),
        new UserDto("Monica", "123"),
        new UserDto("Nancy", "123")
    };

    public UserDto GetUser(UserModel userModel) =>
        _user.FirstOrDefault(u =>
            string.Equals(u.UserName, userModel.UserName) &&
            string.Equals(u.Password, userModel.Password)) ??
            throw new Exception();
}