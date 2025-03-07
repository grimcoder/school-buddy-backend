public interface IUserService
{
    List<UserDto> GetAllUsers();
    void ResetBalances();
}

