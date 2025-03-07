using System.Text.Json;
public class UserService : IUserService
{
    private readonly string _userFilePath = "Data/Users/users.json";
    private readonly string _unknownIcon = "unknown.png";

    public List<UserDto> GetAllUsers()
    {
        if (!File.Exists(_userFilePath)) return new List<UserDto>();

        var json = File.ReadAllText(_userFilePath);


        var users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

        return users.Select(user => new UserDto
        {
            name = user.name,
            age = user.age,
            registered = user.registered.ToString("dd-MM-yyyy HH:mm:ss"),
            email = user.email,
            balance = $"£{user.balance:F2}",
            icon = string.IsNullOrEmpty(user.icon) ? _unknownIcon : user.icon
        }).OrderBy(u => u.name).ToList();
    }

    public void ResetBalances()
    {
        if (!File.Exists(_userFilePath)) return;

        var json = File.ReadAllText(_userFilePath);
        var users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

        foreach (var user in users) user.balance = 0;

        File.WriteAllText(_userFilePath, JsonSerializer.Serialize(users));
    }
}