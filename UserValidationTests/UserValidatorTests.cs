using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserValidationLibrary.Models;
using UserValidationLibrary.Services;

namespace UserValidationTests;

[TestClass]
public class UserValidatorTests
{
    private UserValidator validator = new();

    private User ValidUser => new()
    {
        Name = "Maria",
        Email = "maria@test.com",
        Password = "Senha123",
        BirthDate = DateTime.Today.AddYears(-25)
    };

    // Testa validação bem-sucedida para um usuário válido
    [TestMethod]
    public void Should_Validate_Valid_User()
    {
        var result = validator.Validate(ValidUser);
        Assert.IsTrue(result.IsValid);
    }
}
