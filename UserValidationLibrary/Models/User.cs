namespace UserValidationLibrary.Models;

public class User
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime BirthDate { get; set; }
}
