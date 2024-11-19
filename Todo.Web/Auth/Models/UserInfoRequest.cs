namespace Todo.Web.Auth.Models;

public class UserInfoRequest
{
    public int id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
}