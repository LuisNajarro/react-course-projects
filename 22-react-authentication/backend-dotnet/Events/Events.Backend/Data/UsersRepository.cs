using System.Text;
using System.Text.Json;
using Events.Backend.Util;

namespace Events.Backend.Data;

public static class UsersRepository
{
    private static async Task<UsersList?> ReadData()
    {
        var data = await File.ReadAllTextAsync("./users.json", Encoding.UTF8);
        return JsonSerializer.Deserialize<UsersList>(data);
    }
    
    private static async Task WriteData(UsersList events)
    {
        var data = JsonSerializer.Serialize(events);
        await File.WriteAllTextAsync("./users.json", data, Encoding.UTF8);
    }

    public static async Task<User> Add(User user)
    {
        var storedData = await ReadData();
        var userId = Guid.NewGuid();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
        storedData.Users ??= [];
        user.Id = userId.ToString();
        user.Password = hashedPassword;
        storedData.Users.Add(user);
        await WriteData(storedData);
        return new User { Id = user.Id, Email = user.Email };
    }
    
    public static async Task<User> Get(string email)
    {
        var storedData = await ReadData();
        if (storedData?.Users is null || storedData.Users.Count == 0)
        {
            throw new NotFoundException("Could not find any users.");
        }
        
        var user = storedData.Users.Find(e => e.Email == email);
        if (user is null)
        {
            throw new NotFoundException($"Could not find user for email {email}");
        }

        return user;
    }
}