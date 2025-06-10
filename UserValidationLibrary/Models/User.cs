namespace UserValidationLibrary.Models;

public class User
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime BirthDate { get; set; }
    public string CPF { get; set; } = null!;
    public string Endereco { get; set; } = null!;
    public string CEP { get; set; } = null!;
    public string Telefone { get; set; } = null!;
}
